using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jacobi.Vst3.Interop;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    [ClassInterface(ClassInterfaceType.None)]
    class PluginComponent : IPluginBase
    {
        #region IPluginBase Members

        public int Initialize(object context)
        {
            return TResult.S_OK;
        }

        public int Terminate()
        {
            return TResult.S_OK;
        }

        #endregion
    }
}
