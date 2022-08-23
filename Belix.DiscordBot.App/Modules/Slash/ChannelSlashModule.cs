using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App.Modules.Slash
{
    [Group(name: "channel", description: "Channel Commands")]
    public class ChannelSlashModule : InteractionModuleBase
    {
        public readonly DiscordSocketClient _client;

        public ChannelSlashModule(DiscordSocketClient client)
        {
            _client = client;
        }

        [SlashCommand(name: "create", description: "Create a new voice channel")]
        public async Task Create(string name)
        {
            var guild = _client.GetGuild(1009272583327322122);

            await guild.CreateVoiceChannelAsync(name, x =>
            {
                x.CategoryId = 1009504013693231104;
            });

            await RespondAsync($"Channel {name} was created");
        }
    }
}
