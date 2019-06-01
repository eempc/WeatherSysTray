using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace WeatherSysTray0 {
    public class WeatherFeed {
        private static string baseUrl = @"http://api.openweathermap.org/data/2.5/";
        private static string fullUrl = @"http://api.openweathermap.org/data/2.5/weather?id=3333231&APPID=717930972ee1193baef799e1dbc45961";
        private static string rawJson;
       
        public static double temperatureKelvin = 273.15;
        // https://openweathermap.org/weather-conditions
        public static int weatherCode = 0;
        public static long epochTime = 0;
        public static bool rainExists = false;

        public static void MakeApiCall(string cityCode = "3333231") {
            WebClient client = new WebClient();
            rawJson = client.DownloadString(fullUrl);
            ExtractValues(rawJson);
            WriteFile();
        }

        public static void WriteFile() {
            using (StreamWriter sw = new StreamWriter(GenerateFileName())) {
                sw.Write(rawJson);
            }
        }

        public static string GenerateFileName() {
            string path = @"D:\Temp\WeatherReports";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            string file = epochTime.ToString() + " Glasgow.json";
            return Path.Combine(path, file);
        }

        public static void ExtractValues(string json) {
            if (string.IsNullOrEmpty(json)) return;

            // Top level contains keys for rain and cloud if there any, as well as datetime
            Dictionary<string, object> topLevel = JsonParseOneLevel(json); // Deserialise the top level into a Dictionary<string, object>

            long dateTimeTarget = (long)topLevel["dt"];
            epochTime = dateTimeTarget;

            if (topLevel.ContainsKey("rain")) {
                rainExists = true;
            } else {
                rainExists = false;
            }

            // Temperature enquiry
            string json2 = topLevel["main"].ToString(); // Take the object-value with the key "main" and cast it to string to get a second sub-JSON
            Dictionary<string, object> secondLevel = JsonParseOneLevel(json2); // Deserialise this as the second level into another Dict<string, object>, although this one could be a Dict<string, double> because all the values are numbers
            double tempTarget = Convert.ToDouble(secondLevel["temp"]); // Then use the "temp" key to get the temperature
            temperatureKelvin = tempTarget;


        }

        public static Dictionary<string, object> JsonParseOneLevel(string json) {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }


    }
}
