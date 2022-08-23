using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.Core
{
    public sealed class Builder
    {
        public readonly IServiceCollection Services;

        public readonly ConfigurationBuilder Configuration;

        public Builder(string[] args)
        {
            Services = new ServiceCollection();
            Configuration = new ConfigurationBuilder();
        }

        public Application Build()
        { 
            return new Application(BuildServices());
        }

        private IServiceProvider BuildServices()
        {
            return Services.BuildServiceProvider();
        }

        private void ConfigureServices()
        {
            Services
                .AddSingleton<IConfiguration>(BuildConfiguration());
        }

        private IConfiguration BuildConfiguration()
        {
            return Configuration.Build();
        }
    }
}
