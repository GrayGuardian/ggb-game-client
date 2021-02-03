public class ABVObject
{
    /// <summary>
    /// AB包资源名
    /// </summary>
    public string name;
    /// <summary>
    /// AB包大小
    /// </summary>
    public int size;
    /// <summary>
    /// AB包Hash值
    /// </summary>
    public string hash;
}
public class VObject
{
    /// <summary>
    /// 版本号
    /// </summary>
    public string Version;
    /// <summary>
    /// 更新类型
    /// 0-在线热更 1-自行下载
    /// </summary>
    public int UpdateType;
    /// <summary>
    /// 是否需要重启
    /// </summary>
    public bool IsRestart;
    /// <summary>
    /// AB包版本信息
    /// </summary>
    public ABVObject[] ABs;
}