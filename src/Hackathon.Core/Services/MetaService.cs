using Hackathon.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon.Core.Services
{
    public class MetaService
    {
        private readonly MetaConfig metaConfig;
        public MetaService(IOptions<MetaConfig> metaConfig)
        {
            this.metaConfig = metaConfig?.Value ?? throw new ArgumentNullException(nameof(metaConfig));
        }
        public string GetAgentName(string id)
        {
            var key = id;
            if (!metaConfig.Agents.ContainsKey(key)) return null;

            return metaConfig.Agents[key];
        }
        public string GetRankTier(int id)
        {
            var key = id.ToString();
            if (!metaConfig.RankTiers.ContainsKey(key)) return null;

            return metaConfig.RankTiers[key];
        }
        public string GetWeaponName(string id)
        {
            var key = id;
            if (!metaConfig.Weapons.ContainsKey(key)) return null;

            return metaConfig.Weapons[key];
        }

    }
}
