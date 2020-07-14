using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Data.Models
{
    public class MapStats
    {
        public string MapName { get; set; }
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
