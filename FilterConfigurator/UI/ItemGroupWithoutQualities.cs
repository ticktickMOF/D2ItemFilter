using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter.UI
{
    class ItemGroupWithoutQualities
    {
        public ItemGroupWithoutQualities(string name, IEnumerable<ItemWithoutQualities> items)
        {
            Name = name;
            Items = items.ToList();
            AllItemsEnabled = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.Selected); return seed; });
        }

        public string Name { get; }

        public List<ItemWithoutQualities> Items { get; }


        public BoolNotifyCollection AllItemsEnabled { get; }
    }
}
