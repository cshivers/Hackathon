using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.Core.Services;
using Hackathon.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService playerService;
        public PlayerController(PlayerService playerService)
        {
            this.playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }
        [HttpGet]
        public Task<Player> Get(string id)
        {
            // right now the route is api/player?id={id}
            // we can certainly change this to something like api/player/id

            return playerService.GetPlayerAsync(id);
        }
    }
}
