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
	public class EntertainmentPageViewModel : BaseViewModel
    {
		INavigationService _navigationService;
		int _rootFid = (int)SessionData.RootForums.EntertainmentSection;
        DelegateCommand<Forum> _goToChildForumCommand;
        ObservableCollection<Forum> _forumChilds;

        ICommand GetForumChildsCommand;
        public DelegateCommand<Forum> GoToChildForumCommand => _goToChildForumCommand ?? (_goToChildForumCommand = new DelegateCommand<Forum>(GoToChildForum));
        public ObservableCollection<Forum> ForumChilds
        {
            get { return _forumChilds; }
            set { SetProperty(ref _forumChilds, value); }
        }

		public EntertainmentPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
            GetForumChildsCommand = new Command(async () => await base.RunSafe(GetForumChilds(), true, "Pobieranie danych"));
            GetForumChildsCommand.Execute(null); 
        }

      
		async Task GetForumChilds()
        {
            var forumChildsResponse = await ApiManager.GetForumChilds(_rootFid.ToString());

            if (forumChildsResponse.IsSuccessStatusCode)
            {
                var response = await forumChildsResponse.Content.ReadAsStringAsync();
                response = ApiManager.FixOraghApiResponse(response);
                var json = JsonConvert.DeserializeObject<ObservableCollection<Forum>>(response);
                ForumChilds = json;
            }
        }

		public async void GoToChildForum(Forum forum)
        {
            List<Forum> forumChilds = new List<Forum>();
            bool forumHasChilds = false;

            var forumChildsResponse = await ApiManager.GetForumChilds(forum.Fid);
            if (forumChildsResponse.IsSuccessStatusCode)
            {
                var response = await forumChildsResponse.Content.ReadAsStringAsync();
                response = ApiManager.FixOraghApiResponse(response);

                try
                {
                    forumChilds = JsonConvert.DeserializeObject<List<Forum>>(response);
                    if (forumChilds.Count > 0)
                    {
                        forumHasChilds = true;
                    }
                }
                catch (Exception) { }
            }

            if (forumHasChilds)
            {
                var parameters = new NavigationParameters
                {
					{"Fid", forum.Fid }
                    ,{"ForumName", forum.Name}
                    ,{"ForumChildsList", forumChilds}
                };
                await _navigationService.NavigateAsync(new System.Uri("/ForumsPage/", System.UriKind.Relative), parameters);
            }
            else
            {
                var parameters = new NavigationParameters
                {
                    {"Fid", forum.Fid }
                    ,{"ForumName", forum.Name}
                };
                await _navigationService.NavigateAsync(new System.Uri("/ThreadsPage/", System.UriKind.Relative), parameters);
            }
        }
	}
}
