using LuaScriptForRedis.Helpers;
using LuaScriptForRedis.Redis;
using LuaScriptForRedis.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuaScriptForRedis.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IRedisProvider _redisProvider;
        private readonly ILuaScriptHelper _luaScriptHelper;

        public LeaderboardService(ILuaScriptHelper luaScriptHelper, IRedisProvider redisProvider)
        {
            _redisProvider = redisProvider;
            _luaScriptHelper = luaScriptHelper;
        }


        public async Task<string> GetLeaderboardAsync(string userId)
        {
            var db = _redisProvider.GetDatabase();

            var script = _luaScriptHelper.GetLoadedLuaScript(Enums.LuaScripts.Leaderboard);
            var result = await db.ScriptEvaluateAsync(script, new { userId });

            return result.ToString();
        }
    }
}
