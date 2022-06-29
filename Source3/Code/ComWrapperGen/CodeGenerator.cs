using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ComWrapperGen.Meta;
using static System.Runtime.InteropServices.ComWrappers;

namespace ComWrapperGen;

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
        _context.SetIndent(3);
        var vtables = new VTableGenerator(_vtables, _context);
        vtables.GenInterface(intf);
    }

    public void GenerateABI(InterfaceInfo intf)
    {
        var abi = new AbiGenerator(_abi, _context);
        abi.GenInterface(intf);
    }

    public override string ToString()
    {
        var code = new StringBuilder();
        _context.SetIndent(0);

        code
            .AppendLine("using System;")
            .AppendLine("using System.Collection.Generic;")
            .AppendLine("using System.Runtime.InteropServices;")
            .AppendLine()
            .AppendLine($"namespace {_context.Namespace}")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine($"{_context.Indent(1)}[System.Runtime.Versioning.UnsupportedOSPlatform(\"android\")]")
            .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"browser\")]")
            .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"ios\")]")
            .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"tvos\")]")
            .AppendLine($"{_context.Indent()}partial class {_context.ComWrapperClassName} : ComWrappers")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine($"{_context.Indent(1)}private static readonly Dictionary<String, IntPtr> _interfaces = new();")
            .AppendLine()
            .AppendLine("#pragma warning disable S3963 // 'static' fields should be initialized inline")
            .AppendLine($"{_context.Indent()}static {_context.ComWrapperClassName}()")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine($"{_context.Indent(1)}GetIUnknownImpl(out IntPtr {Naming.QueryInterfaceVarName}, out IntPtr {Naming.AddRefVarName}, out IntPtr {Naming.ReleaseVarName});")
            .AppendLine(_vtables.ToString())
            .AppendLine($"{_context.Indent(-1)}}}")
            .AppendLine("#pragma warning restore S3963 // 'static' fields should be initialized inline")
            .AppendLine()
            .AppendLine($"{_context.Indent()}public {_context.ComWrapperClassName}()")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine($"{_context.Indent(1)}ComWrappers.RegisterForMarshalling(this);")
            .AppendLine($"{_context.Indent(-1)}}}")
            .AppendLine()
            .AppendLine($"{_context.Indent()}protected override unsafe ComInterfaceEntry* ComputeVtables(object obj, CreateComInterfaceFlags flags, out int count)")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine($"{_context.Indent(1)}count = 0;")
            .AppendLine($"{_context.Indent()}return null;")
            .AppendLine($"{_context.Indent(-1)}}}")
            .AppendLine()
            .AppendLine($"{_context.Indent()}protected override object CreateObject(IntPtr externalComObject, CreateObjectFlags flags)")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine($"{_context.Indent(1)}return null;")
            .AppendLine($"{_context.Indent(-1)}}}")
            .AppendLine()
            .AppendLine($"{_context.Indent()}protected override void ReleaseObjects(IEnumerable objects) => throw new NotSupportedException();")
            .AppendLine($"{_context.Indent(-1)}}}")
            .AppendLine($"{_context.Indent(-1)}}}")
            .AppendLine()
            .AppendLine("// ---------------------------------------------------------------------")
            .AppendLine()
            .AppendLine($"namespace {_context.AbiNamespace}")
            .AppendLine($"{_context.Indent()}{{")
            .AppendLine(_abi.ToString())
            .AppendLine($"{_context.Indent()}}}")
        ;

        return code.ToString();
    }
}
