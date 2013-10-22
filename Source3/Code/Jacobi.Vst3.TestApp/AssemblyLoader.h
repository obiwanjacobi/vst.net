#ifndef __ASSEMBLYLOADER__
#define __ASSEMBLYLOADER__

#include <pluginterfaces\base\ipluginbase.h>

class AssemblyLoader
{
public:
	AssemblyLoader()
	{
		_hModule = ::LoadLibrary(L"Jacobi.Vst3.TestPlugin.dll");
	}
	AssemblyLoader(LPTSTR assembly)
	{
		_hModule = ::LoadLibrary(assembly);
	}
	~AssemblyLoader()
	{
		::FreeLibrary(_hModule);
	}

	GetFactoryProc GetPluginFactoryProcedure()
	{
		return (GetFactoryProc)::GetProcAddress(_hModule, "GetPluginFactory");
	}

private:
	HMODULE _hModule;
};

#endif //__ASSEMBLYLOADER__