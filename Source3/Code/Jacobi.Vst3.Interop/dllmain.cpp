#include "pch.h"

HMODULE g_hModule;

extern "C"
{
    BOOL APIENTRY DllMain(HMODULE hModule,
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
    }
}
