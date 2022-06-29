using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.ComWrappers;

namespace ComWrapperGen
{
    internal class Context
    {
        public string Namespace { get; set; }
        public string AbiNamespace { get; set; } = "ABI";
        public string ComWrapperClassName { get; set; }

        // indent
        private int _indent;
        public string Indent(int delta = 0)
        {
            _indent += delta;
            return new string(' ', _indent * 4); ;
        }

        public void SetIndent(int indent) => _indent = indent;
    }
}
