using System.Collections.Generic;

namespace ComWrapperGen.Meta;

internal class MethodInfo
{
    private readonly List<ParameterInfo> _parameters = new();

    public string Name { get; init; }
    public IList<ParameterInfo> Parameters => _parameters;
    public TypeInfo ReturnType { get; init; }

    public bool PreserveSignature { get; init; }
}
