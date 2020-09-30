using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace big_strong_bot
{
    class Program
    {
        private static readonly HttpClient HTTPCLIENT = new HttpClient();
        
        //Telegram API Info
        private static readonly string API_TOKEN = "747776578:AAGu66QpI3Ux-x5bBNPo9EUjFHr4YTkv_04";

        public static async Task Main()
        {
            Console.WriteLine("This is the Big Strong Bot!");
        }
    }
}
