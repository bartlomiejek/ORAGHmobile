using System;
using Xamarin.Forms;
using ORAGH.Views;
using ORAGH.ViewModels;
using Prism.Unity;
using Prism.Modularity; 
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
		    //NavigationService.NavigateAsync(new System.Uri("http://ORAGHmobile/NavigationPage/CustomTabbedPage?selectedTab=Test1Page", System.UriKind.Absolute));
			NavigationService.NavigateAsync(new System.Uri("http://ORAGHmobile/CustomNavigationPage/LoginPage/", System.UriKind.Absolute));
			//NavigationService.NavigateAsync(new System.Uri("http://ORAGHmobile/CustomNavigationPage/MainPage?selectedTab=ActiveTopicsPage/", System.UriKind.Absolute));
			//NavigationService.NavigateAsync(new System.Uri("http://ORAGHmobile/NavigationPage/SecActiveTopicsPage/", System.UriKind.Absolute));
			//NavigationService.NavigateAsync(new System.Uri("http://ORAGHmobile/NavigationPage/PostsPage/", System.UriKind.Absolute));
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<CustomNavigationPage>();
			containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();	        
			containerRegistry.RegisterForNavigation<ActiveTopicsPage, ActiveTopicsPageViewModel>();         
			containerRegistry.RegisterForNavigation<OrchestraPage>();
			containerRegistry.RegisterForNavigation<EntertainmentPage, EntertainmentPageViewModel>();
			containerRegistry.RegisterForNavigation<PostsPage>();  

//			containerRegistry.RegisterForNavigation<CustomTabbedPage>();
	//		containerRegistry.RegisterForNavigation<Test1Page, Test1PageViewModel>();
			//containerRegistry.RegisterForNavigation<Test2Page>();

		}

		//protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        //{
        //    Type authenticationModuleType = typeof(AuthenticationModule.AuthenticationModule);
        //    moduleCatalog.AddModule(
        //     new ModuleInfo(authenticationModuleType)
        //     {
        //         ModuleName = authenticationModuleType.Name,
        //         InitializationMode = InitializationMode.OnDemand
        //     });
        //}
	}
}
