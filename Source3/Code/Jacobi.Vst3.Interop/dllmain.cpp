#include "pch.h"
#include <delayimp.h>

//HMODULE g_hModule;

//char buffer[MAX_PATH + 1];
// Get full path to ijwhost.dll based on current module path
//auto res = GetModuleFileNameA(g_hModule, buffer, MAX_PATH + 1);

#pragma unmanaged

extern "C"
{
    /*BOOL APIENTRY DllMain(HMODULE hModule,
        DWORD  ul_reason_for_call,
        LPVOID lpReserved)
    {
        switch (ul_reason_for_call)
        {
        case DLL_PROCESS_ATTACH:
            g_hModule = hModule;
            break;
        case DLL_PROCESS_DETACH:
            g_hModule = NULL;
            break;
        }
        return TRUE;
    }*/

    FARPROC WINAPI delayLoadFailureHook(unsigned dliNotify, PDelayLoadInfo pdli)
    {
        if (dliNotify != dliFailLoadLib ||
            _strcmpi(pdli->szDll, "ijwhost.dll") != 0) {
            return NULL;
        }


        ::OutputDebugStringA("Loading Ijwhost.dll");

        const char* path = "C:\\Users\\marc\\Documents\\MyProjects\\public\\Jacobi\\Public\\GitHub\\vst.net\\Source3\\Code\\x64\\Debug\\Ijwhost.dll";
        return (FARPROC)::LoadLibraryA(path);
    }

    const PfnDliHook __pfnDliFailureHook2 = delayLoadFailureHook;
}

#pragma managed