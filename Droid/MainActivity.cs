﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using ORAGH.Services; 
using Prism;
using Prism.Ioc;
using Acr.UserDialogs;
using Prism.Unity;
using Microsoft.Practices.Unity;

namespace ORAGH.Droid
{
    [Activity(Label = "ORAGH.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", 
	          MainLauncher = true, 
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
			UserDialogs.Init(this); 
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App(new AndroidInitializer()));
        }
      
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
		public class AndroidInitializer : IPlatformInitializer
		{
			public void RegisterTypes(IContainerRegistry containerRegistry)
			{
				
			}
		}
	}
}


