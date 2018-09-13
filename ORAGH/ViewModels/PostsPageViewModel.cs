using System;
using ORAGH.Models;
using Prism.Mvvm;
using Prism.Navigation;

namespace ORAGH.ViewModels
{
	public class PostsPageViewModel : BindableBase, INavigatingAware
	{
		public PostsPageViewModel()
		{
		}
        
		private Thread _thread;
		public Thread ThreadDetails
		{
			get { return _thread; }
			set { SetProperty(ref _thread, value); }
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("subject"))
			   ThreadDetails = (Thread)parameters["subject"]; 
		}

    }
}
