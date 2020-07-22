using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Hackathon.DiscordBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hackathon.DiscordBot
{
    public class Bot
        : IBot
    {
        private readonly IServiceProvider _serviceProvider;
        private DiscordClient _client;
        private CommandsNextModule _commands;

        public Bot(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync()
        {
            _client = new DiscordClient(GetDiscordConfiguration());

            // Logs Client events
            _client.Ready += OnClientReady;

            // Sets prefix and other bot settings
            _commands = _client.UseCommandsNext(GetCommandsNextConfiguration());

            foreach (KeyValuePair<ulong, DiscordGuild> guild in _client.Guilds)
            {
                Console.WriteLine(guild.Value);
            }

            // Registers command classes
            _commands.RegisterCommands<PlayerCommands>();

            // Connect
            await _client.ConnectAsync();

            _client.GuildAvailable += OnGuildAvailable;

            // Prevents connection from terminating
            await Task.Delay(-1);
        }

        private DiscordConfiguration GetDiscordConfiguration()
        {
            return new DiscordConfiguration
            {
                Token = "BOT_TOKEN",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };
        }

        private CommandsNextConfiguration GetCommandsNextConfiguration()
        {
            using var builder = new DependencyCollectionBuilder();
            builder.AddInstance(_serviceProvider.GetService<IHttpClientFactory>());

            return new CommandsNextConfiguration
            {
                StringPrefix = "v!",
                EnableMentionPrefix = true,
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = true,
                IgnoreExtraArguments = false,
                Dependencies = builder.Build()
            };
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            // Logs client startup
            return Task.CompletedTask;
        }

        private Task OnGuildAvailable(GuildCreateEventArgs e)
        {
            var count = _client.Guilds.Count;
            return _client.UpdateStatusAsync(new DiscordGame($"with {count} server{(count > 1 ? "s" : "")} | v!"), null);
        }
    }
}
