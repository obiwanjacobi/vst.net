using System;
using System.Collections.Generic;

namespace ComWrapperGen.Meta;

internal class InterfaceInfo
{
    private readonly List<MethodInfo> _methods = new();

    public NameInfo Name { get; init; }
    public IList<MethodInfo> Methods => _methods;

    public Guid IID { get; init; }
    
}
