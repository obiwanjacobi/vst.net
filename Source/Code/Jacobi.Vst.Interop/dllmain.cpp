// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

void* g_hModule;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		g_hModule = hModule;
		break;
	//case DLL_THREAD_ATTACH:
	//case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		g_hModule = NULL;
		break;
	}

	return TRUE;
}

}}} // Jacobi::Vst::Interop