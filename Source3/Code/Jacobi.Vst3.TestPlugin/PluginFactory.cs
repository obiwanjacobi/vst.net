using Jacobi.Vst3.Plugin;

namespace Jacobi.Vst3.TestPlugin
{
    public sealed class PluginFactory : PluginClassFactory
    {
        public PluginFactory()
            : base("Jacobi Software", "obiwanjacobi@hotmail.com", "https://github.com/obiwanjacobi/vst.net")
        {
            RegisterClasses();
        }

        private void RegisterClasses()
        {
            var reg = Register(typeof(PluginComponent), ClassRegistration.ObjectClasses.AudioModuleClass);
            reg.Categories = new CategoryCollection(CategoryCollection.Fx);

            Register(typeof(MyEditController), ClassRegistration.ObjectClasses.ComponentControllerClass);
        }
    }
}
