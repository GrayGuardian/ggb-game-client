using UnityEngine;
using UnityEditor;
using System.IO;
public class ResTool : MonoBehaviour
{
    [MenuItem("Tools/资源管理/打开资源文件夹")]
    static void OpenResFolder()
    {
        string path = Path.Combine(Application.persistentDataPath, (string)Json.Instance["config"]["res_fileRoot"]);
        path = new DirectoryInfo(path).FullName;
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
}