#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "Structures.h"

#ifndef _PUBLICUTILITIES_H
#define _PUBLICUTILITIES_H

#pragma warning (disable : 4244 4800 4901)

extern "C" __declspec(dllexport) double ColorLuminance(BYTE R, BYTE G, BYTE B);

#endif
