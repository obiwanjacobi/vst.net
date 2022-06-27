using System;
using System.Text;
using ComWrapperGen.Meta;

namespace ComWrapperGen
{
    internal partial class CodeGenerator
    {
        private readonly StringBuilder _vtables = new();
        private readonly StringBuilder _abi = new();
        private readonly StringBuilder _interfaces = new();
        private readonly StringBuilder _objects = new();
        private readonly Context _context;

        public CodeGenerator(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void GenerateInterfaceVtable(InterfaceInfo intf)
        {
            var vtables = new VTableGenerator(_vtables, _context);
            vtables.GenInterface(intf);
        }

        public void GenerateABI(InterfaceInfo intf)
        {

        }
    }
}
