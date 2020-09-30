using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace big_strong_bot
{
    class Program
    {
        private static readonly HttpClient HTTPCLIENT = new HttpClient();
        
        //Telegram API Info
        private static readonly string API_TOKEN = "747776578:AAGu66QpI3Ux-x5bBNPo9EUjFHr4YTkv_04";
        private static string test_uri = "https://api.telegram.org/bot747776578:AAGu66QpI3Ux-x5bBNPo9EUjFHr4YTkv_04/getupdates";
        
        static async Task Main(string[] args)
        {
            await ProccessUpdates();
        }

        private static async Task ProccessUpdates()
        {
            Console.WriteLine("This is the Big Strong Bot!");
             HTTPCLIENT.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HTTPCLIENT.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", API_TOKEN);

            HTTPCLIENT.DefaultRequestHeaders.Accept.Clear();
            var stringTask = HTTPCLIENT.GetStringAsync(test_uri);

            var msg = await stringTask;
            Console.WriteLine(msg);

            var updateRawJson = await HTTPCLIENT.GetAsync(test_uri);
            if(!updateRawJson.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetUpdate Failed. Status Code: {updateRawJson.StatusCode}");
            }

            Console.WriteLine($"GetUpdate was successful. Parsing JSON now");
            dynamic updateList = JsonConvert.DeserializeObject(await updateRawJson.Content.ReadAsStringAsync());
            foreach(var result in updateList["result"]){
                string update_id = result["update_id"];
                Console.WriteLine($"Sample Update ID: {update_id}");
            }
        }
    }
}
