
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class JsonUtil
{
    public Dictionary<string, JObject> JsonDic = new Dictionary<string, JObject>();

    public JsonUtil()
    {
        Util.Res.LoadAssetBundle("json");
        foreach (var key in GameConst.RES_JSONS)
        {
            string json = Util.Res.LoadString("json", key);
            JObject jObject = JObject.Parse(json);
            JsonDic.Add(key, jObject);
        }
        Util.Res.UnLoadAssetBundle("json");

    }
    public JObject this[string key]
    {
        get
        {
            return JsonDic[key];
        }
    }


}