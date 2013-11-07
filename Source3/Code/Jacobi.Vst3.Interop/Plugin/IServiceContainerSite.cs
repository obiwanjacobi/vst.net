using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Interop.Plugin
{
    public interface IServiceContainerSite
    {
        ServiceContainer ServiceContainer { get; }
    }
}
