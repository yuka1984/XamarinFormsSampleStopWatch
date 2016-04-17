using SwLib.Pcl.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Notifiers;
using Xamarin.Forms;

namespace StopWatch
{
    public class SaveViewModel
    {
        public SaveViewModel(IStopWatchController stopWatchController)
        {
            IsBusy = busyNotifier.ToReadOnlyReactiveProperty();
            SaveCommand =
                busyNotifier.CombineLatest(SaveName, (busy, name) => !busy && !string.IsNullOrWhiteSpace(name))
                    .ToReactiveCommand();
            SaveCommand.Subscribe(x =>
            {
                var result = stopWatchController.Save(SaveName.Value);
                if (result == SaveResultStatus.OK)
                {
                    MessagingCenter.Send(new NavigationMessage(), NavigationMessage.Back);
                    MessagingCenter.Send(this, StopwatchViewModel.Clear);
                }
                else
                {
                    MessagingCenter.Send(new NavigationMessage(), NavigationMessage.DisplayMessage, result.ToString());
                }
            });

            Time = stopWatchController.StopWatchModel.ElapsedTime.ToTimeFormat();
            LapList =
                stopWatchController.StopWatchModel.Laps.Select((x, i) => $"Lap:{i + 1} {x.ToTimeFormat()}").ToList();

            SaveName.Value = DateTime.Now.ToString("yyyy/MM/dd hh:mm");

        }

        private BusyNotifier busyNotifier = new BusyNotifier();
        public ReadOnlyReactiveProperty<bool> IsBusy { get; }
        public ReactiveCommand SaveCommand { get; }
        public ReactiveProperty<string> SaveName { get; } = new ReactiveProperty<string>();
        public string Time { get; }
        public List<string> LapList { get; }
    }
}