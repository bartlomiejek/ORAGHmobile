using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets; 
using Newtonsoft.Json;
using ORAGH.Models;
using Prism; 
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class LoginPageViewModel : BaseViewModel
    {
		INavigationService _navigationService;
		IPageDialogService _dialogService;
		public ICommand LoginCommand { get; set; }

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
				SessionData.username = value; 
			}
		}

		string _password;
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				SessionData.password = value; 
			}
		}
       
		public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
			_navigationService = navigationService;
			_dialogService = dialogService;
			LoginCommand = new Command(async () => await RunSafe(Login(), true, "Autoryzacja"));
        }
        
		async Task Login()
		{
			var authResponse = await ApiManager.AuthoriseUser(_username, _password);

			if (!authResponse.IsSuccessStatusCode)
			{
				_isLogged = false;
				await _dialogService.DisplayAlertAsync("Błąd autoryzacji", "Nieprawidłowy login lub hasło.", "Ok"); 
			}
			else
			{
				_isLogged = true;
				SetIp(); 
				await GoHome();
			}
		}

        async Task GoHome()
		{
			var parameters = new NavigationParameters
            {
				{ "username", _username}
            };
			await _navigationService.NavigateAsync(
				new Uri($"MainPage?selectedTab=ActiveTopicsPage", UriKind.Relative)); 
		}

		void SetIp()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
					SessionData.ip = ip.ToString();
                }
            }
		}
       
    }
}
