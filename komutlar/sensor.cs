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
    public class sensor
    {

        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "CuBIrYURf4pST6buumJcD6dRJPvevBxaRt5RBpBF",
            BasePath = "https://fir-deneme-44be6-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        public IFirebaseClient istemci_firebase;


        public void LedDurumDegis(string led)
        {


            istemci_firebase = new FirebaseClient(config);
            istemci_firebase.Set("ledDurum", led);


        }

        public async Task<string> SicaklikDurumAsync()
        {
            
            istemci_firebase = new FirebaseClient(config);
            FirebaseResponse response = await istemci_firebase.GetAsync("Sicaklik");
            String cevap = response.ResultAs<String>();
            Console.WriteLine(cevap);
            return cevap;
            
        }


    }
}
