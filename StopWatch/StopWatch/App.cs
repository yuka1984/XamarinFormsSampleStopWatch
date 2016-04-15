using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace StopWatch
{
    public class App : Application
    {
        private IUnityContainer _container;
        public App(IUnityContainer container)
        {            
            this._container = container;
            var page = new StopwatchPage();
            var vm = container.Resolve<StopwatchViewModel>();
            page.BindingContext = vm;

            // The root page of your application
            MainPage = page;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
