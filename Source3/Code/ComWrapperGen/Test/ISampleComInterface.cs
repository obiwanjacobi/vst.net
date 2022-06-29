using System;
using System.Runtime.InteropServices;

namespace ComWrapperGen
{
    [Guid("0455F417-8E66-4135-88A0-E3A539D4A418")]
    [ComInterface("0455F417-8E66-4135-88A0-E3A539D4A418")]
    public interface ISampleComInterface
    {
        [PreserveSig]
        Int32 Method1(String name, Int32 count);
        String Method2(String name);
    }
}
