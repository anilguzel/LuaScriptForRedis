using LuaScriptForRedis.Helpers;
using LuaScriptForRedis.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace LuaScriptForRedis.Helpers
{
    public class LuaScriptHelper : ILuaScriptHelper
    {
        private static ConcurrentDictionary<Enums.LuaScripts, Lazy<LoadedLuaScript>> _loadedLuaScripts = new ConcurrentDictionary<Enums.LuaScripts, Lazy<LoadedLuaScript>>();
        private readonly IRedisProvider _redisProvider;

        public LuaScriptHelper(IRedisProvider redisProvider)
        {
            _redisProvider = redisProvider;
        }

        public LoadedLuaScript GetLoadedLuaScript(Enums.LuaScripts type)
        {
            var script = _loadedLuaScripts.GetOrAdd(type,
                k => new Lazy<LoadedLuaScript>(() => GetLoadedLuaScriptFactory(type),
                    LazyThreadSafetyMode.ExecutionAndPublication));

            return script.Value;
        }
        private LoadedLuaScript GetLoadedLuaScriptFactory(Enums.LuaScripts type)
        {
            var script = File.ReadAllText($"LuaScripts/{type}.lua");

            var luaScript = LuaScript.Prepare(script);
            return luaScript.Load(_redisProvider.GetServer());
        }
    }
}
