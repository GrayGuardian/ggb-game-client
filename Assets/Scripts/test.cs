using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JObject jObject = JObject.Parse("{'a':1}");
        Debug.Log(Json.Instance["config"]["http"]);
        Debug.Log(Json.Instance["config"]["download_url"]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
