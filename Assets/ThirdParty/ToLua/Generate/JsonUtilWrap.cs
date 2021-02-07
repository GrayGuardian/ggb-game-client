﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class JsonUtilWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(JsonUtil), typeof(System.Object));
		L.RegFunction("get_Item", get_Item);
		L.RegFunction("New", _CreateJsonUtil);
		L.RegVar("this", _this, null);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("JsonDic", get_JsonDic, set_JsonDic);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateJsonUtil(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				JsonUtil obj = new JsonUtil();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: JsonUtil.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _get_this(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			JsonUtil obj = (JsonUtil)ToLua.CheckObject<JsonUtil>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			Newtonsoft.Json.Linq.JObject o = obj[arg0];
			ToLua.PushObject(L, o);
			return 1;

		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _this(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushvalue(L, 1);
			LuaDLL.tolua_bindthis(L, _get_this, null);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			JsonUtil obj = (JsonUtil)ToLua.CheckObject<JsonUtil>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			Newtonsoft.Json.Linq.JObject o = obj[arg0];
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_JsonDic(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			JsonUtil obj = (JsonUtil)o;
			System.Collections.Generic.Dictionary<string,Newtonsoft.Json.Linq.JObject> ret = obj.JsonDic;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index JsonDic on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_JsonDic(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			JsonUtil obj = (JsonUtil)o;
			System.Collections.Generic.Dictionary<string,Newtonsoft.Json.Linq.JObject> arg0 = (System.Collections.Generic.Dictionary<string,Newtonsoft.Json.Linq.JObject>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<string,Newtonsoft.Json.Linq.JObject>));
			obj.JsonDic = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index JsonDic on a nil value");
		}
	}
}

