namespace Jacobi.Vst.Framework.Plugin
{
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
            return false;
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return false;
        }

        public bool SetEditorKnobMode(int mode)
        {
            return false;
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return 0;
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return 0;
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            return 0;
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            return false;
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            return false;
        }

        public bool BeginSetProgram()
        {
            return false;
        }

        public bool EndSetProgram()
        {
            return false;
        }

        #endregion

        #region IVstPluginCommandStub20 Members

        public bool ProcessEvents(VstEvent[] events)
        {
            return false;
        }

        public bool CanParameterBeAutomated(int index)
        {
            return false;
        }

        public bool String2Parameter(int index, string str)
        {
            return false;
        }

        public bool GetProgramNameIndexed(int index, out string name)
        {
            name = null;
            return false;
        }

        public bool GetInputProperties(int index, VstPinProperties pinProps)
        {
            return false;
        }

        public bool GetOutputProperties(int index, VstPinProperties pinProps)
        {
            return false;
        }

        public VstPluginCategory GetCategory()
        {
            return VstPluginCategory.Unknown;
        }

        public bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag)
        {
            return false;
        }

        public bool OfflinePrepare(int count)
        {
            return false;
        }

        public bool OfflineRun(int count)
        {
            return false;
        }

        public bool ProcessVariableIO(VstVariableIO variableIO)
        {
            return false;
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            return false;
        }

        public bool SetBypass(bool bypass)
        {
            return false;
        }

        public bool GetEffectName(out string name)
        {
            name = null;
            return false;
        }

        public bool GetVendorString(out string vendor)
        {
            vendor = null;
            return false;
        }

        public bool GetProductString(out string product)
        {
            product = null;
            return false;
        }

        public int GetVendorVersion()
        {
            return 0;
        }

        public VstCanDoResult CanDo(string cando)
        {
            return VstCanDoResult.No;
        }

        public int GetTailSize()
        {
            return 0;
        }

        public bool GetParameterProperties(int index, VstParameterProperties paramProps)
        {
            return false;
        }

        public int GetVstVersion()
        {
            return 0;
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
            
        }

        public int GetProgram()
        {
            return 0;
        }

        public void SetProgramName(string name)
        {
            
        }

        public string GetProgramName()
        {
            return null;
        }

        public string GetParameterLabel(int index)
        {
            return null;
        }

        public string GetParameterDisplay(int index)
        {
            return null;
        }

        public string GetParameterName(int index)
        {
            return null;
        }

        public void SetSampleRate(float sampleRate)
        {
            
        }

        public void SetBlockSize(int blockSize)
        {
            
        }

        public void MainsChanged(bool onoff)
        {
            
        }

        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            rect = new System.Drawing.Rectangle();
            return false;
        }

        public bool EditorOpen(System.IntPtr hWnd)
        {
            return false;
        }

        public void EditorClose()
        {
            
        }

        public void EditorIdle()
        {
            
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
        /// Creates summery info based on the <paramref name="plugin"/>.
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
            IVstPluginProgram pluginProgram = plugin.GetInstance<IVstPluginProgram>(false);
            if(pluginProgram != null)
            {
                pluginInfo.NumberOfPrograms = pluginProgram.ProgramCount;
            }

            return pluginInfo;
        }
    }
}
