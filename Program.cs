using System;
using System.Threading.Tasks;
using DiscordBotDeneme.komutlar;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace DiscordBotDeneme
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

       
        

        internal static async Task MainAsync()
        {


            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "MTA1MTUyMjAzNzcxNTMxMjY4MA.GvS2vV.cY9ZcbE6rO9QL0iDakFsTkzwAtZ5evqZz4zV5E",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });



            discord.ComponentInteractionCreated += async (s, e) =>
            {




                sensor led = new sensor();
                
               

                String buton = e.Id.ToString();
                DiscordInteractionResponseBuilder cevap;
                 if (buton == "ledac")
                {

                     cevap = MyFirstModule.LedMessage("Açık", ButtonStyle.Secondary, ButtonStyle.Danger);
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, cevap);
                    led.LedDurumDegis("Açık");
                }
                if (buton == "ledkapat")
                {
                    cevap = MyFirstModule.LedMessage("Kapalı", ButtonStyle.Success, ButtonStyle.Secondary);
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, cevap);
                    led.LedDurumDegis("Kapalı");
                }
                if (buton == "sicaklikolc")
                {
                    
                    
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, MyFirstModule.SicaklikMessage());
                    
                }

                await e.Interaction.CreateResponseAsync(
                    InteractionResponseType.UpdateMessage,
                    new DiscordInteractionResponseBuilder()
                        .WithContent(buton)); 
            };





        commands.RegisterCommands<MyFirstModule>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }


    }
}
