using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Util
{

    public static EncryptUtil Encrypt = new EncryptUtil();
    // public Util()
    // {

    // }
    /// <summary>
    /// 获取字节集哈希值
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static string ComputeHash(byte[] buffer)
    {
        if (buffer == null || buffer.Length < 1)
            return "";
        MD5 md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(buffer);
        StringBuilder sb = new StringBuilder();
        foreach (var b in hash)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
    /// <summary>
    /// 获取文件哈希值
    /// </summary>
    /// <returns></returns>
    public static string ComputeHash(string filePath)
    {
        try
        {
            return ComputeHash(File.ReadAllBytes(filePath));
        }
        catch
        {
            return "";
        }
    }

}