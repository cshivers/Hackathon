using Hackathon.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Core.Services
{
    public interface IPlayerService
    {
        Task<AgentStats> GetAgentAsync(string playerId, string agentName);

        Task<IEnumerable<AgentStats>> GetAgentsAsync(string playerId);

        Task<Player> GetAsync(string playerId);

        Task<MapStats> GetMapAsync(string playerId, string mapName);

        Task<IEnumerable<MapStats>> GetMapsAsync(string playerId);

        Task<WeaponStats> GetWeaponAsync(string playerId, string weaponName);

        Task<IEnumerable<WeaponStats>> GetWeaponsAsync(string playerId);
    }
}
