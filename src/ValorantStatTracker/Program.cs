using System;

namespace ValorantStatTracker
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        } 
        
        public static string CapitalizeFirstLetter(string string1)
        {
            Char[] characters = string1.ToCharArray();
            characters[0] = Char.ToUpper(characters[0]);
            return new string(characters);
        }
    }
}
