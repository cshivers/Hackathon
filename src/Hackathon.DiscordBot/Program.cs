using Hackathon.DiscordBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hackathon.DiscordBot
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            ServiceProvider serviceProvder = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<IBot, Bot>()
                .AddSingleton<IPlayerCommands, PlayerCommands>()
                .AddSingleton<IWeaponCommands, WeaponCommands>()
                .BuildServiceProvider();

            IBot bot = serviceProvder.GetService<IBot>();
            return bot.RunAsync();
        }
    }
}
