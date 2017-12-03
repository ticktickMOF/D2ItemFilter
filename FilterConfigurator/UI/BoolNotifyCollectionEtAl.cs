using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DotNetD2ItemFilter.UI
{
    public abstract class INotifyPropertyChangedWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BoolPropertyChangedValueWrapper : INotifyPropertyChangedWrapper
    {
        private bool _Value;

        public bool Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged(); }
        }
    }

    public class BoolNotifyCollection : INotifyPropertyChangedWrapper, ICollection<BoolPropertyChangedValueWrapper>
    {
        private bool? _Value;

        public bool? Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (!value.HasValue || value == _Value)
                {
                    return;
                }
                disableNotifications = true;
                foreach (var item in Items)
                {
                    item.Value = value.Value;
                }
                disableNotifications = false;
                _Value = value;
                OnPropertyChanged();
                IsTriState = !value.HasValue;
            }
        }


        public bool IsTriState
        {
            get
            {
                return _IsTriState;
            }
            set
            {
                if (_IsTriState != value)
                {
                    _IsTriState = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsTriState;


        private bool disableNotifications = false;

        private List<BoolPropertyChangedValueWrapper> Items = new List<BoolPropertyChangedValueWrapper>();

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        private void UpdateValue()
        {
            bool? @base = Items.FirstOrDefault()?.Value;
            if (@base.HasValue)
            {
                foreach (var item in Items.Skip(1))
                {
                    if (@base != item.Value)
                    {
                        @base = null;
                        break;
                    }
                }
            }
            SetValueNonPropagating(@base);
            OnPropertyChanged(nameof(Value));
        }

        private void SetValueNonPropagating(bool? value)
        {
            _Value = value;
            IsTriState = !value.HasValue;
        }

        public void Add(BoolPropertyChangedValueWrapper item)
        {
            Items.Add(item);
            item.PropertyChanged += Item_PropertyChanged;
            UpdateValue();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!disableNotifications)
            {
                UpdateValue();
            }
        }

        public void Clear()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }
        }

        public bool Contains(BoolPropertyChangedValueWrapper item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(BoolPropertyChangedValueWrapper[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BoolPropertyChangedValueWrapper> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public bool Remove(BoolPropertyChangedValueWrapper item)
        {
            item.PropertyChanged -= Item_PropertyChanged;
            return Items.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Items as IEnumerable).GetEnumerator();
        }
    }
}
