namespace Jacobi.Vst.Framework.Plugin
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Host;

    internal class VstPluginContext
    {
        public VstPluginInfo PlginInfo;
        public ExtensibleObjectRef<IVstPlugin> Plugin;
        public ExtensibleObjectRef<VstHost> Host;
    }
}
