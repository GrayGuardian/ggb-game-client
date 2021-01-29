using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class ResTool : MonoBehaviour
{
    static string resRoot = Path.Combine(Application.persistentDataPath, (string)Json.Instance["config"]["res_fileRoot"]);
    static string buildRoot = Path.Combine(Application.dataPath, "../Build");
    static string abRoot = Path.Combine(Application.dataPath, "../AssetBundles");

    [MenuItem("Tools/资源管理/打开资源文件夹")]
    static void OpenResFolder()
    {
        string path = new DirectoryInfo(resRoot).FullName;
        if (!Directory.Exists(path))
        {
            if (UnityEditor.EditorUtility.DisplayDialog("提示", "资源文件夹不存在 是否创建？\n Url:" + path, "确定", "取消"))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                return;
            }
        }
        string arg = string.Format(@"/open,{0}", path);
        System.Diagnostics.Process.Start("explorer.exe", arg);
    }
    [MenuItem("Tools/资源管理/构建本地资源文件")]
    static void BuildLocalRes()
    {
        //准备工作
        var rootDir = new DirectoryInfo(buildRoot);
        var rootABDir = new DirectoryInfo(Path.Combine(rootDir.FullName, "./AssetBundles"));

        if (!Directory.Exists(rootABDir.FullName))
        {
            Directory.CreateDirectory(rootABDir.FullName);
            Debug.Log("Build - AB文件夹不存在，重新创建");
        }
        if (!Directory.Exists(rootDir.FullName))
        {
            Directory.CreateDirectory(rootDir.FullName);
            Debug.Log("Build文件夹不存在，重新创建");
        }
        // FileInfo[] files;
        // bool isClear = false;
        // files = rootDir.GetFiles();
        // if (files.Length > 0 && (UnityEditor.EditorUtility.DisplayDialog("提示", "是否清空旧资源？", "确定", "取消") || isClear))
        // {
        //     isClear = true;
        //     foreach (var file in files)
        //     {
        //         file.Delete();
        //     }
        // }
        // files = rootABDir.GetFiles();
        // if (files.Length > 0 && (UnityEditor.EditorUtility.DisplayDialog("提示", "是否清空旧资源？", "确定", "取消") || isClear))
        // {
        //     isClear = true;
        //     foreach (var file in files)
        //     {
        //         file.Delete();
        //     }
        // }
        //构建资源
        List<ABVObject> abVObjectList = new List<ABVObject>();
        string[] blackFilesName = new string[] { "AssetBundles" };
        foreach (var file in new DirectoryInfo(abRoot).GetFiles())
        {
            //Debug.Log(string.Format("文件路径：{0} 后缀名：{1} 文件名{2}",VARIABLE,Path.GetExtension(VARIABLE),Path.GetFileNameWithoutExtension(VARIABLE)) );
            //存在后缀名则跳过
            if (Path.GetExtension(file.FullName) == ".manifest")
            {
                continue;
            }
            //黑名单存在则跳过
            if (Array.IndexOf(blackFilesName, Path.GetFileName(file.FullName)) != -1)
            {
                continue;
            }
            var bytes = File.ReadAllBytes(file.FullName);
            //加密数据
            bytes = Util.Encrypt.AesEncrypt(bytes);
            var name = Path.GetFileNameWithoutExtension(file.FullName);
            var size = bytes.Length;
            var hash = Util.ComputeHash(bytes);

            //build AB File
            File.WriteAllBytes(Path.Combine(rootABDir.FullName, name), bytes);
            Debug.Log(string.Format("Build AB Res >>>> name:{0} size:{1} hask:{2}", name, size, hash));

            //build version File
            abVObjectList.Add(new ABVObject() { name = name, size = size, hash = hash });
        }
        VObject vObject = new VObject();
        vObject.version = (string)Json.Instance["config"]["version"];
        vObject.ABs = abVObjectList.ToArray();

        string json = JsonConvert.SerializeObject(vObject);
        Debug.Log("Version Json:" + json);
        //加密数据
        json = Util.Encrypt.AesEncrypt(json);

        File.WriteAllText(Path.Combine(rootDir.FullName, "./Version"), json);
    }


}