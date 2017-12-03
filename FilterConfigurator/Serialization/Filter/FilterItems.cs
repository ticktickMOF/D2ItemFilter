using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNetD2ItemFilter.Serialization.Filter
{
    [Serializable]
    public class FilterItems
    {
        [XmlElement(ElementName = "Include")]
        public FilteredItem[] Items { get; set; }

        [XmlElement(ElementName = "Notify")]
        public FilteredItem[] NotifyItems { get; set; }
    }

    [Serializable]
    public class FilteredItem
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlElement("Quality")]
        public ItemQuality[] Qualities { get; set; }
    }

    

    
}
