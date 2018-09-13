using System;
using System.Collections.Generic;
using ORAGH.ViewModels; 
using Xamarin.Forms;
using Prism.Navigation; 

namespace ORAGH.Views
{
	public partial class SecActiveTopicsPage : ContentPage, IDestructible
    {
        public SecActiveTopicsPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = this.BindingContext as SecActiveTopicsPageViewModel;
			//vm.GetActiveThreadsCommand.Execute(null);
   		}

		public void Destroy()
        {
			ListViewThreads.Behaviors.Clear();
        }
    }
}
