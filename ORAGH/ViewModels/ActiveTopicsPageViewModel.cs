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
using Prism.Services;

namespace ORAGH.ViewModels
{
	public class ActiveTopicsPageViewModel : BaseViewModel, INavigationAware
    {
		INavigationService _navigationService;
		IPageDialogService _dialogService; 
		private DelegateCommand<ThreadViewData> _goToPostPage; 
        private ObservableCollection<ThreadViewData> _activeThreads; 

		public ICommand GetActiveThreadsCommand { get; set; }
		public ObservableCollection<ThreadViewData> ActiveThreads  
		{
			get { return _activeThreads; }
			set { SetProperty(ref _activeThreads, value); }
		}
      
		public ActiveTopicsPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
			_dialogService = dialogService; 
            GetActiveThreadsCommand = new Command(async () => await RunSafe(GetActiveThreads(), true, "Pobieranie danych"));
        }

		public DelegateCommand<ThreadViewData> GoToPostsPageCommand => _goToPostPage ?? (_goToPostPage = new DelegateCommand<ThreadViewData>(GoToPostsPage));
       
		async Task GetActiveThreads()
		{
			var activeThreadsResponse = await ApiManager.GetActiveThreads();   
            
			if (activeThreadsResponse.IsSuccessStatusCode)
            {
				var response = await activeThreadsResponse.Content.ReadAsStringAsync();
				response = ApiManager.FixOraghApiResponse(response);
                var json = JsonConvert.DeserializeObject<List<Thread>>(response);

				ActiveThreads = new ObservableCollection<ThreadViewData>(); 
				ActiveThreads.Clear(); 
				foreach(var thread in json)
				{
					ActiveThreads.Add(new ThreadViewData(thread)); 
				}
            }
            else
            {
				await _dialogService.DisplayAlertAsync("Wystąpił problem podczas pobierania danych", "Błąd", "Ok");
            }
		}
        
		public async void GoToPostsPage(ThreadViewData paramData)
		{
			var parameters = new NavigationParameters
			{
				{ "Tid", paramData}
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
			
        }
	}
}
