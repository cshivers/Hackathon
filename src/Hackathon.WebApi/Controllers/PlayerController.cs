using Hackathon.Core.Services;
using Hackathon.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController 
        : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        // GET api/player/{playerId}
        [HttpGet("{playerId}")]
        public Task<Player> Get(string playerId)
        {
            return _playerService.GetAsync(playerId);
        }

        // GET api/player/{playerId}/weapons
        [HttpGet("{playerId}/weapons")]
        public Task<IEnumerable<WeaponStats>> GetWeapons(string playerId)
        {
            return _playerService.GetWeaponsAsync(playerId);
        }

        // GET api/player/{playerId}/weapons/{weaponName}
        [HttpGet("{playerId}/weapons/{weaponName}")]
        public Task<WeaponStats> GetWeapon(string playerId, string weaponName)
        {
            return _playerService.GetWeaponAsync(playerId, weaponName);
        }

        // GET api/player/{playerId}/agents
        [HttpGet("{playerId}/agents")]
        public Task<IEnumerable<AgentStats>> GetAgents(string playerId)
        {
            return _playerService.GetAgentsAsync(playerId);
        }

        // GET api/player/{playerId}/agents/{agentName}
        [HttpGet("{playerId}/agents/{agentName}")]
        public Task<AgentStats> GetAgent(string playerId, string agentName)
        {
            return _playerService.GetAgentAsync(playerId, agentName);
        }

        // GET api/player/{playerId}/maps
        [HttpGet("{playerId}/maps")]
        public Task<IEnumerable<MapStats>> GetMaps(string playerId)
        {
            return _playerService.GetMapsAsync(playerId);
        }

        // GET api/player/{playerId}/maps/{mapName}
        [HttpGet("{playerId}/maps/{mapName}")]
        public Task<MapStats> GetMap(string playerId, string mapName)
        {
            return _playerService.GetMapAsync(playerId, mapName);
        }
    }
}
