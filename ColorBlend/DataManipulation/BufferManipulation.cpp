#include "DataManipulation.h"

int ClearBuffer(void *Buffer, int BufferSize, UINT32 ClearWith)
{
    __asm
    {
        mov ecx, BufferSize;
        mov edx, 4;
    Repeat:
        imul eax, edx, ecx;
        mov ebx, DWORD PTR Buffer;
        mov edx, DWORD PTR ClearWith;
        mov DWORD PTR[ebx + eax], edx;
        loop Repeat;
    }
    return 0;
}