using AwesomeApp.ServicesHandler;
using System.ComponentModel;
using AwesomeApp.WeatherRestClient;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

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

        public WeatherMainModel WeatherMainModel {
            get {
                return _weatherMainModel;
            }
            set {
                _weatherMainModel = value;
                OnPropertyChanged(nameof(WeatherMainModel));
            }
        }
        private int Contador { get; set; }

        private Items _objeto = new Items();
        public Items Objeto { get {
                return _objeto;
            }
            set {
                _objeto = value;
            }
        }
        //Constructor
        public WeatherViewModel()
        {
            ButtonSearch = new Command(PerfomButtonSearch);

            SelectedService = new ObservableCollection<Items>();
            Objeto = new Items(1, "Madrid");
            SelectedService.Add(Objeto);
            Objeto = new Items(2, "Mendoza");
            SelectedService.Add(Objeto);
            Contador = 2;
        }

        //Call Api
        private async Task InitializeGetWeatherAsync() {
            try {
                IsBusy = true;
                WeatherMainModel = await _weatherServices.GetWeatherDetails(_city);
            }
            catch (Exception e) {
                Console.WriteLine("InitializeGetWeatherAsync: " + e);
            }
            finally {
                IsBusy = false;
            }
        }

        //Button config
        public ICommand ButtonSearch { get; }
        public void PerfomButtonSearch() {
            try {
                Task.Run(async () => {
                    Contador++;
                    Items cosa = new Items(Contador, _city);
                    bool index = SelectedService.Any(x => x.values == cosa.values);
                    if (!index)
                    {
                        SelectedService.Add(cosa);
                    }
                    await InitializeGetWeatherAsync();
                });
                OnPropertyChanged(nameof(ButtonSearch));
            }
            catch (Exception e) {
                Console.WriteLine("PerfomButtonSearch: " + e);
            }
        }

        //List of citys
        public ObservableCollection <Items> SelectedService { get; set; }

        private Items _selectedItem;
        public Items selectedItem {
            get => _selectedItem;
            set {
                try {
                    if (_selectedItem != value) {
                        _selectedItem = value;
                        SelectedItemValue = value.values;
                        _city = _selectedItem.values;
                        OnPropertyChanged(nameof(_selectedItem));
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Picker Excep: " + e);
                }
            }
        }

        private string _SelectedItemValue;
        public string SelectedItemValue {
            get {
                return _SelectedItemValue;
            }
            set {
                if (_SelectedItemValue != value) {
                    _SelectedItemValue = value;
                    OnPropertyChanged(nameof(SelectedItemValue));
                }
            }
        }

        private bool _isBusy;  //for showing loader when the task is initialize
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
    }
}
