using System;
using System.Windows.Forms;

using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;

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
            FillProgram();
            FillParameterList();
        }

        private void FillPropertyList()
        {
            PluginPropertyListVw.Items.Clear();

            // plugin product
            AddProperty("Plugin Name", PluginContext.PluginCommandStub.GetEffectName());
            AddProperty("Product", PluginContext.PluginCommandStub.GetProductString());
            AddProperty("Vendor", PluginContext.PluginCommandStub.GetVendorString());
            AddProperty("Vendor Version", PluginContext.PluginCommandStub.GetVendorVersion().ToString());
            AddProperty("Vst Support", PluginContext.PluginCommandStub.GetVstVersion().ToString());
            AddProperty("Plugin Category", PluginContext.PluginCommandStub.GetCategory().ToString());
            
            // plugin info
            AddProperty("Flags", PluginContext.PluginInfo.Flags.ToString());
            AddProperty("Plugin ID", PluginContext.PluginInfo.PluginID.ToString());
            AddProperty("Plugin Version", PluginContext.PluginInfo.PluginVersion.ToString());
            AddProperty("Audio Input Count", PluginContext.PluginInfo.AudioInputCount.ToString());
            AddProperty("Audio Output Count", PluginContext.PluginInfo.AudioOutputCount.ToString());
            AddProperty("Initial Delay", PluginContext.PluginInfo.InitialDelay.ToString());
            AddProperty("Program Count", PluginContext.PluginInfo.ProgramCount.ToString());
            AddProperty("Parameter Count", PluginContext.PluginInfo.ParameterCount.ToString());
            AddProperty("Tail Size", PluginContext.PluginCommandStub.GetTailSize().ToString());

            // can do
            AddProperty("CanDo:" + VstPluginCanDo.Bypass, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.Bypass.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.MidiProgramNames, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.MidiProgramNames.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.Offline, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.Offline.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.ReceiveVstEvents, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.ReceiveVstEvents.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.ReceiveVstMidiEvent, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.ReceiveVstMidiEvent.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.ReceiveVstTimeInfo, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.ReceiveVstTimeInfo.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.SendVstEvents, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.SendVstEvents.ToString()).ToString());
            AddProperty("CanDo:" + VstPluginCanDo.SendVstMidiEvent, PluginContext.PluginCommandStub.CanDo(VstPluginCanDo.SendVstMidiEvent.ToString()).ToString());
        }

        private void AddProperty(string propName, string propValue)
        {
            ListViewItem lvItem = new ListViewItem(propName);
            lvItem.SubItems.Add(propValue);

            PluginPropertyListVw.Items.Add(lvItem);
        }

        private void FillProgram()
        {
            ProgramIndexNud.Value = PluginContext.PluginCommandStub.GetProgram();
            ProgramNameTxt.Text = PluginContext.PluginCommandStub.GetProgramName();
        }

        private void FillParameterList()
        {
            PluginParameterListVw.Items.Clear();

            for (int i = 0; i < PluginContext.PluginInfo.ParameterCount; i++)
            {
                string name = PluginContext.PluginCommandStub.GetParameterName(i);
                string label = PluginContext.PluginCommandStub.GetParameterLabel(i);
                string display = PluginContext.PluginCommandStub.GetParameterDisplay(i);

                AddParameter(name, display, label, String.Empty);
            }
        }

        private void AddParameter(string paramName, string paramValue, string label, string shortLabel)
        {
            ListViewItem lvItem = new ListViewItem(paramName);
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
                PluginContext.PluginCommandStub.SetProgram((int)ProgramIndexNud.Value);

                FillProgram();
                FillParameterList();
            }
        }

        private void GenerateNoiseBtn_Click(object sender, EventArgs e)
        {
            // plugin does not support processing audio
            if ((PluginContext.PluginInfo.Flags & VstPluginFlags.CanReplacing) == 0)
            {
                return;
            }

            int inputCount = PluginContext.PluginInfo.AudioInputCount;
            int outputCount = PluginContext.PluginInfo.AudioOutputCount;
            int blockSize = 1024;

            VstAudioBufferManager inputMgr = new VstAudioBufferManager(inputCount, blockSize);
            VstAudioBufferManager outputMgr = new VstAudioBufferManager(outputCount, blockSize);

            foreach (VstAudioBuffer buffer in inputMgr.ToArray())
            {
                Random rnd = new Random((int)DateTime.Now.Ticks);

                for (int i = 0; i < blockSize; i++)
                {
                    // generate a value between -1.0 and 1.0
                    buffer[i] = (float)((rnd.NextDouble() * 2.0) - 1.0);
                }
            }

            PluginContext.PluginCommandStub.SetBlockSize(blockSize);
            PluginContext.PluginCommandStub.SetSampleRate(44.8f);

            PluginContext.PluginCommandStub.StartProcess();
            PluginContext.PluginCommandStub.ProcessReplacing(inputMgr.ToArray(), outputMgr.ToArray());
            PluginContext.PluginCommandStub.StopProcess();
        }

    }
}
