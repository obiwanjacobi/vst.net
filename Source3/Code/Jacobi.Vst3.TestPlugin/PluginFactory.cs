using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jacobi.Vst3.Interop;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    
    [ClassInterface(ClassInterfaceType.None)]
    class PluginFactory : IPluginFactory, IPluginFactory2, IPluginFactory3
    {
        private Guid classGuid = Guid.NewGuid();

        #region IPluginFactory Members

        public int GetFactoryInfo(ref PFactoryInfo info)
        {
            info.Flags = PFactoryInfo.FactoryFlags.Unicode;
            info.SafeSetEmail("obiwanjacobi@hotmail.com");
            info.SafeSetUrl("vstnet.codeplex.com");
            info.SafeSetVendor("Jacobi Software");

            return TResult.S_OK;
        }

        public int CountClasses()
        {
            return 1;
        }

        public int GetClassInfo(int index, ref PClassInfo info)
        {
            if (index == 0)
            {
                info.Cardinality = Constants.ClassCardinalityManyInstances;
                info.Category = "Audio Effect";
                info.ClassId = classGuid;
                info.Name = "VST.NET 3 Test Plugin";

                return TResult.S_OK;
            }

            return TResult.S_False;
        }

        public int CreateInstance(ref Guid classId, ref Guid interfaceId, ref IntPtr instance)
        {
            if (instance != IntPtr.Zero)
            {
                return TResult.E_Pointer;
            }

            if (this.classGuid == classId)
            {
                
    
                // substitute your actual object creation code for CreateObject
                object obj = new PluginComponent();

                // return the correct interface
                IntPtr unk = Marshal.GetIUnknownForObject(obj);

                try
                {
                    return TResult.FromInt32(Marshal.QueryInterface(unk, ref interfaceId, out instance));
                }
                finally
                {
                    Marshal.Release(unk);
                }
            }

            instance = IntPtr.Zero;
            return TResult.E_Fail;
        }

        #endregion

        #region IPluginFactory2 Members

        public int GetClassInfo2(int index, ref PClassInfo2 info)
        {
            if (index == 0)
            {
                info.Cardinality = Constants.ClassCardinalityManyInstances;
                info.Category = "Audio Effect";
                info.ClassId = classGuid;
                info.Name = "VST.NET 3 Test Plugin";
                info.ClassFlags = 1;
                info.Vendor = "Jacobi Software";
                info.Version = "1.0";
                info.SdkVersion = "3.5.2";

                return TResult.S_OK;
            }

            return TResult.S_False;
        }

        #endregion

        #region IPluginFactory3 Members


        public int GetClassInfoUnicode(int index, ref PClassInfoW info)
        {
            if (index == 0)
            {
                info.Cardinality = Constants.ClassCardinalityManyInstances;
                info.Category.Value = "Audio Effect";
                info.ClassId = classGuid;
                info.Name = "VST.NET 3 Test Plugin";
                info.ClassFlags = 1;
                info.Vendor = "Jacobi Software";
                info.Version = "1.0";
                info.SdkVersion = "3.5.2";

                return TResult.S_OK;
            }

            return TResult.S_False;
        }

        public int SetHostContext(object context)
        {
            return TResult.S_OK;
        }

        #endregion
    }
}
