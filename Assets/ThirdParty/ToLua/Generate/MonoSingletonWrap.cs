﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class MonoSingletonWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(MonoSingleton), typeof(Singleton<MonoSingleton>));
		L.RegFunction("StartCoroutine", StartCoroutine);
		L.RegFunction("StopCoroutine", StopCoroutine);
		L.RegFunction("StopAllCoroutine", StopAllCoroutine);
		L.RegFunction("New", _CreateMonoSingleton);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("MonoGo", get_MonoGo, set_MonoGo);
		L.RegVar("MonoComponent", get_MonoComponent, set_MonoComponent);
		L.RegVar("MonoNode", get_MonoNode, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMonoSingleton(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				MonoSingleton obj = new MonoSingleton();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: MonoSingleton.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartCoroutine(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MonoSingleton obj = (MonoSingleton)ToLua.CheckObject<MonoSingleton>(L, 1);
			System.Collections.IEnumerator arg0 = ToLua.CheckIter(L, 2);
			UnityEngine.Coroutine o = obj.StartCoroutine(arg0);
			ToLua.PushSealed(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopCoroutine(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			MonoSingleton obj = (MonoSingleton)ToLua.CheckObject<MonoSingleton>(L, 1);
			UnityEngine.Coroutine arg0 = (UnityEngine.Coroutine)ToLua.CheckObject(L, 2, typeof(UnityEngine.Coroutine));
			obj.StopCoroutine(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAllCoroutine(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			MonoSingleton obj = (MonoSingleton)ToLua.CheckObject<MonoSingleton>(L, 1);
			obj.StopAllCoroutine();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MonoGo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MonoSingleton obj = (MonoSingleton)o;
			UnityEngine.GameObject ret = obj.MonoGo;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index MonoGo on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MonoComponent(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MonoSingleton obj = (MonoSingleton)o;
			MonoComponent ret = obj.MonoComponent;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index MonoComponent on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MonoNode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MonoSingleton obj = (MonoSingleton)o;
			UnityEngine.Transform ret = obj.MonoNode;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index MonoNode on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MonoGo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MonoSingleton obj = (MonoSingleton)o;
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckObject(L, 2, typeof(UnityEngine.GameObject));
			obj.MonoGo = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index MonoGo on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MonoComponent(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			MonoSingleton obj = (MonoSingleton)o;
			MonoComponent arg0 = (MonoComponent)ToLua.CheckObject<MonoComponent>(L, 2);
			obj.MonoComponent = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index MonoComponent on a nil value");
		}
	}
}

