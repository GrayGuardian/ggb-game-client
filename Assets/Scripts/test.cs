using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
public class test : MonoBehaviour
{
    public MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        JObject jObject = JObject.Parse("{'a':1}");
        Debug.Log(Json.Instance["config"]["http"]);
        Debug.Log(Json.Instance["config"]["download_url"]);
        Debug.Log(Application.persistentDataPath);

        //Debug.Log(t1("d"));
        //Debug.Log(t2<Material>("common", "mat"));
        mesh.material = t2<Material>("common", "mat");

        Debug.Log(Util.Encrypt.AesDecrypt(Util.Encrypt.AesEncrypt("FDAFSDSFASDFD")));
    }


    public AssetBundle t1(string key)
    {
        //Debug.Log("尝试加载：" + key + );
        string path = System.IO.Path.Combine(Application.dataPath, "../AssetBundles/" + key);
        Debug.Log(path);
        var data = File.ReadAllBytes(path);
        MemoryStream steam = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(steam);
        writer.Write(data);
        AssetBundle asset = AssetBundle.LoadFromStream(steam);
        writer.Close();
        steam.Close();
        return asset;
    }
    public T t2<T>(string key, string name) where T : UnityEngine.Object
    {
        Debug.Log("尝试加载：" + key + "  " + name);
        var asset = t1(key);
        asset.LoadAsset("d");
        return asset.LoadAsset<T>(name);
    }

    // public AssetBundle LoadAssetBundle(string key)
    // {
    //     UnityEngine.Debug.Log("加载AB包：" + key);
    //     if (ConfigUtil.Instance.DEMO_ENV != DEMOENV.RELEASE) return null;
    //     string filePath = Path.Combine(ConfigUtil.Instance.RES_ABFILE_PATH, key);
    //     if (!File.Exists(filePath)) return null;
    //     var data = EncryptUtil.Instance.ReadBytes(filePath);
    //     MemoryStream steam = new MemoryStream();
    //     BinaryWriter writer = new BinaryWriter(steam);
    //     writer.Write(data);
    //     AssetBundle asset = AssetBundle.LoadFromStream(steam);
    //     writer.Close();
    //     steam.Close();
    //     if (_abMap.ContainsKey(key))
    //     {
    //         UnLoadAssetBundle(key);
    //     }
    //     _abMap.Add(key, asset);
    //     return asset;
    // }


    // Update is called once per frame
    void Update()
    {

    }
}
