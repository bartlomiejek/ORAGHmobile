using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ORAGH.Services;
using ORAGH.Services.Implementation;
using Prism.Mvvm;
using Xamarin.Forms;

namespace ORAGH
{
	public class BaseViewModel : BindableBase
    {
		IApiService<IOraghApi> oraghApi = new ApiService<IOraghApi>(ApiConfig.ApiOraghUrl);
		public IUserDialogs PageDialog = UserDialogs.Instance;
        public IApiManager ApiManager;
       
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
			ApiManager = new ApiManager(oraghApi);
        }

		public async Task RunSafe(Task task, bool ShowLoading = true, string loadinMessage = null )
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
					PageDialog.ShowLoading(loadinMessage ?? "Loading");
				}

				await task; 
			}
			catch(Exception e)
			{
				isBusy = false;
				PageDialog.HideLoading();
				Debug.WriteLine(e.ToString());
				await Application.Current.MainPage.DisplayAlert("Serwer nie odpowiada", "Sprawdź połączenie sieciowe lub skontaktuj się z administratorem", "Ok"); 
			}
			finally
			{
				IsBusy = false;
				if (ShowLoading) PageDialog.HideLoading(); 
			}
		}
	}
}
