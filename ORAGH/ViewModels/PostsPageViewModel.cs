using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using ORAGH.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class PostsPageViewModel : BaseViewModel, INavigatingAware
	{
		INavigationService _navigationService;
		ObservableCollection<PostViewData> _posts;
		ThreadViewData _threadDetails;
		string _message; 
        public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
   
		public ObservableCollection<PostViewData> Posts
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
		public ICommand EditorCompletedCommand { get; set; }

		public PostsPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			GetPostsCommand = new Command(async () => await base.RunSafe(GetPosts(), true, "Pobieranie danych"));
			EditorCompletedCommand = new Command(async () => await base.RunSafe(SendPost(), true, "Wysyłanie...")); 
		}
        
		async Task GetPosts()
		{
			var postsResponse = await ApiManager.GetPosts(ThreadDetails.Tid); 

			if (postsResponse.IsSuccessStatusCode)
            {
				var response = await postsResponse.Content.ReadAsStringAsync();
                response = ApiManager.FixOraghApiResponse(response);
				var json = JsonConvert.DeserializeObject<List<Post>>(response);
				Posts = new ObservableCollection<PostViewData>();
				foreach( var post in json)
				{
					Posts.Add(new PostViewData(post)); 
				}
            }
            else
            {
                await PageDialog.AlertAsync("Wystąpił problem podczas pobierania danych", "Błąd", "Ok");
            }
		}
        
        async Task SendPost()
		{
			string message = Message;
			Message = string.Empty; 
			var createPostResponse = await ApiManager.CreatePost(SessionData.username, SessionData.password, _threadDetails.Tid, _threadDetails.Fid, SessionData.ip, message);
			if (!createPostResponse.IsSuccessStatusCode)
            {
				throw new Exception(createPostResponse.StatusCode.ToString()); 
            }

			GetPostsCommand.Execute(null);
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("ThreadViewData"))
			{
				ThreadDetails = (ThreadViewData)parameters["ThreadViewData"];
			}
			GetPostsCommand.Execute(null); 
		}
    }
}
