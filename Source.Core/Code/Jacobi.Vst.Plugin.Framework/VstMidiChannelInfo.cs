namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Plugin.Framework.Common;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// The VstMidiChannelInfo contains Midi Program information for a Midi channel.
    /// </summary>
    /// <remarks>Although an instance of the VstMidiChannelInfo class represents Midi Program information
    /// for one Midi channel, it has no member to identify that Midi channel. This is to allow easy reuse
    /// of one VstMidiChannelInfo instance for multiple channels.</remarks>
    public class VstMidiChannelInfo : ObservableObject
    {
        private CollectionChangeManager<INotifyPropertyChanged> _categoriesMgr;
        private CollectionChangeManager<INotifyPropertyChanged> _programsMgr;

        private VstMidiCategoryCollection _categories;
        /// <summary>
        /// Gets the collection of <see cref="VstMidiCategory"/>s.
        /// </summary>
        public VstMidiCategoryCollection Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new VstMidiCategoryCollection();
                    _categoriesMgr = new CollectionChangeManager<INotifyPropertyChanged>(
                        _categories, OnCollectionItemAdded, OnCollectionItemRemoved);
                }

                return _categories;
            }
        }

        private VstMidiProgramCollection _programs;
        /// <summary>
        /// Gets a collection of <see cref="VstMidiProgram"/>s.
        /// </summary>
        public VstMidiProgramCollection Programs
        {
            get
            {
                if (_programs == null)
                {
                    _programs = new VstMidiProgramCollection();
                    _programsMgr = new CollectionChangeManager<INotifyPropertyChanged>(
                        _programs, OnCollectionItemAdded, OnCollectionItemRemoved);
                }

                return _programs;
            }
        }

        private VstMidiProgram _activeProgram;
        /// <summary>
        /// Gets or sets the active/current Midi Program for this channel.
        /// </summary>
        public VstMidiProgram ActiveProgram
        {
            get
            {
                if (_activeProgram == null &&
                    _programs != null && _programs.Count > 0)
                {
                    // automatically activate the first midi program
                    ActiveProgram = _programs[0];
                }

                return _activeProgram;
            }
            set
            {
                if (_activeProgram != value)
                {
                    if (_activeProgram != null)
                    {
                        _activeProgram.IsActive = false;
                    }

                    SetProperty(value, ref _activeProgram, nameof(ActiveProgram));

                    if (_activeProgram != null)
                    {
                        _activeProgram.IsActive = true;
                    }
                }
            }
        }

        private bool _hasChanged;
        /// <summary>
        /// Indicates anything in the Midi Programs and Categories has changed.
        /// </summary>
        /// <remarks>The framework will automatically reset this property (false)
        /// when the host has inquired if the names have changed.</remarks>
        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                SetProperty(value, ref _hasChanged, nameof(HasChanged));
            }
        }

        private void OnCollectionItemAdded(INotifyPropertyChanged item)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(CollectionItem_PropertyChanged);
        }

        private void OnCollectionItemRemoved(INotifyPropertyChanged item)
        {
            item.PropertyChanged -= new PropertyChangedEventHandler(CollectionItem_PropertyChanged);
        }

        private void CollectionItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanged = true;
        }

        //---------------------------------------------------------------------

        private sealed class CollectionChangeManager<ItemT> : IDisposable
        {
            private readonly INotifyCollectionChanged _collection;
            private readonly Action<ItemT> _addItemHandler;
            private readonly Action<ItemT> _removeItemHandler;
            private List<ItemT> _items;

            public CollectionChangeManager(INotifyCollectionChanged collection, Action<ItemT> addHandler, Action<ItemT> removeHandler)
            {
                _collection = collection;
                _addItemHandler = addHandler;
                _removeItemHandler = removeHandler;

                _collection.CollectionChanged += new NotifyCollectionChangedEventHandler(Collection_CollectionChanged);

                _items = InitializeItems();
            }

            private List<ItemT> InitializeItems()
            {
                var items = new List<ItemT>();
                var enumerator = _collection as IEnumerable<ItemT>;

                if (enumerator != null)
                {
                    items.AddRange(enumerator);
                }

                return items;
            }

            private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems != null)
                {
                    foreach (ItemT item in e.NewItems)
                    {
                        _addItemHandler(item);
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (ItemT item in e.OldItems)
                    {
                        _removeItemHandler(item);
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    var newItems = InitializeItems();

                    foreach (ItemT item in _items)
                    {
                        if (!newItems.Contains(item))
                        {
                            _removeItemHandler(item);
                        }
                    }

                    foreach (ItemT item in newItems)
                    {
                        if (!_items.Contains(item))
                        {
                            _addItemHandler(item);
                        }
                    }

                    _items = newItems;
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                foreach (ItemT item in _items)
                {
                    _removeItemHandler(item);
                }
            }

            #endregion
        }
    }
}
