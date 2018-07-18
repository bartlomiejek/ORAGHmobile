using System;

using Xamarin.Forms;

namespace ORAGH.Views
{
    public class SecActiveTopicsPage : ContentPage
    {
        public SecActiveTopicsPage()
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

