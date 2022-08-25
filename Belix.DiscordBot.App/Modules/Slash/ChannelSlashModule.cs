using Belix.DiscordBot.App.Entities;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App.Modules.Slash
{
    [Group(name: "channel", description: "Channel Commands")]
    public class ChannelSlashModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly GuildOptions _options;
        private readonly DiscordSocketClient _client;

        public ChannelSlashModule(IOptions<GuildOptions> options, DiscordSocketClient client)
        {
            _options = options.Value;
            _client = client;
        }

        [SlashCommand(name: "create", description: "Create a new voice channel")]
        public async Task Create(string name)
        {
            var guild = _client.GetGuild(_options.Id);

            var channel = await guild.CreateVoiceChannelAsync(name, x =>
            {
                x.CategoryId = _options.TemporaryCategoryId;
            });
        }
    }
}
