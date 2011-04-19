using Jacobi.Vst.Framework;
using Jacobi.Vst.Core;

namespace VstNetMidiPlugin.Dmp
{
    internal sealed class Transpose
    {
        private static readonly string ParameterCategoryName = "Trasnpose";

        private Plugin _plugin;

        public Transpose(Plugin plugin)
        {
            _plugin = plugin;

            InitializeParameters();

            _plugin.Opened += new System.EventHandler(Plugin_Opened);
        }

        private void Plugin_Opened(object sender, System.EventArgs e)
        {
            TransposeMgr.HostAutomation = _plugin.Host.GetInstance<IVstHostAutomation>();

            _plugin.Opened -= new System.EventHandler(Plugin_Opened);
        }

        public VstParameterManager TransposeMgr { get; private set; }

        private void InitializeParameters()
        {
            // all parameter definitions are added to a central list.
            VstParameterInfoCollection parameterInfos = _plugin.PluginPrograms.ParameterInfos;

            // retrieve the category for all delay parameters.
            VstParameterCategory paramCategory =
                _plugin.PluginPrograms.GetParameterCategory(ParameterCategoryName);

            // delay time parameter
            VstParameterInfo paramInfo = new VstParameterInfo();
            paramInfo.Category = paramCategory;
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "Transp.";
            paramInfo.Label = "Halfs";
            paramInfo.ShortLabel = "#";
            paramInfo.MinInteger = -100;
            paramInfo.MaxInteger = 100;
            paramInfo.LargeStepFloat = 5.0f;
            paramInfo.SmallStepFloat = 1.0f;
            paramInfo.StepFloat = 2.0f;
            paramInfo.DefaultValue = 0.0f;
            TransposeMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);
        }

        public VstMidiEvent ProcessEvent(VstMidiEvent inEvent)
        {
            if (!MidiHelper.IsNoteOff(inEvent.Data) && !MidiHelper.IsNoteOn(inEvent.Data))
            {
                return inEvent;
            }

            byte[] outData = new byte[4];
            inEvent.Data.CopyTo(outData, 0);

            outData[1] += (byte)TransposeMgr.CurrentValue;

            if (outData[1] > 127)
            {
                outData[1] = 127;
            }

            if (outData[1] < 0)
            {
                outData[1] = 0;
            }

            VstMidiEvent outEvent = new VstMidiEvent(
                inEvent.DeltaFrames, inEvent.NoteLength, inEvent.NoteOffset, 
                outData, inEvent.Detune, inEvent.NoteOffVelocity);

            return outEvent;
        }
    }
}
