#include "pch.h"

using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;

//
// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly:AssemblyTitleAttribute("Jacobi.Vst.Interop")];
[assembly:AssemblyDescriptionAttribute("C++ <=> .NET Interop layer.")];
[assembly:AssemblyProductAttribute("VST.NET")];
[assembly:AssemblyCultureAttribute("")];

// configuration
#ifdef _DEBUG
[assembly:AssemblyConfiguration("Debug")];
#else
[assembly:AssemblyConfiguration("Release")];
#endif

// legal stuff
[assembly:AssemblyCompany("Jacobi Software")];
[assembly:AssemblyCopyright("Copyright © 2008-2020 Jacobi Software")] ;
[assembly:AssemblyTrademark("obiwanjacobi")] ;

[assembly:AssemblyInformationalVersionAttribute("2.0.0-alpha")]
[assembly:AssemblyVersionAttribute("2.0.0.0")]
[assembly:AssemblyFileVersionAttribute("2.0.0.0")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly:ComVisible(false)];
[assembly:AssemblyDelaySignAttribute(false)];
