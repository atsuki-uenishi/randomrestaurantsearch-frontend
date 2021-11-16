using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using RandomRestaurantSearch.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RandomRestaurantSearch.ViewModels
{
    public class LoginVM
    {
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterCommand { get; set; }
        public string Email;
        public string Password;

        INavigationService _navigationService;
        public LoginVM(INavigationService navigationService)
        {
            Email = "";
            Password = "";
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand<object>(Login);
            GoToRegisterCommand = new DelegateCommand<object>(GoToRegister);
        }

        private void GoToRegister(object obj)
        {
            _navigationService.NavigateAsync("RegisterPage");
        }

        private async void Login(object obj)
        {
            if(Email == "" || Password == "")
            {
                await Application.Current.MainPage.DisplayAlert("エラー", "EmailとPasswordを入力してください", "OK");
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    var result = await client.GetStringAsync($"apiのurl");

                }
            }
        }
    }
}



