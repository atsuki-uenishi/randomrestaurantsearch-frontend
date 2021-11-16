using Prism.Commands;
using Prism.Navigation;
using RandomRestaurantSearch.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RandomRestaurantSearch.ViewModels
{
    public class FavoriteVM
    {
        public ICommand ShopDetailCommand { get; set; }
        public ICommand TodayShopCommand { get; set; }
        public ICommand FavoriteUpdateCommand { get; set; }
        public ICommand FavoriteDeleteCommand { get; set; }
        public ObservableCollection<FavoriteShop> FavoriteShops { get; set; }
        INavigationService _navigationService;

        public FavoriteVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            FavoriteShops = new ObservableCollection<FavoriteShop>();
            ShopDetailCommand = new DelegateCommand<object>(GoToDetail);
            TodayShopCommand = new DelegateCommand<object>(TodayShop);
            FavoriteUpdateCommand = new DelegateCommand<object>(FavoriteUpdate);
            FavoriteDeleteCommand = new DelegateCommand<object>(FavoriteDelete);
            ReadFavoriteShops();
        }

        private void FavoriteDelete(object obj)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.DeleteAll<FavoriteShop>();

                FavoriteShops.Clear();
            }
        }

        private void FavoriteUpdate(object obj)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<FavoriteShop>();
                var shops = conn.Table<FavoriteShop>().ToList();

                FavoriteShops.Clear();
                foreach (var shop in shops)
                {
                    FavoriteShops.Add(shop);
                }
            }
        }

        private void TodayShop(object obj)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<FavoriteShop>();
                var shops = conn.Table<FavoriteShop>().ToList();

                FavoriteShops.Clear();
                foreach (var shop in shops)
                {
                    FavoriteShops.Add(shop);
                }

                Random r = new Random();
                FavoriteShop TodayShop = FavoriteShops[r.Next(0, FavoriteShops.Count)];

            }
        }

        private void ReadFavoriteShops()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<FavoriteShop>();
                var shops = conn.Table<FavoriteShop>().ToList();

                FavoriteShops.Clear();
                foreach(var shop in shops)
                {
                    FavoriteShops.Add(shop);
                }
            }
        }

        private async void GoToDetail(object obj)
        {
            var selectedShop = (obj as ListView).SelectedItem as FavoriteShop;
            Shop GoToDetailShop = new Shop();
            Urls ShopUrl = new Urls();
            GoToDetailShop.name = selectedShop.name;
            GoToDetailShop.address = selectedShop.address;
            GoToDetailShop.logo_image = selectedShop.logo_image;

            ShopUrl.pc = selectedShop.url;
            GoToDetailShop.urls = ShopUrl;



            var parameter = new NavigationParameters();
            parameter.Add("selected_shop", GoToDetailShop);
            await _navigationService.NavigateAsync("ShopDetailPage", parameter);
        }
    }
}
