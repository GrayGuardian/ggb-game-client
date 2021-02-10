using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UpRes : MonoBehaviour
{
    VObject refData;

    private void Awake()
    {
        refData = Util.Res.GetRefdata();
        if (refData == null)
        {
            Debug.Log("不需要更新");
            ClearRedundantRes();
        }
        else
        {
            Debug.Log("需要更新");
            //UpdateRes();
            if (refData.Version != Util.Res.Version.Version)
            {
                if (refData.UpdateType == 0)
                {
                    if (refData.ABs.Length > 0)
                    {
                        Debug.Log("在线更新>>下载文件");
                        ShowUI_Updata_Info();
                    }
                    else
                    {
                        Debug.Log("在线更新>>不需要下载文件");
                        DownloadOver();
                    }
                }
                if (refData.UpdateType == 1)
                {
                    Debug.Log("强制更新");
                    ShowUI_Updata_Info();
                }
            }
            else
            {
                Debug.Log("文件损坏 需要更新资源>>" + refData.toString());
                long size = 0;
                foreach (var ab in refData.ABs)
                {
                    size += ab.size;
                }
                ShowUI_Tip(string.Format("文件损坏,需要重新下载\n共计需要下载{0}资源", Util.SizeFormat((long)size)), () =>
               {
                   DownloadRes();
               });
            }
        }
    }
    void DownloadRes()
    {
        Debug.Log("开始下载资源");
        Bar_Node.gameObject.SetActive(true);
        UpABFile(
            refData.ABs,
            () => { Bar_Node.gameObject.SetActive(false); DownloadOver(); },
            (ab) => { Debug.Log(string.Format("AB包[{0}]开始更新", ab.name)); },
            (ab) => { Debug.Log(string.Format("AB包[{0}]更新完毕", ab.name)); },
            (ab, order, size, count) =>
            {
                Bar_Value.fillAmount = (float)size / count;
                Bar_Desc.text = string.Format("正在下载>>>[{0}/{1}]：{2}/{3}", order + 1, refData.ABs.Length, Util.SizeFormat(size), Util.SizeFormat(count));
                //Debug.Log(string.Format("AB包[{0}]正在更新：{1}/{2}", ab.name, size, count));
            }
        );
    }
    void DownloadOver()
    {
        Debug.Log("所有资源更新完毕");
        ClearRedundantRes();
        Util.Res.UpVersion();
    }

    void ClearRedundantRes()
    {
        Debug.Log("清理冗余代码");
        DirectoryInfo dirInfo = new DirectoryInfo(GameConst.DOWNLOAD_TEMPFILE_ROOT);
        foreach (var fileInfo in dirInfo.GetFiles())
        {
            fileInfo.Delete();
        }
        ClearOver();
    }
    void ClearOver()
    {
        Debug.Log("清理完毕");
        AllOver();
    }

    void AllOver()
    {
        Debug.Log("所有操作执行完毕，启用游戏逻辑");
        //Util.Res.LoadAssetBundle("common");
        // Debug.Log(Util.Res.LoadSprite("comm1on", "bg"));
        //MonoSingleton.Instance.MonoGo.AddComponent<LuaClient>();
    }
    /// <summary>
    /// 更新AB包
    /// </summary>
    /// <param name="abInfos">AB包信息数组</param>
    /// <param name="allDownloadOverEvent">全部下载完毕回调</param>
    /// <param name="singleDownloadStartEvent">单AB包开始下载回调</param>
    /// <param name="singleDownloadOverEvent">单AB包下载完毕回调</param>
    /// <param name="singleDownloadUpdateEvent">单AB包下载过程中持续回调</param>
    /// <param name="order"></param>
    public void UpABFile(ABVObject[] abInfos, Action allDownloadOverEvent = null, Action<ABVObject> singleDownloadStartEvent = null, Action<ABVObject> singleDownloadOverEvent = null, Action<ABVObject, int, long, long> singleDownloadUpdateEvent = null, int order = 0)
    {
        if (abInfos == null || abInfos.Length <= order)
        {
            //全部AB包下载更新完毕
            if (allDownloadOverEvent != null) allDownloadOverEvent();
            return;
        }
        ABVObject abInfo = abInfos[order];
        if (singleDownloadStartEvent != null) singleDownloadStartEvent(abInfo);
        Util.Http.Download(GameConst.DOWNLOAD_URL + "AssetBundles/" + abInfo.name, Path.Combine(GameConst.RES_LOCAL_ROOT, "./AssetBundles/", abInfo.name), (abInfo.name + "_" + abInfo.hash + ".temp"),
           () =>
           {
               if (singleDownloadOverEvent != null) singleDownloadOverEvent(abInfo);
               UpABFile(abInfos, allDownloadOverEvent, singleDownloadStartEvent, singleDownloadOverEvent, singleDownloadUpdateEvent, order + 1);
           },
           (downloadSize, countSize) =>
           {
               if (singleDownloadUpdateEvent != null) singleDownloadUpdateEvent(abInfo, order, downloadSize, countSize);
           });
    }

    //-----------ui---------------------

    public RectTransform UI_Updata_Info_Node;
    public RectTransform UI_Tip_Node;
    private Action _onTipSucceedEvent;
    public RectTransform Bar_Node;
    public Text Bar_Desc;
    public Image Bar_Value;
    void ShowUI_Updata_Info()
    {
        RectTransform node = UI_Updata_Info_Node;
        node.Find("VersionText").GetComponent<Text>().text = refData.Version;
        node.Find("ContentText").GetComponent<Text>().text = refData.Content;
              

        if (refData.UpdateType == 0)
        {
            //在线更新
            long size = 0;
            foreach (var ab in refData.ABs)
            {
                size += ab.size;
            }
            Debug.Log(size / 1024f);
            node.Find("DescText").GetComponent<Text>().text = string.Format("共计需要下载{0}资源", Util.SizeFormat(size));
            node.Find("Button/Text").GetComponent<Text>().text = "开始下载";
        }
        else if (refData.UpdateType == 1)
        {
            //自行更新
            node.Find("DescText").GetComponent<Text>().text = "需要手动下载安装包更新";
            node.Find("Button/Text").GetComponent<Text>().text = "前往下载";
        }

        node.gameObject.SetActive(true);
    }
    void HideUI_Updata_Info()
    {
        RectTransform node = UI_Updata_Info_Node;
        node.gameObject.SetActive(false);
    }

    public void OnUpdataBtnClick()
    {
        if (refData.UpdateType == 0)
        {
            //在线更新
            DownloadRes();
        }
        else if (refData.UpdateType == 1)
        {
            //自行更新
            Debug.Log("转跳下载，退出游戏");
        }
        HideUI_Updata_Info();
    }

    void ShowUI_Tip(string desc, Action succeedEvent = null)
    {
        RectTransform node = UI_Tip_Node;
        node.Find("DescText").GetComponent<Text>().text = desc;
        _onTipSucceedEvent = succeedEvent;
        node.gameObject.SetActive(true);
    }
    void HideUI_Tip()
    {
        RectTransform node = UI_Tip_Node;
        node.gameObject.SetActive(false);
    }
    public void OnTipSucceedBtnClick()
    {
        if (_onTipSucceedEvent != null) _onTipSucceedEvent();
        HideUI_Tip();
    }
    //-----------ui---------------------



}
