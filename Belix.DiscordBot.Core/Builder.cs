using Belix.DiscordBot.Core.Interfaces;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.Core
{
    public sealed class Builder<TStartup> where TStartup : IStartup
    {

        public readonly IServiceCollection Services;

        public IConfiguration Configuration => _configurationBuilder.Build();

        private ConfigurationBuilder _configurationBuilder;

        private IEnumerable<Type>? _extensions = null;

        public Builder(string[] args)
        {
            Services = new ServiceCollection();
            _configurationBuilder = new ConfigurationBuilder();
            _configurationBuilder.SetBasePath(AppContext.BaseDirectory);
        }

        public Application<TStartup> Build()
        {
            return new Application<TStartup>(BuildServices(), _extensions);
        }

        public Builder<TStartup> AddConfig(Action<IConfigurationBuilder> action)
        {
            action(_configurationBuilder);

            return this;
        }


        public Builder<TStartup> AddExtensions(Assembly? assembly)
        {
            _extensions = assembly?.GetTypes().Where(t => t.IsDefined(typeof(BotExtensionAttribute))).ToList();

            return this;
        }

        private IServiceProvider BuildServices()
        {
            ConfigureServices();

            return Services.BuildServiceProvider();
        }

        private void ConfigureServices()
        {
            Services
                .AddSingleton<IConfiguration>(Configuration);
        }
    }
}
