using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive; 
using SwLib.Pcl.Controllers;

namespace StopWatch
{
    public class StopwatchViewModel
    {
        public StopwatchViewModel(IStopWatchController stopWatchController)
        {
            var elapsedtime =
                stopWatchController.StopWatchModel.ObserveProperty(x => x.ElapsedTime).ToReadOnlyReactiveProperty();

            Rotation = elapsedtime.Select(x => (double) (x/1000*6%360) - 90).ToReadOnlyReactiveProperty();

            Time = elapsedtime.Select(x => TimeSpan.FromMilliseconds(x).ToString() + "->")
                .ToReadOnlyReactiveProperty();


            StartCommand = IsRuning.Inverse().ToReactiveCommand();
            StartCommand.Subscribe(x =>
            {
                IsRuning.Value = true;
                if (IsStart.Value)
                {
                    stopWatchController.Restart();
                }
                else
                {
                    stopWatchController.Start();
                }
                IsStart.Value = true;
            });
            StopCommand = IsRuning.ToReactiveCommand();
            StopCommand.Subscribe(x =>
            {
                IsRuning.Value = false;
                stopWatchController.Stop();
            });

            ClearCommand = IsRuning.CombineLatest(elapsedtime, (run, t) => t > 0 && !run).ToReactiveCommand();
            ClearCommand.Subscribe(x =>
            {
                IsRuning.Value = false;
                IsStart.Value = false;
                stopWatchController.Clear();
            });

            LapCommand = IsRuning.ToReactiveCommand();
            LapCommand.Subscribe(x => stopWatchController.Lap());

            
                stopWatchController.StopWatchModel.Laps
                .CollectionChangedAsObservable()
                    .Subscribe(x =>
                    {
                        if (x.Action == NotifyCollectionChangedAction.Add)
                        {
                            var newitem = Convert.ToDouble(x.NewItems[0]);
                            Laps.Insert(0, TimeSpan.FromMilliseconds((double)newitem).ToString());
                        }
                        else if (x.Action == NotifyCollectionChangedAction.Reset)
                        {
                            Laps.Clear();
                        }
                        
                    });

            StartCommandTitle = IsStart.Select(x => x ? "ReStart" : "Start").ToReadOnlyReactiveProperty();
        }

        public ReactiveCommand StartCommand { get; }
        public ReactiveCommand StopCommand { get; }
        public ReactiveCommand ClearCommand { get; }
        public ReactiveCommand LapCommand { get; }

        public ReactiveProperty<bool> IsStart { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsRuning { get; } = new ReactiveProperty<bool>(false);

        public ReadOnlyReactiveProperty<string> StartCommandTitle { get; }

        public ObservableCollection<string> Laps { get; } = new ObservableCollection<string>();

        public ReadOnlyReactiveProperty<string> Time { get; }
        public ReadOnlyReactiveProperty<double> Rotation { get; }
    }
}