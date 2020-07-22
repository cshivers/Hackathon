using System.Collections.Generic;

namespace Hackathon.Core.Blitz.Models
{
    public class BlitzResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public Ranks Ranks { get; set; }
        public Stats Stats { get; set; }
    }

    public class Ranks
    {
        public RankDetail Competitive { get; set; }
    }

    public class RankDetail
    {
        public int Tier { get; set; }
    }

    public class Stats
    {
        public StatsDetail Competitive { get; set; }
    }

    public class StatsDetail
    {
        public CompetitiveStatsDetail Last20 { get; set; }
        public CompetitiveStatsDetail Career { get; set; }
    }

    public class CompetitiveStatsDetail
    {
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int Plants { get; set; }
        public int Defuses { get; set; }
        public int FirstBloodsTaken { get; set; }
        public int FirstBloodsGiven { get; set; }
        public int RoundsWonWhenFirstBloodTaken { get; set; }
        public int RoundsLostWhenFirstBloodGiven { get; set; }
        public int LastKills { get; set; }
        public Dictionary<string, DamageStats> WeaponDamageStats { get; set; }
        public Dictionary<string, AgentStats> AgentsStats { get; set; }
        public Dictionary<string, MapStats> MapStats { get; set; }
    }

    public class DamageStats
    {
        public int Kills { get; set; }

        public double? TotalKillRange { get; set; }

        public int AltFireKills { get; set; }

        public int Headshots { get; set; }

        public int Bodyshots { get; set; }

        public int Legshots { get; set; }

        public int Damage { get; set; }

        public int RoundsUsed { get; set; }
    }

    public class AgentStats
    {
        public int Kills { get; set; }
        public int Score { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int RoundsPlayed { get; set; }
        public int PlaytimeMillis { get; set; }
        public int Wins { get; set; }
        public int RoundsWon { get; set; }
        public int Matches { get; set; }
        public Dictionary<string, DamageStats> WeaponDamageStats { get; set; }
        public int Economy { get; set; }
        public AbilityCasts AbilityCasts { get; set; }
    }

    public class AbilityCasts
    {
        public int GrenadeCasts { get; set; }
        public int Ability1Casts { get; set; }
        public int Ability2Casts { get; set; }
        public int UltimateCasts { get; set; }
    }

    public class MapStats
    {
        public int Wins { get; set; }
        public int Matches { get; set; }
        public int RoundsWon { get; set; }
        public int RoundsPlayed { get; set; }
        public int AttackingWon { get; set; }
        public int AttackingPlayed { get; set; }
        public int DefendingWon { get; set; }
        public int DefendingPlayed { get; set; }
    }
}
