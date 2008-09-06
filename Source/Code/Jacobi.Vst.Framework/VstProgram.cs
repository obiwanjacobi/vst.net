namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstProgram class represents one plugin program.
    /// </summary>
    /// <remarks>A plugin program contains all plugin parameter but with different values than other programs.
    /// For this reason the VstProgram implements the <see cref="IVstPluginParameters"/> interface.</remarks>
    public class VstProgram : IVstPluginParameters, IDisposable
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <remarks>The <see cref="Categories"/> are automatically filled as <see cref="VstParameters"/> 
        /// are added to the <see cref="Parameters"/> collection.</remarks>
        public VstProgram()
        {
            Categories = new VstParameterCategoryCollection();
            Parameters.Changed += new EventHandler<EventArgs>(Parameters_Changed);
        }

        /// <summary>
        /// Constructs a new instance based on a collection of parameter <paramref name="categories"/>.
        /// </summary>
        /// <param name="categories">Must not be null.</param>
        public VstProgram(VstParameterCategoryCollection categories)
        {
            Throw.IfArgumentIsNull(categories, "categories");

            Categories = categories;
        }

        private string _name;
        /// <summary>
        /// Gets or sets the name of the plugin program.
        /// </summary>
        /// <remarks>The Name must not exceed 23 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxProgramNameLength, "Name");

                _name = value;
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
        public virtual void Dispose()
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

        private void Parameters_Changed(object sender, EventArgs e)
        {
            foreach (VstParameter parameter in Parameters)
            {
                if (parameter.Info.Category != null)
                {
                    // add category to collection if not present yet
                    if (!Categories.Contains(parameter.Info.Category.Name))
                    {
                        Categories.Add(parameter.Info.Category);
                    }
                }
            }
        }
    }
}
