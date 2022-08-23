using Belix.DiscordBot.Core.Interfaces;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App
{
    internal sealed class Startup : IStartup
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;

        public Startup(DiscordSocketClient client, IConfiguration config)
        {
            _client = client;
            _config = config;

            _client.Log += message =>
            {
                Console.WriteLine(message.Message);
                return Task.CompletedTask;
            };
        }

        public async Task RunAsync()
        {
            string token = _config["token"];

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("token fiend not found in config");
            }

            await _client.LoginAsync(Discord.TokenType.Bot, token);

            await _client.StartAsync();
        }
    }
}
