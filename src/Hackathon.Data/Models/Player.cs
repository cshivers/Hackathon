using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Data.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string RankImage { get; set; }
        public Stats LifetimeStats { get; set; } // career
        public Stats RecentStats { get; set; } // last20
    }
}
