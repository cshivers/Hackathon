using System;

namespace Hackathon.Data.Models
{
    public class WeaponStats
    {
        public Guid WeaponId { get; set; }
        public string WeaponName { get; set; }
        public double TotalKillRange { get; set; }
        public int AltFireKills { get; set; }
        public int Headshots { get; set; }
        public int Bodyshots { get; set; }
        public int Legshots { get; set; }
        public int Damage { get; set; }
        public int RoundsUsed { get; set; }
    }
}
