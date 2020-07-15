using Hackathon.Data;
using Microsoft.Extensions.Options;
using System;

namespace Hackathon.Core.Services
{
    public class MetaService
        : IMetaService
    {
        private readonly MetaConfig _metaConfig;

        public MetaService(IOptions<MetaConfig> metaConfig)
        {
            _metaConfig = metaConfig?.Value ?? throw new ArgumentNullException(nameof(metaConfig));
        }

        public string GetAgentName(string id) => _metaConfig.Agents.TryGetValue(id, out string agent) ? agent : null;
        public string GetRankTier(int id) => _metaConfig.Agents.TryGetValue(id.ToString(), out string rank) ? rank : null;
        public string GetWeaponName(string id) => _metaConfig.Weapons.TryGetValue(id, out string weapon) ? weapon : null;
    }
}
