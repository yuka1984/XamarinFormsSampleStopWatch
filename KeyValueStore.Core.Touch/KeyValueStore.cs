using System;
using System.IO;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace KeyValueStore.Core.Touch
{
    /// <summary>
    ///     iOSキーバリューストア
    /// </summary>
    public class KeyValueStore : KeyValueStoreBase
    {
        private const string DbFileName = "kvs.db3";

        private static readonly string DbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");

        /// <summary>
        ///     コネクションの取得
        /// </summary>
        /// <returns></returns>
        protected override SQLiteConnection GetConnection()
            => new SQLiteConnection(new SQLitePlatformIOS(), Path.Combine(DbPath, DbFileName));

        /// <summary>
        ///     jsonSerialize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override string GetJson(object value) => JsonConvert.SerializeObject(value);

        /// <summary>
        ///     JsonDeSerialize
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected override T JsonToObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}