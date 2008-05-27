namespace Jacobi.Vst.Framework.TestPlugin
{
    using System.IO;
    using System.Text;

    internal class PluginPersistence : IVstPluginPersistence
    {
        private FxTestPlugin _plugin;
        private Encoding _encoding = Encoding.ASCII;

        public PluginPersistence(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstPluginPersistence Members

        public VstProgram ReadProgram(Stream stream)
        {
            return ReadProgram(new BinaryReader(stream, _encoding));
        }

        public void ReadPrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryReader reader = new BinaryReader(stream, _encoding);

            while (stream.Position < stream.Length)
            {
                VstProgram program = ReadProgram(reader);

                programs.Add(program);
            }
        }

        public void WritePrograms(Stream stream, VstProgramCollection programs)
        {
            BinaryWriter writer = new BinaryWriter(stream, _encoding);

            foreach (VstProgram program in programs)
            {
                WriteProgram(writer, program);
            }
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
                    parameter.NormalizedValue = reader.ReadSingle();
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
                writer.Write(parameter.NormalizedValue);
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
