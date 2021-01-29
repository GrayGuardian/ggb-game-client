
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class Json : Singleton<Json>
{
    public Dictionary<string, JObject> jsonDic = new Dictionary<string, JObject>();
    public Json()
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
            string json = File.ReadAllText(file.ToString());
            JObject jObject = JObject.Parse(json);
            jsonDic.Add(key, jObject);
        }
    }
    public JObject this[string key]
    {
        get
        {
            return jsonDic[key];
        }
    }
}