using Belix.DiscordBot.App;
using Belix.DiscordBot.Core;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new Builder<Startup>(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json");

builder.Services
    .AddSingleton<DiscordSocketClient>();

builder
    .Build()
    .Run();