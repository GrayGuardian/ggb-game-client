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
            if (_version == null)
            {
                Debug.Log("Local VJson:" + _vJson);
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
            string path = Path.Combine(PathConst.RES_LOCAL_ROOT, "Version");
            string json = Util.Encrypt.ReadString(path);
            if (json == "")
            {
                UnityEngine.Debug.Log("未找到资源文件夹内版本文件，获取本地版本文件，并加入资源文件夹内");
                Util.File.CopyTo(Path.Combine(PathConst.RESOURCES, "./Version"), path);
                json = Util.Encrypt.ReadString(path);
            }
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
            return Util.Encrypt.AesDecrypt(Util.Http.Get(Util.Json["config"]["download_url"] + "Version"));
        }
    }
    /// <summary>
    /// 更新版本文件
    /// </summary>
    public void UpVersion()
    {
        Debug.Log("更新版本文件>>" + WebVersion.toString());
        Util.Encrypt.WriteString(Path.Combine(PathConst.RES_LOCAL_ROOT, "Version"), WebVersion.toString());
    }
    /// <summary>
    /// 获取更新数据
    /// </summary>
    /// <returns></returns>
    public VObject getRefdata()
    {
        bool isUp = false;
        VObject vObject;
        //校验远程Json文件
        if (Version.toString() != WebVersion.toString())
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
            string path = Path.Combine(PathConst.RES_LOCAL_ROOT, "./AssetBundles", "./" + ab.name);
            byte[] data = Util.File.ReadBytes(path);
            string hash = Util.File.ComputeHash(data);
            long size = data.Length;
            if (hash != ab.hash || size != ab.size)
            {
                isUp = true;
                //判断是否存在缓存文件
                string tempPath = Path.Combine(PathConst.DOWNLOAD_TEMPFILE_ROOT, ab.name + "_" + ab.hash + ".temp");
                if (File.Exists(tempPath))
                {
                    //存在缓存文件
                    FileInfo tempFile = new FileInfo(tempPath);
                    ab.size = ab.size - tempFile.Length;
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
       
        if (Util.Json["config"]["PRO_ENV"].ToString() != "Master") return null;
        if (_abMap.ContainsKey(key)) return null;
        string filePath = Path.Combine(PathConst.RES_LOCAL_ROOT, "./AssetBundles", "./" + key);
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
        if (Util.Json["config"]["PRO_ENV"].ToString() != "Master") return;
        if (!_abMap.ContainsKey(key)) return;
        UnityEngine.Debug.Log("卸载AB包：" + key);
        _abMap[key].Unload(unloadAllLoadedObjects);
        _abMap.Remove(key);
    }

    public UnityEngine.Object Load(string key, string resName)
    {

        return Load<UnityEngine.Object>(key, resName);
    }

    public T Load<T>(string key, string resName) where T : UnityEngine.Object
    {

        T data = default(T);
        if (Util.Json["config"]["PRO_ENV"].ToString() == "Master")
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
                UnityEngine.Debug.Log(string.Format("通过AB包加载资源 key:{0} resName:{1}", key, resName));
                data = asset.LoadAsset<T>(resName);
            }
        }
        else
        {
            string resPath = Path.Combine(PathConst.RESOURCES, "./AB/" + key);
            FileInfo fileInfo = Util.File.GetChildFile(resPath, resName + ".*");
            if (fileInfo != null)
            {
                string dirPath = PathConst.GetRelativePath(fileInfo.DirectoryName, PathConst.RESOURCES);
                resPath = Path.Combine(dirPath, resName);
                UnityEngine.Debug.Log(string.Format("通过Resources加载资源 key:{0} resName:{1} resPath:{2}", key, resName, resPath));
                data = Resources.Load<T>(resPath);
            }
        }

        if (data == default(T))
        {
            string resPath = Path.Combine(PathConst.RESOURCES, "./Default/" + key);
            FileInfo fileInfo = Util.File.GetChildFile(resPath, resName + ".*");
            if (fileInfo == null) return data;
            string dirPath = PathConst.GetRelativePath(fileInfo.DirectoryName, PathConst.RESOURCES);
            resPath = Path.Combine(dirPath, resName);
            //UnityEngine.Debug.Log(string.Format("通过默认文件夹加载资源 key:{0} resName:{1} resPath:{2}", key, resName, resPath));
            data = Resources.Load<T>(resPath);
        }
        return data;
    }

    public string LoadString(string key, string resName)
    {
        UnityEngine.Object o = Load(key, resName);
        if (o == null) return null;
        return o.ToString();
    }

    public byte[] LoadBytes(string key, string resName)
    {
        string str = LoadString(key, resName);
        if (str == null) return null;
        return System.Text.Encoding.UTF8.GetBytes(str);
    }

    public Sprite LoadSprite(string key, string resName)
    {
        return Load<Sprite>(key, resName);
    }

    public GameObject LoadGameObject(string key, string resName)
    {
        return Load<GameObject>(key, resName);
    }
}