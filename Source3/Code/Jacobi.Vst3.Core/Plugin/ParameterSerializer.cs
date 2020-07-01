using Jacobi.Vst3.Plugin;
using System.IO;

namespace Jacobi.Vst3.Core.Plugin
{
    public class ParameterSerializer
    {
        private readonly BStream _stream;

        public ParameterSerializer(IBStream stream, StreamAccessMode mode)
        {
            _stream = new BStream(stream, mode);
        }

        public ParameterSerializer(BStream stream)
        {
            _stream = stream;
        }

        public void Save(ParameterCollection parameters)
        {
            var writer = new BinaryWriter(_stream);
            writer.Write(parameters.Count);

            foreach (var parameter in parameters)
            {
                writer.Write(parameter.Id);
                writer.Write(parameter.PlainValue);
            }

            _stream.Flush();
        }

        public void Load(ParameterCollection parameters)
        {
            var reader = new BinaryReader(_stream);
            var count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                var id = reader.ReadUInt32();
                var value = reader.ReadDouble();

                if (parameters.Contains(id))
                {
                    parameters[id].PlainValue = value;
                }
            }
        }
    }
}
