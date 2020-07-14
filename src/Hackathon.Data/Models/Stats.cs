using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Data.Models
{
    public class Stats
    {
        public AgentStats AgentStats { get; set; }
        public MapStats MapStats { get; set; }
        public WeaponStats WeaponStats { get; set; }
    }
}
