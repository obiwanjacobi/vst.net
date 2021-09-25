namespace ComWrapperGen
{
    internal class Context
    {
        public string Namespace { get; set; }
        public string AbiNamespace { get; set; }
        public string ComWrapperClassName { get; set; }

        // indent
        public int Indent { get; set; }
        public string Tabs => new string(' ', Indent * 4);

    }
}
