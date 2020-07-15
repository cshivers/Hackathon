using Hackathon.Core.Blitz;
using Hackathon.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Core.Services
{
    public class PlayerService
        : IPlayerService
    {
        private const string KEY = "PLAYER_CACHE_KEY";

        private readonly IMemoryCache _cache;
        private readonly IBlitzService _blitzService;
        private readonly IBlitzResponseMapper _blitzResponseMapper;

        public PlayerService(IMemoryCache cache, IBlitzService blitzService, IBlitzResponseMapper blitzResponseMapper)
        {
            _cache = cache;
            _blitzService = blitzService;
            _blitzResponseMapper = blitzResponseMapper;
        }

        public async Task<AgentStats> GetAgentAsync(string playerId, string agentName)
        {
            Player player = await GetOrCreatePlayerObject(playerId);
            return player.RecentStats.AgentStats.FirstOrDefault(agent => agent.AgentName == agentName);
        }

        public async Task<IEnumerable<AgentStats>> GetAgentsAsync(string playerId)
        {
            Player player = await GetOrCreatePlayerObject(playerId);
            return player.RecentStats.AgentStats;
        }

        public Task<Player> GetAsync(string playerId) => GetOrCreatePlayerObject(playerId);

        public async Task<MapStats> GetMapAsync(string playerId, string mapName)
        {
            Player player = await GetOrCreatePlayerObject(playerId);
            return player.RecentStats.MapStats.FirstOrDefault(map => map.MapName == mapName);
        }

        public async Task<IEnumerable<MapStats>> GetMapsAsync(string playerId)
        {
            Player player = await GetOrCreatePlayerObject(playerId);
            return player.RecentStats.MapStats;
        }

        public async Task<WeaponStats> GetWeaponAsync(string playerId, string weaponName)
        {
            Player player = await GetOrCreatePlayerObject(playerId);
            return player.RecentStats.WeaponStats.FirstOrDefault(weapon => weapon.WeaponName == weaponName);
        }

        public async Task<IEnumerable<WeaponStats>> GetWeaponsAsync(string playerId)
        {
            Player player = await GetOrCreatePlayerObject(playerId);
            return player.RecentStats.WeaponStats;
        }

        private Task<Player> GetOrCreatePlayerObject(string playerId)
        {
            return _cache.GetOrCreateAsync($"{KEY}_{playerId}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                // get the response from the blitz api
                Blitz.Models.BlitzResponse blitzResponse = await _blitzService.GetPlayer(playerId);

                // map it to our model
                return _blitzResponseMapper.MapToPlayer(blitzResponse);
            });
        }
    }
}
