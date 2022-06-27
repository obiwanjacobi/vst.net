namespace ComWrapperGen.Meta;

internal class NameInfo
{
    public string Name { get;init; }
    public string Namespace { get; init; }
    public string FullName { get; init; }

    public override string ToString()
        => FullName;
}
