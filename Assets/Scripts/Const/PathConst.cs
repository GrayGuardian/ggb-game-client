using System.IO;
using UnityEngine;

public class PathConst
{
    /// <summary>
    /// Resources文件夹
    /// </summary>
    /// <returns></returns>
    public static string RESOURCES = Path.Combine(Application.dataPath, "./Resources");
    /// <summary>
    /// 本地 Res文件夹
    /// </summary>
    /// <returns></returns>
    public static string RES_LOCAL_ROOT = Path.Combine(Application.persistentDataPath, (string)Util.Json["config"]["res_fileRoot"]);
    /// <summary>
    /// Web Res文件夹
    /// </summary>
    /// <returns></returns>
    public static string RES_WEB_ROOT = Path.Combine(Application.dataPath, "../../koa-game-server/web-server/public/Download");
    /// <summary>
    /// 打包根目录
    /// </summary>
    /// <returns></returns>
    public static string BUILD_ROOT = Path.Combine(Application.dataPath, "../Build");
    /// <summary>
    /// AB包打包目录
    /// </summary>
    /// <returns></returns>
    public static string BUILD_AB_ROOT = Path.Combine(Application.dataPath, "../AssetBundles");

    /// <summary>
    /// 下载临时文件夹 (断点传续用)
    /// </summary>
    public static string DOWNLOAD_TEMPFILE_ROOT = Path.Combine(Application.persistentDataPath, "./Temp");

    /// <summary>
    /// 获取Resources的相对路径
    /// </summary>
    public static string GetRelativeResourcesPath(string filePath)
    {
        string result = Path.GetFullPath(filePath).Replace(Path.GetFullPath(RESOURCES) + "\\", "");
        return filePath == result ? null : result;
    }

}