using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App.Commands.Slash
{
    public class TestSlashCommand: InteractionModuleBase
    {
        [SlashCommand(name: "test", description: "test")]
        public async Task Test(string input)
        {
            await RespondAsync(input);
        }
    }
}
