using System;
using System.IO;
using System.Text;

namespace Jacobi.Vst.Framework.Plugin.IO
{
    /// <summary>
    /// This class writes parameter values of programs to a stream.
    /// </summary>
    /// <remarks>The objects themselves are not serialized, only identifying information (name) and the parameter values.</remarks>
    public class VstProgramWriter
    {
        public VstProgramWriter(Stream output)
        {
            Writer = new BinaryWriter(output);
        }

        public VstProgramWriter(Stream output, Encoding encoding)
        {
            Writer = new BinaryWriter(output, encoding);
        }

        protected BinaryWriter Writer { get; private set; }

        public void Write(VstProgramCollection programs)
        {
            WriteProgramHeader(programs);

            foreach (VstProgram program in programs)
            {
                Write(program);
            }
        }

        public virtual void Write(VstProgram program)
        {
            Writer.Write(program.Name);
            Write(program.Parameters);
        }

        public void Write(VstParameterCollection parameters)
        {
            WriteParameterHeader(parameters);

            foreach (VstParameter parameter in parameters)
            {
                Write(parameter);
            }
        }

        public virtual void Write(VstParameter parameter)
        {
            Writer.Write(parameter.Info.Name);
            Writer.Write(parameter.Value);
        }

        protected virtual void WriteProgramHeader(VstProgramCollection programs)
        {
            Writer.Write(typeof(VstProgram).Name);
            Writer.Write(programs.Count);
        }

        protected virtual void WriteParameterHeader(VstParameterCollection parameters)
        {
            Writer.Write(typeof(VstParameter).Name);
            Writer.Write(parameters.Count);
        }
    }
}
