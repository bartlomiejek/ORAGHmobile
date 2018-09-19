using System;
using Xamarin.Forms;
using ORAGH.Views;
using ORAGH.ViewModels;
using Prism.Unity;
using Prism.Ioc;
using Prism;

namespace ORAGH
{
    public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer){}
        
		protected override void OnInitialized()
		{
			InitializeComponent(); 
			NavigationService.NavigateAsync(new Uri("http://ORAGHmobile/CustomNavigationPage/LoginPage/", UriKind.Absolute));
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<CustomNavigationPage>();
			containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
			containerRegistry.RegisterForNavigation<ActiveTopicsPage, ActiveTopicsPageViewModel>();
			containerRegistry.RegisterForNavigation<OrchestraPage>();
			containerRegistry.RegisterForNavigation<EntertainmentPage>();
			containerRegistry.RegisterForNavigation<ForumsPage>(); 
			containerRegistry.RegisterForNavigation<ThreadsPage>(); 
			containerRegistry.RegisterForNavigation<PostsPage>();
		}
	}
}
