using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetD2ItemFilter
{
    class FilterReloader : IDisposable
    {
        public static FilterReloader Instance;

        static FilterReloader()
        {
            Instance = new FilterReloader(Globals.FilterLocation, Globals.DictionaryLocation);
        }

        private FilterReloader(string filterFileLocation, string dictionaryFileLocation)
        {
            FilterLocation = filterFileLocation;
            DictionaryLocation = dictionaryFileLocation;
            filterWatcher = new FileSystemWatcher(Directory.GetParent(filterFileLocation).FullName, Path.GetFileName(filterFileLocation));
            dictionaryWatcher = new FileSystemWatcher(Directory.GetParent(dictionaryFileLocation).FullName, Path.GetFileName(dictionaryFileLocation));
            ReloadDictionary();
            ReloadFilter();
            filterWatcher.Changed += FileChanged;
            filterWatcher.Created += FileChanged;
            filterWatcher.Deleted += FileChanged;
            filterWatcher.Renamed += FileChanged;
            dictionaryWatcher.Changed += FileChanged;
            dictionaryWatcher.Created += FileChanged;
            dictionaryWatcher.Deleted += FileChanged;
            dictionaryWatcher.Renamed += FileChanged;
            filterWatcher.EnableRaisingEvents = true;
            dictionaryWatcher.EnableRaisingEvents = true;
        }

        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath == FilterLocation)
            {
                ReloadFilter();
            }
            else if (e.FullPath == DictionaryLocation)
            {
                ReloadDictionary();
            }
        }


        private readonly string FilterLocation;
        private readonly string DictionaryLocation;


        private FileSystemWatcher filterWatcher;
        private FileSystemWatcher dictionaryWatcher;
        private static System.Xml.Serialization.XmlSerializer filterSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DotNetD2ItemFilter.Serialization.Filter.FilterItems));
        private static System.Xml.Serialization.XmlSerializer dictionarySerializer = new System.Xml.Serialization.XmlSerializer(typeof(DotNetD2ItemFilter.Serialization.Dictionary.File));

        public UsableFilter Filter { get; private set; }
        public UsableFilter NotificationFilter { get; private set; }
        public HashSet<string> KnownItems { get; private set; }

        private void ReloadFilter()
        {
            if (File.Exists(FilterLocation))
            {
                try
                {
                    using (var filterFile = new FileStream(FilterLocation, FileMode.Open, FileAccess.Read))
                    {
                        DotNetD2ItemFilter.Serialization.Filter.FilterItems fi = (DotNetD2ItemFilter.Serialization.Filter.FilterItems)filterSerializer.Deserialize(filterFile);
                        Filter = new UsableFilter(fi.Items);
                        NotificationFilter = new UsableFilter(fi.NotifyItems);
                    }
                }
                catch (Exception)
                {
                    Filter = null;
                }
            }
            else
            {
                Filter = null;
            }
            D2Client.Instance.GamePrint(2, "Filter Reloaded");

        }

        private void ReloadDictionary()
        {
            if (File.Exists(DictionaryLocation))
            {
                try
                {
                    using (var dictFile = new FileStream(DictionaryLocation, FileMode.Open, FileAccess.Read))
                    {
                        var dict = (DotNetD2ItemFilter.Serialization.Dictionary.File)dictionarySerializer.Deserialize(dictFile);
                        HashSet<string> newKnownItems = new HashSet<string>();
                        foreach (var item in dict.ItemGroups.SelectMany(group => group.Content))
                        {
                            newKnownItems.Add(item.Code);
                        }
                        KnownItems = newKnownItems;
                    }
                }
                catch (Exception)
                {
                    KnownItems = null;
                }
            }
            else
            {
                KnownItems = null;
            }
            D2Client.Instance.GamePrint(2, "Dictionary Reloaded");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    filterWatcher.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FilterReloader() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
