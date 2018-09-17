using Prism.Events;
using Xamarin.Forms;

namespace ORAGH.Views
{
	public class CustomNavigationPage : NavigationPage
	{
		IEventAggregator _eventAggregator;
		public CustomNavigationPage(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
            this.BarBackgroundColor = Color.DarkRed;
		}
	}
}
