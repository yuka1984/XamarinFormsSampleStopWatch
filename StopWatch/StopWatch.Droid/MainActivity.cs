using Android.App;
using Android.Content.PM;
using Android.OS;
using KeyValueStore.Pcl;
using Microsoft.Practices.Unity;
using SwLib.Core.Controller;
using SwLib.Pcl.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace StopWatch.Droid
{
    [Activity(Label = "StopWatch", Icon = "@drawable/icon", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            var container = new UnityContainer();
            container.RegisterType<IStopWatchController, StopWatchController>(new ContainerControlledLifetimeManager());
            container.RegisterType<IKeyValueStore, KeyValueStore.Core.Droid.KeyValueStore>(new ContainerControlledLifetimeManager());

            LoadApplication(new App(container));
        }
    }
}