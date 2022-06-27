using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComWrapperGen.Meta;

internal enum ParameterFlags
{
    None,
    In,
    Out,
    Ref,
}

internal class ParameterInfo
{
    public string Name { get; init; }
    public TypeInfo TypeInfo { get; init; }
    public ParameterFlags Flags { get; init; }
}
