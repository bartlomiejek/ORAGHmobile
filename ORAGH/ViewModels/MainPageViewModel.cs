using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ORAGH.Models;
using Refit;
using Xamarin.Forms;
using ORAGH.Services; 

namespace ORAGH.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            GetDataCommand = new Command(async () => await GetData());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MakeUp> MakeUps { get; set; }
        public ICommand GetDataCommand { get; set; }

        async Task GetData()
        {
            var apiResponse = RestService.For<IMakeUpApi>("http://makeup-api.herokuapp.com");
            var makeUps = await apiResponse.GetMakeUps("maybelline");

            MakeUps = new ObservableCollection<MakeUp>(makeUps);
        }
    }
}
