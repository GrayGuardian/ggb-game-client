using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class ResTool : MonoBehaviour
{

    static void OpenFolder(string path)
    {
        path = new DirectoryInfo(path).FullName;
        if (!Directory.Exists(path))
        {
            if (UnityEditor.EditorUtility.DisplayDialog("提示", "文件夹不存在 是否创建？\n Url:" + path, "确定", "取消"))
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
    [MenuItem("Tools/资源管理/Open Folder/Build AssetBundles Folder")]
    static void OpenABFolder()
    {
        OpenFolder(GameConst.BUILD_AB_ROOT);
    }
    [MenuItem("Tools/资源管理/Open Folder/Build Folder")]
    static void OpenBuildFolder()
    {
        OpenFolder(GameConst.BUILD_ROOT);
    }
    [MenuItem("Tools/资源管理/Open Folder/Local Resource Folder")]
    static void OpenLocalResFolder()
    {
        OpenFolder(GameConst.RES_LOCAL_ROOT);
    }
    [MenuItem("Tools/资源管理/Open Folder/Web Resource Folder")]
    static void OpenWebResFolder()
    {
        OpenFolder(GameConst.RES_WEB_ROOT);
    }

    [MenuItem("Tools/资源管理/Build/Build")]
    static void BuildRes()
    {
        //准备工作
        var rootDir = new DirectoryInfo(GameConst.BUILD_ROOT);
        var rootABDir = new DirectoryInfo(Path.Combine(rootDir.FullName, "./AssetBundles"));
        var versionFile = new FileInfo(Path.Combine(rootDir.FullName, "./Version"));
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
        //构建资源
        List<ABVObject> abVObjectList = new List<ABVObject>();
        string[] blackFilesName = new string[] { "AssetBundles" };
        foreach (var file in new DirectoryInfo(GameConst.BUILD_AB_ROOT).GetFiles())
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
            var bytes = Util.Encrypt.AesEncrypt(Util.File.ReadBytes(file.FullName));
            var name = Path.GetFileNameWithoutExtension(file.FullName);
            var size = bytes.Length;
            var hash = Util.File.ComputeHash(bytes);

            //build AB File
            Util.File.WriteBytes(Path.Combine(rootABDir.FullName, name), bytes);
            Debug.Log(string.Format("Build AB Res >>>> name:{0} size:{1} hask:{2}", name, size, hash));

            //build version File
            abVObjectList.Add(new ABVObject() { name = name, size = size, hash = hash });
        }
        VObject vObject = new VObject();
        vObject.Version = "1.0.9";
        vObject.UpdateType = 0;
        vObject.IsRestart = false;
        vObject.Content = "我是更新描述";
        vObject.ABs = abVObjectList.ToArray();

        string json = vObject.toString();
        Debug.Log("Version Json:" + json);

        Util.Encrypt.WriteString(versionFile.FullName, json);

        if (UnityEditor.EditorUtility.DisplayDialog("提示", "是否更新Resources内版本文件？", "确定", "取消"))
        {
            Util.Encrypt.WriteString(Path.Combine(GameConst.RESOURCES, "./Default/Version"), json);
        }
    }

    [MenuItem("Tools/资源管理/Build/Build ＆ CopyTo Local")]
    static void BuildRes1()
    {
        BuildRes();
        CopyResToLocalRoot();
    }
    [MenuItem("Tools/资源管理/Build/Build ＆ CopyTo Web")]
    static void BuildRes2()
    {
        BuildRes();
        CopyResToWebRoot();
    }
    [MenuItem("Tools/资源管理/Build/Build ＆ CopyTo Local ＆ CopyTo Web")]
    static void BuildRes3()
    {
        BuildRes();
        CopyResToLocalRoot();
        CopyResToWebRoot();
    }
    [MenuItem("Tools/资源管理/Copy/Copy To Local Folder")]
    static void CopyResToLocalRoot()
    {
        var rootDir = new DirectoryInfo(GameConst.RES_LOCAL_ROOT);
        CopyResToRoot(rootDir);
    }
    [MenuItem("Tools/资源管理/Copy/Copy To Web Folder")]
    static void CopyResToWebRoot()
    {
        var rootDir = new DirectoryInfo(GameConst.RES_WEB_ROOT);
        CopyResToRoot(rootDir);
    }
    [MenuItem("Tools/资源管理/Copy/Copy To Local Folder ＆ Web Folder")]
    static void CopyResToLocalRootAndWebRoot()
    {
        CopyResToLocalRoot();
        CopyResToWebRoot();
    }
    static void CopyResToRoot(DirectoryInfo rootDir)
    {
        string path;
        var rootBuildDir = new DirectoryInfo(GameConst.BUILD_ROOT);
        var rootABDir = new DirectoryInfo(Path.Combine(rootBuildDir.FullName, "./AssetBundles"));
        var versionFile = new FileInfo(Path.Combine(rootBuildDir.FullName, "./Version"));
        if (!Directory.Exists(rootDir.FullName))
        {
            Debug.Log("创建资源文件夹");
            Directory.CreateDirectory(rootDir.FullName);
        }
        if (!Directory.Exists(rootBuildDir.FullName))
        {
            UnityEditor.EditorUtility.DisplayDialog("提示", "Build文件夹不存在,请重新构建\n Url:" + rootBuildDir.FullName, "确定");
            return;
        }


        if (!File.Exists(versionFile.FullName))
        {
            UnityEditor.EditorUtility.DisplayDialog("提示", "Build - Version文件不存在,请重新构建\n Url:" + versionFile.FullName, "确定");
        }
        else
        {
            path = Path.Combine(rootDir.FullName, "./Version");
            versionFile.CopyTo(path, true);
            Debug.Log(string.Format("[{0}] Copy To >>> Path:{1}", "Version", path));
        }

        if (!Directory.Exists(rootABDir.FullName))
        {
            UnityEditor.EditorUtility.DisplayDialog("提示", "Build - AB文件夹不存在,请重新构建\n Url:" + rootABDir.FullName, "确定");
        }
        else
        {
            path = Path.Combine(rootDir.FullName, "./AssetBundles");
            if (!Directory.Exists(path))
            {
                Debug.Log("创建资源文件夹 - AB包");
                Directory.CreateDirectory(path);
            }
            foreach (var file in rootABDir.GetFiles())
            {
                file.CopyTo(Path.Combine(path, file.Name), true);
                Debug.Log(string.Format("AB - [{0}] Copy To >>> Path:{1}", file.Name, path));
            };

        }

    }
    [MenuItem("Tools/资源管理/Print Version Json")]
    static void PrintVersionJson()
    {
        var versionFile = new FileInfo(Path.Combine(GameConst.BUILD_ROOT, "./Version"));
        if (!File.Exists(versionFile.FullName))
        {
            UnityEditor.EditorUtility.DisplayDialog("提示", "Build - Version文件不存在,请重新构建\n Url:" + versionFile.FullName, "确定");
            return;
        }
        string str = Util.Encrypt.ReadString(versionFile.FullName);
        Debug.Log(str);
    }

    [MenuItem("Tools/资源管理/Copy All Default AB To Resources")]
    static void CopyAllDefaultABToResources()
    {
        //需要导出的默认AB包资源
        string[] abs = { "lua", "ui_tip" };
        string root = Path.Combine(GameConst.RESOURCES, "./AB");
        foreach (var ab in abs)
        {
            //清理文件夹
            DirectoryInfo buildDir = new DirectoryInfo(Path.Combine(GameConst.RESOURCES, "./Default", "./" + ab));
            Debug.Log(buildDir.FullName);
            FileInfo[] files = buildDir.GetFiles();
            if (files.Length > 0 && UnityEditor.EditorUtility.DisplayDialog("提示", "是否清空导出文件夹\n Url:" + buildDir.FullName, "确定", "取消"))
            {
                foreach (var file in files)
                {
                    file.Delete();
                }
            }

            //开始复制
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(root, ab));
            files = Util.File.GetChildFiles(dir.FullName, "*");
            Dictionary<string, FileInfo> fileMap = new Dictionary<string, FileInfo>();
            foreach (var file in files)
            {
                if (fileMap.ContainsKey(file.Name))
                {
                    Debug.LogError("Error:出现重复项：" + file.Name);
                    return;
                }
                else
                {
                    fileMap.Add(file.Name, file);
                }
            }
            foreach (var file in fileMap.Values)
            {
                UnityEngine.Debug.Log(file.FullName);
                UnityEngine.Debug.Log(Path.Combine(GameConst.RESOURCES, "./Default", "./" + ab, "./" + file.Name));
                Util.File.CopyTo(file.FullName, Path.Combine(GameConst.RESOURCES, "./Default", "./" + ab, "./" + file.Name));
            }
        }
        AssetDatabase.Refresh();
    }
    // [MenuItem("Tools/资源管理/Format Lua File Name")]
    // static void FormatLuaFileName()
    // {
    //     string root = Path.Combine(GameConst.RESOURCES, "./AB/lua");
    //     FileInfo[] files = Util.File.GetChildFiles(root, "*");
    //     foreach (var file in files)
    //     {
    //         Debug.Log(Path.GetExtension(file.FullName));
    //         switch (Path.GetExtension(file.FullName))
    //         {
    //             case ".meta":
    //                 print("meta文件需要删除>>" + file.Name);
    //                 File.Delete(file.FullName);
    //                 break;
    //             case ".lua":
    //                 print("lua文件需要格式化>>" + file.Name);
    //                 file.MoveTo(Path.Combine(file.Directory.FullName, file.Name + ".txt"));
    //                 break;
    //         }
    //     }
    //     AssetDatabase.Refresh();
    // }
}