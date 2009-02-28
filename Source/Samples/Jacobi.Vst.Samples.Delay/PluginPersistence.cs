namespace Jacobi.Vst.Samples.Delay
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    /// <summary>
    /// This class manages custom persistence for all the plugin's programs and parameters.
    /// </summary>
    internal class PluginPersistence : IVstPluginPersistence
    {
        private FxTestPlugin _plugin;
        private Encoding _encoding = Encoding.ASCII;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public PluginPersistence(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstPluginPersistence Members

        /// <summary>
        /// Reads the programs for the <paramref name="stream"/> and fills the <paramref name="programs"/> collection.
        /// </summary>
        /// <param name="stream">Must not be null.</param>
        /// <param name="programs">Must not be null.</param>
        public void ReadPrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryReader reader = new BinaryReader(stream, _encoding);

            int count = reader.ReadInt32();

            for(int i = 0; i < count; i++)
            {
                VstProgram program = ReadProgram(reader);

                programs.Add(program);
            }
        }

        /// <summary>
        /// Writes all the <paramref name="programs"/> to the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">Must not be null.</param>
        /// <param name="programs">Must not be null.</param>
        public void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryWriter writer = new BinaryWriter(stream, _encoding);

            writer.Write(programs.Count);

            foreach (VstProgram program in programs)
            {
                WriteProgram(writer, program);
            }
        }

        /// <summary>
        /// Called just before reading in programs to detect (in)compatibility.
        /// </summary>
        /// <param name="chunkInfo">Must not be null.</param>
        /// <returns>Returns true when the data can be read.</returns>
        public bool CanLoadChunk(VstPatchChunkInfo chunkInfo)
        {
            // TODO: determine version
            return true;
        }

        #endregion

        /// <summary>
        /// Helper method to read one program and its parameters.
        /// </summary>
        /// <param name="reader">Must not be null.</param>
        /// <returns>Never returns null.</returns>
        private VstProgram ReadProgram(BinaryReader reader)
        {
            VstProgram program = CreateProgram();

            program.Name = reader.ReadString();
            int paramCount = reader.ReadInt32();

            for (int i = 0; i < paramCount; i++ )
            {
                string paramName = reader.ReadString();

                if (program.Parameters.Contains(paramName))
                {
                    VstParameter parameter = program.Parameters[paramName];
                    parameter.Value = reader.ReadSingle();
                }
                else
                {
                    // keep stream in sync.
                    reader.ReadSingle();
                }
            }

            return program;
        }

        /// <summary>
        /// Helper method to write one program and its parameters.
        /// </summary>
        /// <param name="writer">Must not be null.</param>
        /// <param name="program">Must not be null.</param>
        private void WriteProgram(BinaryWriter writer, VstProgram program)
        {
            writer.Write(program.Name);
            writer.Write(program.Parameters.Count);

            foreach (VstParameter parameter in program.Parameters)
            {
                writer.Write(parameter.Info.Name);
                writer.Write(parameter.Value);
            }
        }

        /// <summary>
        /// Helper method to create a new program and its parameters.
        /// </summary>
        /// <returns>Never returns null.</returns>
        private VstProgram CreateProgram()
        {
            VstProgram program = new VstProgram(_plugin.ParameterFactory.Categories);

            _plugin.ParameterFactory.CreateParameters(program.Parameters);

            return program;
        }
    }
}
