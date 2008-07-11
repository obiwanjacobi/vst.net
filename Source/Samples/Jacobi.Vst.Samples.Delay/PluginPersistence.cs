namespace Jacobi.Vst.Samples.Delay
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    internal class PluginPersistence : IVstPluginPersistence
    {
        private FxTestPlugin _plugin;
        private Encoding _encoding = Encoding.ASCII;

        public PluginPersistence(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstPluginPersistence Members

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

        public void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryWriter writer = new BinaryWriter(stream, _encoding);

            writer.Write(programs.Count);

            foreach (VstProgram program in programs)
            {
                WriteProgram(writer, program);
            }
        }

        public bool CanLoadChunk(VstPatchChunkInfo chunkInfo)
        {
            // TODO: determine version
            return true;
        }

        #endregion

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
            }

            return program;
        }

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

        private VstProgram CreateProgram()
        {
            VstProgram program = new VstProgram(_plugin.ParameterFactory.Categories);

            _plugin.ParameterFactory.CreateParameters(program.Parameters);

            return program;
        }
    }
}
