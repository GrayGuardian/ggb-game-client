using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        //控制台输出
        MonoSingleton.Instance.MonoGo.AddComponent<TestConsole>();
        MonoSingleton.Instance.MonoGo.AddComponent<LuaClient>();
    }
}
