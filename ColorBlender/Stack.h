#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>

#ifndef _STACK_H
#define _STACK_H

#pragma warning (disable : 4244 4800 4901)

struct ErrorInfo
{
    int ErrorCode;
    char *ErrorFunction;
    char *Tag;
};

extern "C" __declspec(dllexport) void InitializeErrorStack(char *SeparatorString);
extern "C" __declspec(dllexport) void SetSeparator(char *SeparatorString);
extern "C" __declspec(dllexport) BSTR GetSeparator();
extern "C" __declspec(dllexport) BOOL ErrorStackPush(int ErrorCode, char *ErrorFunction);
extern "C" __declspec(dllexport) BOOL ErrorStackPush2(int ErrorCode, char *ErrorFunction, char *Tag);
extern "C" __declspec(dllexport) BOOL ErrorStackPush3(int ErrorCode);
extern "C" __declspec(dllexport) int ErrorStackPushReturn(int ErrorCode, char *ErrorFunction);
extern "C" __declspec(dllexport) int ErrorStackPushReturn2(int ErrorCode, char *ErrorFunction, char *Tag);
extern "C" __declspec(dllexport) BSTR AssembleErrorStack(ErrorInfo *Info, char *Sep);
extern "C" __declspec(dllexport) BSTR ErrorTop();
extern "C" __declspec(dllexport) BSTR ErrorTop2(char *Sep);
extern "C" __declspec(dllexport) BSTR ErrorPop();
extern "C" __declspec(dllexport) BSTR ErrorPop2(char *Sep);
extern "C" __declspec(dllexport) int PopErrorCode();
extern "C" __declspec(dllexport) void ErrorStackClear();
extern "C" __declspec(dllexport) BOOL ErrorStackEmpty();
extern "C" __declspec(dllexport) BOOL ErrorStackFull();
extern "C" __declspec(dllexport) int FirstEmptyIndex();
extern "C" __declspec(dllexport) int ErrorStackTopIndex();
extern "C" __declspec(dllexport) int ErrorStackSize();
extern "C" __declspec(dllexport) int GetErrorStackCapacity();
extern "C" __declspec(dllexport) BOOL CanPush();

#endif