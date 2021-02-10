using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ResUtil
{
    public VObject Version
    {
        get
        {
            if (_vJson != "")
            {
                _version = JsonConvert.DeserializeObject<VObject>(_vJson);
            }
            return _version;
        }
    }
    private VObject _version = null;
    private string _vJson
    {
        get
        {
            string path = Path.Combine(GameConst.RES_LOCAL_ROOT, "Version");
            string json = Util.Encrypt.ReadString(path);
            return json;
        }
    }
    public VObject WebVersion
    {
        get
        {
            if (_webVersion == null)
            {
                Debug.Log("Web VJson:" + _vWebJson);
                _webVersion = JsonConvert.DeserializeObject<VObject>(_vWebJson);
            }
            return _webVersion;
        }
    }
    private VObject _webVersion = null;
    public string _vWebJson
    {
        get
        {
            return Util.Encrypt.AesDecrypt(Util.Http.Get(GameConst.DOWNLOAD_URL + "Version"));
        }
    }
    /// <summary>
    /// 更新版本文件
    /// </summary>
    public void UpVersion()
    {
        Debug.Log("更新版本文件>>" + WebVersion.toString());
        Util.Encrypt.WriteString(Path.Combine(GameConst.RES_LOCAL_ROOT, "Version"), WebVersion.toString());
    }
    /// <summary>
    /// 获取更新数据
    /// </summary>
    /// <returns></returns>
    public VObject GetRefdata()
    {
        bool isUp = false;
        VObject vObject;
        //校验远程Json文件
        if (Version == null || Version.toString() != WebVersion.toString())
        {
            isUp = true;
            vObject = JsonConvert.DeserializeObject<VObject>(WebVersion.toString());
        }
        else
        {
            vObject = JsonConvert.DeserializeObject<VObject>(Version.toString());
        }
        //校验AB包文件
        List<ABVObject> abList = new List<ABVObject>();
        foreach (var ab in vObject.ABs)
        {
            string path = Path.Combine(GameConst.RES_LOCAL_ROOT, "./AssetBundles", "./" + ab.name);
            byte[] data = Util.File.ReadBytes(path);
            string hash = Util.File.ComputeHash(data);
            long size = data.Length;
            if (hash != ab.hash || size != ab.size)
            {
                isUp = true;
                //判断是否存在缓存文件
                string tempPath = Path.Combine(GameConst.DOWNLOAD_TEMPFILE_ROOT, ab.name + "_" + ab.hash + ".temp");
                if (File.Exists(tempPath))
                {
                    //存在缓存文件
                    FileInfo tempFile = new FileInfo(tempPath);
                    ab.size = ab.size - (int)tempFile.Length;
                }
                abList.Add(ab);
            }
        }
        vObject.ABs = abList.ToArray();

        if (isUp)
        {
            return vObject;
        }
        return null;
    }


    private Dictionary<string, AssetBundle> _abMap = new Dictionary<string, AssetBundle>();
    public AssetBundle LoadAssetBundle(string key)
    {
        if (GameConst.PRO_ENV != ENV_TYPE.MASTER) return null;
        if (_abMap.ContainsKey(key)) return null;
        string filePath = Path.Combine(GameConst.RES_LOCAL_ROOT, "./AssetBundles", "./" + key);
        if (!File.Exists(filePath)) return null;
        UnityEngine.Debug.Log("加载AB包：" + key);
        var data = Util.Encrypt.ReadBytes(filePath);
        MemoryStream steam = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(steam);
        writer.Write(data);
        AssetBundle asset = AssetBundle.LoadFromStream(steam);
        writer.Close();
        steam.Close();
        _abMap.Add(key, asset);
        return asset;
    }
    public void UnLoadAssetBundle(string key, bool unloadAllLoadedObjects = false)
    {
        if (GameConst.PRO_ENV != ENV_TYPE.MASTER) return;
        if (!_abMap.ContainsKey(key)) return;
        UnityEngine.Debug.Log("卸载AB包：" + key);
        _abMap[key].Unload(unloadAllLoadedObjects);
        _abMap.Remove(key);
    }

    public UnityEngine.Object Load(string key, string resName, bool loadIsClose = false)
    {

        return Load<UnityEngine.Object>(key, resName, loadIsClose);
    }

    public T Load<T>(string key, string resName, bool loadIsClose = false) where T : UnityEngine.Object
    {

        T data = default(T);
        if (GameConst.PRO_ENV == ENV_TYPE.MASTER)
        {
            AssetBundle asset;
            if (!_abMap.ContainsKey(key))
            {
                asset = LoadAssetBundle(key);
            }
            else
            {
                asset = _abMap[key];
            }
            if (asset != null)
            {
                data = asset.LoadAsset<T>(resName);
                if (data != default(T))
                {
                    UnityEngine.Debug.Log(string.Format("通过AB包加载资源 key:{0} resName:{1}", key, resName));
                }
                if (loadIsClose)
                {
                    UnLoadAssetBundle(key);
                }
            }
        }
        else
        {
            string resPath = Path.Combine(GameConst.RESOURCES, "./AB/" + key);
            FileInfo fileInfo = Util.File.GetChildFile(resPath, resName + ".*");
            if (fileInfo != null)
            {
                string dirPath = GameConst.GetRelativePath(fileInfo.DirectoryName, GameConst.RESOURCES);
                resPath = Path.Combine(dirPath, resName);
                data = Resources.Load<T>(resPath);
                if (data != default(T))
                {
                    UnityEngine.Debug.Log(string.Format("通过伪AB包(Resources)加载资源 key:{0} resName:{1} resPath:{2}", key, resName, resPath));
                }
            }
        }

        if (data == default(T))
        {
            data = Resources.Load<T>("Default/" + key + "/" + resName);
            if (data != default(T))
            {
                UnityEngine.Debug.Log(string.Format("通过默认文件夹加载资源 key:{0} resName:{1}", key, resName));
            }

        }
        if (data == default(T))
        {
            data = Resources.Load<T>(key + "/" + resName);
            if (data != default(T))
            {
                UnityEngine.Debug.Log(string.Format("通过Base文件夹加载资源 key:{0} resName:{1}", key, resName));
            }
        }

        return data;
    }

    public string LoadString(string key, string resName, bool loadIsClose = false)
    {
        UnityEngine.Object o = Load(key, resName, loadIsClose);
        if (o == null) return null;
        return o.ToString();
    }

    public byte[] LoadBytes(string key, string resName, bool loadIsClose = false)
    {
        string str = LoadString(key, resName, loadIsClose);
        if (str == null) return null;
        return System.Text.Encoding.UTF8.GetBytes(str);
    }

    public Sprite LoadSprite(string key, string resName, bool loadIsClose = false)
    {
        return Load<Sprite>(key, resName, loadIsClose);
    }

    public GameObject LoadGameObject(string key, string resName, bool loadIsClose = false)
    {
        return Load<GameObject>(key, resName, loadIsClose);
    }
}