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
        private INavigation _navigation;
        public App(IUnityContainer container)
        {            
            var stopwatchPage = new StopwatchPage();
            var savelistPage = new SaveListPage();
            var tabPage = new TabbedPage();
            tabPage.Children.Add(stopwatchPage);
            tabPage.Children.Add(savelistPage);
            _navigation = tabPage.Navigation;
                     
            this._container = container;

            MessagingCenter.Subscribe<NavigationMessage>(this, NavigationMessage.ToSavePage, message =>
            {
                var savepage = new SavePage();
                savepage.BindingContext = message.ViewModel ?? _container.Resolve<SaveViewModel>();
                if (message.IsModal)
                {
                    tabPage.Navigation.PushModalAsync(savepage);
                }
            });

            MessagingCenter.Subscribe<NavigationMessage>(this, NavigationMessage.Back, sender => _navigation.PopModalAsync());
            MessagingCenter.Subscribe<NavigationMessage, string>(this, NavigationMessage.DisplayMessage, (o, s) =>
            {
                tabPage.DisplayAlert("Message", s, "OK");
            });
            
            stopwatchPage.BindingContext = container.Resolve<StopwatchViewModel>();
            savelistPage.BindingContext = container.Resolve<SaveListViewModel>();

            // The root page of your application
            MainPage = tabPage;
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

    public class NavigationMessage
    {
        public const string DisplayMessage = "DisplayMessage";
        public const string Back = "Back";
        public const string ToSavePage = "ToSavePage";
        public NavigationMessage(bool isModal = false, object viewModel = null)
        {
            IsModal = isModal;
            ViewModel = viewModel;
        }
        public bool IsModal { get; set; }
        public object ViewModel { get; set; }
    }
}

public static class Extentions
{
    public static string ToTimeFormat(this long value)
    {
        return TimeSpan.FromMilliseconds(value).ToString();
    }
}
