using PracticaXamarinART.Base;
using PracticaXamarinART.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracticaXamarinART.ViewModels
{
    public class NewPersonajeViewModel: ViewModelBase
    {
        private Services.ServiceApiSeries service;
        public NewPersonajeViewModel(Services.ServiceApiSeries service)
        {
            this.service = service;
            this.Personaje = new Personaje();
            Task.Run(async () =>
            {
                await this.LoadSeriesAsync();
            });
        }

        private Personaje _Personaje;
        public Personaje Personaje
        {
            get { return this._Personaje; }
            set
            {
                this._Personaje = value;
                OnPropertyChanged("Personaje");
            }
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

        private Serie _SelectedSerie;
        public Serie SelectedSerie
        {
            get { return this._SelectedSerie; }
            set {
                this._SelectedSerie = value;
                 OnPropertyChanged("SelectedSerie");
            }
        }

        public Command InsertarPersonaje
        {
            get
            {
                return new Command(async (id) =>
                {

               

                    await this.service.InsertPersonaje
                    (this.Personaje.Nombre, this.Personaje.Imagen, (int)id);

                    MessagingCenter.Send<PersonajesViewModel>
                    (App.ServiceLocator.PersonajesViewModel, "RELOAD");
                    await Application.Current.MainPage
                    .DisplayAlert("Alert", "Insertado", "Ok");
                });
            }
        }
    }
}
