using System;

using Xamarin.Forms;

namespace ORAGH.Views
{
    public class CustomTabbedPage : ContentPage
    {
        public CustomTabbedPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

