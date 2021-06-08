using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AwesomeApp.WeatherRestClient
{
    public class OpenWeatherMap<T>
    {
        private const string OpenWeatherApi = "https://api.openweathermap.org/data/2.5/weather?q=";
        private const string key = "c14813d0eabee9f14ca702fc44ddc44e";
        HttpClient _httpClient = new HttpClient();

        public async Task<T> GetAllWeathers(string city)
        {
            object nullable = null;
            T getWeatherModels = (T)nullable;
            string json = "";
            string request = OpenWeatherApi + city + "&APPID=" + key;

            try
            {
                json = await _httpClient.GetStringAsync(request);
                getWeatherModels = JsonConvert.DeserializeObject<T>(json);

            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return getWeatherModels;
        }
    }
}
