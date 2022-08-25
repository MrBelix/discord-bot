using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.App.Entities
{
    public class GuildOptions
    {
        public const string Position = "Guild";

        public ulong Id { get; set; }

        public ulong TemporaryCategoryId { get; set; }
    }
}
