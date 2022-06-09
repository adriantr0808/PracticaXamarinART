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
    public class ModificarPersonajeViewModel:ViewModelBase
    {
        private Services.ServiceApiSeries service;
        
        public ModificarPersonajeViewModel(Services.ServiceApiSeries service)
        {
            this.service = service;
            this.SelectedSerie = new Serie();
            this.SelectedPersonaje = new Personaje();
            Task.Run(async () =>
            {
                await this.LoadSeriesAsync();
                await this.LoadPersonajesAsync();
            });



        }

        private ObservableCollection<Personaje> _Personajes;

        public ObservableCollection<Personaje> Personajes
        {
            get { return this._Personajes; }
            set
            {
                this._Personajes = value;
                OnPropertyChanged("Personajes");
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

        private Serie _SelectedSerie;
        public Serie SelectedSerie
        {
            get { return this._SelectedSerie; }
            set
            {
                this._SelectedSerie = value;
                OnPropertyChanged("SelectedSerie");
            }
        }

        private Personaje _SelectedPersonaje;
        public Personaje SelectedPersonaje
        {
            get { return this._SelectedPersonaje; }
            set
            {
                this._SelectedPersonaje = value;
                OnPropertyChanged("SelectedPersonaje");
            }
        }

        private async Task LoadSeriesAsync()
        {
            List<Serie> series =
            await this.service.GetSeriesAsync();
            this.Series =
            new ObservableCollection<Serie>(series);
        }

        private async Task LoadPersonajesAsync()
        {
            List<Personaje> pers =
                await this.service.GetPersonajesAsync();
            this.Personajes =
                new ObservableCollection<Personaje>(pers);
        }

        public Command ModificarPersonaje
        {
            get
            {
                return new Command(async (id) => {
                    await this.service.ModificarPersonaje(this.SelectedSerie.IdSerie, this.SelectedPersonaje.IdPersonaje);
                    MessagingCenter.Send<PersonajesViewModel>
                    (App.ServiceLocator.PersonajesViewModel, "RELOAD");
                    await Application.Current.MainPage
                    .DisplayAlert("Alert", "Modificado", "Ok");
                });
            }
        }
    }
}
