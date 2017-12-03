using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter.UI
{
    class ItemWithQualities
    {
        public ItemWithQualities(string name, string code, bool lowQuality, bool normalQuality, bool superiorQuality, bool magicQuality, bool rareQuality, bool uniqueQuality, bool setQuality, bool craftedQuality, bool honorificQuality)
        {
            Name = name;
            Code = code;
            LowQuality = new BoolPropertyChangedValueWrapper() { Value = lowQuality };
            NormalQuality = new BoolPropertyChangedValueWrapper() { Value = normalQuality };
            SuperiorQuality = new BoolPropertyChangedValueWrapper() { Value = superiorQuality };
            MagicQuality = new BoolPropertyChangedValueWrapper() { Value = magicQuality };
            RareQuality = new BoolPropertyChangedValueWrapper() { Value = rareQuality };
            UniqueQuality = new BoolPropertyChangedValueWrapper() { Value = uniqueQuality };
            SetQuality = new BoolPropertyChangedValueWrapper() { Value = setQuality };
            CraftedQuality = new BoolPropertyChangedValueWrapper() { Value = craftedQuality };
            HonorificQuality = new BoolPropertyChangedValueWrapper() { Value = honorificQuality };
            AllQualities = new BoolNotifyCollection() {
                LowQuality,
                NormalQuality,
                SuperiorQuality,
                MagicQuality,
                RareQuality,
                SetQuality,
                UniqueQuality,
                CraftedQuality,
                HonorificQuality
            };
        }

        public string Name { get; }
        public string Code { get; }

        public BoolPropertyChangedValueWrapper LowQuality { get; }
        public BoolPropertyChangedValueWrapper NormalQuality { get; }
        public BoolPropertyChangedValueWrapper SuperiorQuality { get; }
        public BoolPropertyChangedValueWrapper MagicQuality { get; }
        public BoolPropertyChangedValueWrapper RareQuality { get; }
        public BoolPropertyChangedValueWrapper SetQuality { get; }
        public BoolPropertyChangedValueWrapper UniqueQuality { get; }
        public BoolPropertyChangedValueWrapper CraftedQuality { get; }
        public BoolPropertyChangedValueWrapper HonorificQuality { get; }

        public BoolNotifyCollection AllQualities { get; }
    }
}
