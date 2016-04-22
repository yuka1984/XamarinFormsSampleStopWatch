using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;
using KeyValueStore.Pcl;
using Microsoft.Practices.Unity;
using SwLib.Core.Controller;
using SwLib.Pcl.Controllers;
using UIKit;

namespace StopWatch.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            var container = new UnityContainer();

            container.RegisterType<IStopWatchController, StopWatchController>(new ContainerControlledLifetimeManager());
            container.RegisterType<IKeyValueStore, KeyValueStore.Core.Touch.KeyValueStore>(new ContainerControlledLifetimeManager());

            LoadApplication(new App(container));

            return base.FinishedLaunching(app, options);
        }
    }

    public interface IHoge
    {
        void Start();
        void Stop();
    }

    public abstract class HogeBase : IHoge
    {
        public static class NativeMethods
        {
            [DllImport("__Internal")]
            public static extern void Init();

            [DllImport("__Internal")]
            public static extern void Start();

            [DllImport("__Internal")]
            public static extern void Stop();
        }

        public abstract void Start();
        public abstract void Stop();
    }

    public class Hoge : HogeBase
    {
        private static Lazy<Hoge> _instance = new Lazy<Hoge>(() => new Hoge());
        public static Hoge GetInstance() => _instance.Value;
        private Hoge()
        {
            NativeMethods.Init();
        }

        public override void Start()
        {
            Debug.WriteLine("start");
            NativeMethods.Start();
        }

        public override void Stop()
        {
            Debug.WriteLine("stop");
            NativeMethods.Stop();
        }
    }
    public class Hoge2 : IHoge
    {
        private static Lazy<Hoge> _instance = new Lazy<Hoge>(() => new Hoge());
        public static Hoge GetInstance() => _instance.Value;
        private Hoge2()
        {
            
        }
        public void Start()
        {
            Debug.WriteLine("start");            
        }
        public void Stop()
        {
            Debug.WriteLine("stop");
        }
    }
}
