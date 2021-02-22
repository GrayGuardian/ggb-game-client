using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        //gameObject.AddComponent<UnityEngine.UI.Image>().color = 
        MonoSingleton.Instance.MonoGo.AddComponent<LuaClient>();
    }
}
