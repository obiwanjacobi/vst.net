#include "pch.h"
#include <pluginterfaces/base/funknown.h>
#include <pluginterfaces/base/ipluginbase.h>


bool InitDll()
{
    return true;
}
void ExitDll()
{

}

Jacobi::Vst3::Core::IPluginFactory^ LoadPlugin()
{
    auto interopPath = System::Reflection::Assembly::GetExecutingAssembly()->Location;
    auto pluginPath = System::IO::Path::GetDirectoryName(interopPath);
    //auto pluginName = System::IO::Path::GetFileNameWithoutExtension(interopPath);
    auto pluginName = gcnew System::String("Jacobi.Vst3.TestPlugin");

    auto loader = gcnew Jacobi::Vst3::Core::Common::AssemblyLoader(pluginPath);
    auto pluginAssembly = loader->LoadPlugin(pluginName);
    
    System::Type^ pluginType = nullptr;
    for each (auto type in pluginAssembly->GetTypes())
    {
        if (type->IsPublic)
        {
            if (type->GetInterface("Jacobi.Vst3.Core.IPluginFactory") != nullptr)
            {
                pluginType = type;
                break;
            }
        }
    }

    Jacobi::Vst3::Core::IPluginFactory^ plugin = nullptr;
    if (pluginType != nullptr)
    {
        plugin = safe_cast<Jacobi::Vst3::Core::IPluginFactory^>(System::Activator::CreateInstance(pluginType));
    }

    return plugin;
}

Steinberg::IPluginFactory* GetPluginFactory()
{
    Jacobi::Vst3::Core::IPluginFactory^ pluginFactory = LoadPlugin();
    if (pluginFactory == nullptr) return nullptr;

    Steinberg::IPluginFactory* plugin = nullptr;
    System::IntPtr unknownPtr = System::Runtime::InteropServices::Marshal::GetComInterfaceForObject(
        pluginFactory, Jacobi::Vst3::Core::IPluginFactory::typeid);

    if (unknownPtr != System::IntPtr::Zero)
    {
        Steinberg::FUnknown* unknown = (Steinberg::FUnknown*)unknownPtr.ToPointer();
        unknown->queryInterface(Steinberg::IPluginFactory_iid, (void**)&plugin);
    }
    return plugin;
}
