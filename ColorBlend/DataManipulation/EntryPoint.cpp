#include "DataManipulation.h"

//Required DLL entry points. Not explicitly used.
BOOL WINAPI DllMain(HINSTANCE DLLHandle, DWORD Reason, LPVOID NotUsed)
{
    switch (Reason)
    {
    case DLL_PROCESS_ATTACH:
        //Initialize for each new attached process.
        break;

    case DLL_THREAD_ATTACH:
        //Initialize for each new attached thread.
        break;

    case DLL_THREAD_DETACH:
        //Thread-specific clean-up.
        break;

    case DLL_PROCESS_DETACH:
        //Process-specific clean-up.
        break;
    }

    return TRUE;
}