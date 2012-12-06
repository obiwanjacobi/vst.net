﻿namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using System.Configuration;
    using System.IO;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;

    /// <summary>
    /// The StdPluginCommandStub class provides a default implementation for adapting the <see cref="IVstPluginCommandStub"/> 
    /// interface calls to the framework.
    /// </summary>
    /// <remarks>Each plugin must implement a public class the implements the <see cref="IVstPluginCommandStub"/> interface.
    /// Plugins that use the framework can just derive from this class and override the <see cref="CreatePluginInstance"/> method
    /// to create their plugin root object.</remarks>
    public abstract class StdPluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginContext _pluginCtx;

        /// <summary>
        /// Provides derived classes accces to the root object of the Plugin.
        /// </summary>
        protected IVstPlugin Plugin
        {
            get { return _pluginCtx.Plugin; }
        }

        #region IVstPluginCommandStub Members
        /// <summary>
        /// Called by the Interop loader to retrieve the plugin information.
        /// </summary>
        /// <param name="hostCmdStub">Must not be null.</param>
        /// <returns>Returns a fully populated <see cref="VstPluginInfo"/> instance. Never returns null.</returns>
        /// <remarks>Override <see cref="CreatePluginInfo"/> to change the default behavior of how the plugin info is built.</remarks>
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

        /// <summary>
        /// Gets or sets the custom plugin specific configuration object.
        /// </summary>
        /// <remarks>Can be null if the plugin has not deployed a config file.</remarks>
        public Configuration PluginConfiguration { get; set; }

        #endregion

        #region IVstPluginCommands24 Members
        /// <summary>
        /// Called by the host query inform the plugin on the precision of audio processing it supports.
        /// </summary>
        /// <param name="precision">An indication of either 32 bit or 64 bit samples.</param>
        /// <returns>Returns true when the requested <paramref name="precision"/> is supported.</returns>
        /// <remarks>The implementation just queries the plugin for the <see cref="IVstPluginAudioProcessor"/> and
        /// <see cref="IVstPluginAudioPrecisionProcessor"/> interfaces. Override to change this behavior.</remarks>
        public virtual bool SetProcessPrecision(VstProcessPrecision precision)
        {
            bool canDo = false;

            switch(precision)
            {
                case VstProcessPrecision.Process32:
                    canDo = _pluginCtx.Plugin.Supports<IVstPluginAudioProcessor>();
                    break;
                case VstProcessPrecision.Process64:
                    canDo = _pluginCtx.Plugin.Supports<IVstPluginAudioPrecisionProcessor>();
                    break;
            }
            
            return canDo;
        }

        /// <summary>
        /// Called by the host to retrieve the number of Midi In channels the plugin supports.
        /// </summary>
        /// <returns>Returns the number of Midi In channels, or 0 (zero) if not supported.</returns>
        /// <remarks>The implementation queries the plugin for the <see cref="IVstMidiProcessor"/> interface. 
        /// Override to change this behavior.</remarks>
        public virtual int GetNumberOfMidiInputChannels()
        {
            IVstMidiProcessor midiProcessor = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>();

            if (midiProcessor != null)
            {
                return midiProcessor.ChannelCount;
            }

            return 0;
        }

        /// <summary>
        /// Called by the host to retrieve the number of Midi Out channels the plugin supports.
        /// </summary>
        /// <returns>Returns the number of Midi Out channels, or 0 (zero) if not supported.</returns>
        /// <remarks>The implementation queries the plugin for the <see cref="IVstPluginMidiSource"/> interface. 
        /// Override to change this behavior.</remarks>
        public virtual int GetNumberOfMidiOutputChannels()
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

        /// <summary>
        /// Returns the speaker arrangements for the input and output of the plugin.
        /// </summary>
        /// <param name="input">Filled with the speaker arrangement for the plugin inputs.</param>
        /// <param name="output">Filled with the speaker arrangement for the plugin outputs.</param>
        /// <returns>Returns true when the plugin implements the <see cref="IVstPluginConnections"/> interfcace.</returns>
        public virtual bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
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

        #region Offline processing not implemented
        ///// <summary>
        ///// Informs the plugin offline processor of the number of samples left to be processed.
        ///// </summary>
        ///// <param name="numberOfSamples">The sample count.</param>
        ///// <returns>Returns 0 (zero). It is unclear what this return value represents.</returns>
        ///// <remarks>The implementation queries the plugin for the <see cref="IVstPluginOfflineProcessor"/> interface.
        ///// Override to change this behavior.
        ///// The <see cref="IVstPluginOfflineProcessor"/> interface is under construction.</remarks>
        //public virtual int SetTotalSamplesToProcess(int numberOfSamples)
        //{
        //    IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

        //    if (pluginOffline != null)
        //    {
        //        pluginOffline.TotalSamplesToProcess = numberOfSamples;

        //        // TODO: what to return?
        //    }

        //    return 0;
        //}
        #endregion

        #region Plugin Host/Shell not implemented
        ///// <summary>
        ///// Under construction.
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public virtual int GetNextPlugin(out string name)
        //{
        //    IVstPluginHost pluginHost = _pluginCtx.Plugin.GetInstance<IVstPluginHost>();

        //    if (pluginHost != null)
        //    {

        //    }

        //    name = null;
        //    return 0;
        //}
        #endregion

        /// <summary>
        /// Called just before Process cycle is started.
        /// </summary>
        /// <returns>Returns 0 (zero) when not supported. It is unclear what this return value represents.</returns>
        public virtual int StartProcess()
        {
            IVstPluginProcess pluginProcess = _pluginCtx.Plugin.GetInstance<IVstPluginProcess>();

            if (pluginProcess != null)
            {
                pluginProcess.Start();
                
                // TODO: what to return!?
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Called just after Process cycle is stopped.
        /// </summary>
        /// <returns>Returns 0 (zero) when not supported. It is unclear what this return value represents.</returns>
        public virtual int StopProcess()
        {
            IVstPluginProcess pluginProcess = _pluginCtx.Plugin.GetInstance<IVstPluginProcess>();

            if (pluginProcess != null)
            {
                pluginProcess.Stop();

                // TODO: what to return!?
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Informs the plugin of the pan algorithm to use.
        /// </summary>
        /// <param name="type">The pan algorithm type.</param>
        /// <param name="gain">A gain factor.</param>
        /// <returns>Returns false when not implemented.</returns>
        public virtual bool SetPanLaw(VstPanLaw type, float gain)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                return audioProcessor.SetPanLaw(type, gain);
            }

            return false;
        }

        /// <summary>
        /// Called by the host to query the plugin that supports persistence if the chunk can be read.
        /// </summary>
        /// <param name="chunkInfo">Must not be null.</param>
        /// <returns>Returns <see cref="VstCanDoResult.Yes"/> if the plugin can read the data.</returns>
        /// <remarks>The implementation calls the <see cref="IVstPluginPersistence"/> interface. 
        /// Override to change this behavior.</remarks>
        public virtual VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();

            if (pluginPersistence != null)
            {
                return pluginPersistence.CanLoadChunk(chunkInfo) ? VstCanDoResult.Yes : VstCanDoResult.No;
            }

            return VstCanDoResult.No;
        }

        /// <summary>
        /// Called by the host to query the plugin that supports persistence if the chunk can be read.
        /// </summary>
        /// <param name="chunkInfo">Must not be null.</param>
        /// <returns>Returns <see cref="VstCanDoResult.Yes"/> if the plugin can read the data.</returns>
        /// <remarks>The implementation calls the <see cref="IVstPluginPersistence"/> interface. 
        /// Override to change this behavior.</remarks>
        public virtual VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
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
        /// <summary>
        /// Called by the host when the user presses a key.
        /// </summary>
        /// <param name="ascii">The identification of the key.</param>
        /// <param name="virtualKey">Virtual key information.</param>
        /// <param name="modifers">Additional keys pressed.</param>
        /// <returns>Returns true when the plugin implements <see cref="IVstPluginEditor"/>.</returns>
        /// <remarks>The implementation calls the <see cref="IVstPluginEditor"/> interface. 
        /// Override to change this behavior.</remarks>
        public virtual bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.KeyDown(ascii, virtualKey, modifers);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called by the host when the user releases a key.
        /// </summary>
        /// <param name="ascii">The identification of the key.</param>
        /// <param name="virtualKey">Virtual key information.</param>
        /// <param name="modifers">Additional keys pressed.</param>
        /// <returns>Returns true when the plugin implements <see cref="IVstPluginEditor"/>.</returns>
        /// <remarks>The implementation calls the <see cref="IVstPluginEditor"/> interface. 
        /// Override to change this behavior.</remarks>
        public virtual bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.KeyUp(ascii, virtualKey, modifers);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called by the host to set the mode for turning knobs.
        /// </summary>
        /// <param name="mode">The mode to use for turning knobs.</param>
        /// <returns>Returns true when the mode was set on the plugin editor.</returns>
        public virtual bool SetEditorKnobMode(VstKnobMode mode)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.KnobMode = mode;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves information about a midi program for a specific Midi <paramref name="channel"/>.
        /// </summary>
        /// <param name="midiProgramName">Must not be null.</param>
        /// <param name="channel">The zero-based Midi channel.</param>
        /// <returns>Returns the number of implemented Midi programs.</returns>
        public virtual int GetMidiProgramName(VstMidiProgramName midiProgramName, int channel)
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

                if (program.Category != null)
                {
                    midiProgramName.ParentCategoryIndex = channelInfo.Categories.IndexOf(program.Category);
                }
                else
                {
                    midiProgramName.ParentCategoryIndex = -1;
                }

                return channelInfo.Programs.Count;
            }

            return 0;
        }

        /// <summary>
        /// Retrieves information about the current midi program for a specific Midi <paramref name="channel"/>.
        /// </summary>
        /// <param name="midiProgramName">Must not be null.</param>
        /// <param name="channel">The zero-based Midi channel.</param>
        /// <returns>Returns the number of implemented Midi programs.</returns>
        public virtual int GetCurrentMidiProgramName(VstMidiProgramName midiProgramName, int channel)
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

                    if (channelInfo.ActiveProgram.Category != null)
                    {
                        midiProgramName.ParentCategoryIndex = 
                            channelInfo.Categories.IndexOf(channelInfo.ActiveProgram.Category);
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

        /// <summary>
        /// Retrieves information about a Midi Program Category.
        /// </summary>
        /// <param name="midiCat">Must not be null.</param>
        /// <param name="channel">The zero-based Midi channel.</param>
        /// <returns>Returns the total number of Midi program categories.</returns>
        public virtual int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
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

        /// <summary>
        /// Indicates if the program names or key names for the specified Midi <paramref name="channel"/> has changed.
        /// </summary>
        /// <param name="channel">The zero-base Midi channel.</param>
        /// <returns>Returns true if the Midi Program has changed, otherwise false is returned.</returns>
        public virtual bool HasMidiProgramsChanged(int channel)
        {
            IVstPluginMidiPrograms midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

            if (midiPrograms != null)
            {
                VstMidiChannelInfo channelInfo = midiPrograms.ChannelInfos[channel];

                bool changed = channelInfo.HasChanged;
                
                // reset dirty flag
                channelInfo.HasChanged = false;
                
                return changed;
            }

            return false;
        }

        /// <summary>
        /// Retrieves information about a Midi Key (or note).
        /// </summary>
        /// <param name="midiKeyName">Must not be null.</param>
        /// <param name="channel">The zero-base Midi channel.</param>
        /// <returns>Returns true when the <paramref name="midiKeyName"/>.Name was filled.</returns>
        public virtual bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
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

        /// <summary>
        /// Called by the host just before a new Program is set.
        /// </summary>
        /// <returns>Returns true when the call was forwarded to the plugin's <see cref="IVstPluginPrograms"/> implementation.</returns>
        public virtual bool BeginSetProgram()
        {
            IVstPluginPrograms pluginProgram = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginProgram != null)
            {
                pluginProgram.BeginSetProgram();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called by the host just after a new Program is set.
        /// </summary>
        /// <returns>Returns true when the call was forwarded to the plugin's <see cref="IVstPluginPrograms"/> implementation.</returns>
        public virtual bool EndSetProgram()
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

        /// <summary>
        /// Called by the host when the plugin has implemented the <see cref="IVstMidiProcessor"/> interface.
        /// </summary>
        /// <param name="events">The (Midi) events for the current 'block'.</param>
        /// <returns>Returns true when the call was forwarded to the plugin.</returns>
        public virtual bool ProcessEvents(VstEvent[] events)
        {
            IVstMidiProcessor midiProcessor = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>();

            if (midiProcessor != null)
            {
                midiProcessor.Process(new VstEventCollection(events));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called by the host to query the plugin whether the parameter at <paramref name="index"/> can be automated.
        /// </summary>
        /// <param name="index">The zero-based index into the parameters.</param>
        /// <returns>Returns the value of the <see cref="VstParameterInfo.CanBeAutomated"/> of the parameter at <paramref name="index"/> 
        /// or false if the plugin does not support parameters.</returns>
        public virtual bool CanParameterBeAutomated(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Info.CanBeAutomated;
            }

            return false;
        }

        /// <summary>
        /// Parses the <paramref name="str"/> value to assign to the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-base parameter index.</param>
        /// <param name="str">The value for the parameter.</param>
        /// <returns>Returns true when the parameter was successfully parsed. 
        /// Returns false when the plugin does not implement parameters.</returns>
        public virtual bool String2Parameter(int index, string str)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.ParseValue(str);
            }

            return false;
        }

        /// <summary>
        /// Retrieves the name of the program at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-base index into the plugin Programs.</param>
        /// <returns>Returns null when the plugin does not implement Programs.</returns>
        public virtual string GetProgramNameIndexed(int index)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null)
            {
                VstProgram program = pluginPrograms.Programs[index];
                return program.Name;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the pin properties for the input at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin inputs.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginConnections"/>.</returns>
        public virtual VstPinProperties GetInputProperties(int index)
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

        /// <summary>
        /// Retrieves the pin properties for the output at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin outputs.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginConnections"/>.</returns>
        public virtual VstPinProperties GetOutputProperties(int index)
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

        /// <summary>
        /// Returns the plugin category.
        /// </summary>
        /// <returns>Returns the <see cref="IVstPlugin.Category"/> value.</returns>
        public virtual VstPluginCategory GetCategory()
        {
            return _pluginCtx.Plugin.Category;
        }

        #region Offline processing not implemented
        ///// <summary>
        ///// Under construction
        ///// </summary>
        ///// <param name="audioFiles"></param>
        ///// <param name="count"></param>
        ///// <param name="startFlag"></param>
        ///// <returns>Always returns false</returns>
        //public virtual bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag)
        //{
        //    IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

        //    if (pluginOffline != null)
        //    {
        //        // TODO:
        //        pluginOffline.Notify();
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// Under construction
        ///// </summary>
        ///// <param name="tasks"></param>
        ///// <param name="count"></param>
        ///// <returns>Always returns false</returns>
        //public virtual bool OfflinePrepare(VstOfflineTask[] tasks, int count)
        //{
        //    IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

        //    if (pluginOffline != null)
        //    {
        //        // TODO:
        //        pluginOffline.Prepare();
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// Under construction
        ///// </summary>
        ///// <param name="tasks"></param>
        ///// <param name="count"></param>
        ///// <returns>Always returns false</returns>
        //public virtual bool OfflineRun(VstOfflineTask[] tasks, int count)
        //{
        //    IVstPluginOfflineProcessor pluginOffline = _pluginCtx.Plugin.GetInstance<IVstPluginOfflineProcessor>();

        //    if (pluginOffline != null)
        //    {
        //        // TODO:
        //        pluginOffline.Run();
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// Under construction
        ///// </summary>
        ///// <param name="variableIO"></param>
        ///// <returns>Always returns false</returns>
        //public virtual bool ProcessVariableIO(VstVariableIO variableIO)
        //{
        //    // TODO
        //    return false;
        //}
        #endregion

        /// <summary>
        /// Called by the host to propose a new speaker arrangement.
        /// </summary>
        /// <param name="saInput">Must not be null.</param>
        /// <param name="saOutput">Must not be null.</param>
        /// <returns>Returns false if the plugin does not implement <see cref="IVstPluginConnections"/>.</returns>
        public virtual bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            IVstPluginConnections pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                return pluginConnections.AcceptNewArrangement(saInput, saOutput);
            }

            return false;
        }

        /// <summary>
        /// Called by the host to bypass plugin processing.
        /// </summary>
        /// <param name="bypass">True to bypass, false to process.</param>
        /// <returns>Returns false when the plugin does not implement the <see cref="IVstPluginBypass"/> interface.</returns>
        public virtual bool SetBypass(bool bypass)
        {
            IVstPluginBypass pluginBypass = _pluginCtx.Plugin.GetInstance<IVstPluginBypass>();

            if (pluginBypass != null)
            {
                pluginBypass.Bypass = bypass;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called by the host to retrieve the name of plugin.
        /// </summary>
        /// <returns>Returns the value of <see cref="IVstPlugin.Name"/>.</returns>
        /// <remarks>The plugin name should not exceed 32 characters.</remarks>
        /// <exception cref="InvalidOperationException">Thrown when the name exceeds 31 characters.</exception>
        public virtual string GetEffectName()
        {
            string name = _pluginCtx.Plugin.Name;

            if (name != null && name.Length > Core.Constants.MaxEffectNameLength)
            {
                throw new InvalidOperationException(
                    String.Format(Properties.Resources.StdPluginCommandStub_StringTooLong, 
                        "Plugin.Name", name, Core.Constants.MaxEffectNameLength));
            }

            return name;
        }

        /// <summary>
        /// Called to retrieve the plugin vendor information.
        /// </summary>
        /// <returns>Returns <see cref="IVstPlugin.ProductInfo"/>.Vendor.</returns>
        public virtual string GetVendorString()
        {
            return _pluginCtx.Plugin.ProductInfo.Vendor;
        }

        /// <summary>
        /// Called to retrieve the plugin product information.
        /// </summary>
        /// <returns>Returns <see cref="IVstPlugin.ProductInfo"/>.Product.</returns>
        public virtual string GetProductString()
        {
            return _pluginCtx.Plugin.ProductInfo.Product;
        }

        /// <summary>
        /// Called to retrieve the plugin version information.
        /// </summary>
        /// <returns>Returns <see cref="IVstPlugin.ProductInfo"/>.Version.</returns>
        public virtual int GetVendorVersion()
        {
            return _pluginCtx.Plugin.ProductInfo.Version;
        }

        /// <summary>
        /// Called by the host to query the plugin if a certain behavior or aspect is supported.
        /// </summary>
        /// <param name="cando">The string containing the can-do string, which can be host specific.</param>
        /// <returns>Returns an indication if the capability is supported.</returns>
        /// <remarks>The implementation handles all options in the <see cref="VstPluginCanDo"/> enum.
        /// Override in derived class to implement custom cando behavior.
        /// <seealso cref="VstCanDoHelper.ParsePluginCanDo"/></remarks>
        public virtual VstCanDoResult CanDo(string cando)
        {
            VstCanDoResult result = VstCanDoResult.Unknown;
            
            VstPluginCanDo candoEnum = VstCanDoHelper.ParsePluginCanDo(cando);

            switch (candoEnum)
            {
                case VstPluginCanDo.Bypass:
                    result = _pluginCtx.Plugin.Supports<IVstPluginBypass>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.MidiProgramNames:
                    result = _pluginCtx.Plugin.Supports<IVstPluginMidiPrograms>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.Offline:
                    //result = _pluginCtx.Plugin.Supports<IVstPluginOfflineProcessor>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    result = VstCanDoResult.No;
                    break;
                case VstPluginCanDo.ReceiveVstEvents:
                case VstPluginCanDo.ReceiveVstMidiEvent:
                    result = _pluginCtx.Plugin.Supports<IVstMidiProcessor>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.ReceiveVstTimeInfo:
                    result = ((_pluginCtx.Plugin.Capabilities & VstPluginCapabilities.ReceiveTimeInfo) > 0) ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.SendVstEvents:
                case VstPluginCanDo.SendVstMidiEvent:
                    result = _pluginCtx.Plugin.Supports<IVstPluginMidiSource>() ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Called by the host to retrieve the number of samples that the plugin outputs after the input has gone silent.
        /// </summary>
        /// <returns>Returns <see cref="IVstPluginAudioProcessor.TailSize"/> or zero if not implemented by the plugin.</returns>
        public virtual int GetTailSize()
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                return audioProcessor.TailSize;
            }

            return 0;
        }

        /// <summary>
        /// Called by the host to retrieve information about a plugin parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin parameters.</param>
        /// <returns>Returns a fully filled instance of <see cref="VstParameterProperties"/>.</returns>
        /// <remarks>The implementation uses the <see cref="IVstPluginParameters"/> interface, the <see cref="VstParameter"/> 
        /// at <paramref name="index"/> and the <see cref="VstParameterInfo"/> attached to the parameter 
        /// to fill the <see cref="VstParameterProperties"/> instance.</remarks>
        public virtual VstParameterProperties GetParameterProperties(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                
                // clear the normalization info for this parameter
                parameter.Info.NormalizationInfo = null;

                VstParameterProperties paramProps = new VstParameterProperties();

                // labels
                paramProps.Label = parameter.Info.Label;
                paramProps.ShortLabel = parameter.Info.ShortLabel;

                VstParameterCategory category = parameter.Info.Category;
                if(category != null)
                {
                    // find parameters in current category
                    VstParameterCollection catParams = pluginParameters.Parameters.FindParametersIn(category);
                    
                    paramProps.Flags |= VstParameterPropertiesFlags.ParameterSupportsDisplayCategory;
                    paramProps.CategoryLabel = category.Name;
                    paramProps.Category = (short)(pluginParameters.Categories.IndexOf(category) + 1);
                    paramProps.ParameterCountInCategory = (short)catParams.Count;
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

        /// <summary>
        /// Called by the host to query the plugin what VST version it supports.
        /// </summary>
        /// <returns>Always returns 2400: VST 2.4.</returns>
        public virtual int GetVstVersion()
        {
            return 2400;
        }

        #endregion

        #region IVstPluginCommands10 Members

        /// <summary>
        /// This is the first method called by the host right after the assembly is loaded.
        /// </summary>
        /// <remarks>Always call the base class when overriding.</remarks>
        /// <exception cref="InvalidOperationException">Thrown when the HostCommandStub has not been initialized.</exception>
        public virtual void Open()
        {
            if (!_pluginCtx.Host.HostCommandStub.IsInitialized())
            {
                throw new InvalidOperationException(Properties.Resources.StdPluginCommandStub_HostNotInitialized);
            }

            _pluginCtx.Plugin.Open(_pluginCtx.Host);
        }

        /// <summary>
        /// This is the last method the host calls. Dispose your resources.
        /// </summary>
        /// <remarks>Always call the base class when overriding.</remarks>
        public virtual void Close()
        {
            if (_pluginCtx != null)
            {
                _pluginCtx.Dispose();
                _pluginCtx = null;
            }
        }

        /// <summary>
        /// The plugin should activate the Program at <paramref name="programNumber"/>.
        /// </summary>
        /// <param name="programNumber">A zero-based program number (index).</param>
        public virtual void SetProgram(int programNumber)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null)
            {
                VstProgram program = pluginPrograms.Programs[programNumber];
                pluginPrograms.ActiveProgram = program;
            }
        }

        /// <summary>
        /// Retrieve the current program index.
        /// </summary>
        /// <returns>Returns zero when the plugin does not implement the <see cref="IVstPluginPrograms"/> interface
        /// or no active program was set; <see cref="IVstPluginPrograms.ActiveProgram"/> returns null.</returns>
        public virtual int GetProgram()
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null && 
                pluginPrograms.ActiveProgram != null)
            {
                return pluginPrograms.Programs.IndexOf(pluginPrograms.ActiveProgram);
            }

            return 0;
        }

        /// <summary>
        /// Assign a new name to the current/active program.
        /// </summary>
        /// <param name="name">The new program name.</param>
        /// <remarks>
        /// The implementation uses the <see cref="IVstPluginPrograms.ActiveProgram"/> to set the name.
        /// The name will not be set when the active program (preset) is read-only.
        /// </remarks>
        public virtual void SetProgramName(string name)
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null &&
                pluginPrograms.ActiveProgram != null &&
                !pluginPrograms.ActiveProgram.IsReadOnly)
            {
                pluginPrograms.ActiveProgram.Name = name;
            }
        }

        /// <summary>
        /// Retrieves the name of the current/active program.
        /// </summary>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginPrograms"/> 
        /// or when the active program is not set.</returns>
        /// <remarks>The implementation uses the <see cref="IVstPluginPrograms.ActiveProgram"/> to get the name.</remarks>
        public virtual string GetProgramName()
        {
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null &&
                pluginPrograms.ActiveProgram != null)
            {
                return pluginPrograms.ActiveProgram.Name;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the label for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginParameters"/> 
        /// or when <see cref="VstParameterInfo.ShortLabel"/> was not set.</returns>
        public virtual string GetParameterLabel(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Info.ShortLabel;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the display value for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginParameters"/> 
        /// or when <see cref="VstParameter.DisplayValue"/> was not set.</returns>
        public virtual string GetParameterDisplay(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.DisplayValue;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the name for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginParameters"/> 
        /// or when <see cref="VstParameterInfo.Name"/> was not set.</returns>
        public virtual string GetParameterName(int index)
        {
            IVstPluginParameters pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Info.Name;
            }

            return null;
        }

        /// <summary>
        /// Assigns the <paramref name="sampleRate"/> to the plugin.
        /// </summary>
        /// <param name="sampleRate">The number of audio samples per second.</param>
        /// <remarks>The implementation uses the <see cref="IVstPluginAudioProcessor"/> interface. Override to change behavior.</remarks>
        public virtual void SetSampleRate(float sampleRate)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.SampleRate = sampleRate;
            }
        }

        /// <summary>
        /// Assigns the <paramref name="blockSize"/> to the plugin.
        /// </summary>
        /// <param name="blockSize">The number samples per cycle.</param>
        /// <remarks>The implementation uses the <see cref="IVstPluginAudioProcessor"/> interface. Override to change behavior.</remarks>
        public virtual void SetBlockSize(int blockSize)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.BlockSize = blockSize;
            }
        }

        /// <summary>
        /// Called by the host when the users has turned the plugin on or off.
        /// </summary>
        /// <param name="onoff">True when on, False when off.</param>
        public virtual void MainsChanged(bool onoff)
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

        /// <summary>
        /// Called by the host to retrieve the bounding rectangle of the editor.
        /// </summary>
        /// <param name="rect">The rectangle receiving the bounds.</param>
        /// <returns>Returns true when the <paramref name="rect"/> was set. 
        /// Returns false when the plugin does not implement the <see cref="IVstPluginEditor"/> interface.</returns>
        public virtual bool EditorGetRect(out System.Drawing.Rectangle rect)
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

        /// <summary>
        /// Called by the host to open the plugin custom editor.
        /// </summary>
        /// <param name="hWnd">The handle to the parent window.</param>
        /// <returns>Returns false when the plugin does not implement the <see cref="IVstPluginEditor"/> interface.</returns>
        public virtual bool EditorOpen(IntPtr hWnd)
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.Open(hWnd);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called by the host to close (and destroy) the plugin custom editor.
        /// </summary>
        /// <remarks>The implementation uses the <see cref="IVstPluginEditor"/> interface.
        /// Override to change this behavior.</remarks>
        public virtual void EditorClose()
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.Close();
            }
        }

        /// <summary>
        /// Called by the host when the editor is idle.
        /// </summary>
        /// <remarks>Keep your processing short.</remarks>
        public virtual void EditorIdle()
        {
            IVstPluginEditor pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                pluginEditor.ProcessIdle();
            }
        }

        /// <summary>
        /// Called by the host to retrieve a buffer with Program (and Parameter) content.
        /// </summary>
        /// <param name="isPreset">True if only the current/active program should be serialized, 
        /// otherwise (false) the complete program bank should be serialized.</param>
        /// <returns>Returns null when the plugin does not implement the <see cref="IVstPluginPersistence"/> 
        /// and/or <see cref="IVstPluginPrograms"/> interfaces.</returns>
        public virtual byte[] GetChunk(bool isPreset)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPersistence != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    VstProgramCollection programs = null;

                    if (pluginPrograms != null)
                    {
                        if (isPreset)
                        {
                            programs = new VstProgramCollection();

                            if (pluginPrograms.ActiveProgram != null)
                            {
                                programs.Add(pluginPrograms.ActiveProgram);
                            }
                            else if (pluginPrograms.Programs.Count > 0)
                            {
                                programs.Add(pluginPrograms.Programs[0]);
                            }
                        }
                        else
                        {
                            programs = pluginPrograms.Programs;
                        }
                    }
                    else
                    {
                        // allocate a dummy collection
                        programs = new VstProgramCollection();
                    }

                    pluginPersistence.WritePrograms(stream, programs);

                    return stream.GetBuffer();
                }
            }

            return null;
        }

        /// <summary>
        /// Called by the host to load in a previously serialized program buffer.
        /// </summary>
        /// <param name="data">The buffer provided by the host that contains the program data.</param>
        /// <param name="isPreset">True if only the current/active program should be deserialized, 
        /// otherwise (false) the complete program bank should be deserialized.</param>
        /// <returns>Returns the number of bytes read from the <paramref name="data"/> buffer or 
        /// zero if the plugin does not implement the <see cref="IVstPluginPersistence"/> 
        /// and/or <see cref="IVstPluginPrograms"/> interfaces.</returns>
        public virtual int SetChunk(byte[] data, bool isPreset)
        {
            IVstPluginPersistence pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();
            IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPersistence != null)
            {
                using (MemoryStream stream = new MemoryStream(data, false))
                {
                    // use a temp collection to leave the real Programs 
                    // collection in tact in case of an exception.
                    VstProgramCollection programs = new VstProgramCollection();
                    
                    pluginPersistence.ReadPrograms(stream, programs);

                    if (pluginPrograms != null)
                    {
                        if (isPreset)
                        {
                            VstProgram prog = null;

                            if (programs.Count > 0)
                            {
                                prog = programs[0];
                            }

                            if (prog != null)
                            {
                                VstProgram formerActiveProg = pluginPrograms.ActiveProgram;

                                if (formerActiveProg != null)
                                {
                                    int index = pluginPrograms.Programs.IndexOf(formerActiveProg);
                                    pluginPrograms.Programs.Insert(index, prog);

                                    pluginPrograms.ActiveProgram = prog;

                                    // remove the previously active program that has now been replaced
                                    pluginPrograms.Programs.Remove(formerActiveProg);

                                }
                                else
                                {
                                    pluginPrograms.Programs.Add(prog);
                                    pluginPrograms.ActiveProgram = prog;
                                }
                            }
                        }
                        else
                        {
                            pluginPrograms.ActiveProgram = null;
                            pluginPrograms.Programs.Clear();
                            pluginPrograms.Programs.AddRange(programs);
                        }
                    }

                    return (int)stream.Position;
                }
            }

            return 0;
        }

        #endregion

        #region IVstPluginCommandsBase Members

        /// <summary>
        /// Called by the host once every cycle to process incoming audio as well as output audio.
        /// </summary>
        /// <param name="inputs">An array with audio input buffers.</param>
        /// <param name="outputs">An array with audio output buffers.</param>
        /// <remarks>The implementation calls the <see cref="IVstPluginAudioProcessor"/> interface.</remarks>
        public virtual void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            IVstPluginAudioProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.Process(inputs, outputs);
            }
        }

        /// <summary>
        /// Called by the host once every cycle to process incoming audio as well as output audio.
        /// </summary>
        /// <param name="inputs">An array with audio input buffers.</param>
        /// <param name="outputs">An array with audio output buffers.</param>
        /// <remarks>The implementation calls the <see cref="IVstPluginAudioPrecisionProcessor"/> interface.</remarks>
        public virtual void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            IVstPluginAudioPrecisionProcessor audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioPrecisionProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.Process(inputs, outputs);
            }
        }

        /// <summary>
        /// Called by the host to assign a new <paramref name="value"/> to the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-base index into the parameters collection.</param>
        /// <param name="value">The new value for the parameter.</param>
        /// <remarks>
        /// The method will silently fail to change the parameter value if the current Program is read-only.
        /// </remarks>
        public virtual void SetParameter(int index, float value)
        {
            IVstPluginParameters pluginParams = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParams != null)
            {
                IVstPluginPrograms pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

                if (pluginPrograms != null && 
                    pluginPrograms.ActiveProgram != null && 
                    pluginPrograms.ActiveProgram.IsReadOnly)
                {
                    // do not allow the parameter to change value when the current program/preset is readonly.
                    return;
                }

                VstParameter parameter = pluginParams.Parameters[index];

                parameter.NormalizedValue = value;
            }
        }

        /// <summary>
        /// Called by the host to retrieve the current value of the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-base index into the parameters collection.</param>
        /// <returns>Returns 0.0 when the plugin does not implement the <see cref="IVstPluginParameters"/> interface.</returns>
        public virtual float GetParameter(int index)
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
        /// <returns>Returning null will abort loading the plugin.</returns>
        protected abstract IVstPlugin CreatePluginInstance();

        /// <summary>
        /// Creates summary info based on the <paramref name="plugin"/>.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        /// <returns>Never returns null.</returns>
        /// <remarks>Override to add or change behavior.</remarks>
        protected virtual VstPluginInfo CreatePluginInfo(IVstPlugin plugin)
        {
            VstPluginInfo pluginInfo = new VstPluginInfo();

            IVstPluginAudioProcessor audioProcessor = plugin.GetInstance<IVstPluginAudioProcessor>();

            // determine flags
            if (plugin.Supports<IVstPluginEditor>())
                pluginInfo.Flags |= VstPluginFlags.HasEditor;
            if (audioProcessor != null)
                pluginInfo.Flags |= VstPluginFlags.CanReplacing;
            if (plugin.Supports<IVstPluginAudioPrecisionProcessor>())
                pluginInfo.Flags |= VstPluginFlags.CanDoubleReplacing;
            if (plugin.Supports<IVstPluginPersistence>())
                pluginInfo.Flags |= VstPluginFlags.ProgramChunks;
            if(audioProcessor != null && plugin.Supports<IVstMidiProcessor>())
                pluginInfo.Flags |= VstPluginFlags.IsSynth;
            if ((plugin.Capabilities & VstPluginCapabilities.NoSoundInStop) > 0)
                pluginInfo.Flags |= VstPluginFlags.NoSoundInStop;

            // basic plugin info
            pluginInfo.InitialDelay = plugin.InitialDelay;
            pluginInfo.PluginID = plugin.PluginID;
            pluginInfo.PluginVersion = plugin.ProductInfo.Version;
            
            // audio processing info
            if(audioProcessor != null)
            {
                pluginInfo.AudioInputCount = audioProcessor.InputCount;
                pluginInfo.AudioOutputCount = audioProcessor.OutputCount;
            }

            // parameter info
            IVstPluginParameters pluginParameters = plugin.GetInstance<IVstPluginParameters>();
            if (pluginParameters != null)
            {
                pluginInfo.ParameterCount = pluginParameters.Parameters.Count;
            }

            // program info
            IVstPluginPrograms pluginPrograms = plugin.GetInstance<IVstPluginPrograms>();
            if(pluginPrograms != null)
            {
                pluginInfo.ProgramCount = pluginPrograms.Programs.Count;
            }

            return pluginInfo;
        }
    }
}
