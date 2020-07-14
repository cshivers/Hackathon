using Hackathon.Core.Blitz;
using Hackathon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Core.Services
{
    public class PlayerService
    {
        private readonly MetaService metaService;
        private readonly BlitzService blitzService;
        private readonly BlitzResponseMapper blitzResponseMapper;

        public PlayerService(MetaService metaService, BlitzService blitzService, BlitzResponseMapper blitzResponseMapper)
        {
            this.metaService = metaService;
            this.blitzService = blitzService;
            this.blitzResponseMapper = blitzResponseMapper;
        }
        public async Task<Player> GetPlayerAsync(string playerId)
        {
            // get the response from the blitz api
            var blitzResponse = await blitzService.GetPlayer(playerId);

            // map it to our model
            return blitzResponseMapper.MapToPlayer(blitzResponse);
        }

    }
}
