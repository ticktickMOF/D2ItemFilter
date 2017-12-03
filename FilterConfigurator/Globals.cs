using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    static class Globals
    {
        static public readonly string FilterLocation = Path.Combine(Directory.GetParent(typeof(FilterReloader).Assembly.Location).FullName, "Filter.xml");
        static public readonly string DictionaryLocation = Path.Combine(Directory.GetParent(typeof(FilterReloader).Assembly.Location).FullName, "ItemDictionary.xml");
        static public readonly string NotificationsLocation = Path.Combine(Directory.GetParent(typeof(FilterReloader).Assembly.Location).FullName, "Notify.xml");
    }
}
