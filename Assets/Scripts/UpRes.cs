using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UpRes : MonoBehaviour
{
    VObject UpData;

    private void Awake()
    {
        UpData = ResMgr.Instance.getUpdata();
        if (UpData == null)
        {
            Debug.Log("不需要更新");
        }
        else
        {
            Debug.Log("需要更新");
            //UpdateRes();
            if (UpData.Version != ResMgr.Instance.Version.Version)
            {
                Debug.Log("更新版本");
                ShowUI_Updata_Info();
            }
            else
            {
                Debug.Log("文件损坏 需要更新资源");
                long size = 0;
                foreach (var ab in UpData.ABs)
                {
                    size += ab.size;
                }
                ShowUI_Tip(string.Format("文件损坏,需要重新下载\n共计需要下载{0:N2}M资源", size / 1024f / 1024f), () =>
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
            UpData.ABs,
            () => { Bar_Node.gameObject.SetActive(false); DownloadOver(); },
            (ab) => { Debug.Log(string.Format("AB包[{0}]开始更新", ab.name)); },
            (ab) => { Debug.Log(string.Format("AB包[{0}]更新完毕", ab.name)); },
            (ab, order, size, count) =>
            {
                Bar_Value.fillAmount = (float)(size / count);
                Bar_Desc.text = string.Format("正在下载>>>[{0}/{1}]：{2:N2}MB/{3:N2}MB", order, UpData.ABs.Length, size, count);
                Debug.Log(string.Format("AB包[{0}]正在更新：{1}/{2}", ab.name, size, count));
            }
        );
    }
    void DownloadOver()
    {
        Debug.Log("所有资源更新完毕");

    }
    public void UpABFile(ABVObject[] abInfos, Action allDownloadOverEvent = null, Action<ABVObject> singleDownloadStartEvent = null, Action<ABVObject> singleDownloadOverEvent = null, Action<ABVObject, int, double, double> singleDownloadUpdateEvent = null, int order = 0)
    {
        if (abInfos == null || abInfos.Length <= order)
        {
            //全部AB包下载更新完毕
            if (allDownloadOverEvent != null) allDownloadOverEvent();
            return;
        }
        ABVObject abInfo = abInfos[order];
        if (singleDownloadStartEvent != null) singleDownloadStartEvent(abInfo);
        HttpMgr.Instance.Download(Util.Json["config"]["download_url"] + "AssetBundles/" + abInfo.name, Path.Combine(PathConst.RES_LOCAL_ROOT, "./AssetBundles/", abInfo.name), (abInfo.name + "_" + abInfo.hash + ".temp"),
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
        node.Find("VersionText").GetComponent<Text>().text = UpData.Version;
        node.Find("ContentText").GetComponent<Text>().text = UpData.Content;


        if (UpData.UpdateType == 0)
        {
            //在线更新
            long size = 0;
            foreach (var ab in UpData.ABs)
            {
                size += ab.size;
            }
            node.Find("DescText").GetComponent<Text>().text = string.Format("共计需要下载{0:N2}M资源", size / 1024f / 1024f);
            node.Find("Button/Text").GetComponent<Text>().text = "开始下载";
        }
        else if (UpData.UpdateType == 1)
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
        if (UpData.UpdateType == 0)
        {
            //在线更新
            DownloadRes();
        }
        else if (UpData.UpdateType == 1)
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
