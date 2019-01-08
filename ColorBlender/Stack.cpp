#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <math.h>
#include <Windows.h>
#include "ColorBlender.h"

// Code implements a simple (and limited) application-specific stack. Using the STL resulted
// in compile-time errors from the library. Have no intention of debugging blunders in the
// implementation of the STL...

const int ErrorStackCapacity = 100;

ErrorInfo *ErrorStack[ErrorStackCapacity];

char Separator[100];

void InitializeErrorStack(char *SeparatorString)
{
    for (int i = 0; i < ErrorStackCapacity; i++)
        ErrorStack[i] = NULL;
    strcpy_s(Separator, SeparatorString);
//    strcpy(Separator, SeparatorString);
}

void SetSeparator(char *SeparatorString)
{
    if (SeparatorString == NULL)
        return;
    if (strlen(SeparatorString) < 1)
        return;
    strcpy_s(Separator, SeparatorString);
}

BSTR GetSeparator()
{
    return ::SysAllocString((const OLECHAR *)Separator);
}

BOOL ErrorStackPush(int ErrorCode, char *ErrorFunction)
{
    if (!CanPush())
        return FALSE;
    ErrorInfo *Info = new ErrorInfo;
    Info->ErrorCode = ErrorCode;
    Info->ErrorFunction = new char[strlen(ErrorFunction)];
    strcpy(Info->ErrorFunction, ErrorFunction);
    int NewIndex = FirstEmptyIndex();
    ErrorStack[NewIndex] = Info;
    return TRUE;
}

BOOL ErrorStackPush2(int ErrorCode, char *ErrorFunction, char *Tag)
{
    if (!CanPush())
        return FALSE;
    ErrorInfo *Info = new ErrorInfo;
    Info->ErrorCode = ErrorCode;
    Info->ErrorFunction = new char[strlen(ErrorFunction)];
    strcpy(Info->ErrorFunction, ErrorFunction);
    Info->Tag = new char[strlen(Tag)];
    strcpy(Info->Tag, Tag);
    int NewIndex = FirstEmptyIndex();
    ErrorStack[NewIndex] = Info;
    return TRUE;
}

BOOL ErrorStackPush3(int ErrorCode)
{
    if (!CanPush())
        return FALSE;
    ErrorInfo *Info = new ErrorInfo;
    Info->ErrorCode = ErrorCode;
    strcpy(Info->ErrorFunction, "");
    int NewIndex = FirstEmptyIndex();
    ErrorStack[NewIndex] = Info;
    return TRUE;
}

int ErrorStackPushReturn(int ErrorCode, char *ErrorFunction)
{
    if (CanPush())
    {
        ErrorInfo *Info = new ErrorInfo;
        Info->ErrorCode = ErrorCode;
        Info->ErrorFunction = new char[strlen(ErrorFunction)];
        strcpy(Info->ErrorFunction, ErrorFunction);
        int NewIndex = FirstEmptyIndex();
        ErrorStack[NewIndex] = Info;
    }
    return ErrorCode;
}

int ErrorStackPushReturn2(int ErrorCode, char *ErrorFunction, char *Tag)
{
    if (CanPush())
    {
        ErrorInfo *Info = new ErrorInfo;
        Info->ErrorCode = ErrorCode;
        Info->ErrorFunction = new char[strlen(ErrorFunction)];
        strcpy(Info->ErrorFunction, ErrorFunction);
        Info->Tag = new char[strlen(Tag)];
        strcpy(Info->Tag, Tag);
        char *ErrorMessage = NULL;
        int NewIndex = FirstEmptyIndex();
        ErrorStack[NewIndex] = Info;
    }
    return ErrorCode;
}

BSTR AssembleErrorStack(ErrorInfo *Info, char *Sep)
{
    if (Info == NULL)
        return ::SysAllocString((const OLECHAR *)L"");
    char *stemp = "";
    sprintf(stemp, "%d", Info->ErrorCode);
    if (Info->ErrorFunction != NULL)
    {
        strcat(stemp, Sep);
        strcat(stemp, Info->ErrorFunction);
    }
    else
    {
        strcat(stemp, Sep);
    }
    if (Info->Tag != NULL)
    {
        strcat(stemp, Sep);
        strcat(stemp, Info->Tag);
    }
    return ::SysAllocString((const OLECHAR *)stemp);
}

int PopErrorCode()
{
    if (ErrorStackEmpty())
        return ErrorStackIsEmpty;
    int TopIndex = ErrorStackTopIndex();
    if (TopIndex == -1)
        return ErrorStackIsEmpty;
    int ResultCode = ErrorStack[TopIndex]->ErrorCode;
    delete ErrorStack[TopIndex];
    ErrorStack[TopIndex] = NULL;
    return ResultCode;
}

BSTR ErrorTop2(char *Sep)
{
    if (ErrorStackEmpty())
        return ::SysAllocString((const OLECHAR *)L"");
    int TopIndex = ErrorStackTopIndex();
    if (TopIndex == -1)
        return ::SysAllocString((const OLECHAR *)L"");
    return AssembleErrorStack(ErrorStack[TopIndex], Sep);
}

BSTR ErrorTop()
{
    return ErrorTop2(Separator);
}

BSTR ErrorPop2(char *Sep)
{
    BSTR Top = ErrorTop2(Sep);
    int TopIndex = ErrorStackTopIndex();
    if (TopIndex != -1)
    {
        delete ErrorStack[TopIndex];
        ErrorStack[TopIndex] = NULL;
    }
    return Top;
}

BSTR ErrorPop()
{
    return ErrorPop2(Separator);
}

void ErrorStackClear()
{
    int FirstEmpty = FirstEmptyIndex();
    if (FirstEmpty == -1)
        FirstEmpty = ErrorStackCapacity;
    for (int i = 0; i < FirstEmpty; i++)
    {
        delete ErrorStack[i];
        ErrorStack[i] = NULL;
    }
}

BOOL ErrorStackEmpty()
{
    return FirstEmptyIndex() == 0 ? TRUE : FALSE;
}

BOOL ErrorStackFull()
{
    return FirstEmptyIndex() == -1 ? TRUE : FALSE;
}

int FirstEmptyIndex()
{
    for (int i = 0; i < ErrorStackCapacity; i++)
        if (ErrorStack[i] == NULL)
            return i;
    return -1;
}

int ErrorStackTopIndex()
{
    int FirstEmpty = FirstEmptyIndex();
    if (FirstEmpty == -1)
        return ErrorStackCapacity - 1;
    if (FirstEmpty == 0)
        return -1;
    return FirstEmpty - 1;
}

int ErrorStackSize()
{
    if (FirstEmptyIndex() == -1)
        return ErrorStackCapacity;
    return FirstEmptyIndex() + 1;
}

BOOL CanPush()
{
    return ErrorStackFull() ? FALSE : TRUE;
}

int GetErrorStackCapacity()
{
    return ErrorStackCapacity;
}