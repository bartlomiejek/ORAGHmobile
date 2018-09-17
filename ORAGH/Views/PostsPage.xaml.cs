using System;
using System.Collections.Generic;
using ORAGH.ViewModels; 
using Xamarin.Forms;
using Prism.Navigation; 

namespace ORAGH.Views
{
    public partial class PostsPage : ContentPage
    {
		public PostsPage()
		{
			InitializeComponent();
		}

		public void EditorCompleted(object sender, EventArgs e)
		{
			var vm = this.BindingContext as PostsPageViewModel;
			vm.EditorCompletedCommand.Execute(null); 
		}
	}
}
