using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuaScriptForRedis.Services
{
    public interface ILeaderboardService
    {
        Task<string> GetLeaderboardAsync(string userId);
    }
}
