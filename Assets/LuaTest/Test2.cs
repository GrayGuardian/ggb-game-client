using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public LuaState lua;
    void Awake()
    {
        lua = new LuaState();
        Debug.Log(lua);
        lua.Start();
        string hello =
            @"                
                a = '我是test2的a值'
                print('test2#')                                  
            ";

        lua.DoString(hello, "test2.lua");
        // lua.CheckTop();
    }
    private void OnDestroy()
    {
        lua.Dispose();
    }
}
