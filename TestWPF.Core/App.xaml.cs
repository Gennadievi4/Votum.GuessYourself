using Autofac;
using Autofac.SmartNavigation.Extensions;
using Autofac.SmartNavigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestWPF.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ILifetimeScope Scope { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bilder = new ContainerBuilder()
                .UseAutofind();

            var serivceCollection = new ServiceCollection();
            
        }
    }
}
