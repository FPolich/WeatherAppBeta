using AwesomeApp.ServicesHandler;
using System.ComponentModel;
using AwesomeApp.WeatherRestClient;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AwesomeApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherMainModel _weatherMainModel;
        WeatherServices _weatherServices = new WeatherServices();

        private string _city;   //for entry binding and for method parameter value
        public string City {
            get { return _city; }
            set {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private async Task InitializeGetWeatherAsync() {
            try
            {
                IsBusy = true;
                WeatherMainModel = await _weatherServices.GetWeatherDetails(_city);
            } catch (Exception e)
            {
                Console.WriteLine("InitializeGetWeatherAsync: " + e);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public WeatherMainModel WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                OnPropertyChanged(nameof(WeatherMainModel));
            }
        }

        private bool _isBusy;  //for showing loader when the task is initialize

        public WeatherViewModel()
        {
            ButtonSearch = new Command(PerfomButtonSearch);
        }

        public bool IsBusy {
            get { return _isBusy; }
            set {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand ButtonSearch { get; }
        public void PerfomButtonSearch()
        {
            try
            {
                Task.Run(async () =>
                            {
                                await InitializeGetWeatherAsync();
                            });
                            OnPropertyChanged(nameof(ButtonSearch));
            } catch (Exception e)
            {
                Console.WriteLine("PerfomButtonSearch: " + e);
            }
            
        }
    }
}

