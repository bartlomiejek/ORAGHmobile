using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using ORAGH.Models;
using Prism; 
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class LoginPageViewModel : BaseViewModel //: BindableBase
    {
		INavigationService _navigationService; 
		public ICommand LoginCommand { get; set; }
		public event EventHandler IsActiveChanged;

		protected bool _isLogged = false; 
		public bool IsLogged
		{
			get
			{
				return _isLogged;
			}
		}

		string _username;
		public string Username
		{
			get { return _username; }
			set 
			{
				_username = value; 
			}
		}

		string _password;
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
			}
		}
       
		public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public LoginPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService; 
			LoginCommand = new Command(async () => await RunSafe(Login(), true, "Autoryzacja"));
        }
        
		async Task Login()
		{
			var authResponse = await ApiManager.AuthoriseUser(_username, _password);

			if (!authResponse.IsSuccessStatusCode)
			{
				_isLogged = false;
				await PageDialog.AlertAsync("Nieprawidłowy login lub hasło.", "", "Ok");
			}
			else
			{
				_isLogged = true;
				await GoHome();
			}
		}

        async Task GoHome()
		{
			await _navigationService.NavigateAsync(
				new Uri($"MainPage?selectedTab=SecActiveTopicsPage", UriKind.Relative)); 
		}
    }
}
