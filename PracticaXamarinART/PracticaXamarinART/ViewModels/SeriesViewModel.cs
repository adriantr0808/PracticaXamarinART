using PracticaXamarinART.Base;
using PracticaXamarinART.Models;
using PracticaXamarinART.Services;
using PracticaXamarinART.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracticaXamarinART.ViewModels
{
    public class SeriesViewModel:ViewModelBase
    {
        private ServiceApiSeries service;
        public SeriesViewModel(ServiceApiSeries service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadSeriesAsync();
            });
        }

        private ObservableCollection<Serie> _Series;

        public ObservableCollection<Serie> Series
        {
            get { return this._Series; }
            set
            {
                this._Series = value;
                OnPropertyChanged("Series");
            }
        }


        private async Task LoadSeriesAsync()
        {
            List<Serie> series =
            await this.service.GetSeriesAsync();
            this.Series =
            new ObservableCollection<Serie>(series);
        }

        private Serie _SerieSeleccionada;

        public Serie SerieSeleccionada
        {
            get { return this._SerieSeleccionada; }
            set
            {
                this._SerieSeleccionada = value;
                OnPropertyChanged("SerieSeleccionada");
            }
        }

        public Command ShowDetails
        {
            get
            {
                return new Command(async (serie) =>
                {
                    Serie ser;
                    if (serie == null)
                    {
                     
                        ser = this.SerieSeleccionada;
                    }
                    else
                    {
                       
                        ser = serie as Serie;
                    }

                    SerieView view = new SerieView();
                   
                    SerieViewModel viewmodel =
                    App.ServiceLocator.SerieViewModel;

                    viewmodel.Serie = ser;
                   
                    view.BindingContext = viewmodel;
                    MainMenuView main = App.ServiceLocator.MainMenuView;
                    main.Detail = new NavigationPage(view);
                });
            }
        }


    }
}
