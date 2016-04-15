using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.Generic;

namespace KeyValueStore.Core
{
    public class KeyValueStore : KeyValueStoreBase
    {
        private const string DbFileName = "kvs.db3";

        private string SaveDirectory { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="saveDirectory">保存ディレクトリ</param>
        public KeyValueStore(string saveDirectory)
        {
            if (Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            this.SaveDirectory = saveDirectory;
        }


        /// <summary>
        ///     コネクションの取得
        /// </summary>
        /// <returns></returns>
        protected override SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformGeneric(), Path.Combine(SaveDirectory, DbFileName));
        }

        /// <summary>
        ///     jsonSerialize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override string GetJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        ///     JsonDeSerialize
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected override T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
