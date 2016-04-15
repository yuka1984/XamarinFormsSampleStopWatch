using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore.Pcl
{
    public interface IKeyValueStore
    {
        bool Create(string key, object value);
        bool CreateNew(string key, object value);
        void Delete(string key);
        T GetValue<T>(string key);
    }
}
