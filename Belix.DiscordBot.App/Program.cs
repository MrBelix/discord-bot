using Belix.DiscordBot.App;
using Belix.DiscordBot.App.Extensions;
using Belix.DiscordBot.Core;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = new Builder<Startup>(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json");

builder.AddExtensions(Assembly.GetEntryAssembly());

builder.Services
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton<InteractionService>();

builder
    .Build()
    .Run();