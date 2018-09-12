using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ORAGH.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class SecActiveTopicsPageViewModel : BaseViewModel
    {
		INavigationService _navigationService;
        private ObservableCollection<Thread> _activeThreads; 
		public ObservableCollection<Thread> ActiveThreads  
		{
			get { return _activeThreads; }
			set { SetProperty(ref _activeThreads, value); }
		}

        public ICommand GetActiveThreadsCommand { get; set; }
       
		public SecActiveTopicsPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
			GetActiveThreadsCommand = new Command(async () => await RunSafe(GetActiveThreads(), true, "Pobieranie danych"));
        }
       
		async Task GetActiveThreads()
		{
			var activeThreadsResponse = await ApiManager.GetActiveThreads();   
            
			if (activeThreadsResponse.IsSuccessStatusCode)
            {
				var response = await activeThreadsResponse.Content.ReadAsStringAsync();
				response = ApiManager.FixOraghApiResponse(response);
                var json = JsonConvert.DeserializeObject<List<Thread>>(response);
				ActiveThreads = new ObservableCollection<Thread>(json);
            }
            else
            {
                await PageDialog.AlertAsync("Wystąpił problem podczas pobierania danych", "Błąd", "Ok");
            }
		}
	}
}
