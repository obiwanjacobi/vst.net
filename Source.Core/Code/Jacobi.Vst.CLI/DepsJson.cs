using System.Collections.Generic;

namespace Jacobi.Vst.CLI
{
    internal sealed class DepsJson
    {
        public Dictionary<string, TargetName> Targets { get; set; }
    }

    internal sealed class TargetName : Dictionary<string, Target>
    { }

    internal sealed class Target
    {
        public Dictionary<string, string> Dependencies { get; set; }
        public Dictionary<string, DepInfo> Runtime { get; set; }
    }

    internal sealed class DepInfo
    {
        public string AssemblyVersion { get; set; }
        public string FileVersion { get; set; }
    }
}
