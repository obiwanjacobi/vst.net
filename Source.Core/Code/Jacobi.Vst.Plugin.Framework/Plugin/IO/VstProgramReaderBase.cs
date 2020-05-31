using System;
using System.IO;
using System.Text;

namespace Jacobi.Vst.Framework.Plugin.IO
{
    /// <summary>
    /// The VstProgramReaderBase class provides a stream reader base class that reads
    /// Program and Parameter information.
    /// </summary>
    /// <remarks>
    /// The way the Program and Parameter information is read is dependent on the way
    /// this data was written by the <see cref="VstProgramWriter"/> class.
    /// </remarks>
    public abstract class VstProgramReaderBase
    {
        /// <summary>
        /// Constructs a new instancebased on the <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">A stream that contains data that was previously written by the 
        /// <see cref="VstProgramWriter"/> class. Must not be null.</param>
        protected VstProgramReaderBase(Stream input)
        {
            Reader = new BinaryReader(input);
        }

        /// <summary>
        /// Constructs a new instancebased on the <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">A stream that contains data that was previously written by the 
        /// <see cref="VstProgramWriter"/> class. Must not be null.</param>
        /// <param name="encoding">Must not be null.</param>
        protected VstProgramReaderBase(Stream input, Encoding encoding)
        {
            Reader = new BinaryReader(input, encoding);
        }

        /// <summary>
        /// Gets the reader that derived classes can use to perform custom reads.
        /// </summary>
        protected BinaryReader Reader { get; private set; }

        /// <summary>
        /// The derived class implements this method to create an empty -default- 
        /// Progarm containing all Plugin Parameters.
        /// </summary>
        /// <returns>Never returns null.</returns>
        protected abstract VstProgram CreateProgram();

        /// <summary>
        /// Reads the stream and fills the <paramref name="programs"/> colllection with programs.
        /// </summary>
        /// <param name="programs">The collection does not have to be empty. Must not be null.</param>
        public void ReadPrograms(VstProgramCollection programs)
        {
            int count = ReadProgramHeader(programs);

            for (int i = 0; i < count; i++)
            {
                VstProgram program = ReadProgram();

                programs.Add(program);
            }
        }

        /// <summary>
        /// Returns a new Program instance initialized with the values read from the stream.
        /// </summary>
        /// <returns>Never returns null.</returns>
        /// <remarks>
        /// The Parameters are also read.
        /// </remarks>
        public virtual VstProgram ReadProgram()
        {
            VstProgram program = CreateProgram();

            program.Name = Reader.ReadString();

            ReadParameters(program.Parameters);

            return program;
        }

        /// <summary>
        /// Fills the <paramref name="parameters"/> collection with Parameters.
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
        protected virtual void ReadParameters(VstParameterCollection parameters)
        {
            int count = ReadParameterHeader(parameters);

            for (int i = 0; i < count; i++)
            {
                string name = Reader.ReadString();
                float value = Reader.ReadSingle();

                if (parameters.Contains(name))
                {
                    VstParameter parameter = parameters[name];
                    parameter.Value = value;
                }
                else
                {
                    OnParameterNotFound(parameters, name, value);
                }
            }
        }

        /// <summary>
        /// Called when a Parameter could not be found by name.
        /// </summary>
        /// <param name="parameters">The parameters that were discovered untill now. Can be used to add another Parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        protected virtual void OnParameterNotFound(VstParameterCollection parameters, string name, float value)
        {
            System.Diagnostics.Debug.WriteLine(
                String.Format("Parameter '{0}' was not found.", name), nameof(VstProgramReaderBase));
        }

        /// <summary>
        /// Reads a Program header from the stream.
        /// </summary>
        /// <param name="programs">Must not be null.</param>
        /// <returns>Returns the number of Programs expected after the header.</returns>
        protected virtual int ReadProgramHeader(VstProgramCollection programs)
        {
            string typeName = Reader.ReadString();
            int count = Reader.ReadInt32();

            return count;
        }

        /// <summary>
        /// Reads the Parameter header from the stream.
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
        /// <returns>Returns the number of Parameters expected after the header.</returns>
        protected virtual int ReadParameterHeader(VstParameterCollection parameters)
        {
            string typeName = Reader.ReadString();
            int count = Reader.ReadInt32();

            return count;
        }
    }
}
