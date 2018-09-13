using System;
using Prism;
using Prism.Mvvm;
using Prism.Commands;
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
	public class SecActiveTopicsPageViewModel : BaseViewModel, INavigationAware
    {
		INavigationService _navigationService;
		private DelegateCommand<Thread> _goToPostPage; 
        private ObservableCollection<Thread> _activeThreads; 
		//private string _title;

		public ICommand GetActiveThreadsCommand { get; set; }
		public ObservableCollection<Thread> ActiveThreads  
		{
			get { return _activeThreads; }
			set { SetProperty(ref _activeThreads, value); }
		}
       
		//public string Title
  //      {
  //          get { return _title; }
  //          set { SetProperty(ref _title, value); }
  //      }

		public SecActiveTopicsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GetActiveThreadsCommand = new Command(async () => await RunSafe(GetActiveThreads(), true, "Pobieranie danych"));

            //ActiveThreads = new ObservableCollection<Thread>()
            //{
            //    new Thread() { subject = "Test1", tid = "1"},
            //    new Thread() { subject = "Test2", tid = "2"},
            //};
        }

		public DelegateCommand<Thread> GoToPostsPageCommand => _goToPostPage ?? (_goToPostPage = new DelegateCommand<Thread>(GoToPostsPage));
       
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
        
		public async void GoToPostsPage(Thread paramData)
		{
			var parameters = new NavigationParameters
			{
				{ "subject", paramData}
			};
			await _navigationService.NavigateAsync(new System.Uri("/PostsPage/", System.UriKind.Relative), parameters);

		}

		public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
        
        public void OnNavigatedTo(NavigationParameters parameters)
        {
			//if (parameters.ContainsKey("title"))
                //Title = (string)parameters["title"] + " and Prism";
        }
	}
}
