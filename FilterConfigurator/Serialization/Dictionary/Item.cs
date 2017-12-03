using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNetD2ItemFilter.Serialization.Dictionary
{
    [System.Diagnostics.DebuggerDisplay("{"+ nameof(Name) + "} {" + nameof(Code) + "} {" + nameof(IntCode) + "}")]
    public class Item
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Code {
            get { return _Code; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (value.Length != 4) throw new ArgumentException();
                _Code = value;
            }
        }

        private string _Code;

        
        [XmlIgnore]
        public int IntCode
        {
            get
            {
                int ret = 0;
                for (int i = 0; i < Code.Length; i++)
                {
                    byte b = (byte)Code[i];
                    ret += ((int)b) << (i * 8);
                }
                return ret;
            }
        }
    }
}
