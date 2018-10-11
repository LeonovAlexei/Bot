using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace InstatekaKrdBot
{
    class Program
    {
        static void Main(string[] args)
        {
            int update_id = 0;
            string messageFronId = "";
            string messageText = "";
            string token = "524725723:AAEAM2lmGOIQiBHef3I4inuedYcwQZMIKI4";

            WebClient webClient = new WebClient();
            string startUrl = $"http://api.telegram.org/bot{token}";
            
            while (true)
            {
                string url = $"{startUrl}/getUpdates?offset={update_id+1}";
                string response = webClient.DownloadString(url);
                var array = JObject.Parse(response)["result"].ToArray();

                foreach (var message in array)
                {
                    update_id = Convert.ToInt32(message["update_id"]);
                    try
                    {
                        messageFronId = message["message"]["from"]["id"].ToString();
                        messageText = message["message"]["text"].ToString();
                        Console.WriteLine($"{update_id} {messageFronId} {messageText}");
                        messageText = $"Вы прислали {messageText}";
                        url = $"{startUrl}/sendMessage?chat_id={messageFronId}&text={messageText}";
                        webClient.DownloadString(url);
                    }
                    catch { } 
                    
                }
                Thread.Sleep(50);
            }
        }
    }
}
