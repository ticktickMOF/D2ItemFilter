using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNetD2ItemFilter.Serialization.Dictionary
{
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(Name) + "} Filterable:{" + nameof(QualityFilterable) + "} {Content.Length}")]
    public class ItemGroup
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public bool QualityFilterable { get; set; } = false;
        [XmlElement(ElementName ="Item")]
        public Item[] Content { get; set; }
    }
}
