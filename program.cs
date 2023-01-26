using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace idojaras23
{
    internal class Program
    {
        static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp).ToLocalTime();
            return dt;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Adja meg a város nevét:");
            string city = Console.ReadLine();

            string apiKey = "9513650be858237873fafb82470ccd3c";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=hu";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                JObject weatherData = JObject.Parse(json);

                Console.WriteLine("Idő: " + weatherData["weather"][0]["description"]);
                Console.WriteLine("Naplemente: " + UnixTimeStampToDateTime((double)weatherData["sys"]["sunset"]));
                Console.WriteLine("Napfelkelte: " + UnixTimeStampToDateTime((double)weatherData["sys"]["sunrise"]));
                Console.WriteLine("Hőmérséklet: " + weatherData["main"]["temp"] + " °C");
                Console.WriteLine("Páratartalom: " + weatherData["main"]["humidity"] + "%");
                Console.WriteLine("Eső valószínűsége: " + weatherData["clouds"]["all"] + "%");
            }

            Console.Read();
        }
    }
}
