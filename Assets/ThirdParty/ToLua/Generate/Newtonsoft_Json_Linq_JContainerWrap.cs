﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Newtonsoft_Json_Linq_JContainerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Newtonsoft.Json.Linq.JContainer), typeof(Newtonsoft.Json.Linq.JToken));
		L.RegFunction("Children", Children);
		L.RegFunction("Descendants", Descendants);
		L.RegFunction("DescendantsAndSelf", DescendantsAndSelf);
		L.RegFunction("Add", Add);
		L.RegFunction("AddFirst", AddFirst);
		L.RegFunction("CreateWriter", CreateWriter);
		L.RegFunction("ReplaceAll", ReplaceAll);
		L.RegFunction("RemoveAll", RemoveAll);
		L.RegFunction("Merge", Merge);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("HasValues", get_HasValues, null);
		L.RegVar("First", get_First, null);
		L.RegVar("Last", get_Last, null);
		L.RegVar("Count", get_Count, null);
		L.RegVar("ListChanged", get_ListChanged, set_ListChanged);
		L.RegVar("AddingNew", get_AddingNew, set_AddingNew);
		L.RegVar("CollectionChanged", get_CollectionChanged, set_CollectionChanged);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Children(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			Newtonsoft.Json.Linq.JEnumerable<Newtonsoft.Json.Linq.JToken> o = obj.Children();
			ToLua.PushValue(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Descendants(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken> o = obj.Descendants();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DescendantsAndSelf(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken> o = obj.DescendantsAndSelf();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.Add(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddFirst(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.AddFirst(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateWriter(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			Newtonsoft.Json.JsonWriter o = obj.CreateWriter();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReplaceAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.ReplaceAll(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
			obj.RemoveAll();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Merge(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
				object arg0 = ToLua.ToVarObject(L, 2);
				obj.Merge(arg0);
				return 0;
			}
			else if (count == 3)
			{
				Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject<Newtonsoft.Json.Linq.JContainer>(L, 1);
				object arg0 = ToLua.ToVarObject(L, 2);
				Newtonsoft.Json.Linq.JsonMergeSettings arg1 = (Newtonsoft.Json.Linq.JsonMergeSettings)ToLua.CheckObject<Newtonsoft.Json.Linq.JsonMergeSettings>(L, 3);
				obj.Merge(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Newtonsoft.Json.Linq.JContainer.Merge");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HasValues(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)o;
			bool ret = obj.HasValues;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index HasValues on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_First(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)o;
			Newtonsoft.Json.Linq.JToken ret = obj.First;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index First on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Last(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)o;
			Newtonsoft.Json.Linq.JToken ret = obj.Last;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Last on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)o;
			int ret = obj.Count;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Count on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ListChanged(IntPtr L)
	{
		ToLua.Push(L, new EventObject(typeof(System.ComponentModel.ListChangedEventHandler)));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AddingNew(IntPtr L)
	{
		ToLua.Push(L, new EventObject(typeof(System.ComponentModel.AddingNewEventHandler)));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CollectionChanged(IntPtr L)
	{
		ToLua.Push(L, new EventObject(typeof(System.Collections.Specialized.NotifyCollectionChangedEventHandler)));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ListChanged(IntPtr L)
	{
		try
		{
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject(L, 1, typeof(Newtonsoft.Json.Linq.JContainer));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'Newtonsoft.Json.Linq.JContainer.ListChanged' can only appear on the left hand side of += or -= when used outside of the type 'Newtonsoft.Json.Linq.JContainer'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.ComponentModel.ListChangedEventHandler ev = (System.ComponentModel.ListChangedEventHandler)arg0.func;
				obj.ListChanged += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.ComponentModel.ListChangedEventHandler ev = (System.ComponentModel.ListChangedEventHandler)arg0.func;
				obj.ListChanged -= ev;
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AddingNew(IntPtr L)
	{
		try
		{
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject(L, 1, typeof(Newtonsoft.Json.Linq.JContainer));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'Newtonsoft.Json.Linq.JContainer.AddingNew' can only appear on the left hand side of += or -= when used outside of the type 'Newtonsoft.Json.Linq.JContainer'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.ComponentModel.AddingNewEventHandler ev = (System.ComponentModel.AddingNewEventHandler)arg0.func;
				obj.AddingNew += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.ComponentModel.AddingNewEventHandler ev = (System.ComponentModel.AddingNewEventHandler)arg0.func;
				obj.AddingNew -= ev;
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CollectionChanged(IntPtr L)
	{
		try
		{
			Newtonsoft.Json.Linq.JContainer obj = (Newtonsoft.Json.Linq.JContainer)ToLua.CheckObject(L, 1, typeof(Newtonsoft.Json.Linq.JContainer));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'Newtonsoft.Json.Linq.JContainer.CollectionChanged' can only appear on the left hand side of += or -= when used outside of the type 'Newtonsoft.Json.Linq.JContainer'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Collections.Specialized.NotifyCollectionChangedEventHandler ev = (System.Collections.Specialized.NotifyCollectionChangedEventHandler)arg0.func;
				obj.CollectionChanged += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Collections.Specialized.NotifyCollectionChangedEventHandler ev = (System.Collections.Specialized.NotifyCollectionChangedEventHandler)arg0.func;
				obj.CollectionChanged -= ev;
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

