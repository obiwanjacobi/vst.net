using Jacobi.Vst.Plugin.Framework;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.Delay.UI
{
    [SupportedOSPlatform("windows")]
    public partial class PluginEditorView : UserControl
    {
        public PluginEditorView()
        {
            InitializeComponent();
        }

        internal bool InitializeParameters(IList<VstParameterManager> parameters)
        {
            if (parameters == null || parameters.Count < 4)
                return false;

            BindParameter(parameters[0], label1, trackBar1, label5);
            BindParameter(parameters[1], label2, trackBar2, label6);
            BindParameter(parameters[2], label3, trackBar3, label7);
            BindParameter(parameters[3], label4, trackBar4, label8);

            return true;
        }

        private void BindParameter(VstParameterManager paramMgr, Label label, TrackBar trackBar, Label shortLabel)
        {
            label.Text = paramMgr.ParameterInfo.Name;
            shortLabel.Text = paramMgr.ParameterInfo.ShortLabel;

            var factor = InitTrackBar(trackBar, paramMgr.ParameterInfo);
            var paramState = new ParameterControlState(paramMgr, factor);

            // use databinding for VstParameter/Manager changed notifications.
            trackBar.DataBindings.Add("Value", paramState, nameof(ParameterControlState.Value));
            trackBar.ValueChanged += TrackBar_ValueChanged;
            trackBar.Tag = paramState;
        }

        private float InitTrackBar(TrackBar trackBar, VstParameterInfo parameterInfo)
        {
            // A multiplication factor to convert floats (0.0-1.0) 
            // to an integer range for the TrackBar to work with.
            float factor = 1.0f;

            if (parameterInfo.IsSwitch)
            {
                trackBar.Minimum = 0;
                trackBar.Maximum = 1;
                trackBar.LargeChange = 1;
                trackBar.SmallChange = 1;
                return factor;
            }

            if (parameterInfo.IsStepIntegerValid)
            {
                trackBar.LargeChange = parameterInfo.LargeStepInteger;
                trackBar.SmallChange = parameterInfo.StepInteger;
            }
            else if (parameterInfo.IsStepFloatValid)
            {
                factor = 1 / parameterInfo.StepFloat;
                trackBar.LargeChange = (int)(parameterInfo.LargeStepFloat * factor);
                trackBar.SmallChange = (int)(parameterInfo.StepFloat * factor);
            }

            if (parameterInfo.IsMinMaxIntegerValid)
            {
                trackBar.Minimum = (int)(parameterInfo.MinInteger * factor);
                trackBar.Maximum = (int)(parameterInfo.MaxInteger * factor);
            }
            else
            {
                trackBar.Minimum = 0;
                trackBar.Maximum = (int)factor;
            }

            return factor;
        }

        private void TrackBar_ValueChanged(object? sender, System.EventArgs e)
        {
            var trackBar = (TrackBar?)sender;
            var paramState = (ParameterControlState?)trackBar?.Tag;

            if (trackBar != null &&
                paramState?.ParameterManager.ActiveParameter != null)
            {
                paramState.ParameterManager.ActiveParameter.Value =
                    trackBar.Value / paramState.ValueFactor;
            }
        }

        internal void ProcessIdle()
        {
            // TODO: short idle processing here
        }

        /// <summary>
        /// This class converts the parameter value range to a compatible (integer) TrackBar value range.
        /// </summary>
        private sealed class ParameterControlState
        {
            public ParameterControlState(VstParameterManager parameterManager, float valueFactor)
            {
                ParameterManager = parameterManager;
                ValueFactor = valueFactor;
            }

            public VstParameterManager ParameterManager { get; }
            public float ValueFactor { get; }

            public int Value
            {
                get
                {
                    return (int)(ParameterManager.CurrentValue * ValueFactor);
                }
            }
        }
    }
}
