namespace ComWrapperGen.Meta;

internal class TypeInfo
{
    public NameInfo Name { get; init; }
    
    public bool IsInterface { get; init; }
    public bool IsBlittable { get; init; }
    public bool IsValueType { get; init; }
}
