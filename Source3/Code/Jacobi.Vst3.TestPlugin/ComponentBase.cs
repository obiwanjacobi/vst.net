using System;
using Jacobi.Vst3.Interop;
using Jacobi.Vst3.Interop.Plugin;

namespace Jacobi.Vst3.TestPlugin
{
    public abstract class ComponentBase : IPluginBase, IServiceContainerSite
    {
        protected ComponentBase()
        {
            ServiceContainer = new ServiceContainer();
        }

        #region IServiceContainerSite Members

        public ServiceContainer ServiceContainer { get; protected set; }

        #endregion

        #region IPluginBase Members

        public int Initialize(object context)
        {
            ServiceContainer.Unknown = context;
            
            return TResult.S_OK;
        }

        public int Terminate()
        {
            ServiceContainer.Dispose();
            
            return TResult.S_OK;
        }

        #endregion

        
    }
}
