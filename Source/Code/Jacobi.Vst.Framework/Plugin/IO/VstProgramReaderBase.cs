using System;
using System.IO;
using System.Text;

namespace Jacobi.Vst.Framework.Plugin.IO
{
    public abstract class VstProgramReaderBase
    {
        public VstProgramReaderBase(Stream input)
        {
            Reader = new BinaryReader(input);
        }

        public VstProgramReaderBase(Stream input, Encoding encoding)
        {
            Reader = new BinaryReader(input, encoding);
        }

        protected BinaryReader Reader { get; private set; }

        protected abstract VstProgram CreateProgram();

        public void ReadPrograms(VstProgramCollection programs)
        {
            int count = ReadProgramHeader(programs);

            for (int i = 0; i < count; i++)
            {
                VstProgram program = ReadProgram();
                
                programs.Add(program);
            }
        }

        public virtual VstProgram ReadProgram()
        {
            VstProgram program = CreateProgram();

            program.Name = Reader.ReadString();

            ReadParameters(program.Parameters);

            return program;
        }

        public void ReadParameters(VstParameterCollection parameters)
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
            }
        }

        protected virtual int ReadProgramHeader(VstProgramCollection programs)
        {
            string typeName = Reader.ReadString();
            int count = Reader.ReadInt32();

            return count;
        }

        protected virtual int ReadParameterHeader(VstParameterCollection parameters)
        {
            string typeName = Reader.ReadString();
            int count = Reader.ReadInt32();

            return count;
        }
    }
}
