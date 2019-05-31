using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace WeatherSysTray0 {
    public static class JSON {
        public static Dictionary<string, Dictionary<string, object>> JsonParseOneLevel(string json) {
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);
        }
    }
}
