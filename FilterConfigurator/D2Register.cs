using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetD2ItemFilter;
using System.IO;
using System.Threading;
class D2Register
{
    //static fields are initialized the first time their class is referenced.  Use a seperate container class to make sure these don't get initialized until we are all set up and need them.
    public static class DelayResourceCreation
    {
        public static DropNotifier DropNotifier = new DropNotifier();
    }

    static bool Callback(ref D2UnitAnyStrc pItem) {
        D2ItemsTXT txt = D2Common.GetItemTextRecord(pItem.dwClass);
        string code = txt.szCode;
        var knownItems = FilterReloader.Instance.KnownItems;
        var filter = FilterReloader.Instance.Filter;
        if(knownItems == null || filter == null)
        {
            return true;
        }
        if(!knownItems.Contains(code))
        {
            return true;
        }
        if(filter[code, pItem.ItemData.dwQuality])
        {
            return true;
        }
        return false;

    }

    static Task notifierThread;

    
    
    //callback delegate must be stored in static scope so it doesn't get cleaned up
    static NativeMethods.DropFilterCallback cb = Callback;

    public static int RegisterCallbacks(string unused)
    {
        NativeMethods.RegisterDropFilterCallback(cb);

        notifierThread = Task.Run(() => { while (true) { try { DropNotifier.DoScan(); Thread.Sleep(250); } catch (Exception e) { System.Diagnostics.Debugger.Log(0, "", "Exception on drop filter thread!"); } } });
        
        D2Client.Instance.GamePrint(2, "Registered!");
        
        return 0;
    }
}
