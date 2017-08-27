using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FilterConfigurator.Serialization
{
    [Serializable]
    public class FilterItems
    {
        
    }

    [Serializable]
    public class FilteredItem
    {
        [XmlAttribute]
        public string Code { get; set; }

        [Serializable]
        public class Quality
        {
            [XmlAttribute]
            public int Value { get; set; }
        }

        [XmlElement("Quality")]
        public Quality[] Qualities { get; set; }
    }

    

    
}
