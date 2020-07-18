using DSharpPlus.CommandsNext;
using System.Net.Http;

namespace Hackathon.DiscordBot.Commands
{
    public class WeaponCommands 
        : BaseCommandModule, IWeaponCommands
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WeaponCommands(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
