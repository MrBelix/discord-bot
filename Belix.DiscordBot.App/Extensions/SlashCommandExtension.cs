using Belix.DiscordBot.Core;
using Belix.DiscordBot.Core.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
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

        public SlashCommandExtension(DiscordSocketClient client, IServiceProvider services, InteractionService interactionService)
        {
            _client = client;
            _services = services;
            _interactionService = interactionService;

            _interactionService.Log += message =>
            {
                Console.WriteLine(message.Message);
                return Task.CompletedTask;
            };
        }

        public void Register()
        {
            _client.Ready += OnReady;
        }

        private async Task OnReady()
        {
            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await _interactionService.RegisterCommandsToGuildAsync(1009272583327322122);

            _client.InteractionCreated += OnInteractionCreated;
        }

        private async Task OnInteractionCreated(SocketInteraction interaction)
        {
            var ctx = new SocketInteractionContext(_client, interaction);

            await _interactionService.ExecuteCommandAsync(ctx, _services);
        }
    }
}
