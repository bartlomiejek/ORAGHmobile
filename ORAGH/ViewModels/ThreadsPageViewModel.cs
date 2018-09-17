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
using System;

namespace ORAGH.ViewModels
{
    public class ThreadsPageViewModel : BaseViewModel, INavigationAware
    {
        INavigationService _navigationService;
        IPageDialogService _dialogService;
        DelegateCommand<ThreadViewData> _goToPostPage;
        ObservableCollection<ThreadViewData> _activeThreads;
		string _fid;
		string _title; 

        public ICommand GetThreadsCommand { get; set; }
        public ObservableCollection<ThreadViewData> Threads
        {
            get { return _activeThreads; }
            set { SetProperty(ref _activeThreads, value); }
        }

		public string Title
        {
			get { return _title; }
			set { SetProperty(ref _title, value); }
        }
        public ThreadsPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            GetThreadsCommand = new Command(async () => await RunSafe(GetThreads(), true, "Pobieranie danych"));
        }

        public DelegateCommand<ThreadViewData> GoToPostsPageCommand => _goToPostPage ?? (_goToPostPage = new DelegateCommand<ThreadViewData>(GoToPostsPage));

        async Task GetThreads()
        {
			var activeThreadsResponse = await ApiManager.GetThreads(_fid);

            if (activeThreadsResponse.IsSuccessStatusCode)
            {
                var response = await activeThreadsResponse.Content.ReadAsStringAsync();
                response = ApiManager.FixOraghApiResponse(response);

				List<Thread> threadsList = new List<Thread>(); 
                try
				{
					threadsList = JsonConvert.DeserializeObject<List<Thread>>(response);               
                }
				catch(Exception){}

                Threads = new ObservableCollection<ThreadViewData>();
                Threads.Clear();
				foreach (var thread in threadsList)
                {
                    Threads.Add(new ThreadViewData(thread));
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
                { "ThreadViewData", paramData}
            };
            await _navigationService.NavigateAsync(new System.Uri("/PostsPage/", System.UriKind.Relative), parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
			if (parameters.ContainsKey("Fid"))
            {
                _fid = (string)parameters["Fid"];
            }
            if (parameters.ContainsKey("ForumName"))
            {
                _title = (string)parameters["ForumName"]; 
            }

            GetThreadsCommand.Execute(null); 
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
