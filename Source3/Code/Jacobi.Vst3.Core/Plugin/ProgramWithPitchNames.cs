using System.Collections.Generic;

namespace Jacobi.Vst3.Plugin
{
    public class ProgramWithPitchNames : Program
    {
        private readonly Dictionary<int, string> _pitchNames = new Dictionary<int, string>();

        public ProgramWithPitchNames(string name)
            : base(name)
        { }

        public IDictionary<int, string> PitchNames
        {
            get { return _pitchNames; }
        }
    }
}
