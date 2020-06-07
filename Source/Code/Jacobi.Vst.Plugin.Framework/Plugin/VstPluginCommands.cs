using Jacobi.Vst.Core;
using System;
using System.IO;

namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    public class VstPluginCommands : IVstPluginCommands24
    {
        private readonly VstPluginContext _pluginCtx;

        public VstPluginCommands(VstPluginContext pluginCtx)
        {
            _pluginCtx = pluginCtx ?? throw new ArgumentNullException(nameof(pluginCtx));
        }

        protected VstPluginContext PluginContext { get { return _pluginCtx; } }

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

            switch (precision)
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
            var midiProcessor = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>();

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
            var midiSource = _pluginCtx.Plugin.GetInstance<IVstPluginMidiSource>();

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
        public virtual bool GetSpeakerArrangement(out VstSpeakerArrangement? input, out VstSpeakerArrangement? output)
        {
            var pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                input = pluginConnections.InputSpeakerArrangement;
                output = pluginConnections.OutputSpeakerArrangement;

                return true;
            }

            input = null;
            output = null;
            return false;
        }

        /// <summary>
        /// Not implemented for managed plugins!
        /// </summary>
        /// <param name="name">Filled with the name of the next sub-plugin.</param>
        /// <returns>Returns the unique id of the next sub-plugin.</returns>
        public virtual int GetNextPlugin(out string name)
        {
            // not implemented for managed plugins!
            name = String.Empty;
            return 0;
        }

        /// <summary>
        /// Called just before Process cycle is started.
        /// </summary>
        /// <returns>Returns 0 (zero) when not supported. It is unclear what this return value represents.</returns>
        public virtual int StartProcess()
        {
            var pluginProcess = _pluginCtx.Plugin.GetInstance<IVstPluginProcess>();

            if (pluginProcess != null)
            {
                pluginProcess.Start();
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
            var pluginProcess = _pluginCtx.Plugin.GetInstance<IVstPluginProcess>();

            if (pluginProcess != null)
            {
                pluginProcess.Stop();
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
            var audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

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
            var pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();

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
            var pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();

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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                return pluginEditor.KeyDown(ascii, virtualKey, modifers);
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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

            if (pluginEditor != null)
            {
                return pluginEditor.KeyUp(ascii, virtualKey, modifers);
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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

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
            Throw.IfArgumentIsNull(midiProgramName, nameof(midiProgramName));

            var midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

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
            Throw.IfArgumentIsNull(midiProgramName, nameof(midiProgramName));

            var midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

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
            var midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

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
            var midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

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
            var midiPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginMidiPrograms>();

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
            var pluginProgram = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

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
            var pluginProgram = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

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
            var midiProcessor = _pluginCtx.Plugin.GetInstance<IVstMidiProcessor>();

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
            var pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

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
            var pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

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
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms != null)
            {
                VstProgram program = pluginPrograms.Programs[index];
                return program.Name;
            }

            return String.Empty;
        }

        /// <summary>
        /// Retrieves the pin properties for the input at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin inputs.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginConnections"/>.</returns>
        public virtual VstPinProperties? GetInputProperties(int index)
        {
            var pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                VstConnectionInfo connectionInfo = pluginConnections.InputConnectionInfos[index];

                VstPinProperties pinProps = new VstPinProperties
                {
                    Flags = VstPinPropertiesFlags.PinUseSpeaker,
                    ArrangementType = connectionInfo.SpeakerArrangementType,
                    Label = connectionInfo.Label,
                    ShortLabel = connectionInfo.ShortLabel
                };

                return pinProps;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the pin properties for the output at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin outputs.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginConnections"/>.</returns>
        public virtual VstPinProperties? GetOutputProperties(int index)
        {
            var pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

            if (pluginConnections != null)
            {
                VstConnectionInfo connectionInfo = pluginConnections.OutputConnectionInfos[index];

                VstPinProperties pinProps = new VstPinProperties
                {
                    Flags = VstPinPropertiesFlags.PinUseSpeaker,
                    ArrangementType = connectionInfo.SpeakerArrangementType,
                    Label = connectionInfo.Label,
                    ShortLabel = connectionInfo.ShortLabel
                };

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

        /// <summary>
        /// Called by the host to propose a new speaker arrangement.
        /// </summary>
        /// <param name="saInput">Must not be null.</param>
        /// <param name="saOutput">Must not be null.</param>
        /// <returns>Returns false if the plugin does not implement <see cref="IVstPluginConnections"/>.</returns>
        public virtual bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            var pluginConnections = _pluginCtx.Plugin.GetInstance<IVstPluginConnections>();

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
            var pluginBypass = _pluginCtx.Plugin.GetInstance<IVstPluginBypass>();

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
            var name = _pluginCtx.Plugin.Name;

            if (name != null && name.Length > Core.Constants.MaxEffectNameLength)
            {
                throw new InvalidOperationException(
                    String.Format(Properties.Resources.StdPluginCommandStub_StringTooLong,
                        "Plugin.Name", name, Core.Constants.MaxEffectNameLength));
            }

            return name ?? String.Empty;
        }

        /// <summary>
        /// Called to retrieve the plugin vendor information.
        /// </summary>
        /// <returns>Returns <see cref="IVstPlugin.ProductInfo"/>.Vendor.</returns>
        public virtual string GetVendorString()
        {
            return _pluginCtx.Plugin.ProductInfo.Vendor ?? String.Empty;
        }

        /// <summary>
        /// Called to retrieve the plugin product information.
        /// </summary>
        /// <returns>Returns <see cref="IVstPlugin.ProductInfo"/>.Product.</returns>
        public virtual string GetProductString()
        {
            return _pluginCtx.Plugin.ProductInfo.Product ?? String.Empty;
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

            VstCanDoResult Supports<T>() where T : class
            {
                var yes = _pluginCtx.Plugin.Supports<T>();
                return yes ? VstCanDoResult.Yes : VstCanDoResult.No;
            }

            switch (candoEnum)
            {
                case VstPluginCanDo.Bypass:
                    result = Supports<IVstPluginBypass>();
                    break;
                case VstPluginCanDo.MidiProgramNames:
                    result = Supports<IVstPluginMidiPrograms>();
                    break;
                case VstPluginCanDo.Offline:
                    result = VstCanDoResult.No;
                    break;
                case VstPluginCanDo.ReceiveVstEvents:
                case VstPluginCanDo.ReceiveVstMidiEvent:
                    result = Supports<IVstMidiProcessor>();
                    break;
                case VstPluginCanDo.ReceiveVstTimeInfo:
                    result = ((_pluginCtx.Plugin.Capabilities & VstPluginCapabilities.ReceiveTimeInfo) > 0) ? VstCanDoResult.Yes : VstCanDoResult.No;
                    break;
                case VstPluginCanDo.SendVstEvents:
                case VstPluginCanDo.SendVstMidiEvent:
                    result = Supports<IVstPluginMidiSource>();
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
            var audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

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
        public virtual VstParameterProperties? GetParameterProperties(int index)
        {
            var pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];

                // clear the normalization info for this parameter
                parameter.Info.NormalizationInfo = null;

                VstParameterProperties paramProps = new VstParameterProperties
                {
                    // labels
                    Label = parameter.Info.Label,
                    ShortLabel = parameter.Info.ShortLabel
                };

                VstParameterCategory? category = parameter.Info.Category;
                if (category != null)
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
            _pluginCtx.Plugin.Open(_pluginCtx.Host);
        }

        /// <summary>
        /// This is the last method the host calls. Dispose your resources.
        /// </summary>
        /// <remarks>Always call the base class when overriding.</remarks>
        public virtual void Close()
        {
            _pluginCtx.Dispose();
        }

        /// <summary>
        /// The plugin should activate the Program at <paramref name="programNumber"/>.
        /// </summary>
        /// <param name="programNumber">A zero-based program number (index).</param>
        public virtual void SetProgram(int programNumber)
        {
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

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
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

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
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms?.ActiveProgram != null &&
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
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPrograms?.ActiveProgram != null)
            {
                return pluginPrograms.ActiveProgram.Name;
            }

            return String.Empty;
        }

        /// <summary>
        /// Retrieves the label for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginParameters"/> 
        /// or when <see cref="VstParameterInfo.ShortLabel"/> was not set.</returns>
        public virtual string GetParameterLabel(int index)
        {
            var pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Info.ShortLabel;
            }

            return String.Empty;
        }

        /// <summary>
        /// Retrieves the display value for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginParameters"/> 
        /// or when <see cref="VstParameter.DisplayValue"/> was not set.</returns>
        public virtual string GetParameterDisplay(int index)
        {
            var pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.DisplayValue;
            }

            return String.Empty;
        }

        /// <summary>
        /// Retrieves the name for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns null when the plugin does not implement <see cref="IVstPluginParameters"/> 
        /// or when <see cref="VstParameterInfo.Name"/> was not set.</returns>
        public virtual string GetParameterName(int index)
        {
            var pluginParameters = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParameters != null)
            {
                VstParameter parameter = pluginParameters.Parameters[index];
                return parameter.Info.Name;
            }

            return String.Empty;
        }

        /// <summary>
        /// Assigns the <paramref name="sampleRate"/> to the plugin.
        /// </summary>
        /// <param name="sampleRate">The number of audio samples per second.</param>
        /// <remarks>The implementation uses the <see cref="IVstPluginAudioProcessor"/> interface. Override to change behavior.</remarks>
        public virtual void SetSampleRate(float sampleRate)
        {
            var audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

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
            var audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

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
            var plugin = _pluginCtx?.Plugin;
            if (plugin != null)
            {
                if (onoff)
                {
                    plugin.Resume();
                }
                else
                {
                    plugin.Suspend();
                }
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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

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
            var pluginEditor = _pluginCtx.Plugin.GetInstance<IVstPluginEditor>();

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
            var pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPersistence != null)
            {
                using MemoryStream stream = new MemoryStream();
                VstProgramCollection? programs = null;

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

            return Array.Empty<byte>();
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
            var pluginPersistence = _pluginCtx.Plugin.GetInstance<IVstPluginPersistence>();
            var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

            if (pluginPersistence != null)
            {
                using MemoryStream stream = new MemoryStream(data, false);
                // use a temp collection to leave the real Programs 
                // collection in tact in case of an exception.
                VstProgramCollection programs = new VstProgramCollection();

                pluginPersistence.ReadPrograms(stream, programs);

                if (pluginPrograms != null)
                {
                    if (isPreset)
                    {
                        VstProgram? prog = null;

                        if (programs.Count > 0)
                        {
                            prog = programs[0];
                        }

                        if (prog != null)
                        {
                            VstProgram? formerActiveProg = pluginPrograms.ActiveProgram;

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

                        if (programs.Count != pluginPrograms.Programs.Count)
                        {
                            int count = Math.Min(programs.Count, pluginPrograms.Programs.Count);

                            // replace the range of programs that overlap.
                            for (int i = 0; i < count; i++)
                            {
                                pluginPrograms.Programs[i] = programs[i];
                            }
                        }
                        else
                        {
                            pluginPrograms.Programs.Clear();
                            pluginPrograms.Programs.AddRange(programs);
                        }
                    }
                }

                return (int)stream.Position;
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
            var audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioProcessor>();

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
            var audioProcessor = _pluginCtx.Plugin.GetInstance<IVstPluginAudioPrecisionProcessor>();

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
            var pluginParams = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParams != null)
            {
                var pluginPrograms = _pluginCtx.Plugin.GetInstance<IVstPluginPrograms>();

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
            var pluginParams = _pluginCtx.Plugin.GetInstance<IVstPluginParameters>();

            if (pluginParams != null)
            {
                VstParameter parameter = pluginParams.Parameters[index];

                return parameter.NormalizedValue;
            }

            return 0.0f;
        }

        #endregion
    }
}
