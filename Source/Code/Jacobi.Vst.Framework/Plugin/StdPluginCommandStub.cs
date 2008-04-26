namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using Jacobi.Vst.Core;

    public abstract class StdPluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginContext _pluginCtx;

        #region IVstPluginCommandStub Members

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            return false;
        }

        public int GetNumberOfMidiInputChannels()
        {
            return 0;
        }

        public int GetNumberOfMidiOutputChannels()
        {
            return 0;
        }

        #endregion

        #region IVstPluginCommandStub23 Members

        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            input = null;
            output = null;
            return false;
        }

        public int SetTotalSamplesToProcess(int numberOfSamples)
        {
            return 0;
        }

        public int GetNextPlugin(out string name)
        {
            IVstPluginHost pluginHost = _pluginCtx.Plugin.GetInstance<IVstPluginHost>();

            if (pluginHost != null)
            {

            }

            name = null;
            return 0;
        }

        public int StartProcess()
        {
            return 0;
        }

        public int StopProcess()
        {
            return 0;
        }

        public bool SetPanLaw(int type, float value)
        {
            return false;
        }

        public int BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            return 0;
        }

        public int BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            return 0;
        }

        #endregion

        #region IVstPluginCommandStub21 Members

        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.KeyDown(ascii, virtualKey, modifers);
                return true;
            }

            return false;
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.KeyUp(ascii, virtualKey, modifers);
                return true;
            }

            return false;
        }

        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.KnobMode = mode;
                return true;
            }

            return false;
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgramName, int channel)
        {
            IVstPluginMidiPrograms midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiPrograms != null)
            {
                VstMidiChannelInfo channelInfo = midiPrograms.ChannelInfos[channel];
                VstMidiProgram program = channelInfo.Programs[midiProgramName.CurrentProgramIndex];
                midiProgramName.Name = program.Name;
                midiProgramName.MidiProgram = (char)program.ProgramChange;
                midiProgramName.MidiBankMSB = (char)program.BankSelectMsb;
                midiProgramName.MidiBankLSB = (char)program.BankSelectLsb;

                if (program.ParentCategory != null)
                {
                    midiProgramName.ParentCategoryIndex = channelInfo.Categories.IndexOf(program.ParentCategory);
                }
                else
                {
                    midiProgramName.ParentCategoryIndex = -1;
                }

                return channelInfo.Programs.Count;
            }

            return 0;
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgramName, int channel)
        {
            IVstPluginMidiPrograms midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiPrograms != null)
            {
                VstMidiChannelInfo channelInfo = midiPrograms.ChannelInfos[channel];
                
                if (channelInfo.ActiveProgram != null)
                {
                    midiProgramName.CurrentProgramIndex = channelInfo.Programs.IndexOf(channelInfo.ActiveProgram);
                    midiProgramName.Name = channelInfo.ActiveProgram.Name;
                    midiProgramName.MidiProgram = (char)channelInfo.ActiveProgram.ProgramChange;
                    midiProgramName.MidiBankMSB = (char)channelInfo.ActiveProgram.BankSelectMsb;
                    midiProgramName.MidiBankLSB = (char)channelInfo.ActiveProgram.BankSelectLsb;

                    if (channelInfo.ActiveProgram.ParentCategory != null)
                    {
                        midiProgramName.ParentCategoryIndex = channelInfo.Categories.IndexOf(channelInfo.ActiveProgram.ParentCategory);
                    }
                    else
                    {
                        midiProgramName.ParentCategoryIndex = -1;
                    }
                }
                else
                {
                    midiProgramName.CurrentProgramIndex = -1;
                    //TODO: fix "cannot convert to char"
                    //midiProgramName.MidiProgram = 0xFFFF;
                    //midiProgramName.MidiBankMSB = -1;
                    //midiProgramName.MidiBankLSB = -1;
                    midiProgramName.ParentCategoryIndex = -1;
                }

                return channelInfo.Programs.Count;
            }

            return 0;
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            IVstPluginMidiPrograms midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiPrograms != null)
            {
                VstMidiChannelInfo channelInfo = midiPrograms.ChannelInfos[channel];
                VstMidiCategory progCat = channelInfo.Categories[midiCat.CurrentCategoryIndex];
                
                midiCat.Name = progCat.Name;
                if (progCat.ParentCategory != null)
                {
                    midiCat.ParentCategoryIndex = channelInfo.Categories.IndexOf(progCat.ParentCategory);
                }
                else
                {
                    midiCat.ParentCategoryIndex = -1;
                }

                return channelInfo.Categories.Count;
            }

            return 0;
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            IVstPluginMidiPrograms midiProgram = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiProgram != null)
            {
                //TODO: how do we know!?
            }

            return false;
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            //???
            IVstPluginMidiPrograms midiProgram = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiProgram != null)
            {
                //TODO:
            }

            return false;
        }

        public bool BeginSetProgram()
        {
            IVstPluginPrograms pluginProgram = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginProgram != null)
            {
                pluginProgram.BeginSetProgram();
                return true;
            }

            return false;
        }

        public bool EndSetProgram()
        {
            IVstPluginPrograms pluginProgram = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginProgram != null)
            {
                pluginProgram.EndSetProgram();
                return true;
            }

            return false;
        }

        #endregion

        #region IVstPluginCommandStub20 Members

        public bool ProcessEvents(VstEvent[] events)
        {
            IVstMidiProcessor midiProcessor = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>();

            if (midiProcessor != null)
            {
                midiProcessor.Process(new VstEventCollection(events));
                return true;
            }

            return false;
        }

        public bool CanParameterBeAutomated(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.CanBeAutomated;
            }

            return false;
        }

        public bool String2Parameter(int index, string str)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.ParseValue(str);
            }

            return false;
        }

        public bool GetProgramNameIndexed(int index, out string name)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null)
            {
                VstProgram program = pluginPrograms.Programs[index];
                name = program.Name;
                return true;
            }

            name = null;
            return false;
        }

        public bool GetInputProperties(int index, VstPinProperties pinProps)
        {
            // TODO
            return false;
        }

        public bool GetOutputProperties(int index, VstPinProperties pinProps)
        {
            // TODO
            return false;
        }

        public VstPluginCategory GetCategory()
        {
            return _pluginCtx.Plugin.Instance.Category;
        }

        public bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag)
        {
            // TODO
            return false;
        }

        public bool OfflinePrepare(int count)
        {
            // TODO
            return false;
        }

        public bool OfflineRun(int count)
        {
            // TODO
            return false;
        }

        public bool ProcessVariableIO(VstVariableIO variableIO)
        {
            // TODO
            return false;
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            // TODO
            return false;
        }

        public bool SetBypass(bool bypass)
        {
            IVstPluginBypass pluginBypass = _pluginCtx.Plugin.GetInstance<IVstPluginBypass>();

            if (pluginBypass != null)
            {
                pluginBypass.Bypass = bypass;
                return true;
            }

            return false;
        }

        public bool GetEffectName(out string name)
        {
            name = _pluginCtx.Plugin.Instance.Name;

            return !String.IsNullOrEmpty(name);
        }

        public bool GetVendorString(out string vendor)
        {
            vendor = _pluginCtx.Plugin.Instance.ProductInfo.Vendor;

            return !String.IsNullOrEmpty(vendor);
        }

        public bool GetProductString(out string product)
        {
            product = _pluginCtx.Plugin.Instance.ProductInfo.Product;

            return !String.IsNullOrEmpty(product);
        }

        public int GetVendorVersion()
        {
            return _pluginCtx.Plugin.Instance.ProductInfo.Version;
        }

        public VstCanDoResult CanDo(VstPluginCanDo cando)
        {
            VstCanDoResult result = VstCanDoResult.No;

            switch (cando)
            {
                case VstPluginCanDo.Bypass:
                    result = _pluginCtx.Plugin.GetInstance<IVstPluginBypass>() == null ? VstCanDoResult.No : VstCanDoResult.Yes;
                    break;
                case VstPluginCanDo.MidiProgramNames:
                    result = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>() == null ? VstCanDoResult.No : VstCanDoResult.Yes;
                    break;
                case VstPluginCanDo.Offline:
                    result = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>() == null ? VstCanDoResult.No : VstCanDoResult.Yes;
                    break;
                case VstPluginCanDo.ReceiveVstEvents:
                case VstPluginCanDo.ReceiveVstMidiEvent:
                    result = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>() == null ? VstCanDoResult.No : VstCanDoResult.Yes;
                    break;
                case VstPluginCanDo.ReceiveVstTimeInfo:
                    // TODO: define interface?
                    result = VstCanDoResult.Unknown;
                    break;
                case VstPluginCanDo.SendVstEvents:
                case VstPluginCanDo.SendVstMidiEvent:
                    // TODO: define new capability?
                    result = VstCanDoResult.Unknown;
                    break;
            }

            return result;
        }

        public int GetTailSize()
        {
            IVstAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstAudioProcessor>();

            if (audioProcessor != null)
            {
                return audioProcessor.TailSize;
            }

            return 0;
        }

        public bool GetParameterProperties(int index, VstParameterProperties paramProps)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                
                // TODO: fill paramProps

                //return true;
            }

            return false;
        }

        public int GetVstVersion()
        {
            return 2400;
        }

        #endregion

        #region IVstPluginCommandStub10 Members

        public void Open()
        {
            _pluginCtx.Plugin.Instance.Open(_pluginCtx.Host.Instance);
        }

        public void Close()
        {
            _pluginCtx.Plugin.Instance.Dispose();
            _pluginCtx.Host.Instance.Dispose();
            _pluginCtx = null;
        }

        public void SetProgram(int programNumber)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null)
            {
                VstProgram program = pluginPrograms.Programs[programNumber];
                pluginPrograms.ActiveProgram = program;
            }
        }

        public int GetProgram()
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null && 
                pluginPrograms.ActiveProgram != null)
            {
                return pluginPrograms.Programs.IndexOf(pluginPrograms.ActiveProgram);
            }

            return 0;
        }

        public void SetProgramName(string name)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null &&
                pluginPrograms.ActiveProgram != null)
            {
                pluginPrograms.ActiveProgram.Name = name;
            }
        }

        public string GetProgramName()
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null &&
                pluginPrograms.ActiveProgram != null)
            {
                return pluginPrograms.ActiveProgram.Name;
            }

            return null;
        }

        public string GetParameterLabel(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Label;
            }

            return null;
        }

        public string GetParameterDisplay(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.DisplayValue;
            }

            return null;
        }

        public string GetParameterName(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Name;
            }

            return null;
        }

        public void SetSampleRate(float sampleRate)
        {
            IVstAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.SampleRate = sampleRate;
            }
        }

        public void SetBlockSize(int blockSize)
        {
            IVstAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.BlockSize = blockSize;
            }
        }

        public void MainsChanged(bool onoff)
        {
            IVstPlugin plugin = _pluginCtx.Plugin.Instance;

            if (onoff)
            {
                plugin.Resume();
            }
            else
            {
                plugin.Suspend();
            }
        }

        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                rect = pluginEditor.Bounds;
                return true;
            }

            rect = new System.Drawing.Rectangle();
            return false;
        }

        public bool EditorOpen(System.IntPtr hWnd)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.Open(hWnd);
                return true;
            }

            return false;
        }

        public void EditorClose()
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.Dispose();
            }
        }

        public void EditorIdle()
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.ProcessIdle();
            }
        }

        public int GetChunk(out byte[] data, bool isPreset)
        {
            data = null;
            return 0;
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            return 0;
        }

        #endregion

        #region IVstPluginCommandStubBase Members

        public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
        {
            IVstPlugin plugin = CreatePlugin();

            if (plugin != null)
            {
                _pluginCtx = new VstPluginContext();
                _pluginCtx.Host = new ExtensibleObjectRef<Host.VstHost>(new Host.VstHost(hostCmdStub));
                _pluginCtx.Plugin = new ExtensibleObjectRef<IVstPlugin>(plugin);
                _pluginCtx.PlginInfo = CreatePluginInfo(plugin);

                return _pluginCtx.PlginInfo;
            }

            return null;
        }

        public void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            IVstAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstAudioProcessor>();

            if (audioProcessor != null)
            {
                VstAudioChannel[] audioInputs = new VstAudioChannel[inputs.Length];

                int index = 0;
                foreach (VstAudioBuffer audioBuffer in inputs)
                {
                    audioInputs[index] = new VstAudioChannel(audioBuffer, false);
                    index++;
                }

                VstAudioChannel[] audioOutputs = new VstAudioChannel[outputs.Length];

                index = 0;
                foreach (VstAudioBuffer audioBuffer in outputs)
                {
                    audioOutputs[index] = new VstAudioChannel(audioBuffer, true);
                    index++;
                }

                audioProcessor.Process(audioInputs, audioOutputs);
            }
        }

        public void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            IVstAudioPrecissionProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstAudioPrecissionProcessor>();

            if (audioProcessor != null)
            {
                VstAudioPrecisionChannel[] audioInputs = new VstAudioPrecisionChannel[inputs.Length];

                int index = 0;
                foreach (VstAudioPrecisionBuffer audioBuffer in inputs)
                {
                    audioInputs[index] = new VstAudioPrecisionChannel(audioBuffer, false);
                    index++;
                }

                VstAudioPrecisionChannel[] audioOutputs = new VstAudioPrecisionChannel[outputs.Length];

                index = 0;
                foreach (VstAudioPrecisionBuffer audioBuffer in outputs)
                {
                    audioOutputs[index] = new VstAudioPrecisionChannel(audioBuffer, true);
                    index++;
                }

                audioProcessor.Process(audioInputs, audioOutputs);
            }
        }

        public void SetParameter(int index, float value)
        {
            IVstPluginParameters pluginParams = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParams != null)
            {
                VstParameter parameter = pluginParams.Parameters[index];
                parameter.NormalizedValue = value;
            }
        }

        public float GetParameter(int index)
        {
            IVstPluginParameters pluginParams = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParams != null)
            {
                VstParameter parameter = pluginParams.Parameters[index];
                return parameter.NormalizedValue;
            }

            return 0.0f;
        }

        #endregion

        /// <summary>
        /// Derived class must override and create the plugin instance.
        /// </summary>
        /// <returns>Returning null will abort loading plugin.</returns>
        protected abstract IVstPlugin CreatePlugin();

        /// <summary>
        /// Creates summary info based on the <paramref name="plugin"/>.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        /// <returns>Never returns null.</returns>
        private VstPluginInfo CreatePluginInfo(IVstPlugin plugin)
        {
            VstPluginInfo pluginInfo = new VstPluginInfo();

            // determine flags
            if (plugin.Supports<IVstPluginEditor>(false))
                pluginInfo.Flags |= VstPluginInfoFlags.HasEditor;
            if (plugin.Supports<IVstAudioProcessor>(false))
                pluginInfo.Flags |= VstPluginInfoFlags.CanReplacing;
            if (plugin.Supports<IVstAudioPrecissionProcessor>(false))
                pluginInfo.Flags |= VstPluginInfoFlags.CanDoubleReplacing;
            if (plugin.Supports<IVstPluginPersistence>(false))
                pluginInfo.Flags |= VstPluginInfoFlags.ProgramChunks;
            if ((plugin.Capabilities & VstPluginCapabilities.IsSynth) > 0)
                pluginInfo.Flags |= VstPluginInfoFlags.IsSynth;
            if ((plugin.Capabilities & VstPluginCapabilities.NoSoundInStop) > 0)
                pluginInfo.Flags |= VstPluginInfoFlags.NoSoundInStop;

            // basic plugin info
            pluginInfo.InitialDelay = plugin.InitialDelay;
            pluginInfo.PluginID = plugin.PluginID;
            pluginInfo.PluginVersion = plugin.ProductInfo.Version;
            
            // audio processing info
            IVstAudioProcessor audioProcessor = plugin.GetInstance<IVstAudioProcessor>(false);
            if(audioProcessor != null)
            {
                pluginInfo.NumberOfAudioInputs = audioProcessor.InputCount;
                pluginInfo.NumberOfAudioOutputs = audioProcessor.OutputCount;
            }

            // program info
            IVstPluginPrograms pluginPrograms = plugin.GetInstance<IVstPluginPrograms>(false);
            if(pluginPrograms != null)
            {
                pluginInfo.NumberOfPrograms = pluginPrograms.Programs.Count;
            }

            return pluginInfo;
        }
    }
}
