using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace ValorantStatTracker.Commands
{
    public class Statistics : BaseCommandModule
    {
        [Command("stats")]
        [DSharpPlus.CommandsNext.Attributes.Description("Returns some statistics about a player.")]
        public async Task Add(CommandContext ctx,
            [DSharpPlus.CommandsNext.Attributes.Description("Player ID")] [RemainingText] string playerId)
        {
            string playerTag = playerId.Replace('#', '-');
            var response = await Bot._client.GetAsync($"https://hackathon.devsite.in/api/player/{playerTag.ToLower()}");
            if (response.IsSuccessStatusCode)
            {
                var jsonBody = JsonSerializer.Deserialize<Rootobject>(await response.Content.ReadAsStringAsync());

                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{playerId}",
                    Color = DiscordColor.DarkRed,
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
                    {
                        Url = jsonBody.rankImage,
                    },
                };
                List<Agentstat> agentsDescending = jsonBody.lifetimeStats.agentStats.OrderByDescending(o => o.kills).ToList();
                List<Weaponstat1> weaponsDescending = jsonBody.lifetimeStats.weaponStats.OrderByDescending(o => o.damage).ToList();
                List<Mapstat> mapsDescending = jsonBody.lifetimeStats.mapStats.OrderByDescending(o => o.roundsWon).ToList();

                int totalKills = jsonBody.lifetimeStats.kills;
                int totalDeaths = jsonBody.lifetimeStats.deaths;
                int totalWins = 0;
                int totalRounds = 0;
                int totalRoundsWon = 0;
                int totalAssists = jsonBody.lifetimeStats.assists;
                int totalPlants = jsonBody.lifetimeStats.plants;
                int totalDefuses = jsonBody.lifetimeStats.defuses;
                int moneySpent = 0;
                int firstBloods = 0;
                foreach (var agent in jsonBody.lifetimeStats.agentStats)
                {
                    totalWins += agent.wins;
                    totalRounds += agent.roundsPlayed;
                    totalRoundsWon += agent.roundsWon;
                    moneySpent += agent.economy;
                    firstBloods += agent.firstBloodsGiven;
                }

                int totalHeadshots = 0;
                int totalBodyshots = 0;
                int totalLegshots = 0;
                int totalDamage = 0;
                foreach (var weapon in jsonBody.lifetimeStats.weaponStats)
                {
                    totalHeadshots += weapon.headshots;
                    totalBodyshots += weapon.bodyshots;
                    totalLegshots += weapon.legshots;
                    totalDamage += weapon.damage;
                }

                int matchesPlayed = 0;
                int roundsPlayed = 0;
                int attackingWon = 0;
                int defendingWon = 0;
                foreach (var map in jsonBody.lifetimeStats.mapStats)
                {
                    matchesPlayed += map.matches;
                    roundsPlayed += map.roundsPlayed;
                    attackingWon += map.attackingWon;
                    defendingWon += map.defendingWon;
                }

                int roundsLost = totalRounds - totalRoundsWon;
                double roundRatio = (double)totalRoundsWon / roundsLost;
                double killDeath = (double)totalKills / (double)totalDeaths;
                embed.AddField("**General Info**",
                    $"**Current Rank**: {jsonBody.rank}\n**Top Agent**: {Program.CapitalizeFirstLetter(agentsDescending[0].agentName)}\n**Top Weapon**: {Program.CapitalizeFirstLetter(weaponsDescending[0].weaponName)}\n**Top Map**: {Program.CapitalizeFirstLetter(mapsDescending[0].mapName)}", inline: true);
                embed.AddField("Kill / Death",
                    $"**Kills**: {totalKills}\n**Deaths**: {totalDeaths}\n**K/D**: {killDeath:F2}\n**Assists**: {totalAssists}", inline: true);

                var embed2 = new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.DarkRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter()
                    {
                        Text = "Visit our GitHub! https://github.com/cshivers/Hackathon",
                        IconUrl = "https://cdn.discordapp.com/attachments/732928999839367249/733638171547402250/valorant.png"
                    }
                };
                embed2.AddField("**Weapon Stats**", $"**Damage**: {totalDamage}\n**Headshots**: {totalHeadshots}\n**Bodyshots**: {totalBodyshots}\n**Legshots**: {totalLegshots}", inline: true);
                embed2.AddField("**Agent Stats**", $"**Total Plants**: {totalPlants}\n**Total Defuses**: {totalDefuses}\n**$ Spent**: {moneySpent}\n**First Bloods**: {firstBloods}", inline: true);
                embed2.AddField("**Map Stats**", $"**Matches Played**: {matchesPlayed}\n**Rounds Played**: {roundsPlayed}\n**Attacking Won**: {attackingWon}\n**Defending Won**: {defendingWon}", inline: true);
                await ctx.Channel.SendMessageAsync(embed: embed.Build()).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(embed: embed2.Build()).ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Error finding user! Make sure to include the tag (eg. Valorant#123)").ConfigureAwait(false);
            }
        }
    }


    public class Rootobject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string rank { get; set; }
        public string rankImage { get; set; }
        public Lifetimestats lifetimeStats { get; set; }
        public Recentstats recentStats { get; set; }
    }

    public class Lifetimestats
    {
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int plants { get; set; }
        public int defuses { get; set; }
        public Agentstat[] agentStats { get; set; }
        public Mapstat[] mapStats { get; set; }
        public Weaponstat1[] weaponStats { get; set; }
    }

    public class Agentstat
    {
        public string agentId { get; set; }
        public string agentName { get; set; }
        public int kills { get; set; }
        public int score { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int roundsPlayed { get; set; }
        public int playtimeMillis { get; set; }
        public int wins { get; set; }
        public int roundsWon { get; set; }
        public int matches { get; set; }
        public Weaponstat[] weaponStats { get; set; }
        public int economy { get; set; }
        public Abilitycasts abilityCasts { get; set; }
        public int plants { get; set; }
        public int defuses { get; set; }
        public int firstBloodsTaken { get; set; }
        public int firstBloodsGiven { get; set; }
        public int roundsWonWhenFirstBloodTaken { get; set; }
        public int roundsLostWhenFirstBloodGiven { get; set; }
        public int lastKills { get; set; }
    }

    public class Abilitycasts
    {
        public int grenadeCasts { get; set; }
        public int ability1Casts { get; set; }
        public int ability2Casts { get; set; }
        public int ultimateCasts { get; set; }
    }

    public class Weaponstat
    {
        public string weaponId { get; set; }
        public string weaponName { get; set; }
        public float totalKillRange { get; set; }
        public int altFireKills { get; set; }
        public int headshots { get; set; }
        public int bodyshots { get; set; }
        public int legshots { get; set; }
        public int damage { get; set; }
        public int roundsUsed { get; set; }
    }

    public class Mapstat
    {
        public string mapName { get; set; }
        public int wins { get; set; }
        public int matches { get; set; }
        public int roundsWon { get; set; }
        public int roundsPlayed { get; set; }
        public int attackingWon { get; set; }
        public int attackingPlayed { get; set; }
        public int defendingWon { get; set; }
        public int defendingPlayed { get; set; }
    }

    public class Weaponstat1
    {
        public string weaponId { get; set; }
        public string weaponName { get; set; }
        public float totalKillRange { get; set; }
        public int altFireKills { get; set; }
        public int headshots { get; set; }
        public int bodyshots { get; set; }
        public int legshots { get; set; }
        public int damage { get; set; }
        public int roundsUsed { get; set; }
    }

    public class Recentstats
    {
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int plants { get; set; }
        public int defuses { get; set; }
        public Agentstat1[] agentStats { get; set; }
        public Mapstat1[] mapStats { get; set; }
        public Weaponstat3[] weaponStats { get; set; }
    }

    public class Agentstat1
    {
        public string agentId { get; set; }
        public string agentName { get; set; }
        public int kills { get; set; }
        public int score { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int roundsPlayed { get; set; }
        public int playtimeMillis { get; set; }
        public int wins { get; set; }
        public int roundsWon { get; set; }
        public int matches { get; set; }
        public Weaponstat2[] weaponStats { get; set; }
        public int economy { get; set; }
        public Abilitycasts1 abilityCasts { get; set; }
        public int plants { get; set; }
        public int defuses { get; set; }
        public int firstBloodsTaken { get; set; }
        public int firstBloodsGiven { get; set; }
        public int roundsWonWhenFirstBloodTaken { get; set; }
        public int roundsLostWhenFirstBloodGiven { get; set; }
        public int lastKills { get; set; }
    }

    public class Abilitycasts1
    {
        public int grenadeCasts { get; set; }
        public int ability1Casts { get; set; }
        public int ability2Casts { get; set; }
        public int ultimateCasts { get; set; }
    }

    public class Weaponstat2
    {
        public string weaponId { get; set; }
        public string weaponName { get; set; }
        public float totalKillRange { get; set; }
        public int altFireKills { get; set; }
        public int headshots { get; set; }
        public int bodyshots { get; set; }
        public int legshots { get; set; }
        public int damage { get; set; }
        public int roundsUsed { get; set; }
    }

    public class Mapstat1
    {
        public string mapName { get; set; }
        public int wins { get; set; }
        public int matches { get; set; }
        public int roundsWon { get; set; }
        public int roundsPlayed { get; set; }
        public int attackingWon { get; set; }
        public int attackingPlayed { get; set; }
        public int defendingWon { get; set; }
        public int defendingPlayed { get; set; }
    }

    public class Weaponstat3
    {
        public string weaponId { get; set; }
        public string weaponName { get; set; }
        public float totalKillRange { get; set; }
        public int altFireKills { get; set; }
        public int headshots { get; set; }
        public int bodyshots { get; set; }
        public int legshots { get; set; }
        public int damage { get; set; }
        public int roundsUsed { get; set; }
    }


}
