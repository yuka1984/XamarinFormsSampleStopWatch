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

        private long _elapsedTime;

        public long ElapsedTime
        {
            get
            {
                return _elapsedTime;
            }
            set
            {
                if (SetProperty(ref _elapsedTime, value))
                {
                    
                }
            }
        }

        public ReadOnlyObservableCollection<long> Laps { get; }
    }
}