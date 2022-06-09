using PracticaXamarinART.Base;
using PracticaXamarinART.Models;
using PracticaXamarinART.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracticaXamarinART.ViewModels
{
    public class PersonajesViewModel:ViewModelBase
    {
        private ServiceApiSeries service;
        public PersonajesViewModel(ServiceApiSeries service)
        {
            this.service = service;
            MessagingCenter.Subscribe<PersonajesViewModel>
             (this, "RELOAD", async (sender) =>
             {
                 await this.LoadPersonajesAsync();
             });

        }

     

         private async Task LoadPersonajesAsync()
        {
            List<Personaje> pers =
                await this.service.GetPersonajesAsync();
            this.Personajes =
                new ObservableCollection<Personaje>(pers);
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
    }
}
