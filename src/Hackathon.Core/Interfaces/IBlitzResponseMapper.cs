using Hackathon.Core.Blitz.Models;
using Hackathon.Data.Models;

namespace Hackathon.Core.Blitz
{
    public interface IBlitzResponseMapper
    {
        Player MapToPlayer(BlitzResponse blitzResponse);
    }
}
