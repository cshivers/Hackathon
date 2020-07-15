using Hackathon.Core.Blitz;
using Hackathon.Core.Blitz.Models;
using System.Threading.Tasks;

namespace Hackathon.Core.Services
{
    public interface IBlitzService
    {
        Task<BlitzResponse> GetPlayer(string id);
    }
}
