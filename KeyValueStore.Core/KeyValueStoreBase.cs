using System.Linq;
using KeyValueStore.Pcl;
using SQLite.Net;

namespace KeyValueStore.Core
{
    /// <summary>
    ///     キーバリューストアベース
    /// </summary>
    public abstract class KeyValueStoreBase : IKeyValueStore
    {
        /// <summary>
        ///     コンストラクタ
        /// </summary>
        protected KeyValueStoreBase()
        {
            var con = GetConnection();
            con.CreateTable<Kvs>();
            con.Close();
            con.Dispose();
        }

        /// <summary>
        ///     CreateNew
        ///     キーが存在していたらセットしない。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CreateNew(string key, object value)
        {
            using (var con = GetConnection())
            {
                return con.InsertOrIgnore(new Kvs {Key = key, Value = GetJson(value)}, typeof (Kvs)) > 0;
            }
        }

        /// <summary>
        ///     Create
        ///     キーが存在していたら上書き
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Create(string key, object value)
        {
            using (var con = GetConnection())
            {
                return con.InsertOrReplace(new Kvs {Key = key, Value = GetJson(value)}, typeof (Kvs)) > 0;
            }
        }

        /// <summary>
        ///     バリューの取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            using (var con = GetConnection())
            {
                var json = con.Table<Kvs>().FirstOrDefault(kvs => kvs.Key == key)?.Value;

                if (string.IsNullOrEmpty(json))
                {
                    return default(T);
                }
                return JsonToObject<T>(json);
            }
        }

        /// <summary>
        ///     削除
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            using (var con = GetConnection())
            {
                con.Delete<Kvs>(key);
            }
        }

        /// <summary>
        ///     コネクションの取得
        /// </summary>
        /// <returns></returns>
        protected abstract SQLiteConnection GetConnection();

        /// <summary>
        ///     jsonSerialize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract string GetJson(object value);

        /// <summary>
        ///     JsonDeSerialize
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected abstract T JsonToObject<T>(string json);
    }
}