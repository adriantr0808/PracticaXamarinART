using Autofac;
using Microsoft.Extensions.Configuration;
using PracticaXamarinART.ViewModels;
using PracticaXamarinART.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PracticaXamarinART.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }
        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<MainMenuView>().SingleInstance();
            builder.RegisterType<ServiceApiSeries>();
            builder.RegisterType<MainMenuViewModel>();
            builder.RegisterType<SeriesViewModel>();
            builder.RegisterType<SerieViewModel>();
            builder.RegisterType<PersonajesViewModel>();
            builder.RegisterType<NewPersonajeViewModel>();
            builder.RegisterType<ModificarPersonajeViewModel>();

            string resourceName = "PracticaXamarinART.appsettings.json";
            Stream stream =
            GetType().GetTypeInfo().Assembly
            .GetManifestResourceStream(resourceName);
            IConfiguration configuration =
            new ConfigurationBuilder().AddJsonStream(stream)
            .Build();
            builder.Register<IConfiguration>(z => configuration);
            this.container = builder.Build();
        }

        public MainMenuView MainMenuView
        {
            get
            {
                return this.container.Resolve<MainMenuView>();
            }
        }


            public MainMenuViewModel MainMenuViewModel
            {
                get
                {
                return this.container.Resolve<MainMenuViewModel>();
                }
            }

        public SerieViewModel SerieViewModel
        {
            get
            {
                return this.container.Resolve<SerieViewModel>();
            }
        }

        public SeriesViewModel SeriesViewModel
        {
            get
            {
                return this.container.Resolve<SeriesViewModel>();
            }
        }
        public PersonajesViewModel PersonajesViewModel
        {
            get
            {
                return this.container.Resolve<PersonajesViewModel>();
            }
        }

        public NewPersonajeViewModel NewPersonajeViewModel
        {
            get
            {
                return this.container.Resolve<NewPersonajeViewModel>();
            }
        }

        public ModificarPersonajeViewModel ModificarPersonajeViewModel
        {
            get
            {
                return this.container.Resolve<ModificarPersonajeViewModel>();
            }
        }




    }

    }

