using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace AwesomeApp.WeatherRestClient
{
    public class WeatherMainModel
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public WeatherTempDetails main { get; set; }
        public List<WeatherSubDetails> weather { get; set; }
        public WeatherWindDetails wind { get; set; }
        public WeatherSysDetails sys { get; set; }
    }

    public class WeatherSubDetails
    {
        [JsonProperty("main")]
        public string main { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
    }

    public class WeatherSysDetails
    {
        [JsonProperty("country")]
        public string country { get; set; }
    }

    public class WeatherTempDetails
    {
        [JsonProperty("temp")]
        public string temp { get; set; }
        public string tempFormat { 
            get {
                double aux = Math.Round(double.Parse(temp) - 273,1);
                return aux.ToString() + " °C";
            } 
            set { } }

        [JsonProperty("humidity")]
        public string humidity { get; set; }

        public string humidityFormat { 
            get {
                return humidity + " %";
            } set { } }
    }

    public class WeatherWindDetails
    {
        [JsonProperty("speed")]
        public string speed { get; set; }
        public string speedFormat
        {
            get {
                return speed + " m/s";
            }
            set { }
        }
    }

    public class Items {
        public int id { get; set; }
        public string values { get; set; }
        public Items (int Id, string Values) {
            values = Values;
            id = Id;
        }

        public Items() { }
    }
}
