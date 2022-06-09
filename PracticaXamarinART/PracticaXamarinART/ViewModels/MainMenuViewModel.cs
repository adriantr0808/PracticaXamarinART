using PracticaXamarinART.Base;
using PracticaXamarinART.Code;
using PracticaXamarinART.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PracticaXamarinART.ViewModels
{
    public class MainMenuViewModel: ViewModelBase
    {
        public MainMenuViewModel()
        {
            ObservableCollection<MenuPageItem> menu =
                new ObservableCollection<MenuPageItem>
                {
                    new MenuPageItem
                    {
                        Titulo="Modificar Personaje",
                         Tipo=typeof(ModificarPersonajesView)
                    },
                    new MenuPageItem
                    {
                        Titulo="Nuevo Personaje",
                         Tipo=typeof(PersonajeNewView)
                    },
                    new MenuPageItem
                    {
                        Titulo="Series",
                        Tipo=typeof(SeriesListView)
                    }
                };
            this.MenuPrincipal = menu;
        }

            private ObservableCollection<MenuPageItem> _MenuPrincipal;

        public ObservableCollection<MenuPageItem> MenuPrincipal
        {
            get { return this._MenuPrincipal; }
            set
            {
                this._MenuPrincipal = value;
                OnPropertyChanged("MenuPrincipal");
            }
        }

        public Command PaginaSeleccionada
        {
            get
            {
                return new Command(async (seleccionado) => {

                    MainMenuView masterPage = App.ServiceLocator.MainMenuView;

                    var item = (MenuPageItem)seleccionado;
                    var tipo = item.Tipo;
                    masterPage.Detail =
                    new NavigationPage((Page)Activator.CreateInstance(tipo));
                    masterPage.IsPresented = false;

                   ;
                });
            }
        }

    }
}

