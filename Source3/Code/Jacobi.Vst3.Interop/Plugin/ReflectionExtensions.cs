using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Jacobi.Vst3.Interop.Plugin
{
    internal static class ReflectionExtensions
    {
        public static Assembly GetExportAssembly()
        {
            var stack = new StackTrace();

            // TODO: this will probably fail when running in a managed host!
            var result = (from sf in stack.GetFrames().Reverse()
                         let m = sf.GetMethod()
                         where m != null && m.DeclaringType != null && m.DeclaringType.Assembly != null
                         select m.DeclaringType.Assembly).FirstOrDefault();

            return result;
        }

        public static Version GetAssemblyVersion(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        //public static string GetClassGuid(this Type classType)
        //{
        //    // .NET takes the GuidAttribute value and puts it in the GUID property on Type.
        //    if (classType.GUID != Guid.Empty)
        //    {
        //        return classType.GUID.ToString().ToUpperInvariant();
        //    }

        //    // get the Guid Attibute
        //    var attrs = classType.GetCustomAttributes(typeof(GuidAttribute), true);

        //    if (attrs != null && attrs.Length > 0)
        //    {
        //        var guidAttr = (GuidAttribute)attrs[0];
        //        return guidAttr.Value.ToUpperInvariant();
        //    }

        //    return null;
        //}

        public static string GetDisplayName(this Type classType)
        {
            var attrs = classType.GetCustomAttributes(typeof(DisplayNameAttribute), true);

            if (attrs != null && attrs.Length > 0)
            {
                var guidAttr = (DisplayNameAttribute)attrs[0];
                return guidAttr.DisplayName;
            }

            return null;
        }
    }
}
