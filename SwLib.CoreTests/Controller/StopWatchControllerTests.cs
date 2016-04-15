using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwLib.Core.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SwLib.Core.Controller.Tests
{
    [TestClass()]
    public class StopWatchControllerTests
    {
        private StopWatchController controller;
        [TestMethod()]
        public void StopWatchControllerTest()
        {
            //controller = new StopWatchController();
            //controller.StopWatchModel.PropertyChanged += StopWatchModelOnPropertyChanged;
            //controller.Start();

            //Thread.Sleep(1000);

            //controller.Stop();

            //Thread.Sleep(1000);

            //controller.Restart();

            //Thread.Sleep(1000);

            //controller.Stop();

            //controller.Clear();

            //controller.Start();

            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(100);
            //    controller.Lap();
            //}
            //controller.Stop();
            
        }

        private void StopWatchModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Debug.WriteLine(propertyChangedEventArgs.PropertyName, JsonConvert.SerializeObject(sender));
        }

        [TestMethod()]
        public void DisposeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StartTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RestartTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LapTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StopTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ClearTest()
        {
            Assert.Fail();
        }
    }
}