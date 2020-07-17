using System;
using System.Collections.Generic;
using System.Linq;
using Hackathon.Core.Blitz.Models;
using Hackathon.Core.Services;
using Hackathon.Data.Models;

namespace Hackathon.Core.Blitz
{
    public class BlitzResponseMapper
        : IBlitzResponseMapper
    {
        private readonly IMetaService _metaService;

        public BlitzResponseMapper(IMetaService metaService)
        {
            _metaService = metaService;
        }

        public Player MapToPlayer(BlitzResponse blitzResponse)
        {
            Player player =  new Player
            {
                Id            = Guid.Parse(blitzResponse.Id),
                Name          = $"{blitzResponse.Name} #{blitzResponse.Tag}",
                Rank          = _metaService.GetRankTier(blitzResponse.Ranks.Competitive.Tier),
                RankImage     = _metaService.GetRankImage(blitzResponse.Ranks.Competitive.Tier),
                LifetimeStats = MapToPlayerStats(blitzResponse.Stats.Competitive.Career),
                RecentStats   = MapToPlayerStats(blitzResponse.Stats.Competitive.Last20),
            };
            return player;
        }

        private Data.Models.Stats MapToPlayerStats(CompetitiveStatsDetail competitiveStatsDetail)
        {
            Dictionary<string, DamageStats> weaponStats = competitiveStatsDetail.WeaponDamageStats;
            Dictionary<string, Models.MapStats> mapStats = competitiveStatsDetail.MapStats;
            Dictionary<string, Models.AgentStats> agentStats = competitiveStatsDetail.AgentsStats;

            Data.Models.Stats playerStats = new Data.Models.Stats()
            {
                Kills                         = competitiveStatsDetail.Kills,
                Deaths                        = competitiveStatsDetail.Deaths,
                Assists                       = competitiveStatsDetail.Assists,
                Plants                        = competitiveStatsDetail.Plants,
                Defuses                       = competitiveStatsDetail.Defuses,
                FirstBloodsGiven              = competitiveStatsDetail.FirstBloodsGiven,
                FirstBloodsTaken              = competitiveStatsDetail.FirstBloodsTaken,
                RoundsLostWhenFirstBloodGiven = competitiveStatsDetail.RoundsLostWhenFirstBloodGiven,
                RoundsWonWhenFirstBloodTaken  = competitiveStatsDetail.RoundsWonWhenFirstBloodTaken,
                LastKills                     = competitiveStatsDetail.LastKills,
            };
            
            playerStats.WeaponStats = weaponStats.Select(x => new Data.Models.WeaponStats()
            {
                WeaponId       = Guid.Parse(x.Key),
                WeaponName     = _metaService.GetWeaponName(x.Key),
                TotalKillRange = weaponStats[x.Key].TotalKillRange,
                AltFireKills   = weaponStats[x.Key].AltFireKills,
                Kills          = weaponStats[x.Key].Kills,
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
                    AgentName      = _metaService.GetAgentName(x.Key),
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
                        WeaponName     = _metaService.GetWeaponName(x.Key),
                        TotalKillRange = x.Value.TotalKillRange,
                        AltFireKills   = x.Value.AltFireKills,
                        Kills          = x.Value.Kills,
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
                    }
                }).ToList();

            return playerStats;
        }
    }
}
