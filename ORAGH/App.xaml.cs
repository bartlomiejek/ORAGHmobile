using System;

using Xamarin.Forms;
using ORAGH.Views; 

namespace ORAGH
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://localhost:5000";

        #region to consieder
        static bool _isLogged = false; 
        #endregion

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();
            
            if (_isLogged)
            {
                if (Device.RuntimePlatform == Device.iOS)
                    MainPage = new MainPage();
                else
                    MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                    MainPage = new LoginPage();
                else
                    MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
