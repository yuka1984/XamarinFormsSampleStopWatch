using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using KeyValueStore.Pcl;
using SwLib.Core.Models;
using SwLib.Interface.Models;
using SwLib.Pcl.Controllers;

namespace SwLib.Core.Controller
{
    public class StopWatchController : IStopWatchController, IDisposable
    {

        private const string SaveKey = "SaveStopWatchModels";
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly StopWatchModel _stopWatchModel;
        private readonly IKeyValueStore _keyValueStore;
        private readonly ObservableCollection<Tuple<string, IStopWatchModel>> _saveStopWatchModels;
        private Timer _updatetimer;

        private List<Tuple<string, StopWatchModel>> SaveModels
            =>
                _keyValueStore.GetValue<List<Tuple<string, StopWatchModel>>>(SaveKey) ??
                new List<Tuple<string, StopWatchModel>>();

        public StopWatchController(IKeyValueStore keyValueStore)
        {
            _stopWatchModel = new StopWatchModel();
            _keyValueStore = keyValueStore;
            _saveStopWatchModels =
                new ObservableCollection<Tuple<string, IStopWatchModel>>(
                    SaveModels.Select(x => Tuple.Create(x.Item1, (IStopWatchModel) x.Item2)));
            SaveStopWatchModels = new ReadOnlyObservableCollection<Tuple<string, IStopWatchModel>>(_saveStopWatchModels);
        }

        public void Dispose()
        {
            _updatetimer?.Dispose();
        }

        /// <summary>時間情報モデル</summary>
        public IStopWatchModel StopWatchModel => _stopWatchModel;

        /// <summary>保存時間情報</summary>
        public ReadOnlyObservableCollection<Tuple<string, IStopWatchModel>> SaveStopWatchModels { get; }

        /// <summary>ストップウォッチがスタートしているかを示します。</summary>
        public bool IsStart => _stopwatch.IsRunning;

        /// <summary>ストップウォッチをスタートします</summary>
        public void Start()
        {
            if (IsStart) return;
            Clear();
            _updatetimer =
                new Timer(state => TimeUpdate(), null, 10, 10);
            _stopwatch.Restart();
        }

        /// <summary>ストップウォッチをリスタートします</summary>
        public void Restart()
        {
            if (IsStart) return;
            _updatetimer =
                new Timer(state => TimeUpdate(), null, 10, 10);
            _stopwatch.Start();
        }

        /// <summary>ラップを行います</summary>
        public void Lap()
        {
            if (_stopwatch.ElapsedMilliseconds == 0) return;
            var lap = _stopwatch.ElapsedMilliseconds;
            if (_stopWatchModel.laps.All(x => x != lap))
            {
                _stopWatchModel.laps.Add(lap);
            }
        }

        /// <summary>ストップウォッチそストップします</summary>
        public void Stop()
        {
            _stopwatch.Stop();
            _updatetimer.Dispose();
            _updatetimer = null;
            _stopWatchModel.ElapsedTime = _stopwatch.ElapsedMilliseconds;
        }

        /// <summary>ストップウォッチをクリアします</summary>
        public void Clear()
        {
            if (IsStart) return;
            _stopwatch.Reset();
            TimeUpdate();
            _stopWatchModel.laps.Clear();
        }

        /// <summary>計測結果の保存を行います。</summary>
        /// <param name="name">保存名</param>
        /// <returns></returns>
        public SaveResultStatus Save(string name)
        {
            if (IsStart)
                return SaveResultStatus.Started;

            var models = SaveModels;
            if (models.Any(x => x.Item1.Equals(name)))
                return SaveResultStatus.NameRepeat;

            var savemodel = Tuple.Create(name, _stopWatchModel);
            models.Add(savemodel);
            _keyValueStore.Create(SaveKey, models);
            var savedmodel = SaveModels.Last();
            _saveStopWatchModels.Add(Tuple.Create(savedmodel.Item1, (IStopWatchModel)savedmodel.Item2));

            return SaveResultStatus.OK;
        }

        /// <summary>
        ///     保存されている計測結果をクリアします。
        /// </summary>
        public void SaveClearAll()
        {
            _keyValueStore.Delete(SaveKey);
            _saveStopWatchModels.Clear();
        }

        private void TimeUpdate() => _stopWatchModel.ElapsedTime = _stopwatch.ElapsedMilliseconds;
    }
}