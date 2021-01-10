using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuaScriptForRedis.Redis
{
    class RedisProvider : IRedisProvider
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexerLazy;
        private static string host = "xx:6379";
        private static string password = "xx";

        public RedisProvider()
        {
            _connectionMultiplexerLazy = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        public IDatabase GetDatabase(int database = 0)
        {
            return _connectionMultiplexerLazy.Value.GetDatabase(database);
        }

        public IServer GetServer()
        {
            return _connectionMultiplexerLazy.Value.GetServer(host);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect($"{host},{password}");
        }
    }
}