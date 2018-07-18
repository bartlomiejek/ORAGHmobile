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
using Xamarin.Forms;

namespace ORAGH
{
	public class BaseViewModel : BindableBase, INotifyPropertyChanged 
    {
        public IUserDialogs PageDialog = UserDialogs.Instance;
        public IApiManager ApiManager;
        
        IApiService<IMakeUpApi> makeUpApi = new ApiService<IMakeUpApi>(ApiConfig.ApiUrl);
		IApiService<IOraghApi> oraghApi = new ApiService<IOraghApi>(ApiConfig.ApiOraghUrl); 


		public event PropertyChangedEventHandler PropertyChanged = delegate {};

        public bool IsBusy { get; set; }
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
				IsBusy = false;
				UserDialogs.Instance.HideLoading();
				Debug.WriteLine(e.ToString());
				await Application.Current.MainPage.DisplayAlert("Connect failed", "Sprawdź połączenie sieciowe", "Ok"); 
			}
			finally
			{
				IsBusy = false;
				if (ShowLoading) UserDialogs.Instance.HideLoading(); 
			}
		}

       /* public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion*/
    }
}
