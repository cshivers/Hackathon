using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using ValorantStatTracker.Commands;

namespace ValorantStatTracker
{
    public class Bot
    {
        public static DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public static HttpClient _client = new HttpClient();

        public async Task RunAsync()
        {
            

            // Bot connection information
            var config = new DiscordConfiguration
            {
                Token = "BOT_TOKEN",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            // Logs Client events
            Client.Ready += OnClientReady;

            // Sets prefix and other bot settings
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "v!" },
                EnableMentionPrefix = true,
                EnableDms = true,
                CaseSensitive = false,
                EnableDefaultHelp = true,
                IgnoreExtraArguments = false,
                DmHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            foreach (var guild in Client.Guilds)
            {
                Console.WriteLine(guild.Value);
            }
            // Registers command classes
            Commands.RegisterCommands<Statistics>();
            await Client.ConnectAsync();
            Client.GuildDownloadCompleted += OnGuildDownloadComplete;
            // Prevents connection from terminating
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            // Logs client startup
            return Task.CompletedTask;
        }

        private Task OnGuildDownloadComplete(GuildDownloadCompletedEventArgs e)
        {
            Client.UpdateStatusAsync(new DiscordActivity($"{Client.Guilds.Count} servers | v!", ActivityType.Watching));
            return Task.CompletedTask;
        }
    }
}
