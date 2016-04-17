using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using SwLib.Pcl.Controllers;

namespace StopWatch
{
    public class SaveListViewModel
    {
        public SaveListViewModel(IStopWatchController stopWatchController)
        {
            SaveItemKeys = stopWatchController.SaveStopWatchModels
                .ToReadOnlyReactiveCollection(x => x.Item1);
            SelectedSaveItemKey = new ReactiveProperty<string>(mode:ReactivePropertyMode.DistinctUntilChanged);
            
            SelectedSaveItemTime = SelectedSaveItemKey
                .Select(x => stopWatchController.SaveStopWatchModels.FirstOrDefault(y => y.Item1.Equals(x)))
                .Select(x => x.Item2.ElapsedTime.ToTimeFormat())
                .ToReadOnlyReactiveProperty();
            SelectedSaveItemLaps = SelectedSaveItemKey
                .Select(x => stopWatchController.SaveStopWatchModels.FirstOrDefault(y => y.Item1.Equals(x)))
                .Select(x => x.Item2.Laps.Select((y, i) => $"Lap:{i + 1} {y.ToTimeFormat()}").ToList())
                .ToReadOnlyReactiveProperty();
        }

        public ReadOnlyReactiveCollection<string> SaveItemKeys { get; }
        public ReactiveProperty<string> SelectedSaveItemKey { get; }
        public ReadOnlyReactiveProperty<string> SelectedSaveItemTime { get; }
        public ReadOnlyReactiveProperty<List<string>> SelectedSaveItemLaps { get; }
        
    }
}
