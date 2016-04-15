using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwLib.Interface.Models
{
    /// <summary>時間情報モデルインターフェース</summary>
    public interface IStopWatchModel : INotifyPropertyChanged
    {
        /// <summary>スタートからの経過時間</summary>
        long ElapsedTime { get; }

        /// <summary>ラップタイムリスト</summary>
        ReadOnlyObservableCollection<long> Laps { get; }
    }
}
