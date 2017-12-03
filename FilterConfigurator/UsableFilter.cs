using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    class UsableFilter
    {
        public UsableFilter(IEnumerable<Serialization.Filter.FilteredItem> items)
        {
            if(items == null)
            {
                return;
            }
            foreach (var item in items)
            {
                if (item.Qualities != null)
                {
                    BackingData[item.Code] = new HashSet<ItemQuality>(item.Qualities);
                }
                else
                {
                    BackingData[item.Code] = null;
                }
            }
        }


        private Dictionary<string, HashSet<ItemQuality>> BackingData { get; } = new Dictionary<string, HashSet<ItemQuality>>();

        public bool this[string itemCode, ItemQuality? quality]
        {
            get
            {
                HashSet<ItemQuality> temp;
                if(!BackingData.TryGetValue(itemCode, out temp))
                {
                    return false;
                }
                if(temp == null)
                {
                    return true;
                }
                if (quality.HasValue)
                {
                    return temp.Contains(quality.Value);
                }
                else
                {
                    return true;
                }
            }
        }
    }

    static class Extension
    {
        public static V GetValueOrDefault<K, V>(this Dictionary<K, V> dict, K key, V defaultValue)
        {
            V @out;
            if (!dict.TryGetValue(key, out @out))
            {
                return defaultValue;
            }
            return @out;
        }
    }
}
