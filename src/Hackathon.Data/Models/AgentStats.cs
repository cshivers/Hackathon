using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Data.Models
{
    public class AgentStats
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public int Kills { get; set; }
        public int Score { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int RoundsPlayed { get; set; }
        public int PlaytimeMillis { get; set; }
        public int Wins { get; set; }
        public int RoundsWon { get; set; }
        public int Matches { get; set; }
        public List<WeaponStats> WeaponStats { get; set; }
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
}
