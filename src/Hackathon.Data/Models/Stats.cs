using System.Collections.Generic;

namespace Hackathon.Data.Models
{
    public class Stats
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
        public List<AgentStats> AgentStats { get; set; }
        public List<MapStats> MapStats { get; set; }
        public List<WeaponStats> WeaponStats { get; set; }
    }
}
