using System;
using System.Linq;
using System.Text;
using ComWrapperGen.Meta;
using static System.Runtime.InteropServices.ComWrappers;

namespace ComWrapperGen
{
    partial class CodeGenerator
    {
        private class VTableGenerator
        {
            private const int IUnknownMethodCount = 3;

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
                _code.AppendLine($"{_context.Indent()}{{")
                    // allocate vtable memory: one IntPtr for each method (add 3 extra slots for IUnknown)
                    .AppendLine($"{_context.Indent(1)}var vTable = (IntPtr*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof({_context.ComWrapperClassName}), IntPtr.Size * {intf.Methods.Count + IUnknownMethodCount});")
                    // IUnknown method ptrs
                    .AppendLine($"{_context.Indent()}vTable[{i++}] = {Naming.QueryInterfaceVarName};")
                    .AppendLine($"{_context.Indent()}vTable[{i++}] = {Naming.AddRefVarName};")
                    .AppendLine($"{_context.Indent()}vTable[{i++}] = {Naming.ReleaseVarName};")
                ;

                foreach (var method in intf.Methods)
                {
                    var abiName = $"{_context.AbiNamespace}.{intf.Name.Name}{Naming.ManagedWrapper}.{method.Name}";
                    var abiParamTypes = String.Join(", ", method.Parameters.Select(p => ToInteropTypeName(p.TypeInfo)));
                    _code.AppendLine($"{_context.Indent()}vTable[{i++}] = (IntPtr)(delegate* unmanaged<IntPtr, {abiParamTypes}, Int32>)&{abiName};");
                }

                _code
                    .AppendLine()
                    .AppendLine($"{_context.Indent()}{Naming.InterfacesProp}.Add(\"{intf.IID}\", vTable);")
                    .AppendLine($"{_context.Indent(-1)}}}");
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
