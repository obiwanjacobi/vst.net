using Jacobi.Vst.Core;
using Jacobi.Vst.Host.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.Host
{
    partial class PluginForm : Form
    {
        public PluginForm()
        {
            InitializeComponent();
        }

        public VstPluginContext PluginContext { get; set; }

        private void DataToForm()
        {
            FillPropertyList();
            FillProgramList();
            FillProgram();
            FillParameterList();
        }

        private void FillPropertyList()
        {
            PluginPropertyListVw.Items.Clear();

            // plugin product
            AddProperty("Plugin Name", PluginContext.PluginCommandStub.Commands.GetEffectName());
            AddProperty("Product", PluginContext.PluginCommandStub.Commands.GetProductString());
            AddProperty("Vendor", PluginContext.PluginCommandStub.Commands.GetVendorString());
            AddProperty("Vendor Version", PluginContext.PluginCommandStub.Commands.GetVendorVersion().ToString());
            AddProperty("Vst Support", PluginContext.PluginCommandStub.Commands.GetVstVersion().ToString());
            AddProperty("Plugin Category", PluginContext.PluginCommandStub.Commands.GetCategory().ToString());

            // plugin info
            AddProperty("Flags", PluginContext.PluginInfo.Flags.ToString());
            AddProperty("Plugin ID", PluginContext.PluginInfo.PluginID.ToString());
            AddProperty("Plugin Version", PluginContext.PluginInfo.PluginVersion.ToString());
            AddProperty("Audio Input Count", PluginContext.PluginInfo.AudioInputCount.ToString());
            AddProperty("Audio Output Count", PluginContext.PluginInfo.AudioOutputCount.ToString());
            AddProperty("Initial Delay", PluginContext.PluginInfo.InitialDelay.ToString());
            AddProperty("Program Count", PluginContext.PluginInfo.ProgramCount.ToString());
            AddProperty("Parameter Count", PluginContext.PluginInfo.ParameterCount.ToString());
            AddProperty("Tail Size", PluginContext.PluginCommandStub.Commands.GetTailSize().ToString());

            // can do
            AddProperty("CanDo:" + VstPluginCanDo.Bypass, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Bypass)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.MidiProgramNames, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.MidiProgramNames)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.Offline, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Offline)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.ReceiveVstEvents, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstEvents)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.ReceiveVstMidiEvent, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstMidiEvent)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.ReceiveVstTimeInfo, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstTimeInfo)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.SendVstEvents, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.SendVstEvents)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.SendVstMidiEvent, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.SendVstMidiEvent)).ToString());

            AddProperty("CanDo:" + VstPluginCanDo.ConformsToWindowRules, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.ConformsToWindowRules)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.Metapass, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Metapass)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.MixDryWet, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.MixDryWet)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.Multipass, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.Multipass)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.NoRealTime, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.NoRealTime)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.PlugAsChannelInsert, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.PlugAsChannelInsert)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.PlugAsSend, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.PlugAsSend)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.SendVstTimeInfo, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.SendVstTimeInfo)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x1in1out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x1in1out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x1in2out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x1in2out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x2in1out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x2in1out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x2in2out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x2in2out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x2in4out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x2in4out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x4in2out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x4in2out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x4in4out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x4in4out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x4in8out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x4in8out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x8in4out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x8in4out)).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.x8in8out, PluginContext.PluginCommandStub.Commands.CanDo(VstCanDoHelper.ToString(VstPluginCanDo.x8in8out)).ToString());
        }

        private void AddProperty(string propName, string propValue)
        {
            var lvItem = new ListViewItem(propName);
            lvItem.SubItems.Add(propValue);

            PluginPropertyListVw.Items.Add(lvItem);
        }

        private void FillProgramList()
        {
            ProgramListCmb.Items.Clear();

            for (int index = 0; index < PluginContext.PluginInfo.ProgramCount; index++)
            {
                ProgramListCmb.Items.Add(
                    PluginContext.PluginCommandStub.Commands.GetProgramNameIndexed(index));
            }
        }

        private void FillProgram()
        {
            ProgramIndexNud.Value = PluginContext.PluginCommandStub.Commands.GetProgram();
            ProgramListCmb.Text = PluginContext.PluginCommandStub.Commands.GetProgramName();
        }

        private void FillParameterList()
        {
            PluginParameterListVw.Items.Clear();

            for (int i = 0; i < PluginContext.PluginInfo.ParameterCount; i++)
            {
                string name = PluginContext.PluginCommandStub.Commands.GetParameterName(i);
                string label = PluginContext.PluginCommandStub.Commands.GetParameterLabel(i);
                string display = PluginContext.PluginCommandStub.Commands.GetParameterDisplay(i);

                AddParameter(name, display, label, String.Empty);
            }
        }

        private void AddParameter(string paramName, string paramValue, string label, string shortLabel)
        {
            var lvItem = new ListViewItem(paramName);
            lvItem.SubItems.Add(paramValue);
            lvItem.SubItems.Add(label);
            lvItem.SubItems.Add(shortLabel);

            PluginParameterListVw.Items.Add(lvItem);
        }

        private void PluginForm_Load(object sender, EventArgs e)
        {
            if (PluginContext == null)
            {
                Close();
            }
            else
            {
                DataToForm();
            }
        }

        private void ProgramIndexNud_ValueChanged(object sender, EventArgs e)
        {
            if (ProgramIndexNud.Value < PluginContext.PluginInfo.ProgramCount &&
                ProgramIndexNud.Value >= 0)
            {
                PluginContext.PluginCommandStub.Commands.SetProgram((int)ProgramIndexNud.Value);

                FillProgram();
                FillParameterList();
            }
        }

        private void GenerateNoiseBtn_Click(object sender, EventArgs e)
        {
            // plugin does not support processing audio
            if ((PluginContext.PluginInfo.Flags & VstPluginFlags.CanReplacing) == 0)
            {
                MessageBox.Show(this, "This plugin does not process any audio.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int inputCount = PluginContext.PluginInfo.AudioInputCount;
            int outputCount = PluginContext.PluginInfo.AudioOutputCount;
            int blockSize = 1024;

            // wrap these in using statements to automatically call Dispose and cleanup the unmanaged memory.
            using var inputMgr = new VstAudioBufferManager(inputCount, blockSize);
            using var outputMgr = new VstAudioBufferManager(outputCount, blockSize);

            var rnd = new Random((int)DateTime.Now.Ticks);

            foreach (VstAudioBuffer buffer in inputMgr.Buffers)
            {
                var span = buffer.AsSpan();
                for (int i = 0; i < blockSize; i++)
                {
                    // generate a value between -1.0 and 1.0
                    //buffer[i] = (float)((rnd.NextDouble() * 2.0) - 1.0);
                    span[i] = (float)((rnd.NextDouble() * 2.0) - 1.0);
                }
            }

            PluginContext.PluginCommandStub.Commands.SetBlockSize(blockSize);
            PluginContext.PluginCommandStub.Commands.SetSampleRate(44100f);

            VstAudioBuffer[] inputBuffers = inputMgr.Buffers.ToArray();
            VstAudioBuffer[] outputBuffers = outputMgr.Buffers.ToArray();

            PluginContext.PluginCommandStub.Commands.MainsChanged(true);
            PluginContext.PluginCommandStub.Commands.StartProcess();
            PluginContext.PluginCommandStub.Commands.ProcessReplacing(inputBuffers, outputBuffers);
            PluginContext.PluginCommandStub.Commands.StopProcess();
            PluginContext.PluginCommandStub.Commands.MainsChanged(false);

            for (int i = 0; i < inputBuffers.Length && i < outputBuffers.Length; i++)
            {
                for (int j = 0; j < blockSize; j++)
                {
                    if (inputBuffers[i][j] != outputBuffers[i][j])
                    {
                        if (outputBuffers[i][j] != 0.0)
                        {
                            MessageBox.Show(this, "The plugin has processed the audio.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }

            MessageBox.Show(this, "The plugin has passed the audio unchanged to its outputs.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateMidiBtn_Click(object sender, EventArgs e)
        {
            // plugin does not support processing midi
            if (PluginContext.PluginCommandStub.Commands.CanDo(
                VstCanDoHelper.ToString(VstPluginCanDo.ReceiveVstMidiEvent)) != VstCanDoResult.Yes)
            {
                MessageBox.Show(this, "This plugin does not process any midi.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var noteCount = 10;
            var midiNoteOns = 
                Enumerable.Range(60, noteCount).Select(n => new byte[] { 0x81, (byte)n, 100 });
            var midiNoteOffs =
                Enumerable.Range(60, noteCount).Select(n => new byte[] { 0x80, (byte)n, 0 });

            int inputCount = PluginContext.PluginInfo.AudioInputCount;
            int outputCount = PluginContext.PluginInfo.AudioOutputCount;
            int blockSize = 256;

            // Most MIDI plugins also implement a (dummy) audio processor.
            // So we allocate (dummy) audio buffers too...
            using var inputMgr = new VstAudioBufferManager(inputCount, blockSize);
            using var outputMgr = new VstAudioBufferManager(outputCount, blockSize);

            PluginContext.PluginCommandStub.Commands.SetBlockSize(blockSize);
            PluginContext.PluginCommandStub.Commands.SetSampleRate(44100f);

            VstAudioBuffer[] inputBuffers = inputMgr.Buffers.ToArray();
            VstAudioBuffer[] outputBuffers = outputMgr.Buffers.ToArray();

            PluginContext.PluginCommandStub.Commands.MainsChanged(true);
            PluginContext.PluginCommandStub.Commands.StartProcess();

            var events = midiNoteOns.Select(note => new VstMidiEvent(0, 20, 0, note, 0, 0));

            if (!PluginContext.PluginCommandStub.Commands.ProcessEvents(events.ToArray()))
                MessageBox.Show(this, "The plugin returned false on ProcessEvents of note-on messages.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            PluginContext.PluginCommandStub.Commands.ProcessReplacing(inputBuffers, outputBuffers);

            events = midiNoteOffs.Select(note => new VstMidiEvent(0, 0, 0, note, 0, 0));

            if (!PluginContext.PluginCommandStub.Commands.ProcessEvents(events.ToArray()))
                MessageBox.Show(this, "The plugin returned false on ProcessEvents of note-off messages.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            PluginContext.PluginCommandStub.Commands.ProcessReplacing(inputBuffers, outputBuffers);

            PluginContext.PluginCommandStub.Commands.StopProcess();
            PluginContext.PluginCommandStub.Commands.MainsChanged(false);

            MessageBox.Show(this, "The plugin has processed the midi events.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditorBtn_Click(object sender, EventArgs e)
        {
            var dlg = new EditorFrame
            {
                PluginCommandStub = PluginContext.PluginCommandStub,
                HostCommandStub = (DummyHostCommandStub)PluginContext.HostCommandStub
            };

            

            //PluginContext.PluginCommandStub.Commands.MainsChanged(true);
            dlg.ShowDialog(this);
            //PluginContext.PluginCommandStub.Commands.MainsChanged(false);
        }
    }
}
