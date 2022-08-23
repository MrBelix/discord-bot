﻿using Belix.DiscordBot.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.Core
{
    public sealed class Application
    {
        private readonly IServiceProvider _services;

        public Application(IServiceProvider services)
        {
            _services = services;
        }

        public void Run() => RunAsync().GetAwaiter().GetResult();

        private async Task RunAsync()
        {
            var startup = _services.GetRequiredService<IStartup>();
            await startup.RunAsync();

            await Task.Delay(Timeout.Infinite);
        }
    }
}