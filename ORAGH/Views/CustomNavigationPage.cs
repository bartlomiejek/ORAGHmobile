using System;
using ORAGH.Events;
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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//_eventAggregator.GetEvent<UpdateNavBarEvent>().Subscribe(UpdateTitle);
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		//	_eventAggregator.GetEvent<UpdateNavBarEvent>().Unsubscribe(UpdateTitle);
		}

		//void UpdateTitle(string title)
		//{
		//	this.Title = title; 
		//	//if (isShowingTheLoging)
		//	//{
		//	//	this.BarBackgroundColor = Color.Black;
		//	//}
		//	//else
		//	//{
		//	//	this.BarBackgroundColor = Color.Red;
		//	//}
		//}
	}
}
