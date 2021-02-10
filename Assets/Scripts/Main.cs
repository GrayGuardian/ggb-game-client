using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        //控制台输出
        MonoSingleton.Instance.MonoGo.AddComponent<TestConsole>();
        Debug.Log("加载Main Awake" + "   " + gameObject.name);
        MonoSingleton.Instance.MonoGo.AddComponent<LuaClient>();
    }
}
