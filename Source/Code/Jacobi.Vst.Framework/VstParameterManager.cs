﻿namespace Jacobi.Vst.Framework
{
    using System;
    using System.ComponentModel;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// catches parameter changes and communicates these back to the source component
    /// </summary>
    public class VstParameterManager : ObservableObject
    {
        /// <summary>
        /// Constructs a new instance based on the parameter type information.
        /// </summary>
        /// <param name="parameterInfo">Must not be null.</param>
        /// <remarks>The <see cref="VstParameterInfo.ParameterManager"/> property is set to <b>this</b> instance.</remarks>
        public VstParameterManager(VstParameterInfo parameterInfo)
        {
            Throw.IfArgumentIsNull(parameterInfo, "parameterInfo");

            ParameterInfo = parameterInfo;
            ParameterInfo.ParameterManager = this;
        }

        /// <summary>
        /// Gets the meta data for the parameter this instance manages.
        /// </summary>
        public VstParameterInfo ParameterInfo { get; private set; }

        private VstParameter _activeParameter;
        /// <summary>
        /// Get the current active parameter instance.
        /// </summary>
        public VstParameter ActiveParameter
        {
            get { return _activeParameter; }
            private set
            {
                SetProperty(value, ref _activeParameter, "ActiveParameter");
            }
        }

        private float _currentValue;
        /// <summary>
        /// Gets the current parameter value.
        /// </summary>
        public float CurrentValue
        {
            get { return _currentValue; }
            private set
            {
                SetProperty(value, ref _currentValue, "CurrentValue");
            }
        }

        private float _previousValue;
        /// <summary>
        /// Gets the previous parameter value.
        /// </summary>
        public float PreviousValue
        {
            get { return _previousValue; }
            private set
            {
                SetProperty(value, ref _previousValue, "PreviousValue");
            }
        }

        /// <summary>
        /// Subscribes to the events of the <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">Must not be null.</param>
        public void SubscribeTo(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, "parameter");

            parameter.PropertyChanged += new PropertyChangedEventHandler(Parameter_PropertyChanged);
        }

        /// <summary>
        /// Changes the <see cref="CurrentValue"/> and <see cref="PreviousValue"/> properties.
        /// </summary>
        /// <param name="newValue">The new value of the parameter.</param>
        /// <remarks>
        /// If you wish to implement parameter value smoothing 
        /// (where changes in value are smoothed out over time),
        /// this is the place to call the smoothing logic.
        /// </remarks>
        protected virtual void ChangeValue(float newValue)
        {
            PreviousValue = CurrentValue;
            CurrentValue = newValue;
        }

        private void Parameter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            VstParameter currentParameter = sender as VstParameter;

            switch (e.PropertyName)
            {
                case "Value":
                    HandleParameterValueChanged(currentParameter);
                    break;
                case "IsActive":
                    HandleParameterActivityChanged(currentParameter);
                    break;
            }
        }

        /// <summary>
        /// Called when any parameter managed by this instance has changed value.
        /// </summary>
        /// <param name="parameter">Must not be null.</param>
        protected virtual void HandleParameterValueChanged(VstParameter parameter)
        {
            if (parameter != ActiveParameter) return;

            ChangeValue(parameter.Value);
        }

        /// <summary>
        /// Called when any parameter managed by this instance has changed the <see cref="IActivatable.IsActive"/> property.
        /// </summary>
        /// <param name="parameter">Must not be null.</param>
        protected virtual void HandleParameterActivityChanged(VstParameter parameter)
        {
            if (parameter == ActiveParameter && !parameter.IsActive)
            {
                ActiveParameter = null;
                ChangeValue(0.0f);
            }

            if (ActiveParameter == null && parameter.IsActive)
            {
                ActiveParameter = parameter;
                ChangeValue(parameter.Value);
            }
        }
    }
}
