using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotDeneme.komutlar
{
    public class MyFirstModule : BaseCommandModule
    {

        public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "CuBIrYURf4pST6buumJcD6dRJPvevBxaRt5RBpBF",
            BasePath = "https://fir-deneme-44be6-default-rtdb.europe-west1.firebasedatabase.app/"
        };

       public IFirebaseClient istemci_firebase;

        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Greetings! Thank you for executing me!");
        }

        [Command("sa")]
        public async Task SaCommand(CommandContext ctx)
        {
            var myButton = new DiscordButtonComponent(ButtonStyle.Danger, "my_custom_id", "Buton deneme");
            var builder = new DiscordMessageBuilder()
                .WithContent("as!")
                .AddComponents(myButton);
            await ctx.RespondAsync(builder);

        }

        [Command("led")]
        public async Task LedDurumCommand(CommandContext ctx)
        {

            istemci_firebase = new FirebaseClient(config);
            FirebaseResponse response = await istemci_firebase.GetAsync("ledDurum");
            String cevap = response.ResultAs<String>();
            

                
            var openButton = new DiscordButtonComponent(ButtonStyle.Success, "ledac", "Aç");
            var closeButton = new DiscordButtonComponent(ButtonStyle.Danger, "ledkapat", "Kapat");
            var builder = new DiscordMessageBuilder()
                .WithContent("Led Durumu: "+cevap)
                .AddComponents(openButton,closeButton);

            await ctx.Channel.SendMessageAsync(builder);

            



        }
        [Command("sicaklik")]
        public async Task SicaklikDurumCommand(CommandContext ctx)
        {

            sensor sicaklik = new sensor();

            

            var openButton = new DiscordButtonComponent(ButtonStyle.Success, "sicaklikolc", "Ölç");
            var closeButton = new DiscordButtonComponent(ButtonStyle.Danger, "ledkapat", "Kapat");
            var builder = new DiscordMessageBuilder()
                .WithContent("Sicaklik: " + sicaklik.SicaklikDurumAsync().Result)
                .AddComponents(openButton);

            await ctx.Channel.SendMessageAsync(builder);





        }

        public static DiscordInteractionResponseBuilder SicaklikMessage()
        {

            sensor sicaklik = new sensor();
            var openButton = new DiscordButtonComponent(ButtonStyle.Primary, "sicaklikolc", " Tekrar Ölç");

            var builder = new DiscordInteractionResponseBuilder()
                .WithContent("Sicaklik: " + sicaklik.SicaklikDurumAsync().Result)
                .AddComponents(openButton);
            return builder;
        }



        public static DiscordInteractionResponseBuilder LedMessage(string ledDurum,ButtonStyle ilk,ButtonStyle ikinci)
        {

            var openButton = new DiscordButtonComponent(ilk, "ledac", "Aç");
            var closeButton = new DiscordButtonComponent(ikinci, "ledkapat", "Kapat");
            var builder = new DiscordInteractionResponseBuilder()
                .WithContent("Led Durumu: " + ledDurum)
                .AddComponents(openButton, closeButton);
            return builder;
        }

        
        
}
}
