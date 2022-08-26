using Belix.DiscordBot.App.Entities;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Options;
using System.Diagnostics;

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
            Stopwatch stopwatch = Stopwatch.StartNew();
            /**
            var guild = _client.GetGuild(_options.Id);

            var channel = await guild.CreateVoiceChannelAsync(name, x =>
            {
                x.CategoryId = _options.TemporaryCategoryId;
            });

            var invite = await channel.CreateInviteAsync(maxUses: 1);

            await Context.User.SendMessageAsync(invite.Url);
            **/

            await RespondAsync($"User {Context.User.Username} has created new temparary channel: \"{name}\"");
        }
    }
}
