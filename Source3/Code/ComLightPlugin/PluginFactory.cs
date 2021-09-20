using System;
using System.Runtime.InteropServices;

namespace ComLightPlugin
{
    internal class PluginFactory : IPluginFactory
    {
        private readonly static Guid ClassId = Guid.NewGuid();

        public Int32 CountClasses()
        {
            return 1;
        }

        public Int32 CreateInstance([In] ref Guid classId, [In] ref Guid interfaceId, [In, Out] ref IntPtr instance)
        {
            instance = IntPtr.Zero;
            return TResult.E_Fail;
        }

        public Int32 GetClassInfo([In] Int32 index, [In, Out] ref PClassInfo info)
        {
            info.Cardinality = 1;
            info.Category = "Effect";
            info.ClassId = ClassId;
            info.Name = "TestClass";
            return TResult.S_OK;
        }

        public Int32 GetFactoryInfo([In, Out] ref PFactoryInfo info)
        {
            info.Email = "obiwanjacobi@hotmail.com";
            info.Flags = PFactoryInfo.FactoryFlags.NoFlags;
            info.Url = "";
            info.Vendor = "Jacobi Software";
            return TResult.S_OK;
        }
    }
}
