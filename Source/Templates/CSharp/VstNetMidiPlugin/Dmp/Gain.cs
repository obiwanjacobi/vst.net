using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;

namespace VstNetMidiPlugin.Dmp
{
    /// <summary>
    /// Change the velocity of MIDI notes.
    /// </summary>
    internal sealed class Gain
    {
        private const string ParameterCategoryName = "Gain";

        private readonly Plugin _plugin;

        public Gain(Plugin plugin)
        {
            _plugin = plugin;

            InitializeParameters();

            _plugin.Opened += new System.EventHandler(Plugin_Opened);
        }

        private void Plugin_Opened(object sender, System.EventArgs e)
        {
            GainMgr.HostAutomation = _plugin.Host.GetInstance<IVstHostAutomation>();

            _plugin.Opened -= new System.EventHandler(Plugin_Opened);
        }

        public VstParameterManager GainMgr { get; private set; }

        private void InitializeParameters()
        {
            // all parameter definitions are added to a central list.
            VstParameterInfoCollection parameterInfos = _plugin.PluginPrograms.ParameterInfos;

            // retrieve the category for all delay parameters.
            VstParameterCategory paramCategory =
                _plugin.PluginPrograms.GetParameterCategory(ParameterCategoryName);

            // delay time parameter
            VstParameterInfo paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = true,
                Name = "Gain",
                Label = "Db",
                ShortLabel = "Db",
                MinInteger = -100,
                MaxInteger = 100,
                LargeStepFloat = 20.0f,
                SmallStepFloat = 1.0f,
                StepFloat = 10.0f,
                DefaultValue = 0.0f
            };
            GainMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);
        }

        public VstMidiEvent ProcessEvent(VstMidiEvent inEvent)
        {
            if (!MidiHelper.IsNoteOff(inEvent.Data) &&
                !MidiHelper.IsNoteOn(inEvent.Data))
            {
                return inEvent;
            }

            byte[] outData = new byte[4];
            inEvent.Data.CopyTo(outData, 0);

            outData[2] += (byte)GainMgr.CurrentValue;

            if (outData[2] > 127)
            {
                outData[2] = 127;
            }

            if (outData[2] < 0)
            {
                outData[2] = 0;
            }

            // MidiEvents are immutable, 
            // so we create a new object for the new data.
            VstMidiEvent outEvent = new VstMidiEvent(
                inEvent.DeltaFrames, inEvent.NoteLength, inEvent.NoteOffset,
                outData, inEvent.Detune, inEvent.NoteOffVelocity);

            return outEvent;
        }
    }
}
