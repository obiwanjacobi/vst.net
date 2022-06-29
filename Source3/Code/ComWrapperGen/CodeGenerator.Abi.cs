using System.Linq;
using System.Text;
using ComWrapperGen.Meta;

namespace ComWrapperGen
{
    internal partial class CodeGenerator
    {
        private class AbiGenerator
        {
            private StringBuilder _abi;
            private Context _context;

            public AbiGenerator(StringBuilder abi, Context context)
            {
                _abi = abi;
                _context = context;
            }

            public void GenInterface(InterfaceInfo intf)
            {
                _context.SetIndent(1);

                _abi
                    .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"android\")]")
                    .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"browser\")]")
                    .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"ios\")]")
                    .AppendLine($"{_context.Indent()}[System.Runtime.Versioning.UnsupportedOSPlatform(\"tvos\")]")
                    .AppendLine($"{_context.Indent()}unsafe internal static partial class {intf.Name.Name}{Naming.ManagedWrapper}")
                    .AppendLine($"{_context.Indent()}{{")
                    .AppendLine($"{_context.Indent(1)}")
                ;

                foreach (var method in intf.Methods)
                {
                    _abi.AppendLine($"{_context.Indent()}[UnmanagedCallersOnly]");
                        
                    // TODO: gen method impl.
                    if (method.PreserveSignature)
                    {
                        var methodDecl = $"public static {method.ReturnType.Name} {method.Name}(System.IntPtr self, {string.Join(", ", method.Parameters.Select(p => $"{p.TypeInfo.Name} {p.Name}"))})";

                        _abi.AppendLine($"{_context.Indent()}{methodDecl}")
                            .AppendLine($"{_context.Indent()}{{");
                    }
                    else
                    {
                        var methodDecl = $"public static System.Int32 {method.Name}(System.IntPtr self, {string.Join(", ", method.Parameters.Select(p => $"{p.TypeInfo.Name} {p.Name}"))}, System.IntPtr result)";

                        _abi.AppendLine($"{_context.Indent()}{methodDecl}")
                            .AppendLine($"{_context.Indent()}{{");
                    }

                    _abi.AppendLine($"{_context.Indent()}}}")
                        .AppendLine();
                }

                _abi.AppendLine($"{_context.Indent(-1)}}}")
                ;
            }
        }
    }
}
