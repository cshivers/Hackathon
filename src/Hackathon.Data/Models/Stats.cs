using System.Collections.Generic;

namespace Hackathon.Data.Models
{
    public class Stats
    {
        public List<AgentStats> AgentStats { get; set; }
        public List<MapStats> MapStats { get; set; }
        public List<WeaponStats> WeaponStats { get; set; }
    }
}
