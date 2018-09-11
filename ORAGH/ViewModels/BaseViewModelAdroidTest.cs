using System;
using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ORAGH.ViewModels
{
	public class BaseViewModelAdroidTest : BindableBase 
    {
		public IUserDialogs PageDialog = UserDialogs.Instance;  

        public BaseViewModelAdroidTest()
        {
        }
    }
}
