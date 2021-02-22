using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        MonoSingleton.Instance.MonoGo.AddComponent<LuaClient>();
    }
}
