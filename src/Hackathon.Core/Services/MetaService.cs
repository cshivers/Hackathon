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
            // TODO: pull the corresponding value from meta config and return a default string "ex: N/A"
            throw new NotImplementedException();
        }
        public string GetRankTier(int id)
        {
            var key = id.ToString();
            if (!metaConfig.RankTiers.ContainsKey(key)) return null;

            return metaConfig.RankTiers[key];
        }
        public string GetWeaponName(string id)
        {
            // TODO: pull the corresponding value from meta config and return a default string "ex: N/A"
            throw new NotImplementedException();
        }

    }
}
