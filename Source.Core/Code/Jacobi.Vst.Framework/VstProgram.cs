namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;
    using System;
    using System.Collections;

    /// <summary>
    /// The VstProgram class represents one plugin program.
    /// </summary>
    /// <remarks>A plugin program contains all plugin parameter but with different values than other programs.
    /// For this reason the VstProgram implements the <see cref="IVstPluginParameters"/> interface.</remarks>
    public class VstProgram : ObservableObject, IVstPluginParameters, IActivatable, IDisposable
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <remarks>The <see cref="Categories"/> are automatically filled as <see cref="VstParameter"/> instances
        /// are added to the <see cref="Parameters"/> collection.</remarks>
        public VstProgram()
        {
            Categories = new VstParameterCategoryCollection();

            Parameters.CollectionChanged += new EventHandler<NotifyCollectionChangedEventArgs>(Parameters_CollectionChanged);
        }

        /// <summary>
        /// Constructs a new instance based on a collection of parameter <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">Must not be null.</param>
        public VstProgram(VstParameterCategoryCollection categories)
        {
            Throw.IfArgumentIsNull(categories, nameof(categories));

            Categories = categories;
        }

        /// <summary>
        /// True if the program is a readonly factory preset.
        /// </summary>
        /// <remarks>
        /// Read-only programs do not allow their parameters to change value.
        /// </remarks>
        public bool IsReadOnly { get; set; }

        private string _name;
        /// <summary>
        /// Gets or sets the name of the plugin program.
        /// </summary>
        /// <remarks>The Name must not exceed 24 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxProgramNameLength, nameof(Name));

                SetProperty(value, ref _name, nameof(Name));
            }
        }

        #region IVstPluginParameters Members

        /// <summary>
        /// Gets a collection of parameter categories that were pass in the constructor.
        /// </summary>
        public VstParameterCategoryCollection Categories { get; private set; }

        private VstParameterCollection _parameters;
        /// <summary>
        /// Gets a collection of parameter instances that defines the program.
        /// </summary>
        /// <remarks>The program does nothing to fill the collection. 
        /// The plugin logic should fill the parameters.</remarks>
        public VstParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new VstParameterCollection();
                }

                return _parameters;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes a plugin program.
        /// </summary>
        /// <remarks>Also disposes all <see cref="VstParameter"/> instances in the <see cref="Parameters"/> collection.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _name = null;
            Categories = null;

            if (_parameters != null)
            {
                _parameters.Clear();    // disposes VstParameter instances
                _parameters = null;
            }
        }

        #endregion

        private void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IEnumerable parameters;

            if (e.NewItems != null)
            {
                parameters = e.NewItems;
            }
            else
            {
                parameters = Parameters;

                // TODO: this will trigger a lot of CollectionChanged events...
                Categories.Clear();
            }

            foreach (VstParameter parameter in parameters)
            {
                // add category to collection if not present yet
                if (parameter.Info.Category != null &&
                    !Categories.Contains(parameter.Info.Category.Name))
                {
                    Categories.Add(parameter.Info.Category);
                }
            }
        }

        #region IActivatable Members

        /// <summary>
        /// Gets or sets an indication if the program is active (true).
        /// </summary>
        public bool IsActive
        {
            get
            {
                return Parameters.IsActive;
            }
            set
            {
                Parameters.IsActive = value;
            }
        }

        #endregion
    }
}
