#include "pch.h"

using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;

[assembly:AssemblyProductAttribute("VST.NET2")];
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

[assembly:AssemblyInformationalVersionAttribute("2.0.0")]
[assembly:AssemblyVersionAttribute("2.0.0.0")]
[assembly:AssemblyFileVersionAttribute("2.0.0.0")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly:ComVisible(false)];
[assembly:AssemblyDelaySignAttribute(false)];
