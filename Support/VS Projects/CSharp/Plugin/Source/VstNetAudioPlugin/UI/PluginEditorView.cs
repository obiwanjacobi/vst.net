using System.Collections.Generic;
using System.Windows.Forms;
using Jacobi.Vst.Framework;

namespace VstNetAudioPlugin.UI
{
    public partial class PluginEditorView : UserControl
    {
        public PluginEditorView()
        {
            InitializeComponent();
        }

        internal bool InitializeParameters(List<VstParameterManager> parameters)
        {
            if (parameters == null || parameters.Count < 4) return false;

            BindParameter(parameters[0], label1, trackBar1, label5);
            BindParameter(parameters[1], label2, trackBar2, label6);
            BindParameter(parameters[2], label3, trackBar3, label7);
            BindParameter(parameters[3], label4, trackBar4, label8);

            return true;
        }

        private void BindParameter(VstParameterManager paramMgr, Label label, TrackBar trackBar, Label shortLabel)
        {
            // NOTE: This code works best with integer parameter values.
            label.Text = paramMgr.ParameterInfo.Name;
            shortLabel.Text = paramMgr.ParameterInfo.ShortLabel;

            if (paramMgr.ParameterInfo.IsStepIntegerValid)
            {
                trackBar.LargeChange = paramMgr.ParameterInfo.LargeStepInteger;
                trackBar.SmallChange = paramMgr.ParameterInfo.StepInteger;
            }

            if (paramMgr.ParameterInfo.IsMinMaxIntegerValid)
            {
                trackBar.Minimum = paramMgr.ParameterInfo.MinInteger;
                trackBar.Maximum = paramMgr.ParameterInfo.MaxInteger;
            }

            // use databinding for VstParameter/Manager changed notifications.
            trackBar.DataBindings.Add("Value", paramMgr, "ActiveParameter.Value");
            trackBar.ValueChanged += new System.EventHandler(TrackBar_ValueChanged);
            trackBar.Tag = paramMgr;
        }

        private void TrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            var trackBar = (TrackBar)sender;
            var paramMgr = (VstParameterManager)trackBar.Tag;

            paramMgr.ActiveParameter.Value = trackBar.Value;
        }

        internal void ProcessIdle()
        {
            // TODO: short idle processing here
        }
    }
}
