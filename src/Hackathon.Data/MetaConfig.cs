using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Data
{
    public class MetaConfig
    {
        public Dictionary<string, string> Agents { get; set; }
        public Dictionary<string, string> RankTiers { get; set; }
        public Dictionary<string, string> RankImages { get; set; }
        public Dictionary<string,string> Weapons { get; set; }
    }
}
