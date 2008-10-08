namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// catches parameter changes and communicates these back to the source component
    /// </summary>
    public class VstParameterManager
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
        /// <summary>
        /// Get the current active parameter instance.
        /// </summary>
        public VstParameter ActiveParameter { get; private set; }
        /// <summary>
        /// Gets the current parameter value.
        /// </summary>
        public float CurrentValue { get; private set; }
        /// <summary>
        /// Gets the previous parameter value.
        /// </summary>
        public float PreviousValue { get; private set; }

        /// <summary>
        /// Subscribes to the events of the <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">Must not be null.</param>
        public void SubscribeTo(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, "parameter");

            parameter.ValueChangedCallback = new EventHandler<EventArgs>(Parameter_ValueChanged);
            parameter.ActivationChangedCallback = new EventHandler<EventArgs>(Parameter_ActivationChanged);
        }

        /// <summary>
        /// Changes the <see cref="CurrentValue"/> and <see cref="PreviousValue"/> properties.
        /// </summary>
        /// <param name="newValue">The new value of the parameter.</param>
        public void ChangeValue(float newValue)
        {
            PreviousValue = CurrentValue;
            CurrentValue = newValue;

            OnValueChanged();
        }

        /// <summary>
        /// The ValueChanged event is raised after the parameter value has changed on the manager.
        /// </summary>
        public event EventHandler<EventArgs> ValueChanged;

        /// <summary>
        /// Raises the <see cref="ValueChanged"/> event.
        /// </summary>
        protected virtual void OnValueChanged()
        {
            EventHandler<EventArgs> handler = ValueChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the changing of the normalized value.
        /// </summary>
        /// <param name="sender">A reference to VstParameter.</param>
        /// <param name="e"><see cref="EventArgs.Empty"/>.</param>
        private void Parameter_ValueChanged(object sender, EventArgs e)
        {
            VstParameter currentParameter = sender as VstParameter;

            // early exit when change is done on an inactive parameter
            if(currentParameter != ActiveParameter) return;

            ChangeValue(currentParameter.Value);
        }

        /// <summary>
        /// Handles the (de)activation of parameter instances.
        /// </summary>
        /// <param name="sender">A reference to VstParameter.</param>
        /// <param name="e"><see cref="EventArgs.Empty"/>.</param>
        private void Parameter_ActivationChanged(object sender, EventArgs e)
        {
            VstParameter currentParameter = sender as VstParameter;

            if (currentParameter == ActiveParameter && !currentParameter.IsActive)
            {
                ActiveParameter = null;
                ChangeValue(0.0f);
            }

            if (ActiveParameter == null && currentParameter.IsActive)
            {
                ActiveParameter = currentParameter;
                ChangeValue(currentParameter.Value);
            }
        }
    }
}
