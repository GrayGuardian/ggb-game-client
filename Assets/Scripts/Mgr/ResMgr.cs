using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ResMgr : Singleton<ResMgr>
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
            return Util.Encrypt.AesDecrypt(HttpMgr.Instance.Get(Util.Json["config"]["download_url"] + "Version"));
        }
    }

    /// <summary>
    /// 获取更新数据
    /// </summary>
    /// <returns></returns>
    public VObject getUpdata()
    {
        bool isUp = false;
        VObject vObject;
        //校验远程Json文件
        if (Version.toString() != WebVersion.toString())
        {
            isUp = true;
            vObject = WebVersion;
        }
        else
        {
            vObject = Version;
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


}