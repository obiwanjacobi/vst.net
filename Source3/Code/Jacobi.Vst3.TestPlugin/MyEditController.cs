using System;
using System.Runtime.InteropServices;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Edit Controller")]
    [Guid("D74D670B-28B8-4AB2-9180-D4D12B52F54B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MyEditController : Jacobi.Vst3.Plugin.EditController
    {
    }
}
