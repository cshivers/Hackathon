using System;
using System.Linq;
using Hackathon.Core.Services;
using Hackathon.Data.Models;

namespace Hackathon.Core.Blitz
{
    public class BlitzResponseMapper
    {
        private readonly MetaService metaService;

        public BlitzResponseMapper(MetaService metaService)
        {
            this.metaService = metaService;
        }

        public Player MapToPlayer(BlitzResponse blitzResponse)
        {
            var player =  new Player
            {
                Id            = Guid.Parse(blitzResponse.Id),
                Name          = $"{blitzResponse.Name} #{blitzResponse.Tag}",
                Rank          = metaService.GetRankTier(blitzResponse.Ranks.Competitive.Tier),
                LifetimeStats = this.MapToPlayerStats(blitzResponse.Stats.Competitive.Career),
                RecentStats   = this.MapToPlayerStats(blitzResponse.Stats.Competitive.Last20),
            };
            return player;
        }

        private Data.Models.Stats MapToPlayerStats(CompetitiveStatsDetail competitiveStatsDetail)
        {
            var playerStats = new Data.Models.Stats();
            var weaponStats = competitiveStatsDetail.WeaponDamageStats;
            var mapStats    = competitiveStatsDetail.MapStats;
            var agentStats  = competitiveStatsDetail.AgentsStats;

            playerStats.WeaponStats = weaponStats.Select(x => new Data.Models.WeaponStats()
            {
                WeaponId       = Guid.Parse(x.Key),
                WeaponName     = metaService.GetWeaponName(x.Key),
                TotalKillRange = weaponStats[x.Key].TotalKillRange,
                AltFireKills   = weaponStats[x.Key].AltFireKills,
                Headshots      = weaponStats[x.Key].Headshots,
                Bodyshots      = weaponStats[x.Key].Bodyshots,
                Legshots       = weaponStats[x.Key].Legshots,
                Damage         = weaponStats[x.Key].Damage,
                RoundsUsed     = weaponStats[x.Key].RoundsUsed,
            }).ToList();

            playerStats.MapStats = mapStats.Select(x => new Data.Models.MapStats()
            {
                MapName         = x.Key,
                Wins            = mapStats[x.Key].Wins,
                Matches         = mapStats[x.Key].Matches,
                RoundsWon       = mapStats[x.Key].RoundsWon,
                RoundsPlayed    = mapStats[x.Key].RoundsPlayed,
                AttackingWon    = mapStats[x.Key].AttackingWon,
                AttackingPlayed = mapStats[x.Key].AttackingPlayed,
                DefendingWon    = mapStats[x.Key].DefendingWon,
                DefendingPlayed = mapStats[x.Key].DefendingPlayed
            }).ToList();

            playerStats.AgentStats = agentStats.Select(x => new Data.Models.AgentStats()
                {
                    AgentId        = Guid.Parse(x.Key),
                    AgentName      = metaService.GetAgentName(x.Key),
                    Kills          = agentStats[x.Key].Kills,
                    Score          = agentStats[x.Key].Score,
                    Deaths         = agentStats[x.Key].Deaths,
                    Assists        = agentStats[x.Key].Assists,
                    RoundsPlayed   = agentStats[x.Key].RoundsPlayed,
                    PlaytimeMillis = agentStats[x.Key].PlaytimeMillis,
                    Wins           = agentStats[x.Key].Wins,
                    RoundsWon      = agentStats[x.Key].RoundsWon,
                    Matches        = agentStats[x.Key].Matches,
                    Economy        = agentStats[x.Key].Economy,

                    WeaponStats = agentStats[x.Key].WeaponDamageStats.Select(x => new Data.Models.WeaponStats()
                    {
                        WeaponId       = Guid.Parse(x.Key),
                        WeaponName     = metaService.GetWeaponName(x.Key),
                        TotalKillRange = x.Value.TotalKillRange,
                        AltFireKills   = x.Value.AltFireKills,
                        Headshots      = x.Value.Headshots,
                        Bodyshots      = x.Value.Bodyshots,
                        Damage         = x.Value.Damage,
                        Legshots       = x.Value.Legshots,
                        RoundsUsed     = x.Value.RoundsUsed,
                    }).ToList(),

                    AbilityCasts = new Data.Models.AbilityCasts()
                    {
                        GrenadeCasts  = agentStats[x.Key].AbilityCasts.GrenadeCasts,
                        Ability1Casts = agentStats[x.Key].AbilityCasts.Ability1Casts,
                        Ability2Casts = agentStats[x.Key].AbilityCasts.Ability2Casts,
                        UltimateCasts = agentStats[x.Key].AbilityCasts.UltimateCasts,
                    },

                    Plants                        = agentStats[x.Key].Plants,
                    Defuses                       = agentStats[x.Key].Defuses,
                    FirstBloodsTaken              = agentStats[x.Key].FirstBloodsTaken,
                    FirstBloodsGiven              = agentStats[x.Key].FirstBloodsGiven,
                    RoundsWonWhenFirstBloodTaken  = agentStats[x.Key].RoundsWonWhenFirstBloodTaken,
                    RoundsLostWhenFirstBloodGiven = agentStats[x.Key].RoundsLostWhenFirstBloodGiven,
                    LastKills                     = agentStats[x.Key].LastKills,
                }).ToList();

            return playerStats;
        }

    }
}
