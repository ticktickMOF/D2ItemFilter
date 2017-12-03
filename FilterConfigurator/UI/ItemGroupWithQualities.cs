using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter.UI
{
    class ItemGroupWithQualities
    {
        public ItemGroupWithQualities(string name, IEnumerable<ItemWithQualities> items)
        {
            Name = name;
            Items = items.ToList();
            LowQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.LowQuality); return seed; });
            NormalQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.NormalQuality); return seed; });
            SuperiorQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.SuperiorQuality); return seed; });
            MagicQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.MagicQuality); return seed; });
            RareQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.RareQuality); return seed; });
            SetQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.SetQuality); return seed; });
            UniqueQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.UniqueQuality); return seed; });
            CraftedQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.CraftedQuality); return seed; });
            HonorificQuality = Items.Aggregate(new BoolNotifyCollection(), (seed, i) => { seed.Add(i.HonorificQuality); return seed; });
        }

        public string Name { get; }

        public List<ItemWithQualities> Items { get; }


        public BoolNotifyCollection LowQuality { get; }

        public BoolNotifyCollection NormalQuality { get; }

        public BoolNotifyCollection SuperiorQuality { get; }

        public BoolNotifyCollection MagicQuality { get; }

        public BoolNotifyCollection RareQuality { get; }

        public BoolNotifyCollection SetQuality { get; }

        public BoolNotifyCollection UniqueQuality { get; }

        public BoolNotifyCollection CraftedQuality { get; }

        public BoolNotifyCollection HonorificQuality { get; }

    }
}
