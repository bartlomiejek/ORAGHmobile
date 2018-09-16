
using System;
using System.Collections.Generic;
using ORAGH.ViewModels;
using Prism.Events;
using Prism.Navigation;
using ORAGH.Events; 
using Xamarin.Forms;


namespace ORAGH.Views
{
    public partial class LoginPage : ContentPage
    {
		//IEventAggregator _eventAggregator;
        public LoginPage(IEventAggregator eventAggregator)
        {
			InitializeComponent();
		//	_eventAggregator = eventAggregator;
			//usernameEntry.Completed += (object sender, EventArgs e) =>
			//{
			//	passwordEntry.Focus();
			//};

			//passwordEntry.Completed += (object sender, EventArgs e) =>
			//{
			//};
        }

        void OnSignUpButtonClicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

		protected override void OnAppearing()
        {
            base.OnAppearing();
          //  _eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
         //   _eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(false);
        }
    }
}
