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
	public class MainPageViewModel : BindableBase 
	{

		INavigationService _navigationService;
		public MainPageViewModel(INavigationService navigationService)
		{         
			_navigationService = navigationService;
		}
	}
}
