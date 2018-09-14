using System;
using System.Collections.Generic;
using ORAGH.ViewModels; 
using Xamarin.Forms;
using Prism.Navigation; 

namespace ORAGH.Views
{
	public partial class ActiveTopicsPage : ContentPage
    {
        public ActiveTopicsPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = this.BindingContext as ActiveTopicsPageViewModel;
			vm.GetActiveThreadsCommand.Execute(null);
		}
    }
}
