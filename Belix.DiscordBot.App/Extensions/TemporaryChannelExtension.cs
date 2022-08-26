using System;
using System.Timers;
using Belix.DiscordBot.App.Entities;
using Belix.DiscordBot.Core;
using Belix.DiscordBot.Core.Interfaces;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace Belix.DiscordBot.App.Extensions
{


    [BotExtension]
    public class TemporaryChannelExtension : IExtension
    {
        private readonly DiscordSocketClient _client;
        private readonly GuildOptions _options;
        private readonly System.Timers.Timer _timer;

        public TemporaryChannelExtension(DiscordSocketClient client, IOptions<GuildOptions> options)
        {
            _client = client;
            _options = options.Value;

            _timer = new System.Timers.Timer(60 * 1000);
            _timer.Elapsed += OnElapsed;

        }

        private async void OnElapsed(object? sender, ElapsedEventArgs e)
        {
            var guild = _client.GetGuild(_options.Id);
            var categoryChannel = guild.GetCategoryChannel(_options.TemporaryCategoryId);
            var minTime = DateTimeOffset.Now.AddMinutes(-10).ToUnixTimeSeconds();

            foreach (var channel in categoryChannel.Channels)
            {
                SocketVoiceChannel? voice = channel as SocketVoiceChannel;

                if (voice != null && voice.ConnectedUsers.Count < 1 && voice.CreatedAt.ToUnixTimeSeconds() < minTime)
                {
                    await voice.DeleteAsync();
                }
            }

        }

        public void Register()
        {
            _client.Ready += () =>
            {
                _timer.Start();

                return Task.CompletedTask;
            };
        }

    }
}
