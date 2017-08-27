using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FilterConfigurator.Serialization
{
    [Serializable]
    [System.Diagnostics.DebuggerDisplayAttribute("{Name} : {Value}")]
    public class Quality
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public int Value { get; set; }
    }
}
