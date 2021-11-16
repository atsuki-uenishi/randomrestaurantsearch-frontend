using Prism.Commands;
using Prism.Navigation;
using RandomRestaurantSearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace RandomRestaurantSearch.ViewModels
{
    public class RecommendedsVM
    {
        INavigationService _navigationService;
        public ObservableCollection<RecommendedShop> RecommendedShops { get; set; }
        public ICommand GoToPostCommand { get; set; }
        public ICommand PostDetailCommand { get; set; }
        public RecommendedsVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToPostCommand = new DelegateCommand<object>(GoToPost);
            PostDetailCommand = new DelegateCommand<object>(PostDetail);
            RecommendedShops = new ObservableCollection<RecommendedShop>();
        }

        private void PostDetail(object obj)
        {
            throw new NotImplementedException();
        }

        private void GoToPost(object obj)
        {
            _navigationService.NavigateAsync("RecommendedsPost");
        }
    }
}
