using Prism;
using Prism.Ioc;
using Prism.Unity;
using RandomRestaurantSearch.ViewModels;
using RandomRestaurantSearch.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RandomRestaurantSearch
{
    public partial class App : PrismApplication
    {
        public static string DatabasePath;
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        public App(string databasePath,IPlatformInitializer initializer = null) : base(initializer)
        {
            DatabasePath = databasePath;

            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/HomePage");
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomeVM>();
            containerRegistry.RegisterForNavigation<SearchRestaurantPage, SearchRestaurantVM>();
            containerRegistry.RegisterForNavigation<FavoritePage, FavoriteVM>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginVM>();
            containerRegistry.RegisterForNavigation<RecommendedsPage, RecommendedsVM>();
            containerRegistry.RegisterForNavigation<RecommendedsPostPage, RecommendedsPostVM>();
            containerRegistry.RegisterForNavigation<ShopDetailPage, ShopDetailVM>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterVM>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/HomePage");

        }
    }
}
