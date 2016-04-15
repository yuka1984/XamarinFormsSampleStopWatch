using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Practices.Unity;
using SwLib.Core.Controller;
using SwLib.Pcl.Controllers;

namespace StopWatch.Droid
{
    [Activity(Label = "StopWatch", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            var container = new UnityContainer();
            container.RegisterType<IStopWatchController, StopWatchController>();
            
            LoadApplication(new App(container));
        }
    }
}

