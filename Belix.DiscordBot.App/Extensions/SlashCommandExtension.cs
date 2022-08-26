using Belix.DiscordBot.App.Entities;
using Belix.DiscordBot.Core;
using Belix.DiscordBot.Core.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App.Extensions
{

    [BotExtension]
    public class SlashCommandExtension : IExtension
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly InteractionService _interactionService;
        private readonly GuildOptions _guildOptions;

        public SlashCommandExtension(DiscordSocketClient client, IServiceProvider services, InteractionService interactionService, IOptions<GuildOptions> guildOptions)
        {
            _client = client;
            _services = services;
            _interactionService = interactionService;
            _guildOptions = guildOptions.Value;
        }

        public void Register()
        {
            _client.Ready += OnReady;
        }

        private async Task OnReady()
        {
                await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
                await _interactionService.RegisterCommandsToGuildAsync(_guildOptions.Id);

            _client.InteractionCreated += OnInteractionCreated;
        }

        private async Task OnInteractionCreated(SocketInteraction interaction)
        {
            var ctx = new SocketInteractionContext(_client, interaction);

            await _interactionService.ExecuteCommandAsync(ctx, _services);
        }
    }
}
