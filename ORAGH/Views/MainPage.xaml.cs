using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Prism.Navigation; 

namespace ORAGH.Views
{
	public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
			InitializeComponent();
			this.CurrentPageChanged += CurrentPageHasChanged; 
        }

		protected void CurrentPageHasChanged(object sender, EventArgs e) 
		{ 
			this.Title = this.CurrentPage.Title;        
		} 
	}
}
