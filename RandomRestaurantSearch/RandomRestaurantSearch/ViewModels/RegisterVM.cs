using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RandomRestaurantSearch.ViewModels
{
    public class RegisterVM
    {
        public ICommand RegisterCommand { get; set; }
        INavigationService _navigationService;
        public string Email;
        public string Password;

        public RegisterVM(INavigationService navigationService)
        {
            Email = "";
            Password = "";
            _navigationService = navigationService;
            RegisterCommand = new DelegateCommand<object>(Regsiter);
        }

        private async void Regsiter(object obj)
        {
            if (Email == "" || Password == "")
            {
                await Application.Current.MainPage.DisplayAlert("エラー", "EmailとPasswordを入力してください", "OK");
            }
        }
    }
}
