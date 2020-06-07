#pragma once

#include "../pch.h"
#include "UnmanagedArray.h"
#include "../MemoryTracker.h"
#include "VstPluginCommandsImpl.h"

namespace Jacobi {
namespace Vst {
namespace Host {
namespace Interop {


/// <summary>
/// The VstPluginCommandStub class implements the <see cref="Jacobi::Vst::Core::Host::IVstPluginCommandStub"/>
/// interface that is called by the host to access the Plugin.
/// </summary>
/// <remarks>
/// The class also implements the <see cref="Jacobi::Vst::Core::Legacy::IVstPluginCommandsLegacy20"/> 
/// interface for legacy method support.
/// </remarks>
ref class VstPluginCommandStub : Jacobi::Vst::Core::Host::IVstPluginCommandStub, System::IDisposable
{
public:
    ~VstPluginCommandStub()
    {
        this->!VstPluginCommandStub();
    }
    !VstPluginCommandStub()
    {
        delete _cmdImpl;
    }

    // IVstPluginCommandStub
    /// <summary>
    /// Gets or sets the Plugin Context for this implementation.
    /// </summary>
    virtual property Jacobi::Vst::Core::Host::IVstPluginContext^ PluginContext;

    virtual property Jacobi::Vst::Core::IVstPluginCommands24^ Commands;

internal:
    /// <summary>Constructs a new instance based on an <b>Vst2Plugin</b> structure.</summary>
    VstPluginCommandStub(::Vst2Plugin* pPlugin);

private:
    Jacobi::Vst::Host::Interop::VstPluginCommandsImpl^ _cmdImpl;
};

}}}} // Jacobi::Vst::Host::Interop