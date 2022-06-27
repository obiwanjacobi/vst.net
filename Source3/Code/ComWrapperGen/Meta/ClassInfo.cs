using System.Collections.Generic;

namespace ComWrapperGen.Meta;

internal class ClassInfo
{
    public NameInfo Name { get; init; }
    public IList<InterfaceInfo> Interfaces { get; init; }
}
