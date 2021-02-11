using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 更新数据
/// </summary>
public class RefData
{
    /// <summary>
    /// 更新类型
    /// 0 - 无需更新
    /// 1 - 客户端更新
    /// 2 - 首次打开下载资源
    /// 3 - 在线更新资源
    /// 4 - 版本文件不符，无需下载资源
    /// 5 - 文件损坏，需要更新
    /// </summary>
    public int type;
    /// <summary>
    /// 共计下载文件大小
    /// </summary>
    public int size;
    /// <summary>
    /// 更新数据
    /// </summary>
    public VObject data;
}

public class ResUtil
{
    public VObject Version
    {
        get
        {
            if (_vJson != "")
            {
                return JsonConvert.DeserializeObject<VObject>(_vJson);
            }
            return null;
        }
    }
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
            if(_webVersion == null)
            {
                _webVersion = JsonConvert.DeserializeObject<VObject>(_vWebJson);
            }
            return _webVersion;
        }
    }
    private VObject _webVersion;
    
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
    public RefData GetRefData(){
        VObject data = JsonConvert.DeserializeObject<VObject>(WebVersion.toString());

        //校验AB包文件
        int sizeSum = 0;
        List<ABVObject> abList = new List<ABVObject>();
        foreach (var ab in WebVersion.ABs)
        {
            string path = Path.Combine(GameConst.RES_LOCAL_ROOT, "./AssetBundles", "./" + ab.name);
            byte[] bytes = Util.File.ReadBytes(path);
            string hash = Util.File.ComputeHash(bytes);
            long size = bytes.Length;
            if (hash != ab.hash || size != ab.size)
            {
                //判断是否存在缓存文件
                string tempPath = Path.Combine(GameConst.DOWNLOAD_TEMPFILE_ROOT, ab.name + "_" + ab.hash + ".temp");
                if (File.Exists(tempPath))
                {
                    //存在缓存文件
                    FileInfo tempFile = new FileInfo(tempPath);
                    ab.size = ab.size - (int)tempFile.Length;  
                }
                sizeSum += ab.size;
                abList.Add(ab);
            }
        }
        data.ABs = abList.ToArray();

        int type = 0;
        if(data.ClientVersion != Application.version){
            // 客户端更新
            type = 1;
        }
        else if(Version == null && data.ABs.Length>0){
            // 首次打开，需要下载资源
            type = 2;
        }
        else if((Version == null || Version.toString() != WebVersion.toString()) && data.ABs.Length > 0){
            // 在线下载资源更新
            type = 3;
        }
        else if((Version == null || Version.toString() != WebVersion.toString()) && data.ABs.Length == 0){
            // 版本文件不符，无需下载资源
            type = 4;
        }
        else if(data.ABs.Length>0){
            // 文件损坏，需要更新
            type = 5;
        }

        RefData refdata = new RefData();
        refdata.type = type;
        refdata.size = sizeSum;
        refdata.data = data;
        return refdata;
    }
    /// <summary>
    /// 清理冗余资源
    /// </summary>
    public void ClearRedundantRes()
    {
        DirectoryInfo dirInfo;
        // 清理临时文件
        dirInfo = new DirectoryInfo(GameConst.DOWNLOAD_TEMPFILE_ROOT);
        foreach (var fileInfo in dirInfo.GetFiles())
        {
            fileInfo.Delete();
        }
        // 清理多余AB包文件
        List<string> abNameList = new List<string>();
        foreach (var ab in WebVersion.ABs)
        {
            abNameList.Add(ab.name);
        }
        dirInfo = new DirectoryInfo(Path.Combine(GameConst.RES_LOCAL_ROOT, "./AssetBundles"));
        foreach (var fileInfo in dirInfo.GetFiles())
        {
            if(abNameList.IndexOf(fileInfo.Name) == -1){
                UnityEngine.Debug.Log("清理多余AB包文件>>"+fileInfo.Name);
                fileInfo.Delete();
            }
        }
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