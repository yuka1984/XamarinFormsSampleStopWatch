﻿using System;
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
}
