using Belix.DiscordBot.Core.Interfaces;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.Core
{
    public sealed class Builder<T> where T : IStartup
    {
        public readonly IServiceCollection Services;

        public readonly ConfigurationBuilder Configuration;

        private IEnumerable<Type>? _extensions = null;


        public Builder(string[] args)
        {
            Services = new ServiceCollection();
            Configuration = new ConfigurationBuilder();
        }

        public Application<T> Build()
        { 
            return new Application<T>(BuildServices(), _extensions);
        }

        public void AddExtensions(Assembly? assembly)
        {
            _extensions = assembly?.GetTypes().Where(t => t.IsDefined(typeof(BotExtensionAttribute))).ToList();
        }

        private IServiceProvider BuildServices()
        {
            ConfigureServices();

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
