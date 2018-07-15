using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using ORAGH.Models;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class LoginPageViewModel : BaseViewModel
    {
		protected bool _isLogged = false; 
		public bool IsLogged
		{
			get
			{
				return _isLogged;
			}
		}

		private string _username;
		public string Username
		{
			get { return _username; }
			set 
			{
				_username = value; 
				//base.PropertyChanged += new PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Username")); 
			}
		}
		private string _password; 
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
			//	base.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Password")); 
			}
		}
        

		public ICommand LoginCommand { get; set; }
		public LoginPageViewModel()
        {
			LoginCommand = new Command(async () => await RunSafe(Login())); 
        }
        
		async Task Login()
        {
			var authResponse = await ApiManager.AuthoriseUser(_username, _password);
            
			if(authResponse.IsSuccessStatusCode)
			{
				_isLogged = true; 
				PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsLogged")); 
            }
			else 
			{
				_isLogged = false;
				await PageDialog.AlertAsync("Nieprawidłowy login lub hasło.", "", "Ok"); 
			}
			                          
		}
    }
}
