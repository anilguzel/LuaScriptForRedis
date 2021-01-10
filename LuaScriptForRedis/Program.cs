using LuaScriptForRedis.Helpers;
using LuaScriptForRedis.Redis;
using LuaScriptForRedis.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace LuaScriptForRedis
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddScoped<ILeaderboardService, LeaderboardService>()
           .AddSingleton<IRedisProvider, RedisProvider>()
           .AddSingleton<ILuaScriptHelper, LuaScriptHelper>()
           .BuildServiceProvider();
            
        retry:
            var leaderboardService = serviceProvider.GetService<ILeaderboardService>();

            Console.WriteLine("UserId giriniz");
            var userId = Console.ReadLine();

            var response = await leaderboardService.GetLeaderboardAsync(userId);
            Console.WriteLine(response);

            goto retry;
        }
    }
}
