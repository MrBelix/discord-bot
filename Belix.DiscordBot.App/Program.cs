using Belix.DiscordBot.App;
using Belix.DiscordBot.App.Extensions;
using Belix.DiscordBot.Core;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = new Builder<Startup>(args);

builder
    .AddConfig(builder => builder.AddJsonFile("appsettings.json"))
    .AddInjections()
    .AddExtensions(Assembly.GetEntryAssembly())
    .Build()
    .Run();