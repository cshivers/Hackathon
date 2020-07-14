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

        public PlayerService(MetaService metaService, BlitzService blitzService)
        {
            this.metaService = metaService;
            this.blitzService = blitzService;
        }
        public async Task<Player> GetPlayerAsync(string playerId)
        {
            // get the response from the blitz api
            var blitzResponse = await blitzService.GetPlayer(playerId);

            // map it to our model
            return GetPlayerFromBlitzResponse(blitzResponse); // in a real world application, we might have a factory responsible for this
        }
        private Player GetPlayerFromBlitzResponse(BlitzResponse blitzResponse)
        {
            var player =  new Player
            {
                Id = Guid.Parse(blitzResponse.Id), // if we have time, maybe add some safety checks to these
                Name = $"{blitzResponse.Name} #{blitzResponse.Tag}",
                Rank = metaService.GetRankTier(blitzResponse.Ranks.Competitive.Tier)
                // TODO: LifetimeStats come from blitzResponse.Stats.Competitive.Career
                // TODO: RecentStats come from blitzResponse.Stats.Competitive.Last20
                // The two properties above share types in both our Model and the BlitzResponse
                // It might be useful to create another helper method to save on code
                // While we're at it, using extension methods for each of these wouldn't be a bad idea ;)
            };

            return player;
        }
    }
}
