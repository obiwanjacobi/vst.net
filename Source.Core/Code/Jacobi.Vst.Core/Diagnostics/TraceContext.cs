namespace Jacobi.Vst.Core.Diagnostics
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// This class represents the context that is used during tracing debug messages.
    /// </summary>
    /// <remarks>
    /// Internally this class uses the <see cref="TraceSource"/> class to output its trace messages. \
    /// Therefor you can configure its options in a standard config file.
    /// </remarks>
    public partial class TraceContext
    {
        private TraceSource _traceSource;
        private string _contextDescription;
        private Type _commandInterface;

        /// <summary>
        /// Consrtucts a new instance using the <paramref name="contextName"/> and specified <paramref name="commandInterface"/>.
        /// </summary>
        /// <param name="contextName">The name of the context by which it can be referenced in the config file. Must not be null or empty.</param>
        /// <param name="commandInterface">An optional indication of what command interface this instance traces for. 
        /// This parameter is also used to translate the <b>opcode</b> parameter of the <see cref="WriteDispatchBegin"/> method.</param>
        public TraceContext(string contextName, Type commandInterface)
        {
            _traceSource = new TraceSource(contextName);

            if (commandInterface != null)
            {
                _contextDescription = String.Format("[{0}]: ", commandInterface.FullName);
                _commandInterface = commandInterface;
            }
        }

        /// <summary>
        /// Indicates if the specified <paramref name="eventType"/> is enable and should produce a trace.
        /// </summary>
        /// <param name="eventType">An indication of the type of trace event.</param>
        /// <returns>Returns true when the specified <paramref name="eventType"/> should produce a trace.</returns>
        public bool ShouldTrace(TraceEventType eventType)
        {
            return _traceSource.Switch.ShouldTrace(eventType);
        }

        /// <summary>
        /// Writes a start trace of the Dispatch method.
        /// </summary>
        /// <param name="opcode">The dispatched opcode.</param>
        /// <param name="index">Optional dispatch paremeter.</param>
        /// <param name="value">Optional dispatch paremeter.</param>
        /// <param name="ptr">Optional dispatch paremeter.</param>
        /// <param name="opt">Optional dispatch paremeter.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Verbose"/> event type.
        /// </remarks>
        public void WriteDispatchBegin(int opcode, int index, IntPtr value, IntPtr ptr, float opt)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = "Dispatch (Begin) ";
                OpcodeInfo opcodeInfo = LookupOpcodeInfo(opcode);

                if (opcodeInfo != null)
                {
                    message += opcodeInfo.FormatArguments(index, value, ptr, opt);
                }
                else
                {
                    message += String.Format("Opcode={0} Index={1}, Value={2} Ptr={3} Opt={4}.", opcode, index, value, ptr, opt);
                }

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        /// <summary>
        /// Writes an end trace of the Dispatch method.
        /// </summary>
        /// <param name="result">The value returned from the Dispatch method.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Verbose"/> event type.
        /// </remarks>
        public void WriteDispatchEnd(IntPtr result)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("Dispatch (End) Result={0}.", result);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        /// <summary>
        /// Writes an error trace for the <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">Must not be null.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Error"/> event type.
        /// </remarks>
        public void WriteError(Exception exception)
        {
            Throw.IfArgumentIsNull(exception, nameof(exception));

            if (_traceSource.Switch.ShouldTrace(TraceEventType.Error))
            {
                string message = exception.ToString();

                WriteEvent(TraceEventType.Error, message);
            }
        }

        /// <summary>
        /// Writes a trace for the Process method.
        /// </summary>
        /// <param name="inputBufferCount">The number of input buffers.</param>
        /// <param name="outputBufferCount">The number of output buffers.</param>
        /// <param name="inputSampleCount">The number of samples in the input buffers.</param>
        /// <param name="outputSampleCount">The number of samples in the output buffers.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Verbose"/> event type.
        /// </remarks>
        public void WriteProcess(int inputBufferCount, int outputBufferCount, int inputSampleCount, int outputSampleCount)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("Process InputBufferCount={0}, OutputBufferCount={1} InputSampleCount={2} OutputSampleCount={3}.", inputBufferCount, outputBufferCount, inputSampleCount, outputSampleCount);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        /// <summary>
        /// Writes a start trace for the GetParameters method.
        /// </summary>
        /// <param name="parameterIndex">The index of the parameter requested.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Verbose"/> event type.
        /// </remarks>
        public void WriteGetParameterBegin(int parameterIndex)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("GetParameter (Begin) ParameterIndex={0}.", parameterIndex);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        /// <summary>
        /// Writes the end trace for the GetParameter method.
        /// </summary>
        /// <param name="value">The parameter value that will be returned from the GetParameter method.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Verbose"/> event type.
        /// </remarks>
        public void WriteGetParameterEnd(float value)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("GetParameter (End) Value={0}.", value);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        /// <summary>
        /// Writes a trace for the SetParameter method.
        /// </summary>
        /// <param name="parameterIndex">The index of the parameter.</param>
        /// <param name="value">The value for the paremeter.</param>
        /// <remarks>
        /// The trace text is output as a <see cref="TraceEventType.Verbose"/> event type.
        /// </remarks>
        public void WriteSetParameter(int parameterIndex, float value)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("SetParameter ParameterIndex={0}, Value={1}.", parameterIndex, value);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        /// <summary>
        /// Writes a generic trace containing the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="eventType">The type of trace event.</param>
        /// <param name="message">The trace message text.</param>
        public void WriteEvent(TraceEventType eventType, string message)
        {
            if (_traceSource.Switch.ShouldTrace(eventType))
            {
                _traceSource.TraceEvent(eventType, 0, _contextDescription + message);
            }
        }

        /// <summary>
        /// Writes a generic trace containing the <paramref name="format"/>ted string containing the <paramref name="parameters"/>.
        /// </summary>
        /// <param name="eventType">The type of trace event.</param>
        /// <param name="format">A string formatter.</param>
        /// <param name="parameters">Variable arguments used in the <paramref name="format"/> string.</param>
        public void WriteEvent(TraceEventType eventType, string format, params object[] parameters)
        {
            if (_traceSource.Switch.ShouldTrace(eventType))
            {
                string message = String.Format(format, parameters);
                _traceSource.TraceEvent(eventType, 0, _contextDescription + message);
            }
        }

        // refer to TraceContext.OpcodeInfo.cs
        private OpcodeInfo LookupOpcodeInfo(int opcode)
        {
            OpcodeInfo[] lookupTable = null;

            if (typeof(IVstHostCommands20).IsAssignableFrom(_commandInterface))
            {
                lookupTable = _dispatchHost;
            }

            if (typeof(IVstPluginCommands24).IsAssignableFrom(_commandInterface))
            {
                lookupTable = _dispatchPlugin;
            }

            if (lookupTable != null &&
                opcode < lookupTable.Length)
            {
                return lookupTable[opcode];
            }

            return null;
        }
    }
}
