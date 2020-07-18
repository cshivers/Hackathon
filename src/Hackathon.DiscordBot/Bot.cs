using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Hackathon.DiscordBot.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.DiscordBot
{
    public class Bot
        : IBot
    {
        private readonly IServiceProvider _serviceProvider;
        private DiscordClient _client;
        private CommandsNextExtension _commands;

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

            _client.GuildDownloadCompleted += OnGuildDownloadComplete;

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
            return new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "v!" },
                EnableMentionPrefix = true,
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = true,
                IgnoreExtraArguments = false,
                DmHelp = false,
                Services = _serviceProvider
            };
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            // Logs client startup
            return Task.CompletedTask;
        }

        private Task OnGuildDownloadComplete(GuildDownloadCompletedEventArgs e)
        {
            _client.UpdateStatusAsync(new DiscordActivity($"{_client.Guilds.Count} servers | v!", ActivityType.Watching));
            return Task.CompletedTask;
        }
    }
}
