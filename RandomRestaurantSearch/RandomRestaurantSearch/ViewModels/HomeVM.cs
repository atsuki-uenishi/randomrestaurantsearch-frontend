using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RandomRestaurantSearch.ViewModels
{
    public class HomeVM
    {
        public ICommand GoToLoginCommand { get; set; }

        INavigationService _navigationService;
        public HomeVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToLoginCommand = new DelegateCommand<object>(GoToLogin);
        }

        private void GoToLogin(object obj)
        {
            _navigationService.NavigateAsync("LoginPage");
        }
    }
}
