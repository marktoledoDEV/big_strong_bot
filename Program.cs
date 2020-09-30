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
        private static string message_test_uri = "https://api.telegram.org/bot747776578:AAGu66QpI3Ux-x5bBNPo9EUjFHr4YTkv_04/sendmessage?chat_id={0}&text={1}";

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

            var updateRawJson = await HTTPCLIENT.GetAsync(test_uri);
            if (!updateRawJson.IsSuccessStatusCode)
            {
                Console.WriteLine($"GetUpdate Failed. Status Code: {updateRawJson.StatusCode}");
                return;
            }

            Console.WriteLine($"GetUpdate was successful. Parsing JSON now");
            dynamic updateList = JsonConvert.DeserializeObject(await updateRawJson.Content.ReadAsStringAsync());
            foreach (var result in updateList["result"])
            {
                var personData = result["message"]["chat"];

                string first_name = personData["first_name"];
                string last_name = personData["last_name"];
                string user_name = personData["username"];

                string message = String.Format("Hello {0} {1}! Would you like me to refer to you as @{2}",
                                                 personData["first_name"],
                                                 personData["last_name"],
                                                 personData["username"]);

                string message_URI = String.Format(message_test_uri, personData["id"], message);
                var response = await HTTPCLIENT.GetAsync(message_URI);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Response Failed. Reason: " + response.ReasonPhrase);
                    return;
                }
                Console.WriteLine("Message Should Have been sent");
            }
        }
    }
}
