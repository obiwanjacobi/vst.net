using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Jacobi.Vst3.Interop.Plugin
{
    public static class ReflectionExtensions
    {
        public static Assembly GetExportAssembly()
        {
            var stack = new StackTrace();

            var result = (from sf in stack.GetFrames()
                         let m = sf.GetMethod()
                         where m != null && m.DeclaringType != null && m.DeclaringType.Assembly != null
                         select m.DeclaringType.Assembly).LastOrDefault();

            return result;
        }

        public static Version GetAssemblyVersion(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        public static string GetGuidFromType(this Type classType)
        {
            var attrs = classType.GetCustomAttributes(typeof(GuidAttribute), true);

            if (attrs != null && attrs.Length > 0)
            {
                var guidAttr = (GuidAttribute)attrs[0];
                return guidAttr.Value.ToUpperInvariant();
            }

            return null;
        }

        public static string GetNameFromType(this Type classType)
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
