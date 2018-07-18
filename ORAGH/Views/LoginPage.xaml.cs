
using System;
using System.Collections.Generic;
using ORAGH.ViewModels; 

using Xamarin.Forms;

namespace ORAGH.Views
{
    public partial class LoginPage : ContentPage
    {
		
        public LoginPage()
        {
			//LoginPageViewModel _loginPageViewModel = new LoginPageViewModel();
			//this.BindingContext = _loginPageViewModel;
			//this.InitializeComponent();

			//usernameEntry.Completed += (object sender, EventArgs e) =>
			//{
			//	//_loginPageViewModel.Username = usernameEntry.Text; 
			//	passwordEntry.Focus();
			//};

			//passwordEntry.Completed += (object sender, EventArgs e) =>
			//{
			////	_loginPageViewModel.Password = passwordEntry.Text;
			//	_loginPageViewModel.LoginCommand.Execute(null);
			//};
        }

        void OnSignUpButtonClicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
			
   //         if (Device.RuntimePlatform == Device.iOS)
			//{
				
				////_loginPageViewModel.Username = usernameEntry;
				////_loginPageViewModel.Password = passwordEntry; 

				//Navigation.PushModalAsync(new NavigationPage(new MainPage()));            
           // }   
           // //   MainPage = new MainPage();
           // else
           //     Navigation.PushAsync(new MainPage());
           ////     MainPage = new NavigationPage(new MainPage());
        }


    }
}
