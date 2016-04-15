using System.Collections.ObjectModel;
using SwLib.Interface.Models;

namespace SwLib.Core.Models
{
    internal class StopWatchModel : BindableBase, IStopWatchModel
    {
        internal StopWatchModel()
        {
            laps = new ObservableCollection<long>();
            Laps = new ReadOnlyObservableCollection<long>(laps);
        }

        public ObservableCollection<long> laps { get; }

        public long ElapsedTime { get; private set; }

        public ReadOnlyObservableCollection<long> Laps { get; }

        public void SetElapsedTime(long elapsedTime)
        {
            ElapsedTime = elapsedTime;
            OnPropertyChanged(nameof(ElapsedTime));
        }
    }
}