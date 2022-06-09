using PracticaXamarinART.Services;
using PracticaXamarinART.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticaXamarinART
{
    public partial class App : Application
    {
        private static ServiceIoC _ServiceLocator;

        public static ServiceIoC ServiceLocator
        {
            get
            {
                return _ServiceLocator = _ServiceLocator ?? new ServiceIoC();
            }
        }

        public App()
        {
            InitializeComponent();
            MainMenuView mainmenu =
               App.ServiceLocator.MainMenuView;
            mainmenu.Detail =
                new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            MainPage = mainmenu;
          
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
