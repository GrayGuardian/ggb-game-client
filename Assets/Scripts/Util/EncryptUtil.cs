using System.Text;
using System.IO;
using System.Security.Cryptography;
using System;

public class EncryptUtil
{
    const string DEFAULT_KEY = "21232f297a57a5a743894a0e4a801fc3";
    /// <summary>
    /// 读取文件 字节流
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    public byte[] ReadBytes(string path)
    {
        byte[] data = Util.File.ReadBytes(path);
        return AesDecrypt(data);
    }
    /// <summary>
    /// 读取文件 字符串
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="encoding">编码</param>
    /// <returns></returns>
    public string ReadString(string path)
    {
        string str = Util.File.ReadString(path);
        return AesDecrypt(str, null);
    }
    /// <summary>
    /// 写到文件 字节流
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="data">数据</param>
    /// <returns></returns>
    public void WriteBytes(string path, byte[] data)
    {
        data = AesEncrypt(data);
        File.WriteAllBytes(path, data);
    }
    /// <summary>
    /// 写到文件 字符串
    /// </summary>
    /// <param name="key">密钥</param>
    /// <param name="path">文件路径</param>
    /// <param name="data">数据</param>
    /// <param name="encoding">编码</param>
    /// <returns></returns>
    public void WriteString(string path, string data)
    {
        data = AesEncrypt(data, null);
        File.WriteAllText(path, data);
    }
    /// <summary>
    ///  AES 加密
    /// </summary>
    /// <param name="data">明文（待加密）</param>
    /// <param name="key">密钥 32位</param>
    /// <returns></returns>
    public byte[] AesEncrypt(byte[] data, string key = null)
    {
        try
        {
            if (data.Length == 0) return data;
            key = key == null ? DEFAULT_KEY : key;
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            return cTransform.TransformFinalBlock(data, 0, data.Length);
        }
        catch
        {
            return new byte[] { };
        }
    }
    /// <summary>
    ///  AES 解密
    /// </summary>
    /// <param name="data">明文（待解密）</param>
    /// <param name="key">密钥 32位</param>
    /// <returns></returns>
    public byte[] AesDecrypt(byte[] data, string key = null)
    {
        try
        {
            if (data.Length == 0) return data;
            key = key == null ? DEFAULT_KEY : key;
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            return cTransform.TransformFinalBlock(data, 0, data.Length);
        }
        catch
        {
            return new byte[] { };
        }
    }
    /// <summary>
    ///  AES 加密
    /// </summary>
    /// <param name="str">明文（待加密）</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public string AesEncrypt(string str, string key = null)
    {
        try
        {
            if (str == "") return str;
            key = key == null ? DEFAULT_KEY : key;
            byte[] data;
            data = Encoding.UTF8.GetBytes(str);
            data = AesEncrypt(data, key);
            return Convert.ToBase64String(data, 0, data.Length);
        }
        catch
        {
            return "";
        }
    }
    /// <summary>
    ///  AES 解密
    /// </summary>
    /// <param name="data">明文（待解密）</param>
    /// <param name="key">密钥</param>
    /// <returns></returns>
    public string AesDecrypt(string str, string key = null)
    {
        try
        {
            if (str == "") return str;
            key = key == null ? DEFAULT_KEY : key;
            byte[] data;
            data = Convert.FromBase64String(str);
            data = AesDecrypt(data, key);
            return Encoding.UTF8.GetString(data);
        }
        catch
        {
            return "";
        }
    }

}