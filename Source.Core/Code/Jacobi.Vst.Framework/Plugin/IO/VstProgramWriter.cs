using System;
using System.IO;
using System.Text;

namespace Jacobi.Vst.Framework.Plugin.IO
{
    /// <summary>
    /// The VstProgramWriter class writes Programs and its Parameters to a stream.
    /// </summary>
    /// <remarks>
    /// The objects themselves are not serialized, only identifying information 
    /// (name) and the Parameter values.
    /// </remarks>
    public class VstProgramWriter
    {
        /// <summary>
        /// Constructs a new instance on an <paramref name="output"/> stream.
        /// </summary>
        /// <param name="output">Will receive the data written. Must not be null.</param>
        public VstProgramWriter(Stream output)
        {
            Writer = new BinaryWriter(output);
        }

        /// <summary>
        /// Constructs a new instance on an <paramref name="output"/> stream.
        /// </summary>
        /// <param name="output">Will receive the data written. Must not be null.</param>
        /// <param name="encoding">Must not be null.</param>
        public VstProgramWriter(Stream output, Encoding encoding)
        {
            Writer = new BinaryWriter(output, encoding);
        }

        /// <summary>
        /// Gets the writer used for writing to the output stream.
        /// </summary>
        protected BinaryWriter Writer { get; private set; }

        /// <summary>
        /// Writes the Programs in the <paramref name="programs"/> collection to the output stream.
        /// </summary>
        /// <param name="programs">Must not be null.</param>
        public void Write(VstProgramCollection programs)
        {
            WriteProgramHeader(programs);

            foreach (VstProgram program in programs)
            {
                Write(program);
            }
        }

        /// <summary>
        /// Writes the <paramref name="program"/> to the output stream.
        /// </summary>
        /// <param name="program">Must not be null.</param>
        public virtual void Write(VstProgram program)
        {
            Writer.Write(program.Name);
            Write(program.Parameters);
        }

        /// <summary>
        /// Writes the Parameters in <paramref name="parameters"/> to the output stream.
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
        protected void Write(VstParameterCollection parameters)
        {
            WriteParameterHeader(parameters);

            foreach (VstParameter parameter in parameters)
            {
                Write(parameter);
            }
        }

        /// <summary>
        /// Writes the <paramref name="parameter"/> to the output stream.
        /// </summary>
        /// <param name="parameter">Must not be null.</param>
        protected virtual void Write(VstParameter parameter)
        {
            Writer.Write(parameter.Info.Name);
            Writer.Write(parameter.Value);
        }

        /// <summary>
        /// Writes a header for the <paramref name="programs"/> to the output stream.
        /// </summary>
        /// <param name="programs">Must not be null.</param>
        protected virtual void WriteProgramHeader(VstProgramCollection programs)
        {
            Writer.Write(typeof(VstProgram).FullName);
            Writer.Write(programs.Count);
        }

        /// <summary>
        /// Writes a header for the <paramref name="parameters"/> to the output stream.
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
        protected virtual void WriteParameterHeader(VstParameterCollection parameters)
        {
            Writer.Write(typeof(VstParameter).FullName);
            Writer.Write(parameters.Count);
        }
    }
}
