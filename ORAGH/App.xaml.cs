using System;
using Xamarin.Forms;
using ORAGH.Views;
using Prism.Unity;
using Prism.Modularity; 
using Prism.Ioc;
using ORAGH.ViewModels;
using Prism;

namespace ORAGH
{
    public partial class App : PrismApplication
	{
		//public App() {
		//	InitializeComponent(); 
		//}
		public App(IPlatformInitializer initializer = null) : base(initializer){
			
		}
        
		protected override void OnInitialized()
		{
			InitializeComponent();
			NavigationService.NavigateAsync("LoginPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<MainPage>();
			containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(); 

			//containerRegistry.RegisterForNavigation<CustomNavigationPage>(); 
			containerRegistry.RegisterForNavigation<SecActiveTopicsPage>();
			//containerRegistry.RegisterForNavigation<SecOrchestra>(); 
		}
        //public static bool UseMockDataStore = false;
        //public static string BackendUrl = "https://localhost:5000";

        //#region to consieder
        //static bool _isLogged = false; 
        //#endregion

        //public App()
        //{
        //    InitializeComponent();

        //    if (_isLogged)
        //    {
        //        if (Device.RuntimePlatform == Device.iOS)
        //            MainPage = new MainPage();
        //        else
        //            MainPage = new NavigationPage(new MainPage());
        //    }
        //    else
        //    {
        //        if (Device.RuntimePlatform == Device.iOS)
        //            MainPage = new LoginPage();
        //        else
        //            MainPage = new NavigationPage(new LoginPage());
        //    }
        //}   
	}
}
