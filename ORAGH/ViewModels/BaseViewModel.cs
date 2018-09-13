using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ORAGH.Services;
using ORAGH.Services.Implementation;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ORAGH
{
	public class BaseViewModel : BindableBase
    {
		public IUserDialogs PageDialog = UserDialogs.Instance;
        public IApiManager ApiManager;
        
        IApiService<IMakeUpApi> makeUpApi = new ApiService<IMakeUpApi>(ApiConfig.ApiUrl);
		IApiService<IOraghApi> oraghApi = new ApiService<IOraghApi>(ApiConfig.ApiOraghUrl);


		bool isBusy = false;
        public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				SetProperty(ref isBusy, value);
			}
		}

        public BaseViewModel()
        {
			ApiManager = new ApiManager(makeUpApi, oraghApi);
        }

		public async Task RunSafe(Task task, bool ShowLoading = true, string loadinMessage = null)
		{
			try
			{
				if (IsBusy)	
				{
					return; 
				}

				IsBusy = true; 

                if (ShowLoading)
				{
					UserDialogs.Instance.ShowLoading(loadinMessage ?? "Loading");
				}

				await task; 
			}
			catch(Exception e)
			{
				isBusy = false;
				UserDialogs.Instance.HideLoading();
				Debug.WriteLine(e.ToString());
				await Application.Current.MainPage.DisplayAlert("Serwer nie odpowiada", "Sprawdź połączenie sieciowe lub skontaktuj się z administratorem", "Ok"); 
			}
			finally
			{
				IsBusy = false;
				if (ShowLoading) UserDialogs.Instance.HideLoading(); 
			}
		}

     
        //protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "",Action onChanged = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(backingStore, value))
        //        return false;

        //    backingStore = value;
        //    onChanged?.Invoke();
        //    return true;
        //}
	}
}
