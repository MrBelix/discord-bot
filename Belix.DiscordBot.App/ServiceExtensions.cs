using Belix.DiscordBot.App.Entities;
using Belix.DiscordBot.Core;
using Belix.DiscordBot.Core.Interfaces;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App
{
    static class ServiceExtensions
    {
        public static Builder<T> AddInjections<T>(this Builder<T> builder) where T : IStartup
        {
            builder.Services
                .Configure<GuildOptions>(
                    builder.Configuration.GetSection(GuildOptions.Position)
                )
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<InteractionService>();

            return builder;
        }
    }
}
