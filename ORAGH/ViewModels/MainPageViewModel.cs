﻿using System;
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
	public class MainPageViewModel : BaseViewModel
    {
		
		//INavigationService _navigationService; 
		//public ObservableCollection<MakeUp> MakeUps { get; set; }
		//public ObservableCollection<User> Users { get; set; }
  //      public ICommand GetDataCommand { get; set; }
		//public ICommand GetUserCommand { get; set; }

		public MainPageViewModel(INavigationService navigationService)
		{
			
			//_navigationService = navigationService; 
			//GetDataCommand = new Command(async () => await GetData());
			//GetUserCommand = new Command(async () => await GetUser()); 
		}

  //      async Task GetData()
  //      {
		//	var makeUpsResponse = await ApiManager.GetMakeUps("meybelline");

		//	if (makeUpsResponse.IsSuccessStatusCode)
		//	{
		//		var response = await makeUpsResponse.Content.ReadAsStringAsync();
		//		var json = await Task.Run(() => JsonConvert.DeserializeObject<List<MakeUp>>(response));
		//		MakeUps = new ObservableCollection<MakeUp>(json);
		//	}
		//	else
		//	{
		//		await PageDialog.AlertAsync("Unable to get data", "Error", "Ok");
		//	}
  //      }
        
		//async Task GetUser()
		//{
		//	var userResponse = await ApiManager.GetUser("JKS"); 

		//	if( userResponse.IsSuccessStatusCode)
		//	{
		//		var response = await userResponse.Content.ReadAsStringAsync();
		//		var json = await Task.Run(() => JsonConvert.DeserializeObject<User>(response));
		//		Users = new ObservableCollection<User>();
		//		Users.Add(json); 
  //          }
		//	else 
		//	{
		//		await PageDialog.AlertAsync("Unable to get data", "Error", "Ok"); 
		//	}
		//}
    }
}
