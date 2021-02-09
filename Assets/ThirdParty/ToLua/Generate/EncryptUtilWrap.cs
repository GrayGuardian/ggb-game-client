﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class EncryptUtilWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(EncryptUtil), typeof(System.Object));
		L.RegFunction("ReadBytes", ReadBytes);
		L.RegFunction("ReadString", ReadString);
		L.RegFunction("WriteBytes", WriteBytes);
		L.RegFunction("WriteString", WriteString);
		L.RegFunction("AesEncrypt", AesEncrypt);
		L.RegFunction("AesDecrypt", AesDecrypt);
		L.RegFunction("New", _CreateEncryptUtil);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateEncryptUtil(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				EncryptUtil obj = new EncryptUtil();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: EncryptUtil.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadBytes(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			byte[] o = obj.ReadBytes(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string o = obj.ReadString(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteBytes(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			byte[] arg1 = ToLua.CheckByteBuffer(L, 3);
			obj.WriteBytes(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WriteString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.WriteString(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AesEncrypt(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<byte[]>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
				byte[] o = obj.AesEncrypt(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string o = obj.AesEncrypt(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes<byte[], string>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				byte[] o = obj.AesEncrypt(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes<string, string>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				string o = obj.AesEncrypt(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: EncryptUtil.AesEncrypt");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AesDecrypt(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<byte[]>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
				byte[] o = obj.AesDecrypt(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string o = obj.AesDecrypt(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes<byte[], string>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				byte[] o = obj.AesDecrypt(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes<string, string>(L, 2))
			{
				EncryptUtil obj = (EncryptUtil)ToLua.CheckObject<EncryptUtil>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				string o = obj.AesDecrypt(arg0, arg1);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: EncryptUtil.AesDecrypt");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

