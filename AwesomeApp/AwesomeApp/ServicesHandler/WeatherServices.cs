using AwesomeApp.WeatherRestClient;
using System;
using System.Threading.Tasks;

namespace AwesomeApp.ServicesHandler
{
    public class WeatherServices
    {
        OpenWeatherMap<WeatherMainModel> _openWeatherRest = new OpenWeatherMap<WeatherMainModel>();
        public async Task<WeatherMainModel> GetWeatherDetails(string city)
        {
            WeatherMainModel getWeatherDetails = null;
            try
            {
                getWeatherDetails = await _openWeatherRest.GetAllWeathers(city);
            } catch (Exception e)
            {
                Console.WriteLine("GetWeatherDetailsTask: " + e);
            }
            return getWeatherDetails;
        }
    }
}
