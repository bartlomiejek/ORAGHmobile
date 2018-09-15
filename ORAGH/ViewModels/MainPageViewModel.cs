using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ORAGH.Models;
using Refit;
using Xamarin.Forms;
using ORAGH.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using Prism.Navigation;
using Prism.Mvvm;

namespace ORAGH.ViewModels
{
	public class MainPageViewModel : BaseViewModel//, INavigatedAware
	{

		INavigationService _navigationService;
		private Dictionary<string, Forum> _forums;

		public ObservableCollection<MakeUp> MakeUps { get; set; }
		public ObservableCollection<User> Users { get; set; }
		public Dictionary<string, Forum> Forums { get => _forums; set => _forums = value; }
		//public ICommand GetUserCommand { get; set; }
		public ICommand GetForumsCommand { get; set; }
		public MainPageViewModel(INavigationService navigationService)
		{

			_navigationService = navigationService;
			//GetUserCommand = new Command(async () => await GetUser()); 
			//GetForumsCommand = new Command(async () => await GetForum()); 
		}

		async Task GetForum()
		{
			var forumResponse = await ApiManager.GetForums(); 
			if( forumResponse.IsSuccessStatusCode)
			{
				var response = await forumResponse.Content.ReadAsStringAsync();
				var json = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string,Forum>>(response));
				Forums = json; 
			}
			else 
			{
				await PageDialog.AlertAsync("Unable to get forum data", "Error", "Ok"); 
			}
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			
		}

		async Task GetUser()
		{
			var userResponse = await ApiManager.GetUser("JKS"); 

			if( userResponse.IsSuccessStatusCode)
			{
				var response = await userResponse.Content.ReadAsStringAsync();
				var json = await Task.Run(() => JsonConvert.DeserializeObject<User>(response));
				Users = new ObservableCollection<User>();
				Users.Add(json); 
		          }
			else 
			{
				await PageDialog.AlertAsync("Unable to get data", "Error", "Ok"); 
			}
		}
	}
}
