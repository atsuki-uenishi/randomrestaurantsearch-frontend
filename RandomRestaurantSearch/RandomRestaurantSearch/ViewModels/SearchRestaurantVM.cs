using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RandomRestaurantSearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RandomRestaurantSearch.ViewModels
{
    public class SearchRestaurantVM : INotifyPropertyChanged
    {
        public string restaurantGenre1;
        public string restaurantGenre2;
        public string restaurantGenre3;
        private string searchGenre;
        IGeolocator locator = CrossGeolocator.Current;
        public double latitude;
        public double longitude;

        public ICommand SearchCommand { get; set; }

        public ObservableCollection<Shop> SearchResults { get; set; }

        public ICommand ShopDetailCommand { get; set; }

        IPageDialogService _pageDialogService;

        INavigationService _navigationService;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string RestaurantGenre1
        {
            get { return restaurantGenre1; }
            set
            {
                restaurantGenre1 = value;
                OnPropertyChanged("RestaurantGenre");
            }
        }

        public string RestaurantGenre2
        {
            get { return restaurantGenre2; }
            set
            {
                restaurantGenre2 = value;
                OnPropertyChanged("RestaurantGenre");
            }
        }

        public string RestaurantGenre3
        {
            get { return restaurantGenre3; }
            set
            {
                restaurantGenre3 = value;
                OnPropertyChanged("RestaurantGenre");
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Genres
        {
            get;
            set;
        }

        public SearchRestaurantVM(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            Genres = new ObservableCollection<string>();
            SearchCommand = new DelegateCommand<string>(GetSearchResults);
            SearchResults = new ObservableCollection<Shop>();
            ShopDetailCommand = new DelegateCommand<object>(GoToDetail);
            _pageDialogService = pageDialogService;
            GetGenres();
        }

        private async void GoToDetail(object obj)
        {
            var selectedShop = (obj as ListView).SelectedItem as Shop;

            var parameter = new NavigationParameters();
            parameter.Add("selected_shop", selectedShop);
            await _navigationService.NavigateAsync("ShopDetailPage", parameter);
        }

        private async void GetSearchResults(string query)
        {
            searchGenre = "";
            Random randomGenre = new System.Random();
            int targetGenre = randomGenre.Next(1, 4);
            if (RestaurantGenre1 == "指定なし" && RestaurantGenre2 != "指定なし" && RestaurantGenre3 != "指定なし")
            {
                targetGenre = randomGenre.Next(2, 4);
            }
            else if (RestaurantGenre2 == "指定なし" && RestaurantGenre1 != "指定なし" && RestaurantGenre3 != "指定なし")
            {
                targetGenre = randomGenre.Next(1, 4);
                if(targetGenre == 2)
                {
                    for (var i = 0; i < 100; i++)
                    {
                        targetGenre = randomGenre.Next(1, 4);
                        if(targetGenre != 2)
                        {
                            break;
                        }
                    }
                    
                }
            }
            else if (RestaurantGenre3 == "指定なし" && RestaurantGenre2 != "指定なし" && RestaurantGenre1 != "指定なし")
            {
                targetGenre = randomGenre.Next(1, 3);
            }
            else if(RestaurantGenre1 == "指定なし" && RestaurantGenre3 == "指定なし" && RestaurantGenre2 != "指定なし")
            {
                targetGenre = 2;
            }
            else if(RestaurantGenre2 == "指定なし" && RestaurantGenre3 == "指定なし" && RestaurantGenre1 != "指定なし")
            {
                targetGenre = 1;
            }
            else if(RestaurantGenre1 == "指定なし" && RestaurantGenre2 == "指定なし" && RestaurantGenre3 != "指定なし")
            {
                targetGenre = 3;
            }
            else if(RestaurantGenre1 == "指定なし" && RestaurantGenre2 == "指定なし" && RestaurantGenre3 == "指定なし")
            {
                await Application.Current.MainPage.DisplayAlert("エラー", "ジャンルを1つ選んでください", "OK");
            }

            if (targetGenre == 1)
            {
                searchGenre = RestaurantGenre1;
            }
            else if(targetGenre == 2)
            {
                searchGenre = RestaurantGenre2;
            }
            else
            {
                searchGenre = RestaurantGenre3;
            }
            
            await GetLocation();
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync($"http://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key=e2928c8e46f9ac3b&format=json&lat={Latitude}&lng={Longitude}&keyword={searchGenre}");

                var data = JsonConvert.DeserializeObject<Rootobject>(result);

                SearchResults.Clear();
                foreach (var shop in data.results.shop)
                {
                    SearchResults.Add(shop);
                }
            }
            await locator.StopListeningAsync();
        }

        private void GetGenres()
        {
            Genres.Clear();
            Genres.Add("指定なし");
            Genres.Add("焼肉");
            Genres.Add("居酒屋");
            Genres.Add("寿司");
            Genres.Add("鍋");
            Genres.Add("中華");
            Genres.Add("韓国料理");
            Genres.Add("カレー");
            Genres.Add("ラーメン");
            Genres.Add("うどん");
            Genres.Add("パスタ");
            Genres.Add("お好み焼き");
            Genres.Add("丼もの");
            Genres.Add("ハンバーグ");
            Genres.Add("ステーキ");
            Genres.Add("ハンバーガー");
            Genres.Add("イタリアン");
            Genres.Add("焼鳥");
            Genres.Add("スイーツ");
        }

        private async Task  GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);

                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}
