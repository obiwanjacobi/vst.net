namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using System.IO;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;

    public abstract class StdPluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginContext _pluginCtx;

        #region IVstPluginCommandStub Members

        public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
        {
            IVstPlugin plugin = CreatePluginInstance();

            if (plugin != null)
            {
                _pluginCtx = new VstPluginContext();
                _pluginCtx.Host = new Host.VstHost(hostCmdStub, plugin);
                _pluginCtx.Plugin = plugin;
                _pluginCtx.PluginInfo = CreatePluginInfo(plugin);

                return _pluginCtx.PluginInfo;
            }

            return null;
        }

        #endregion

        #region IVstPluginCommands24 Members

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            bool canDo = false;

            switch(precision)
            {
                case VstProcessPrecision.Process32:
                    canDo = _pluginCtx.Plugin.Supports<IVstPluginAudioProcessor>();
                    break;
                case VstProcessPrecision.Process64:
                    canDo = _pluginCtx.Plugin.Supports<IVstPluginAudioPrecissionProcessor>();
                    break;
            }
            
            return canDo;
        }

        public int GetNumberOfMidiInputChannels()
        {
            IVstMidiProcessor midiProcessor = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>();

            if (midiProcessor != null)
            {
                return midiProcessor.ChannelCount;
            }

            return 0;
        }

        public int GetNumberOfMidiOutputChannels()
        {
            IVstPluginMidiSource midiSource = _pluginCtx.Plugin.GetInstance<IVstPluginMidiSource>();

            if (midiSource != null)
            {
                return midiSource.ChannelCount;
            }
            
            return 0;
        }

        #endregion

        #region IVstPluginCommands23 Members

        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            IVstPluginConnections pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if(pluginConnections != null)
            {
                input = pluginConnections.InputSpeakerArrangement;
                output = pluginConnections.OutputSpeakerArrangement;
                
                return true;
            }

            input = null;
            output = null;
            return false;
        }

        public int SetTotalSamplesToProcess(int numberOfSamples)
        {
            IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

            if (pluginOffline != null)
            {
                pluginOffline.TotalSamplesToProcess = numberOfSamples;

                // TODO: what to return?
            }

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
            IVstPluginProcess pluginProcess = _pluginCtx.Plugin.GetInstance<IVstPluginProcess>();

            if (pluginProcess != null)
            {
                pluginProcess.Start();
                
                // TODO: what to return!?
            }

            return 0;
        }

        public int StopProcess()
        {
            IVstPluginProcess pluginProcess = _pluginCtx.Plugin.GetInstance<IVstPluginProcess>();

            if (pluginProcess != null)
            {
                pluginProcess.Stop();

                // TODO: what to return!?
            }

            return 0;
        }

        public bool SetPanLaw(VstPanLaw type, float value)
        {
            // TODO: where to put PanLaw?
            // Plugin? Editor? AudioProcessor?
            return false;
        }

        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();

            if (pluginPersistence != null)
            {
                return pluginPersistence.CanLoadChunk(chunkInfo) ? VstCanDoResult.Yes : VstCanDoResult.No;
            }

            return VstCanDoResult.No;
        }

        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();

            if (pluginPersistence != null)
            {
                return pluginPersistence.CanLoadChunk(chunkInfo) ? VstCanDoResult.Yes : VstCanDoResult.No;
            }

            return VstCanDoResult.No;
        }

        #endregion

        #region IVstPluginCommands21 Members

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
                midiProgramName.MidiProgram = program.ProgramChange;
                midiProgramName.MidiBankMSB = program.BankSelectMsb;
                midiProgramName.MidiBankLSB = program.BankSelectLsb;

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
                    midiProgramName.MidiProgram = channelInfo.ActiveProgram.ProgramChange;
                    midiProgramName.MidiBankMSB = channelInfo.ActiveProgram.BankSelectMsb;
                    midiProgramName.MidiBankLSB = channelInfo.ActiveProgram.BankSelectLsb;

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
                    midiProgramName.MidiProgram = (byte)0xFF;
                    midiProgramName.MidiBankMSB = (byte)0xFF;
                    midiProgramName.MidiBankLSB = (byte)0xFF;
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
            IVstPluginMidiPrograms midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiPrograms != null)
            {
                //TODO: how do we know!?
            }

            return false;
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            IVstPluginMidiPrograms midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiPrograms != null)
            {
                VstMidiChannelInfo channelInfo = midiPrograms.ChannelInfos[channel];
                VstMidiProgram program = channelInfo.Programs[midiKeyName.CurrentProgramIndex];

                midiKeyName.Name = program.GetKeyName(midiKeyName.CurrentKeyNumber);
                return true;
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

        #region IVstPluginCommands20 Members

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
                return parameter.Info.CanBeAutomated;
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

        public string GetProgramNameIndexed(int index)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null)
            {
                VstProgram program = pluginPrograms.Programs[index];
                return program.Name;
            }

            return null;
        }

        public VstPinProperties GetInputProperties(int index)
        {
            IVstPluginConnections pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                VstConnectionInfo connectionInfo = pluginConnections.InputConnectionInfos[index];

                VstPinProperties pinProps = new VstPinProperties();
                pinProps.Flags = VstPinPropertiesFlags.PinUseSpeaker;
                pinProps.ArrangementType = connectionInfo.SpeakerArrangementType;
                pinProps.Label = connectionInfo.Label;
                pinProps.ShortLabel = connectionInfo.ShortLabel;

                return pinProps;
            }

            return null;
        }

        public VstPinProperties GetOutputProperties(int index)
        {
            IVstPluginConnections pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                VstConnectionInfo connectionInfo = pluginConnections.OutputConnectionInfos[index];

                VstPinProperties pinProps = new VstPinProperties();
                pinProps.Flags = VstPinPropertiesFlags.PinUseSpeaker;
                pinProps.ArrangementType = connectionInfo.SpeakerArrangementType;
                pinProps.Label = connectionInfo.Label;
                pinProps.ShortLabel = connectionInfo.ShortLabel;
                
                return pinProps;
            }

            return null;
        }

        public VstPluginCategory GetCategory()
        {
            return _pluginCtx.Plugin.Category;
        }

        public bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag)
        {
            IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

            if (pluginOffline != null)
            {
                // TODO:
                pluginOffline.Notify();
            }

            return false;
        }

        public bool OfflinePrepare(VstOfflineTask[] tasks, int count)
        {
            IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

            if (pluginOffline != null)
            {
                // TODO:
                pluginOffline.Prepare();
            }

            return false;
        }

        public bool OfflineRun(VstOfflineTask[] tasks, int count)
        {
            IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

            if (pluginOffline != null)
            {
                // TODO:
                pluginOffline.Run();
            }

            return false;
        }

        public bool ProcessVariableIO(VstVariableIO variableIO)
        {
            // TODO
            return false;
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            IVstPluginConnections pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                pluginConnections.InputSpeakerArrangement = saInput;
                pluginConnections.OutputSpeakerArrangement = saOutput;
                return true;
            }

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

        public string GetEffectName()
        {
            string name = _pluginCtx.Plugin.Name;

            if (name != null && name.Length > Core.Constants.MaxEffectNameLength)
            {
                throw new InvalidOperationException("The plugin returned a name that is too long.");
            }

            return name;
        }

        public string GetVendorString()
        {
            return _pluginCtx.Plugin.ProductInfo.Vendor;
        }

        public string GetProductString()
        {
            return _pluginCtx.Plugin.ProductInfo.Product;
        }

        public int GetVendorVersion()
        {
            return _pluginCtx.Plugin.ProductInfo.Version;
        }

        public VstCanDoResult CanDo(VstPluginCanDo cando)
        {
            VstCanDoResult result = VstCanDoResult.No;

            switch (cando)
            {
                case VstPluginCanDo.Bypass:
                    result = _pluginCtx.Plugin.Supports<IVstPluginBypass>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.MidiProgramNames:
                    result = _pluginCtx.Plugin.Supports<IVstPluginMidiPrograms>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.Offline:
                    result = _pluginCtx.Plugin.Supports<IVstPluginOfflineProcessor>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.ReceiveVstEvents:
                case VstPluginCanDo.ReceiveVstMidiEvent:
                    result = _pluginCtx.Plugin.Supports<IVstMidiProcessor>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.ReceiveVstTimeInfo:
                    // TODO: define interface?
                    result = VstCanDoResult.Unknown;
                    break;
                case VstPluginCanDo.SendVstEvents:
                case VstPluginCanDo.SendVstMidiEvent:
                    result = _pluginCtx.Plugin.Supports<IVstPluginMidiSource>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
            }

            return result;
        }

        public int GetTailSize()
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                return audioProcessor.TailSize;
            }

            return 0;
        }

        public VstParameterProperties GetParameterProperties(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                
                VstParameterProperties paramProps = new VstParameterProperties();

                // labels
                paramProps.Label = parameter.Info.Label;
                paramProps.ShortLabel = parameter.Info.ShortLabel;

                if(parameter.Category != null)
                {
                    // find parameters in current category
                    VstParameterCollection catParams = pluginParameters.Parameters.FindParametersIn(parameter.Category);
                    
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterSupportsDisplayCategory;
                    paramProps.CategoryLabel = parameter.Category.Name;
                    paramProps.Category = (short)(pluginParameters.Categories.IndexOf(parameter.Category) + 1);
                    paramProps.NumParametersInCategory = (short)catParams.Count;
                }

                if (parameter.Info.IsStepFloatValid)
                {
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterUsesFloatStep;
                    paramProps.StepFloat = parameter.Info.StepFloat;
                    paramProps.SmallStepFloat = parameter.Info.SmallStepFloat;
                    paramProps.LargeStepFloat = parameter.Info.LargeStepFloat;
                }

                if (parameter.Info.IsMinMaxIntegerValid)
                {
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterUsesIntegerMinMax;
                    paramProps.MinInteger = parameter.Info.MinInteger;
                    paramProps.MaxInteger = parameter.Info.MaxInteger;
                }

                if (parameter.Info.IsStepIntegerValid)
                {
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterUsesIntStep;
                    paramProps.StepInteger = parameter.Info.StepInteger;
                    paramProps.LargeStepInteger = parameter.Info.LargeStepInteger;
                }

                if (parameter.Info.IsSwitch)
                {
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterIsSwitch;
                }

                if (parameter.Info.CanRamp)
                {
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterCanRamp;
                }

                // order of parameters is also display order
                paramProps.Flags |= VstParameterPropertiesFlags.ParameterSupportsDisplayIndex;
                paramProps.DisplayIndex = (short)index;

                return paramProps;
            }

            return null;
        }

        public int GetVstVersion()
        {
            return 2400;
        }

        #endregion

        #region IVstPluginCommands10 Members

        public void Open()
        {
            if (!_pluginCtx.Host.HostCommandStub.IsInitialized())
            {
                throw new InvalidOperationException("The HostCommandStub has not been initialized.");
            }

            _pluginCtx.Plugin.Open(_pluginCtx.Host);
        }

        public void Close()
        {
            _pluginCtx.Dispose();
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
                return parameter.Info.ShortLabel;
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
                return parameter.Info.Name;
            }

            return null;
        }

        public void SetSampleRate(float sampleRate)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.SampleRate = sampleRate;
            }
        }

        public void SetBlockSize(int blockSize)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.BlockSize = blockSize;
            }
        }

        public void MainsChanged(bool onoff)
        {
            IVstPlugin plugin = _pluginCtx.Plugin;

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

        public bool EditorOpen(IntPtr hWnd)
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
                pluginEditor.Close();
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

        public byte[] GetChunk(bool isPreset)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPersistence != null && pluginPrograms != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    VstProgramCollection programs = null;

                    if (isPreset && pluginPrograms.ActiveProgram != null)
                    {
                        programs = new VstProgramCollection();
                        programs.Add(pluginPrograms.ActiveProgram);
                    }
                    else
                    {
                        programs = pluginPrograms.Programs;
                    }

                    pluginPersistence.WritePrograms(stream, programs);

                    return stream.GetBuffer();
                }
            }

            return null;
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPersistence != null && pluginPrograms != null)
            {
                using (MemoryStream stream = new MemoryStream(data, false))
                {
                    if (isPreset)
                    {
                        VstProgram prog = pluginPersistence.ReadProgram(stream);

                        VstProgram formerActiveProg = pluginPrograms.ActiveProgram;

                        pluginPrograms.Programs.Add(prog);
                        pluginPrograms.ActiveProgram = prog;

                        // remove the previously active program that has now been replaced
                        if (formerActiveProg != null)
                        {
                            pluginPrograms.Programs.Remove(formerActiveProg);
                        }
                    }
                    else
                    {
                        // use a temp collection to leave the real Programs 
                        // collection in tact in case of an exception.
                        VstProgramCollection programs = new VstProgramCollection();
                        
                        pluginPersistence.ReadPrograms(stream, programs);

                        pluginPrograms.ActiveProgram = null;
                        pluginPrograms.Programs.Clear();
                        pluginPrograms.Programs.AddRange(programs);
                    }

                    return (int)stream.Position;
                }
            }

            return 0;
        }

        #endregion

        #region IVstPluginCommandsBase Members

        public void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

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
            IVstPluginAudioPrecissionProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioPrecissionProcessor>();

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
                parameter.Value = value;
            }
        }

        public float GetParameter(int index)
        {
            IVstPluginParameters pluginParams = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParams != null)
            {
                VstParameter parameter = pluginParams.Parameters[index];
                return parameter.Value;
            }

            return 0.0f;
        }

        #endregion

        /// <summary>
        /// Derived class must override and create the plugin instance.
        /// </summary>
        /// <returns>Returning null will abort loading plugin.</returns>
        protected abstract IVstPlugin CreatePluginInstance();

        /// <summary>
        /// Creates summary info based on the <paramref name="plugin"/>.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        /// <returns>Never returns null.</returns>
        private VstPluginInfo CreatePluginInfo(IVstPlugin plugin)
        {
            VstPluginInfo pluginInfo = new VstPluginInfo();

            // determine flags
            if (plugin.Supports<IVstPluginEditor>())
                pluginInfo.Flags |= VstPluginFlags.HasEditor;
            if (plugin.Supports<IVstPluginAudioProcessor>())
                pluginInfo.Flags |= VstPluginFlags.CanReplacing;
            if (plugin.Supports<IVstPluginAudioPrecissionProcessor>())
                pluginInfo.Flags |= VstPluginFlags.CanDoubleReplacing;
            if (plugin.Supports<IVstPluginPersistence>())
                pluginInfo.Flags |= VstPluginFlags.ProgramChunks;
            if ((plugin.Capabilities & VstPluginCapabilities.IsSynth) > 0)
                pluginInfo.Flags |= VstPluginFlags.IsSynth;
            if ((plugin.Capabilities & VstPluginCapabilities.NoSoundInStop) > 0)
                pluginInfo.Flags |= VstPluginFlags.NoSoundInStop;

            // basic plugin info
            pluginInfo.InitialDelay = plugin.InitialDelay;
            pluginInfo.PluginID = plugin.PluginID;
            pluginInfo.PluginVersion = plugin.ProductInfo.Version;
            
            // audio processing info
            IVstPluginAudioProcessor audioProcessor = plugin.GetInstance<IVstPluginAudioProcessor>();
            if(audioProcessor != null)
            {
                pluginInfo.NumberOfAudioInputs = audioProcessor.InputCount;
                pluginInfo.NumberOfAudioOutputs = audioProcessor.OutputCount;
            }

            // parameter info
            IVstPluginParameters pluginParameters = plugin.GetInstance<IVstPluginParameters>();
            if (pluginParameters != null)
            {
                pluginInfo.NumberOfParameters = pluginParameters.Parameters.Count;
            }

            // program info
            IVstPluginPrograms pluginPrograms = plugin.GetInstance<IVstPluginPrograms>();
            if(pluginPrograms != null)
            {
                pluginInfo.NumberOfPrograms = pluginPrograms.Programs.Count;
            }

            return pluginInfo;
        }
    }
}
