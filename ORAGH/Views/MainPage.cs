using System.Threading.Tasks;
using Xamarin.Forms;
using Refit;
using ORAGH.Models; 

namespace ORAGH
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Browse"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    break;
            }

            Children.Add(itemsPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CallApi(); 
        }

        async Task CallApi()
        {
            var nsAPI = RestService.For<IMakeUpApi>("http://makeup-api.herokuapp.com");
            var sugars = await nsAPI.GetMakeUps();

            var oraghAPI = RestService.For<IOraghApi>("http://192.168.1.103/forumnew");
            var users = await oraghAPI.GetUsers(); 
            User user = await oraghAPI.GetUser("JKS"); 
        }
    }
}
