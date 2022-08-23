using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App.Modules.Slash
{
    public class TestSlashModule: InteractionModuleBase
    {
        [SlashCommand(name: "test", description: "test")]
        public async Task Test(string input)
        {
            await RespondAsync(input);
        }
    }
}
