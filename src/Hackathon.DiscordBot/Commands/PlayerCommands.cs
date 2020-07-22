using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Hackathon.Data.Models;
using Hackathon.DiscordBot.Helpers;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hackathon.DiscordBot.Commands
{
    public class PlayerCommands
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PlayerCommands(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Command("stats")]
        [Description("Returns some statistics about a player.")]
        public async Task Add(CommandContext ctx, [Description("Player ID")][RemainingText] string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                await ctx.Channel.SendMessageAsync("Error finding user! Make sure to include the tag (eg. Valorant#123)").ConfigureAwait(false);
                return;
            }

            string playerTag = playerId.Replace('#', '-');
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"https://hackathon.devsite.in/api/player/{playerTag.ToLower()}");

            if (!response.IsSuccessStatusCode)
            {
                await ctx.Channel.SendMessageAsync("Error finding user! Make sure to include the tag (eg. Valorant#123)").ConfigureAwait(false);
                return;
            }

            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            Player player = JsonSerializer.Deserialize<Player>(await response.Content.ReadAsStringAsync(), options);

            AgentStats bestAgent   = player.LifetimeStats.AgentStats.OrderByDescending(agent => agent.Kills).First();
            WeaponStats bestWeapon = player.LifetimeStats.WeaponStats.OrderByDescending(weapon => weapon.Kills).First();
            MapStats bestMap       = player.LifetimeStats.MapStats.OrderByDescending(map => map.RoundsWon).First();

            AggregatedWeaponStats aggregatedWeaponStats = player.LifetimeStats.WeaponStats
                .Aggregate(new AggregatedWeaponStats(), (aggregated, current) =>
                {
                    aggregated.TotalDamage += current.Damage;
                    aggregated.TotalHeadshots += current.Headshots;
                    aggregated.TotalBodyshots += current.Bodyshots;
                    aggregated.TotalLegshots += current.Legshots;

                    return aggregated;
                });

            AggregatedMapStats aggregatedMapStats = player.LifetimeStats.MapStats
                 .Aggregate(new AggregatedMapStats(), (aggregated, current) =>
                 {
                     aggregated.TotalMatches += current.Matches;
                     aggregated.TotalRoundsPlayed += current.RoundsPlayed;
                     aggregated.TotalRoundsWonOnAttack += current.AttackingWon;
                     aggregated.TotalRoundsWonOnDefence += current.DefendingWon;

                     return aggregated;
                 });


            int totalKills        = player.LifetimeStats.Kills;
            int totalDeaths       = player.LifetimeStats.Deaths;
            int roundsPlayed      = player.LifetimeStats.RoundsPlayed;
            int roundsWon         = player.LifetimeStats.RoundsWon;
            int roundsLost        = roundsPlayed - roundsWon;
            double roundRatio     = (double)roundsWon / roundsLost;
            double killDeathRatio = (double)totalKills / (double)totalDeaths;

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Title = player.Name,
                Color = DiscordColor.DarkRed, 
                ThumbnailUrl = player.RankImage
            };

            embed.AddField("**General Info**", @$"**Current Rank**: {player.Rank}
**Top Agent**: {bestAgent.AgentName.CapitalizeFirstLetter()}
**Top Weapon**: {bestWeapon.WeaponName.CapitalizeFirstLetter()}
**Top Map**: {bestMap.MapName.CapitalizeFirstLetter()}", inline: true);
            
            embed.AddField("Kill / Death", @$"**Kills**: {totalKills}
**Deaths**: {totalDeaths}
**K/D**: {killDeathRatio:F2}
**Assists**: {player.LifetimeStats.Assists}", inline: true);

            var embed2 = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.DarkRed,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = "Visit our GitHub! https://github.com/cshivers/Hackathon",
                    IconUrl = "https://cdn.discordapp.com/attachments/732928999839367249/733638171547402250/valorant.png"
                }
            };

            string agentPlayedNumbers = string.Join("\n", player.LifetimeStats.AgentStats.Select(agent => $"**{agent.AgentName}**: {agent.Kills}/{agent.Deaths}/{agent.Assists}"));

            embed2.AddField("**Agent K/D/A**", agentPlayedNumbers, inline: true);

            embed2.AddField("**Weapon Stats**", @$"**Damage**: {aggregatedWeaponStats.TotalDamage}
**Headshots**: {aggregatedWeaponStats.TotalHeadshots}
**Bodyshots**: {aggregatedWeaponStats.TotalBodyshots}
**Legshots**: {aggregatedWeaponStats.TotalLegshots}", inline: true);
            
            embed2.AddField("**Map Stats**", @$"**Matches Played**: {aggregatedMapStats.TotalMatches}
**Rounds Played**: {aggregatedMapStats.TotalRoundsPlayed}
**Attacking Won**: {aggregatedMapStats.TotalRoundsWonOnAttack}
**Defending Won**: {aggregatedMapStats.TotalRoundsWonOnDefence}", inline: true);
            
            await ctx.Channel.SendMessageAsync(embed: embed.Build()).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(embed: embed2.Build()).ConfigureAwait(false);
        }

        private class AggregatedWeaponStats
        {
            public int TotalDamage { get; set; }
            public int TotalHeadshots { get; set; }
            public int TotalBodyshots { get; set; }
            public int TotalLegshots { get; set; }
        }

        private class AggregatedMapStats
        {
            public int TotalMatches { get; set; }
            public int TotalRoundsPlayed { get; set; }
            public int TotalRoundsWonOnAttack { get; set; }
            public int TotalRoundsWonOnDefence { get; set; }
        }
    }
}
