using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ORAGH.ViewModels
{
	public class SecActiveTopicsPageViewModel : BindableBase, IActiveAware
    {
		INavigationService _navigationService; 

		public event EventHandler IsActiveChanged;

		public DelegateCommand OnLoginCommand { get; set; }
              
		private bool _isActive; 
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				SetProperty(ref _isActive, value, RaiseIsActiveChanged);
			}
		}

		public SecActiveTopicsPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;

			OnLoginCommand = new DelegateCommand(GoHome); 
        }

		protected virtual void RaiseIsActiveChanged()
		{
			IsActiveChanged?.Invoke(this, EventArgs.Empty); 
		}

		async void GoHome()
		{
			await _navigationService.NavigateAsync(new Uri($"MainPage", UriKind.Relative), null, false);
		}
	}
}
