using System;
using System.Collections.ObjectModel;
using SwLib.Interface.Models;

namespace SwLib.Pcl.Controllers
{
    /// <summary>ストップウォッチコントローラインターフェース</summary>
    public interface IStopWatchController
    {
        /// <summary>時間情報モデル</summary>
        IStopWatchModel StopWatchModel { get; }

        /// <summary>保存時間情報</summary>
        ReadOnlyObservableCollection<Tuple<string, IStopWatchModel>> SaveStopWatchModels { get; } 

        /// <summary>ストップウォッチがスタートしているかを示します。</summary>
        bool IsStart { get; }

        /// <summary>ストップウォッチをスタートします</summary>
        void Start();

        /// <summary>ストップウォッチをリスタートします</summary>
        void Restart();

        /// <summary>ラップを行います</summary>
        void Lap();

        /// <summary>ストップウォッチそストップします</summary>
        void Stop();

        /// <summary>ストップウォッチをクリアします</summary>
        void Clear();

        /// <summary>計測結果の保存を行います。</summary>
        /// <param name="name">保存名</param>
        /// <returns></returns>
        SaveResultStatus Save(string name);

        /// <summary>
        /// 保存されている計測結果をクリアします。
        /// </summary>
        void SaveClearAll();


    }
    /// <summary>保存処理結果</summary>
    public enum SaveResultStatus
    {
       　/// <summary>成功</summary>
        OK,
        /// <summary>名前重複</summary>
        NameRepeat,
        /// <summary>スタートしていません</summary>
        NotStarted,
    }
}