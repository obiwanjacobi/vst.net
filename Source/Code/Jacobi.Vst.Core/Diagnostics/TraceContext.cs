namespace Jacobi.Vst.Core.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Configuration;

    public class TraceContext
    {
        private TraceSource _traceSource;
        private string _contextDescription;
        private Type _commandInterface;

        public TraceContext(string contextName, Type commandInterface)
        {
            _traceSource = new TraceSource(contextName);

            _contextDescription = String.Format("[{0}]: ", commandInterface.FullName);
            _commandInterface = commandInterface;
        }

        public bool ShouldTrace(TraceEventType eventType)
        {
            return _traceSource.Switch.ShouldTrace(eventType);
        }

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

        public void WriteDispatchEnd(IntPtr result)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("Dispatch (End) Result={0}.", result);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        public void WriteError(Exception exception)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Error))
            {
                string message = exception.ToString();

                WriteEvent(TraceEventType.Error, message);
            }
        }

        public void WriteProcess(int inputBufferCount, int outputBufferCount, int inputSampleCount, int outputSampleCount)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("Process InputBufferCount={0}, OutputBufferCount={1} InputSampleCount={2} OutputSampleCount={3}.", inputBufferCount, outputBufferCount, inputSampleCount, outputSampleCount);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        public void WriteGetParameterBegin(int parameterIndex)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("GetParameter (Begin) ParameterIndex={0}.", parameterIndex);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        public void WriteGetParameterEnd(float value)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("GetParameter (End) Value={0}.", value);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        public void WriteSetParameter(int parameterIndex, float value)
        {
            if (_traceSource.Switch.ShouldTrace(TraceEventType.Verbose))
            {
                string message = String.Format("SetParameter ParameterIndex={0}, Value={1}.", parameterIndex, value);

                WriteEvent(TraceEventType.Verbose, message);
            }
        }

        public void WriteEvent(TraceEventType eventType, string message)
        {
            if (_traceSource.Switch.ShouldTrace(eventType))
            {
                _traceSource.TraceEvent(eventType, 0, _contextDescription + message);
            }
        }

        public void WriteEvent(TraceEventType eventType, string format, params object[] parameters)
        {
            if (_traceSource.Switch.ShouldTrace(eventType))
            {
                string message = String.Format(format, parameters);
                _traceSource.TraceEvent(eventType, 0, _contextDescription + message);
            }
        }

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

            if (lookupTable != null)
            {
                if (opcode < lookupTable.Length)
                {
                    return lookupTable[opcode];
                }
            }

            return null;
        }

        //---------------------------------------------------------------------

        private class OpcodeInfo
        {
            public OpcodeInfo(string description, bool deprecated)
            {
                Description = description;
                Deprecated = deprecated;
            }

            public string Description { get; private set; }
            public bool Deprecated { get; private set; }

            public string FormatArguments(int index, IntPtr value, IntPtr ptr, float opt)
            {
                string result = Description;

                if (Deprecated)
                {
                    result += " (deprecated)";
                }

                result += String.Format(", Index={0}, Value={1} Ptr={2} Opt={3}.", index, value, ptr, opt);

                return result;
            }
        }

        private static readonly OpcodeInfo[] _dispatchPlugin = 
        {
            // VST 1.0
            new OpcodeInfo("effOpen", false),
	        new OpcodeInfo("effClose", false),
	        new OpcodeInfo("effSetProgram", false),
	        new OpcodeInfo("effGetProgram", false),
	        new OpcodeInfo("effSetProgramName", false),
	        new OpcodeInfo("effGetProgramName", false),
	        new OpcodeInfo("effGetParamLabel", false),
	        new OpcodeInfo("effGetParamDisplay", false),
	        new OpcodeInfo("effGetParamName", false),
	        new OpcodeInfo("effGetVu", true),
	        new OpcodeInfo("effSetSampleRate", false),
	        new OpcodeInfo("effSetBlockSize", false),
	        new OpcodeInfo("effMainsChanged", false),
	        new OpcodeInfo("effEditGetRect", false),
	        new OpcodeInfo("effEditOpen", false),
	        new OpcodeInfo("effEditClose", false),
	        new OpcodeInfo("effEditDraw", true),
	        new OpcodeInfo("effEditMouse", true),
	        new OpcodeInfo("effEditKey", true),
	        new OpcodeInfo("effEditIdle", false),
	        new OpcodeInfo("effEditTop", true),
	        new OpcodeInfo("effEditSleep", true),
	        new OpcodeInfo("effIdentify", true),
	        new OpcodeInfo("effGetChunk", false),
	        new OpcodeInfo("effSetChunk", false),
        };

        private static readonly OpcodeInfo[] _dispatchHost =
        {
            // VST 1.0
            new OpcodeInfo("audioMasterAutomate", false),
	        new OpcodeInfo("audioMasterVersion", false),
	        new OpcodeInfo("audioMasterCurrentId", false),
	        new OpcodeInfo("audioMasterIdle", false),
	        new OpcodeInfo("audioMasterPinConnected", true),
        };

    }
}
