using PracticaXamarinART.Base;
using PracticaXamarinART.Models;
using PracticaXamarinART.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PracticaXamarinART.ViewModels
{
    public class SerieViewModel: ViewModelBase
    {
        private Services.ServiceApiSeries service;

        public SerieViewModel(Services.ServiceApiSeries service)
        {
            this.service = service;
        }

        private Serie  _Serie;
        public Serie Serie
        {
            get { return this._Serie; }
            set
            {
                this._Serie = value;
                OnPropertyChanged("Serie");
            }
        }

        private ObservableCollection<Personaje> _PersonajesSerie;

        public ObservableCollection<Personaje> PersonajesSerie
        {
            get { return this._PersonajesSerie; }
            set
            {
                this._PersonajesSerie = value;
                OnPropertyChanged("Personajes");
            }
        }

        public Command ShowPersonajes
        {
            get
            {
                return new Command(async () =>
                {
                  List<Personaje> pers =await this.service.GetPersonajesSerieAsync(this.Serie.IdSerie);
                    this.PersonajesSerie= new ObservableCollection<Personaje>(pers);
                    PersonajesView view = new PersonajesView();

                    PersonajesViewModel viewmodel =
                    App.ServiceLocator.PersonajesViewModel;

                    viewmodel.Personajes = this.PersonajesSerie;

                    view.BindingContext = viewmodel;
                    MainMenuView main = App.ServiceLocator.MainMenuView;
                    main.Detail = new NavigationPage(view);
                });
            }
        }


    }
}
