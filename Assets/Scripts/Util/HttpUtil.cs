using System.Diagnostics;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

public class HttpUtil
{
    public CookieContainer cookies = new CookieContainer();
    /// <summary>
    /// GET方法(自动维护cookie)
    /// </summary>
    public string Get(string url, string referer = "", int timeout = 2000, Encoding encode = null)
    {
        string dat;
        HttpWebResponse res = null;
        HttpWebRequest req = null;
        try
        {
            req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = cookies;
            req.AllowAutoRedirect = false;
            req.Timeout = timeout;
            req.Referer = referer;
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0;%20WOW64; rv:47.0) Gecko/20100101 Firefox/47.0";
            res = (HttpWebResponse)req.GetResponse();
            cookies.Add(res.Cookies);
            dat = new StreamReader(res.GetResponseStream(), encode ?? Encoding.UTF8).ReadToEnd();
            res.Close();
            req.Abort();
        }
        catch
        {
            return null;
        }
        return dat;
    }


    /// <summary>
    /// Post方法(自动维护cookie)
    /// </summary>
    public string Post(string url, string postdata, CookieContainer cookie = null, string referer = "", int timeout = 2000, Encoding encode = null)
    {
        string html = null;
        HttpWebRequest request;
        HttpWebResponse response;
        if (encode == null) encode = Encoding.UTF8;
        try
        {
            byte[] byteArray = encode.GetBytes(postdata); // 转化
            request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            if (cookie == null) cookie = new CookieContainer();
            request.CookieContainer = cookie;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; InfoPath.1)";
            request.Method = "POST";
            request.Referer = referer;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            request.Timeout = timeout;
            Stream newStream = request.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);    //写入参数
            newStream.Close();
            response = (HttpWebResponse)request.GetResponse();
            cookie.Add(response.Cookies);
            StreamReader str = new StreamReader(response.GetResponseStream(), encode);
            html = str.ReadToEnd();
        }
        catch
        {
            return "";
        }
        return html;
    }


    public void Download(string url, string saveFile, string tempFileName = null, Action cb = null, Action<int, int> downloadingCb = null)
    {
        MonoSingleton.Instance.StartCoroutine(_download(url, saveFile, tempFileName, cb, downloadingCb));
    }
    IEnumerator _download(string url, string saveFile, string tempFileName = null, Action cb = null, Action<int, int> downloadingCb = null)
    {
        string fileName = Path.GetFileName(saveFile);
        tempFileName = tempFileName == null ? (fileName + ".temp") : tempFileName;
        //删除本地临时文件
        string strTmpFile = Path.Combine(GameConst.DOWNLOAD_TEMPFILE_ROOT, tempFileName);

        //临时文件夹不存在则创建
        if (!File.Exists(GameConst.DOWNLOAD_TEMPFILE_ROOT))
        {
            Directory.CreateDirectory(GameConst.DOWNLOAD_TEMPFILE_ROOT);
        }

        //打开上次下载的文件或新建文件 

        long lStartPos = 0;
        System.IO.FileStream fs;
        if (File.Exists(strTmpFile))
        {
            fs = System.IO.File.OpenWrite(strTmpFile);
            lStartPos = fs.Length;
            fs.Seek(lStartPos, System.IO.SeekOrigin.Current); //移动文件流中的当前指针 
        }
        else
        {
            fs = new System.IO.FileStream(strTmpFile, System.IO.FileMode.Create);
            lStartPos = 0;
        }

        //打开网络连接 
        HttpWebRequest request;
        request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
        request.Timeout = 5000;
        if (lStartPos > 0)
        {
            request.AddRange((int)lStartPos); //设置Range值
        }

        //获取临时文件大小，注意，不要再创建request，连续请求可能卡死，开启下次请求前请先结束上一次的请求。
        //重要的事说三遍：不要连续请求！不要连续请求！不要连续请求！
        long countLength = request.GetResponse().ContentLength;

        //向服务器请求，获得服务器回应数据流 
        System.IO.Stream ns = request.GetResponse().GetResponseStream();
        int len = 1024 * 8;
        byte[] nbytes = new byte[len];
        int nReadSize = 0;
        nReadSize = ns.Read(nbytes, 0, len);
        while (nReadSize > 0)
        {
            fs.Write(nbytes, 0, nReadSize);
            nReadSize = ns.Read(nbytes, 0, len);
            int dDownloadedLength = (int)(fs.Length - lStartPos);
            int dTotalLength = (int)countLength;
            //UnityEngine.Debug.Log(string.Format("已下载 {0} / {1}", dDownloadedLength, dTotalLength));
            if (downloadingCb != null) downloadingCb(dDownloadedLength, dTotalLength);
            yield return false;
        }
        fs.Close();
        ns.Close();
        //保存文件夹不存在则创建
        if (!File.Exists(Path.GetDirectoryName(saveFile)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(saveFile));
        }
        //清理原有文件
        if (File.Exists(saveFile))
        {
            File.Delete(saveFile);
        }
        //下载完成移动到保存路径

        Util.File.MoveTo(strTmpFile, saveFile);
        if (cb != null) cb();
    }

}

