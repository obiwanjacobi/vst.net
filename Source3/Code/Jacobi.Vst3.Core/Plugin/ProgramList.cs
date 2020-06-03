using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jacobi.Vst3.Plugin
{
    public class ProgramList : Collection<Program>
    {
        public ProgramList(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public Program AddProgram(string name)
        {
            var program = new Program(name);

            Add(program);

            return program;
        }

        public ProgramWithPitchNames AddProgramWithPitchNames(string name)
        {
            var program = new ProgramWithPitchNames(name);

            Add(program);

            return program;
        }
    }

    

    
}
