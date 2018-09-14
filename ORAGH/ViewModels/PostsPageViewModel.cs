using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using ORAGH.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class PostsPageViewModel : BaseViewModel, INavigatingAware
	{
		INavigationService _navigationService;
		private ObservableCollection<Post> _posts;
		private ThreadViewData _threadDetails;
   
		public ObservableCollection<Post> Posts
        {
			get { return _posts; }
			set { SetProperty(ref _posts, value); }
        }
             
        public ThreadViewData ThreadDetails
        {
            get { return _threadDetails; }
            set { SetProperty(ref _threadDetails, value); }
        }

		public ICommand GetPostsCommand { get; set; }

		public PostsPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			GetPostsCommand = new Command(async () => await RunSafe(GetPosts(), true, "Pobieranie danych"));         
		}
        
		async Task GetPosts()
		{
			var postsResponse = await ApiManager.GetPosts(ThreadDetails.Tid); 

			if (postsResponse.IsSuccessStatusCode)
            {
				var response = await postsResponse.Content.ReadAsStringAsync();
                response = ApiManager.FixOraghApiResponse(response);
				var json = JsonConvert.DeserializeObject<List<Post>>(response);
                Posts = new ObservableCollection<Post>(json);
            }
            else
            {
                await PageDialog.AlertAsync("Wystąpił problem podczas pobierania danych", "Błąd", "Ok");
            }
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("Tid"))
			{
				ThreadDetails = (ThreadViewData)parameters["Tid"];
			}
			GetPostsCommand.Execute(null); 
		}

    }
}
