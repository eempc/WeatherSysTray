using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace WeatherSysTray0 {
    public class WeatherFeed {
        private static string baseUrl = @"http://api.openweathermap.org/data/2.5/";
        private static string fullUrl = @"http://api.openweathermap.org/data/2.5/weather?id=3333231&APPID=717930972ee1193baef799e1dbc45961";
        private static string rawJson;

        public static double temperatureKelvin = 273.15;
        // https://openweathermap.org/weather-conditions
        public static int weatherCode = 0;
        public static long epochTime = 0;

        public static void MakeApiCall(string cityCode = "3333231") {
            WebClient client = new WebClient();
            rawJson = client.DownloadString(fullUrl);
            ExtractValues(rawJson);
        }

        public static void ExtractValues(string json) {
            if (string.IsNullOrEmpty(json)) return;

            var topLevel = JsonParseOneLevel(json); // Deserialise the top level into a Dictionary<string, object>
            string json2 = topLevel["main"].ToString(); // Take the object-value with the key "main" and cast it to string to get a second sub-JSON
            var secondLevel = JsonParseOneLevel(json2); // Deserialise this as the second level into another Dict<string, object>, although this one could be a Dict<string, double> because all the values are numbers

            double tempTarget = Convert.ToDouble(secondLevel["temp"]); // Then use the "temp" key to get the temperature
            temperatureKelvin = tempTarget;

            long dateTimeTarget = (long)topLevel["dt"];
            epochTime = dateTimeTarget;

        }

        public static Dictionary<string, object> JsonParseOneLevel(string json) {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }


    }
}
