using System.Diagnostics;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class HttpResult
{
    public int code;
    public byte[] bytes;
    public string content;
}
public class HttpUtil
{
    /// <summary>
    /// 主线程
    /// </summary>
    private SynchronizationContext _mainThreadSynContext;

    public HttpUtil()
    {
        _mainThreadSynContext = SynchronizationContext.Current;
    }
    private void MainCallBack(object param)
    {
        var tag = (string)param.GetType().GetProperty("tag").GetValue(param);
        switch (tag)
        {
            case "Get":
                var getCb = (Action)param.GetType().GetProperty("cb").GetValue(param);
                if (getCb != null) getCb();
                break;
            case "GetError":
                var getErrorCb = (Action)param.GetType().GetProperty("cb").GetValue(param);
                if (getErrorCb != null) getErrorCb();
                break;
            case "Post":
                var postCb = (Action)param.GetType().GetProperty("cb").GetValue(param);
                if (postCb != null) postCb();
                break;
            case "PostError":
                var postErrorCb = (Action)param.GetType().GetProperty("cb").GetValue(param);
                if (postErrorCb != null) postErrorCb();
                break;
            case "DownloadOver":
                var downloadOverCb = (Action)param.GetType().GetProperty("cb").GetValue(param);
                if (downloadOverCb != null) downloadOverCb();
                break;
            case "DownloadIng":
                var size = (int)param.GetType().GetProperty("size").GetValue(param);
                var count = (int)param.GetType().GetProperty("count").GetValue(param);
                var downloadIngCb = (Action<int, int>)param.GetType().GetProperty("cb").GetValue(param);
                if (downloadIngCb != null) downloadIngCb(size, count);
                break;
        }

    }
    public void Get_Asyn(string url, Action<HttpResult> cb = null, Action errorCb = null)
    {
        Thread thread = new Thread(new ThreadStart(() =>
        {
            HttpResult result = Get(url, () =>
            {
                _mainThreadSynContext.Post(new SendOrPostCallback(MainCallBack), new { tag = "GetError", cb = errorCb });
            });
            Action t_cb = () => { if (cb != null) cb(result); };
            if (result.bytes.Length > 0)
            {
                _mainThreadSynContext.Post(new SendOrPostCallback(MainCallBack), new { tag = "Get", cb = t_cb });
            }
        }));
        thread.Start();
    }

    /// <summary>
    /// GET方法
    /// </summary>
    public HttpResult Get(string url, Action errorCb = null)
    {
        HttpWebResponse res = null;
        HttpWebRequest req = null;
        try
        {
            req = (HttpWebRequest)WebRequest.Create(url);
            req.AllowAutoRedirect = false;
            req.Timeout = 1000;
            res = (HttpWebResponse)req.GetResponse();
            int code = (int)res.StatusCode;

            Stream sr = res.GetResponseStream();
            List<byte> byteArray = new List<byte>();
            while (true)
            {
                int b = sr.ReadByte();
                if (b == -1) break;
                byteArray.Add((byte)b);
            }
            sr.Close();
            res.Close();
            req.Abort();
            byte[] bytes = byteArray.ToArray();
            string content = Encoding.UTF8.GetString(bytes);
            if (bytes.Length > 0)
            {
                return new HttpResult() { code = code, bytes = bytes, content = content };
            }
        }
        catch
        {
            if (errorCb != null) errorCb();
        }
        return new HttpResult() { code = -1, bytes = new byte[] { }, content = "" };
    }

    public void Post_Asyn(string url, byte[] body, string token, Action<HttpResult> cb = null, Action errorCb = null)
    {
        Thread thread = new Thread(new ThreadStart(() =>
        {
            HttpResult result = Post(url, body, token, () =>
             {
                 _mainThreadSynContext.Post(new SendOrPostCallback(MainCallBack), new { tag = "PostError", cb = errorCb });
             });

            Action t_cb = () => { if (cb != null) cb(result); };
            if (result.bytes.Length > 0)
            {
                _mainThreadSynContext.Post(new SendOrPostCallback(MainCallBack), new { tag = "Post", cb = t_cb });
            }
        }));
        thread.Start();
    }


    /// <summary>
    /// Post方法
    /// </summary>
    public HttpResult Post(string url, byte[] body, string token = "", Action errorCb = null)
    {
        HttpWebResponse res;
        HttpWebRequest req;
        Encoding encode = Encoding.Default;
        try
        {
            req = (HttpWebRequest)WebRequest.Create(new Uri(url));
            req.Method = "POST";
            req.Accept = "*/*";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; InfoPath.1)";
            req.ContentType = "text/plain";
            req.ContentLength = body.Length;
            if (token != "")
            {
                req.Headers.Add("Token", token);
            }
            req.Timeout = 1000;
            Stream newStream = req.GetRequestStream();
            newStream.Write(body, 0, body.Length);    //写入参数
            newStream.Close();
            res = (HttpWebResponse)req.GetResponse();
            int code = (int)res.StatusCode;

            Stream sr = res.GetResponseStream();
            List<byte> byteArray = new List<byte>();
            while (true)
            {
                int b = sr.ReadByte();
                if (b == -1) break;
                byteArray.Add((byte)b);
            }
            sr.Close();
            res.Close();
            req.Abort();
            byte[] bytes = byteArray.ToArray();
            string content = Encoding.UTF8.GetString(bytes);
            if (bytes.Length > 0)
            {
                return new HttpResult() { code = code, bytes = bytes, content = content };
            }
        }
        catch
        {
            if (errorCb != null) errorCb();
        }
        return new HttpResult() { code = -1, bytes = new byte[] { }, content = "" };
    }

    public void Download(string url, string saveFile, string tempFileName = null, Action cb = null, Action<int, int> downloadingCb = null, Action errorCb = null)
    {
        MonoSingleton.Instance.StartCoroutine(new CatchableEnumerator(_download(url, saveFile, tempFileName, cb, downloadingCb, errorCb), errorCb));

    }
    IEnumerator _download(string url, string saveFile, string tempFileName = null, Action cb = null, Action<int, int> downloadingCb = null, Action errorCb = null)
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
        HttpWebResponse response;
        request = (HttpWebRequest)WebRequest.Create(new Uri(url));
        request.Method = "HEAD";
        request.Timeout = 100;
        response = (HttpWebResponse)request.GetResponse();

        request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
        request.Timeout = 2000;
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
        int size = 0, count = 0;
        while (nReadSize > 0)
        {
            fs.Write(nbytes, 0, nReadSize);
            nReadSize = ns.Read(nbytes, 0, len);
            size = (int)(fs.Length - lStartPos);
            count = (int)countLength;
            //UnityEngine.Debug.Log(string.Format("已下载 {0} / {1}", size, count));
            if (downloadingCb != null) downloadingCb(size + (int)lStartPos, count + (int)lStartPos);
            yield return 1;
        }
        fs.Close();
        ns.Close();
        if (size < count)
        {
            if (errorCb != null) errorCb();
        }
        else
        {
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

}

