#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _UTILITIES_H
#define _UTILITIES_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) int BitCount(BYTE SourceValue);
extern "C" __declspec(dllexport) int BitCountTable(BYTE SourceValue);

#endif
