using AwesomeApp.WeatherRestClient;
using System;
using System.Threading.Tasks;

namespace AwesomeApp.ServicesHandler
{
    public class WeatherServices
    {
        OpenWeatherMap<WeatherMainModel> _openWeatherRest = new OpenWeatherMap<WeatherMainModel>();  //new model
        public async Task<WeatherMainModel> GetWeatherDetails(string city)          //take city and call api
        {
            WeatherMainModel getWeatherDetails = null;                              //initilize model for try catch
            try {
                getWeatherDetails = await _openWeatherRest.GetAllWeathers(city);    //call api 
            } catch (Exception e) {
                Console.WriteLine("GetWeatherDetailsTask: " + e);
            }
            return getWeatherDetails;
        }
    }
}
