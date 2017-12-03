using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter.UI
{
    class ItemWithoutQualities
    {
        public ItemWithoutQualities(string name, string code, bool selected)
        {
            Name = name;
            Code = code;
            Selected = new BoolPropertyChangedValueWrapper() { Value = selected } ;
        }

        public string Name { get; }
        public string Code { get; }

        public BoolPropertyChangedValueWrapper Selected { get; }
    }
}
