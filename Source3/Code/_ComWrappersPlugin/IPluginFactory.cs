using System;

namespace ComWrappersPlugin
{
    public interface IPluginFactory
    {
        Int32 GetFactoryInfo(ref PFactoryInfo info);
        Int32 CountClasses();
        Int32 GetClassInfo(Int32 index, ref PClassInfo info);
        Int32 CreateInstance(ref Guid classId, ref Guid interfaceId, ref IntPtr instance);
    }
}
