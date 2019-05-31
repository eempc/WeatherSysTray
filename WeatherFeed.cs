using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using System.IO;

namespace WeatherSysTray0 {
    public class WeatherFeed {
        public const double absoluteZeroDelta = 273.15;
        private double temperature = 0;
        private static string baseUrl = @"http://api.openweathermap.org/data/2.5/";
        private static string fullUrl = @"http://api.openweathermap.org/data/2.5/weather?id=3333231&APPID=717930972ee1193baef799e1dbc45961";
        // API key is free up to a certain amount of calls, for one city, only need to make one call per 10 minutes
        private static string api = "717930972ee1193baef799e1dbc45961";

        public double Kelvin {
            get {
                return temperature;
            }
            set {
                temperature = value;
            }
        }

        public double Celsius {
            get {
                return temperature + absoluteZeroDelta;
            }
        }

        public double Fahrenheit {
            get {
                return (temperature - absoluteZeroDelta) * 9 / 5 + 32;
            }
        }

        private static string MakeApiCall(string cityCode = "3333231") {
            WebClient client = new WebClient();
            return client.DownloadString(fullUrl);
        }

        public static double ExtractTemperature() {   
            string json = File.ReadAllText(@"D:\Projects\PythonScripts\glasgow.json"); // Raw JSON file with different types of variables          
            var topLevel = JsonParseOneLevel(json); // Deserialise the top level into a Dictionary<string, object>
            string json2 = topLevel["main"].ToString(); // Take the object with the key "main" and cast it to string to get a second sub-JSON
            var secondLevel = JsonParseOneLevel(json2); // Deserialise this as the second level into another Dict<string, object>, although this one could be a Dict<string, double> because all the values are numbers
            double target = Convert.ToDouble(secondLevel["temp"]); // Then use the "temp" key to get the temperature
            Random rng = new Random();
            return rng.Next(280, 320);
        }

        public static Dictionary<string, object> JsonParseOneLevel(string json) {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }


    }
}
