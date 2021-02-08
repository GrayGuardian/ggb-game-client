﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class MonoComponentWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MonoComponent), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("AddListenEvent", AddListenEvent);
		L.RegFunction("DelListenEvent", DelListenEvent);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddListenEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MonoComponent obj = (MonoComponent)ToLua.CheckObject<MonoComponent>(L, 1);
			System.Action<string> arg0 = (System.Action<string>)ToLua.CheckDelegate<System.Action<string>>(L, 2);
			obj.AddListenEvent(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DelListenEvent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MonoComponent obj = (MonoComponent)ToLua.CheckObject<MonoComponent>(L, 1);
			System.Action<string> arg0 = (System.Action<string>)ToLua.CheckDelegate<System.Action<string>>(L, 2);
			obj.DelListenEvent(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
