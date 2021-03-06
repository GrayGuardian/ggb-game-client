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
    /// 客户端版本号（不一致则提示下载安装包更新）
    /// </summary>
    public string ClientVersion;
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