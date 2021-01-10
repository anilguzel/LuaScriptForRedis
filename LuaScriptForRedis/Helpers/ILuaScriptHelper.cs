using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuaScriptForRedis.Helpers
{
    public interface ILuaScriptHelper
    {
        LoadedLuaScript GetLoadedLuaScript(Enums.LuaScripts type);
    }
}
