using System.Collections;
using UnityEngine;

public class MonoMgr : Singleton<MonoMgr>
{
    public GameObject MonoGo;
    public MonoComponent MonoComponent;
    public MonoMgr()
    {
        MonoGo = new GameObject("MonoGo");
        MonoComponent = MonoGo.AddComponent<MonoComponent>();
        GameObject.DontDestroyOnLoad(MonoGo);
    }
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return MonoComponent.StartCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine)
    {
        MonoComponent.StopCoroutine(routine);
    }
    public void StopAllCoroutine()
    {
        MonoComponent.StopAllCoroutines();
    }

}