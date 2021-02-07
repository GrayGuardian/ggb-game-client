﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class System_EnumWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.Enum), null);
		L.RegFunction("GetUnderlyingType", GetUnderlyingType);
		L.RegFunction("GetValues", GetValues);
		L.RegFunction("GetName", GetName);
		L.RegFunction("GetNames", GetNames);
		L.RegFunction("IsDefined", IsDefined);
		L.RegFunction("Format", Format);
		L.RegFunction("Equals", Equals);
		L.RegFunction("GetHashCode", GetHashCode);
		L.RegFunction("ToString", ToString);
		L.RegFunction("CompareTo", CompareTo);
		L.RegFunction("HasFlag", HasFlag);
		L.RegFunction("GetTypeCode", GetTypeCode);
		L.RegFunction("Parse", Parse);
		L.RegFunction("ToObject", ToObject);
		L.RegFunction("ToInt", ToInt);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUnderlyingType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Type arg0 = ToLua.CheckMonoType(L, 1);
			System.Type o = System.Enum.GetUnderlyingType(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetValues(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Type arg0 = ToLua.CheckMonoType(L, 1);
			System.Array o = System.Enum.GetValues(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetName(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Type arg0 = ToLua.CheckMonoType(L, 1);
			object arg1 = ToLua.ToVarObject(L, 2);
			string o = System.Enum.GetName(arg0, arg1);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNames(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Type arg0 = ToLua.CheckMonoType(L, 1);
			string[] o = System.Enum.GetNames(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDefined(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Type arg0 = ToLua.CheckMonoType(L, 1);
			object arg1 = ToLua.ToVarObject(L, 2);
			bool o = System.Enum.IsDefined(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Format(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Type arg0 = ToLua.CheckMonoType(L, 1);
			object arg1 = ToLua.ToVarObject(L, 2);
			string arg2 = ToLua.CheckString(L, 3);
			string o = System.Enum.Format(arg0, arg1, arg2);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equals(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
			object arg0 = ToLua.ToVarObject(L, 2);
			bool o = obj != null ? obj.Equals(arg0) : arg0 == null;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
			int o = obj.GetHashCode();
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
				string o = obj.ToString();
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2)
			{
				System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				string o = obj.ToString(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Enum.ToString");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompareTo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
			object arg0 = ToLua.ToVarObject(L, 2);
			int o = obj.CompareTo(arg0);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasFlag(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
			System.Enum arg0 = (System.Enum)ToLua.CheckObject<System.Enum>(L, 2);
			bool o = obj.HasFlag(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTypeCode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Enum obj = (System.Enum)ToLua.CheckObject<System.Enum>(L, 1);
			System.TypeCode o = obj.GetTypeCode();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Parse(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<System.Type, string>(L, 1))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				object o = System.Enum.Parse(arg0, arg1);
				ToLua.Push(L, (Enum)o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes<System.Type, string, bool>(L, 1))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				object o = System.Enum.Parse(arg0, arg1, arg2);
				ToLua.Push(L, (Enum)o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Enum.Parse");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToObject(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<System.Type, int>(L, 1))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
				object o = System.Enum.ToObject(arg0, arg1);
				ToLua.Push(L, (Enum)o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<System.Type, object>(L, 1))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				object arg1 = ToLua.ToVarObject(L, 2);
				object o = System.Enum.ToObject(arg0, arg1);
				ToLua.Push(L, (Enum)o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Enum.ToObject");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToInt(IntPtr L)
	{
		try
        {
            object arg0 = ToLua.CheckObject<System.Enum>(L, 1);
            int ret = Convert.ToInt32(arg0);
            LuaDLL.lua_pushinteger(L, ret);
            return 1;
        }
        catch (Exception e)
        {
            return LuaDLL.toluaL_exception(L, e);
        }
	}
}

