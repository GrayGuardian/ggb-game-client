
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class JsonUtil
{
    public Dictionary<string, JObject> JsonDic = new Dictionary<string, JObject>();
    
    public JsonUtil()
    {
        string root = Path.Combine(Application.dataPath, "Scripts/Json");
        FileInfo[] files = new DirectoryInfo(root).GetFiles();
        foreach (var file in files)
        {
            if (Path.GetExtension(file.ToString()) != ".json")
            {
                continue;
            }
            string key = Path.GetFileNameWithoutExtension(file.ToString());
            string json = Util.File.ReadString(file.ToString());
            JObject jObject = JObject.Parse(json);
            JsonDic.Add(key, jObject);
        }
    }
    public JObject this[string key]
    {
        get
        {
            return JsonDic[key];
        }
    }


}