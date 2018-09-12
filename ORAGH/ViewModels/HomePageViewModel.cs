﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
//using System.Windows.Input;
using Newtonsoft.Json;
using ORAGH.Models;
using Prism.Mvvm;
using Prism.Navigation;

namespace ORAGH.ViewModels
{
	public class HomePageViewModel : BaseViewModel, INavigatedAware
    {
		INavigationService _navigationService;
		private Dictionary<string, Forum> _forums;

		public Dictionary<string, Forum> Forums { get => _forums; set => _forums = value; }

		public ICommand GetForumsCommand { get; set; }
		public HomePageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
        }

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		async Task GetForum()
        {
            var forumResponse = await ApiManager.GetForums();
            if (forumResponse.IsSuccessStatusCode)
            {
                var response = await forumResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string, Forum>>(response));
                Forums = json;
            }
            else
            {
                await PageDialog.AlertAsync("Unable to get forum data", "Error", "Ok");
            }
        }
	}
}
