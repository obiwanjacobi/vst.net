#ifndef __ASSEMBLYLOADER__
#define __ASSEMBLYLOADER__

#include <pluginterfaces\base\ipluginbase.h>

extern "C"
{
	typedef bool (PLUGIN_API *InitModuleProc) ();
	typedef bool (PLUGIN_API *ExitModuleProc) ();
}

class AssemblyLoader
{
public:
	AssemblyLoader()
	{
		_hModule = ::LoadLibrary(L"Jacobi.Vst3.TestPlugin.dll");
		InitModule();
	}
	AssemblyLoader(LPTSTR assembly)
	{
		_hModule = ::LoadLibrary(assembly);
		InitModule();
	}

	~AssemblyLoader()
	{
		ExitModule();
		DeleteModule();
	}

	inline InitModuleProc GetInitModuleProcedure()
	{
		return (InitModuleProc)::GetProcAddress(_hModule, "InitDll");
	}

	inline ExitModuleProc GetExitModuleProcedure()
	{
		return (ExitModuleProc)::GetProcAddress(_hModule, "ExitDll");
	}

	inline GetFactoryProc GetPluginFactoryProcedure()
	{
		return (GetFactoryProc)::GetProcAddress(_hModule, "GetPluginFactory");
	}

	inline bool IsValid() { return _hModule != NULL; }

	Steinberg::IPluginFactory* GetPluginFactory()
	{
		GetFactoryProc proc = GetPluginFactoryProcedure();

		if (proc != NULL)
		{
			return proc();
		}

		return NULL;
	}

	bool InitModule()
	{
		InitModuleProc proc = GetInitModuleProcedure();

		if (proc != NULL)
		{
			if(proc() == false)
			{
				DeleteModule();
				return false;
			}
		}

		return true;
	}

	bool ExitModule()
	{
		ExitModuleProc proc = GetExitModuleProcedure();

		if (proc != NULL)
		{
			return proc();
		}

		return true;
	}

	inline void DeleteModule()
	{
		::FreeLibrary(_hModule);
		_hModule = NULL;
	}

private:
	HMODULE _hModule;
};

#endif //__ASSEMBLYLOADER__