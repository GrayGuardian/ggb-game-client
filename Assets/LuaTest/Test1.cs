using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public LuaState lua;
    void Awake()
    {
        lua = new LuaState();
        lua.Start();
        string hello =
            @"      
                a = '我是test1的a值'          
                print('test1#')  
                abc = function()
	                return '我是Func abc';
                end
            ";

        lua.DoString(hello, "test1.lua");
        // lua.CheckTop();
    }
    private void OnDestroy()
    {
        lua.Dispose();
    }
}
