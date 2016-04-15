using SQLite.Net.Attributes;

namespace KeyValueStore.Core
{
    /// <summary>
    ///     Kvsテーブルモデル
    /// </summary>
    [Table("Kvs")]
    public class Kvs
    {
        /// <summary>
        ///     Key
        /// </summary>
        [PrimaryKey]
        public string Key { get; set; }

        /// <summary>
        ///     Value
        /// </summary>
        public string Value { get; set; }
    }
}