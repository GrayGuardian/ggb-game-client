using Newtonsoft.Json;

public class ABVObject
{
    /// <summary>
    /// AB包资源名
    /// </summary>
    public string name;
    /// <summary>
    /// AB包大小
    /// </summary>
    public long size;
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
    /// 更新内容
    /// </summary>
    public string Content;
    /// <summary>
    /// AB包版本信息
    /// </summary>
    public ABVObject[] ABs;

    public string toString()
    {
        return JsonConvert.SerializeObject(this);
    }
}