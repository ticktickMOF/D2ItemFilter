using DotNetD2ItemFilter.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using System.Linq;
using System;

namespace DotNetD2ItemFilter.UI
{
    class DesignTimeViewModel : ViewModel
    {
        public DesignTimeViewModel() : base()
        {
            FilterItemGroups = new List<dynamic>() {

                new ItemGroupWithoutQualities("Bar",
                    new List<ItemWithoutQualities>()
                    {
                        new ItemWithoutQualities("oiu", "wer", true),
                        new ItemWithoutQualities("mnb", "ou", false)
                    }
                ),
                new ItemGroupWithQualities("Foo",
                    new List<ItemWithQualities>() {
                        new ItemWithQualities("asdf", "qwe",
                            true,false,true,false,true,false,true,false,true),
                        new ItemWithQualities("qwer", "xcv",
                        true, true, false, true, false, true, false, true,false)
                    }
                ),
            };
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoadSettings();

            InitializeComponent();
        }

        private void LoadSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Serialization.Dictionary.File));
            XmlSerializer serializer2 = new XmlSerializer(typeof(Serialization.Filter.FilterItems));

            var filterFilePath = Globals.FilterLocation;
            var dictionaryFilepath = Globals.DictionaryLocation;

            ViewModel vm = new ViewModel();
            vm.FilterItemGroups = new List<dynamic>();
            Serialization.Dictionary.File dict = null;
            if (File.Exists(dictionaryFilepath))
            {
                using (var dictFile = new FileStream(dictionaryFilepath, FileMode.Open, FileAccess.Read))
                {
                    dict = (Serialization.Dictionary.File)serializer.Deserialize(dictFile);

                }
            }

            if (dict != null)
            {

                UsableFilter filter;
                UsableFilter notify;
                if (File.Exists(filterFilePath))
                {
                    using (var filterFile = new FileStream(filterFilePath, FileMode.Open, FileAccess.Read))
                    {
                        Serialization.Filter.FilterItems fi = (Serialization.Filter.FilterItems)serializer2.Deserialize(filterFile);
                        filter = new UsableFilter(fi.Items);
                        notify = new UsableFilter(fi.NotifyItems);
                    }
                }
                else
                {
                    filter = new UsableFilter(dict.ItemGroups.SelectMany(group => group.Content.Select(item => new Serialization.Filter.FilteredItem() { Code = item.Code, Qualities = group.QualityFilterable ? Enum.GetValues(typeof(ItemQuality)).Cast<ItemQuality>().ToArray() : new ItemQuality[0] })));
                    notify = new UsableFilter(new Serialization.Filter.FilteredItem[0]);
                }

                vm.FilterItemGroups = dict.ItemGroups.Aggregate(new List<dynamic>(), (seed, group) =>
                {
                    if (group.QualityFilterable)
                    {
                        seed.Add(
                            new ItemGroupWithQualities(group.Name,
                                group.Content.Select(item => new ItemWithQualities(
                                    item.Name,
                                    item.Code,
                                    filter[item.Code, ItemQuality.Low],
                                    filter[item.Code, ItemQuality.Normal],
                                    filter[item.Code, ItemQuality.Superior],
                                    filter[item.Code, ItemQuality.Magic],
                                    filter[item.Code, ItemQuality.Rare],
                                    filter[item.Code, ItemQuality.Unique],
                                    filter[item.Code, ItemQuality.Set],
                                    filter[item.Code, ItemQuality.Crafted],
                                    filter[item.Code, ItemQuality.Honorific]
                                ))
                            )
                        );
                    }
                    else
                    {
                        seed.Add(
                            new ItemGroupWithoutQualities(
                                group.Name,
                                group.Content.Select(item => new ItemWithoutQualities(
                                    item.Name,
                                    item.Code,
                                    filter[item.Code, null])
                                )
                            )
                        );
                    }
                    return seed;
                });

                vm.NotifyItemGroups = dict.ItemGroups.Aggregate(new List<dynamic>(), (seed, group) =>
                {
                    if (group.QualityFilterable)
                    {
                        seed.Add(
                            new ItemGroupWithQualities(group.Name,
                                group.Content.Select(item => new ItemWithQualities(
                                    item.Name,
                                    item.Code,
                                    notify[item.Code, ItemQuality.Low],
                                    notify[item.Code, ItemQuality.Normal],
                                    notify[item.Code, ItemQuality.Superior],
                                    notify[item.Code, ItemQuality.Magic],
                                    notify[item.Code, ItemQuality.Rare],
                                    notify[item.Code, ItemQuality.Unique],
                                    notify[item.Code, ItemQuality.Set],
                                    notify[item.Code, ItemQuality.Crafted],
                                    notify[item.Code, ItemQuality.Honorific]
                                ))
                            )
                        );
                    }
                    else
                    {
                        seed.Add(
                            new ItemGroupWithoutQualities(
                                group.Name,
                                group.Content.Select(item => new ItemWithoutQualities(
                                    item.Name,
                                    item.Code,
                                    notify[item.Code, null])
                                )
                            )
                        );
                    }
                    return seed;
                });



                //DataContext = new DesignTimeViewModel();
                DataContext = vm;
            }
            else
            {
                //Error.Text = "No dictionary file found!";
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer2 = new XmlSerializer(typeof(Serialization.Filter.FilterItems));

            var filterFilePath = Globals.FilterLocation;

            Serialization.Filter.FilterItems filter = new Serialization.Filter.FilterItems();
            filter.Items = new Serialization.Filter.FilteredItem[0];
            filter.NotifyItems = new Serialization.Filter.FilteredItem[0];

            ViewModel vm = (ViewModel)DataContext;
            foreach (var group in vm.FilterItemGroups)
            {
                var withQualities = group as ItemGroupWithQualities;
                var withoutQualities = group as ItemGroupWithoutQualities;
                if (withQualities != null)
                {
                    filter.Items = filter.Items.Concat(withQualities.Items.Where(item => item.AllQualities.Value != false).Select(item =>
                    {
                        var ret = new Serialization.Filter.FilteredItem();
                        ret.Code = item.Code;
                        var qualities = new List<ItemQuality>();
                        if(item.LowQuality.Value)
                        {
                            qualities.Add(ItemQuality.Low);
                        }
                        if(item.NormalQuality.Value)
                        {
                            qualities.Add(ItemQuality.Normal);
                        }
                        if (item.SuperiorQuality.Value)
                        {
                            qualities.Add(ItemQuality.Superior);
                        }
                        if (item.MagicQuality.Value)
                        {
                            qualities.Add(ItemQuality.Magic);
                        }
                        if (item.RareQuality.Value)
                        {
                            qualities.Add(ItemQuality.Rare);
                        }
                        if (item.SetQuality.Value)
                        {
                            qualities.Add(ItemQuality.Set);
                        }
                        if (item.UniqueQuality.Value)
                        {
                            qualities.Add(ItemQuality.Unique);
                        }
                        if (item.CraftedQuality.Value)
                        {
                            qualities.Add(ItemQuality.Crafted);
                        }
                        if (item.HonorificQuality.Value)
                        {
                            qualities.Add(ItemQuality.Honorific);
                        }
                        ret.Qualities = qualities.ToArray();
                        return ret;
                    })).ToArray();
                }
                else if(withoutQualities != null)
                {
                    filter.Items = filter.Items.Concat(withoutQualities.Items.Where(item => item.Selected.Value).Select(item =>
                    {
                        var ret = new Serialization.Filter.FilteredItem();
                        ret.Code = item.Code;
                        
                        ret.Qualities = null;
                        return ret;
                    })).ToArray();
                }
            }
            foreach (var group in vm.NotifyItemGroups)
            {
                var withQualities = group as ItemGroupWithQualities;
                var withoutQualities = group as ItemGroupWithoutQualities;
                if (withQualities != null)
                {
                    filter.NotifyItems = filter.NotifyItems.Concat(withQualities.Items.Where(item => item.AllQualities.Value != false).Select(item =>
                    {
                        var ret = new Serialization.Filter.FilteredItem();
                        ret.Code = item.Code;
                        var qualities = new List<ItemQuality>();
                        if (item.LowQuality.Value)
                        {
                            qualities.Add(ItemQuality.Low);
                        }
                        if (item.NormalQuality.Value)
                        {
                            qualities.Add(ItemQuality.Normal);
                        }
                        if (item.SuperiorQuality.Value)
                        {
                            qualities.Add(ItemQuality.Superior);
                        }
                        if (item.MagicQuality.Value)
                        {
                            qualities.Add(ItemQuality.Magic);
                        }
                        if (item.RareQuality.Value)
                        {
                            qualities.Add(ItemQuality.Rare);
                        }
                        if (item.SetQuality.Value)
                        {
                            qualities.Add(ItemQuality.Set);
                        }
                        if (item.UniqueQuality.Value)
                        {
                            qualities.Add(ItemQuality.Unique);
                        }
                        if (item.CraftedQuality.Value)
                        {
                            qualities.Add(ItemQuality.Crafted);
                        }
                        if (item.HonorificQuality.Value)
                        {
                            qualities.Add(ItemQuality.Honorific);
                        }
                        ret.Qualities = qualities.ToArray();
                        return ret;
                    })).ToArray();
                }
                else if (withoutQualities != null)
                {
                    filter.NotifyItems = filter.NotifyItems.Concat(withoutQualities.Items.Where(item => item.Selected.Value).Select(item =>
                    {
                        var ret = new Serialization.Filter.FilteredItem();
                        ret.Code = item.Code;

                        ret.Qualities = null;
                        return ret;
                    })).ToArray();
                }
            }

            using (var filterFile = new FileStream(filterFilePath, FileMode.Create, FileAccess.Write))
            {
                serializer2.Serialize(filterFile, filter);
            }
        }
    }
}
