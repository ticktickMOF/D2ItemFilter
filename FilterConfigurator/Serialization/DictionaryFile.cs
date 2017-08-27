using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FilterConfigurator.Serialization
{
    [Serializable]
    [XmlRoot(ElementName ="ItemDictionary")]
    public class DictionaryFile
    {
        public Quality[] Qualities { get; set; }
        [XmlElement(ElementName ="Group")]
        public ItemGroup[] ItemGroups { get; set; }
    }
}
