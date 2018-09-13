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
		private DelegateCommand<ThreadView> _goToPostPage; 
        private ObservableCollection<ThreadView> _activeThreads; 
		private string _title;

		public ICommand GetActiveThreadsCommand { get; set; }
		public ObservableCollection<ThreadView> ActiveThreads  
		{
			get { return _activeThreads; }
			set { SetProperty(ref _activeThreads, value); }
		}
       
		public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

		public DelegateCommand<ThreadView> GoToPostsPageCommand => _goToPostPage ?? (_goToPostPage = new DelegateCommand<ThreadView>(GoToPostsPage));

		public SecActiveTopicsPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
			GetActiveThreadsCommand = new Command(async () => await RunSafe(GetActiveThreads(), true, "Pobieranie danych"));

			ActiveThreads = new ObservableCollection<ThreadView>()
			{
				new ThreadView() { subject = "Test1", tid = "1"}, 
				new ThreadView() { subject = "Test2", tid = "2"}, 
			}; 
        }
       
		async Task GetActiveThreads()
		{
			var activeThreadsResponse = await ApiManager.GetActiveThreads();   
            
			if (activeThreadsResponse.IsSuccessStatusCode)
            {
				var response = await activeThreadsResponse.Content.ReadAsStringAsync();
				response = ApiManager.FixOraghApiResponse(response);
                var json = JsonConvert.DeserializeObject<List<Thread>>(response);
			//	ActiveThreads = new ObservableCollection<Thread>(json);
            }
            else
            {
                await PageDialog.AlertAsync("Wystąpił problem podczas pobierania danych", "Błąd", "Ok");
            }
		}
        
		public async void GoToPostsPage(ThreadView paramData)
		{
			var parameters = new NavigationParameters
			{
				{ "subject", paramData}
			};
			await _navigationService.NavigateAsync(new System.Uri("http://ORAGHmobile/NavigationPage/PostsPage/", System.UriKind.Absolute), parameters);

		}

		public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
        
        public void OnNavigatedTo(NavigationParameters parameters)
        {
			if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
	}
}
