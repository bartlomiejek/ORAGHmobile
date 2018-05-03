
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ORAGH.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void OnSignUpButtonClicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
                Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            //   MainPage = new MainPage();
            else
                Navigation.PushAsync(new MainPage());
           //     MainPage = new NavigationPage(new MainPage());
        }


    }
}
