public class Util
{
    public static HttpUtil Http = new HttpUtil();
    public static SocketUtil Socket = new SocketUtil();
    public static ResUtil Res = new ResUtil();
    public static FileUtil File = new FileUtil();
    public static EncryptUtil Encrypt = new EncryptUtil();
    public static JsonUtil Json = new JsonUtil();

    public static string SizeFormat(long byteSize)
    {
        if (byteSize > (1024f * 1024f * 1024f) * 0.95f)
        {
            return string.Format("{0:N2}GB", byteSize / (1024f * 1024f * 1024f));
        }
        else if (byteSize > (1024f * 1024f) * 0.95f)
        {
            return string.Format("{0:N2}MB", byteSize / (1024f * 1024f));
        }
        else if (byteSize > 1024f * 0.95f)
        {
            return string.Format("{0:N2}KB", byteSize / 1024f);
        }
        return string.Format("{0:N2}Bytes", byteSize);
    }
}