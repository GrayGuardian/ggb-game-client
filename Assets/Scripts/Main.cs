using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        //控制台输出
#if UNITY_EDITOR

#else
        MonoSingleton.Instance.MonoGo.AddComponent<TestConsole>().visible = true;
#endif

        MonoSingleton.Instance.MonoGo.AddComponent<LuaClient>();
    }
}
