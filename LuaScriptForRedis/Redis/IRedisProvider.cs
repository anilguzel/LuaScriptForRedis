using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuaScriptForRedis.Redis
{
    public interface IRedisProvider
    {
        IDatabase GetDatabase(int database = 0);

        IServer GetServer();
    }
}
