using System;

using Xamarin.Forms;

namespace ORAGH.ViewModels
{
    public class ForumViewModel : ContentPage
    {
        public ForumViewModel()
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

