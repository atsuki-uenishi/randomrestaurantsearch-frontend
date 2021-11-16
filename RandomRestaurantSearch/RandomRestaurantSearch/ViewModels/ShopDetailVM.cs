using Prism.Commands;
using Prism.Navigation;
using RandomRestaurantSearch.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RandomRestaurantSearch.ViewModels
{
    public class ShopDetailVM : INavigationAware, INotifyPropertyChanged
    {
        public  ICommand FavoriteCommand { get; set; }

        FavoriteShop favoriteShop;

        public ShopDetailVM()
        {
            FavoriteCommand = new DelegateCommand<Shop>(FavoriteRestaurant);
        }

        private void FavoriteRestaurant(Shop shop)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                favoriteShop = new FavoriteShop();
                favoriteShop.name = selectedShop.name;
                favoriteShop.address = selectedShop.address;
                favoriteShop.logo_image = selectedShop.logo_image;
                favoriteShop.url = selectedShop.urls.pc;

                conn.CreateTable<FavoriteShop>();
                int restaurantsInserted = conn.Insert(favoriteShop);
                if(restaurantsInserted >= 1)
                {
                    App.Current.MainPage.DisplayAlert("Success", "お気に入りに追加しました", "OK");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Failue", "エラーが発生しました", "OK");
                }
            }
        }

        Shop selectedShop;
        private string shopUrl;
        public string ShopUrl
        {
            get { return shopUrl; }
            set
            {
                shopUrl = value;
                OnPropertyChanged("ShopUrl");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            selectedShop = parameters["selected_shop"] as Shop;
            ShopUrl = selectedShop.urls.pc;
        }
    }
}
