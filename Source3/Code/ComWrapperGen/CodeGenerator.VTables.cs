using System;
using System.Linq;
using System.Text;

namespace ComWrapperGen
{
    partial class CodeGenerator
    {
        private class VTableGenerator
        {
            private readonly StringBuilder _code;
            private readonly Context _context;

            public VTableGenerator(StringBuilder code, Context context)
            {
                _code = code ?? throw new ArgumentNullException(nameof(code));
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public void GenInterface(Type intf)
            {
                var methods = intf.GetMethods();

                int i = 0;
                // allocate vtable memory: one IntPtr for each method (add 3 extra slots for IUnknown)
                _code.AppendLine($"{_context.Tabs}{Naming.VTableVarName} = (IntPtr*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof({_context.ComWrapperClassName}), IntPtr.Size * {methods.Length + 3}");
                // IUnknown method ptrs
                _code.AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = {Naming.QueryInterfaceVarName};");
                _code.AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = {Naming.AddRefVarName};");
                _code.AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = {Naming.ReleaseVarName};");

                foreach (var method in methods)
                {
                    var abiName = $"{_context.AbiNamespace}.{intf.Name}{Naming.ManagedWrapper}.{method.Name}";
                    var abiParams = String.Join(", ", method.GetParameters().Select(p => ToInteropTypeName(p.ParameterType)));
                    _code.AppendLine($"{_context.Tabs}{Naming.VTableVarName}[{i++}] = (IntPtr)(delegate* unmanaged<IntPtr, {abiParams}Int32>)&{abiName};");
                }
            }

            private string ToInteropTypeName(Type type)
            {
                if (!type.IsValueType)
                    return "IntPtr";
                return type.Name;
            }
        }
    }
}
