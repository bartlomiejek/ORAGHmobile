using System;
using System.Collections.Generic;
using ORAGH.ViewModels; 
using Xamarin.Forms;

namespace ORAGH.Views
{
    public partial class SecActiveTopicsPage : ContentPage
    {
        public SecActiveTopicsPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = this.BindingContext as SecActiveTopicsPageViewModel;
			vm.GetActiveThreadsCommand.Execute(null);
   		}
    }
}
