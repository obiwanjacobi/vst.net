using System;
using System.Linq;
using System.Text;
using ComWrapperGen.Meta;

namespace ComWrapperGen
{
    partial class CodeGenerator
    {
        private class VTableGenerator
        {
            private readonly StringBuilder _code;
            private readonly Context _context;
            // TODO: track dependent assemblies for generating 'usings'

            public VTableGenerator(StringBuilder code, Context context)
            {
                _code = code ?? throw new ArgumentNullException(nameof(code));
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public void GenInterface(InterfaceInfo intf)
            {
                int i = 0;
                // allocate vtable memory: one IntPtr for each method (add 3 extra slots for IUnknown
                _code.AppendLine($"{_context.Tabs}var {Naming.VTableVarName} = (IntPtr*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof({_context.ComWrapperClassName}), IntPtr.Size * {intf.Methods.Count + 3});")
                    // IUnknown method ptrs
                    .AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = {Naming.QueryInterfaceVarName};")
                    .AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = {Naming.AddRefVarName};")
                    .AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = {Naming.ReleaseVarName};")
                ;

                foreach (var method in intf.Methods)
                {
                    var abiName = $"{_context.AbiNamespace}.{intf.Name}{Naming.ManagedWrapper}.{method.Name}";
                    var abiParams = String.Join(", ", method.Parameters.Select(p => ToInteropTypeName(p.TypeInfo)));
                    _code.AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = (IntPtr)(delegate* unmanaged<IntPtr, {abiParams}, Int32>)&{abiName};");
                }
            }

            private static string ToInteropTypeName(TypeInfo typeInfo)
            {
                if (!typeInfo.IsValueType)
                    return "IntPtr";
                return typeInfo.Name.Name;
            }
        }
    }
}
