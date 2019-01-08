; Listing generated by Microsoft (R) Optimizing Compiler Version 19.12.25831.0 

	TITLE	C:\Users\Stuart\Desktop\Projects4\ColorBlend\ColorBlender\ImageSplitter.cpp
	.686P
	.XMM
	include listing.inc
	.model	flat

INCLUDELIB MSVCRTD
INCLUDELIB OLDNAMES

PUBLIC	_ImageCombine@24
PUBLIC	_ImageSplit@20
EXTRN	_ClearBufferDWord@16:PROC
EXTRN	_CopyBufferRegionPixel@32:PROC
; Function compile flags: /Odtp
; File c:\users\stuart\desktop\projects4\colorblend\colorblender\imagesplitter.cpp
_TEXT	SEGMENT
_SubImage$1 = -40					; size = 24
_Src$ = -16						; size = 4
_Result$2 = -12						; size = 4
_Images$ = -8						; size = 4
_i$3 = -4						; size = 4
_Source$ = 8						; size = 4
_Width$ = 12						; size = 4
_Height$ = 16						; size = 4
_Results$ = 20						; size = 4
_SubCount$ = 24						; size = 4
_ImageSplit@20 PROC

; 7    : {

	push	ebp
	mov	ebp, esp
	sub	esp, 40					; 00000028H

; 8    :     if (Source == NULL)

	cmp	DWORD PTR _Source$[ebp], 0
	jne	SHORT $LN5@ImageSplit

; 9    :         return NullPointer;

	mov	eax, 3
	jmp	$LN1@ImageSplit
$LN5@ImageSplit:

; 10   :     if (Results == NULL)

	cmp	DWORD PTR _Results$[ebp], 0
	jne	SHORT $LN6@ImageSplit

; 11   :         return NullPointer;

	mov	eax, 3
	jmp	$LN1@ImageSplit
$LN6@ImageSplit:

; 12   :     if (SubCount < 1)

	cmp	DWORD PTR _SubCount$[ebp], 1
	jge	SHORT $LN7@ImageSplit

; 13   :         return NoActionTaken;

	mov	eax, 12					; 0000000cH
	jmp	$LN1@ImageSplit
$LN7@ImageSplit:

; 14   :     ImageDefintionStruct *Images = (ImageDefintionStruct *)Results;

	mov	eax, DWORD PTR _Results$[ebp]
	mov	DWORD PTR _Images$[ebp], eax

; 15   :     if (Images == NULL)

	cmp	DWORD PTR _Images$[ebp], 0
	jne	SHORT $LN8@ImageSplit

; 16   :         return NullPointer;

	mov	eax, 3
	jmp	$LN1@ImageSplit
$LN8@ImageSplit:

; 17   : 
; 18   :     UINT32 *Src = (UINT32 *)Source;

	mov	ecx, DWORD PTR _Source$[ebp]
	mov	DWORD PTR _Src$[ebp], ecx

; 19   : 
; 20   :     for (int i = 0; i < SubCount; i++)

	mov	DWORD PTR _i$3[ebp], 0
	jmp	SHORT $LN4@ImageSplit
$LN2@ImageSplit:
	mov	edx, DWORD PTR _i$3[ebp]
	add	edx, 1
	mov	DWORD PTR _i$3[ebp], edx
$LN4@ImageSplit:
	mov	eax, DWORD PTR _i$3[ebp]
	cmp	eax, DWORD PTR _SubCount$[ebp]
	jge	SHORT $LN3@ImageSplit

; 21   :     {
; 22   :         ImageDefintionStruct SubImage = Images[i];

	imul	ecx, DWORD PTR _i$3[ebp], 24
	add	ecx, DWORD PTR _Images$[ebp]
	mov	edx, DWORD PTR [ecx]
	mov	DWORD PTR _SubImage$1[ebp], edx
	mov	eax, DWORD PTR [ecx+4]
	mov	DWORD PTR _SubImage$1[ebp+4], eax
	mov	edx, DWORD PTR [ecx+8]
	mov	DWORD PTR _SubImage$1[ebp+8], edx
	mov	eax, DWORD PTR [ecx+12]
	mov	DWORD PTR _SubImage$1[ebp+12], eax
	mov	edx, DWORD PTR [ecx+16]
	mov	DWORD PTR _SubImage$1[ebp+16], edx
	mov	eax, DWORD PTR [ecx+20]
	mov	DWORD PTR _SubImage$1[ebp+20], eax

; 23   :         int Result = CopyBufferRegionPixel(Source, Width, Height, SubImage.Buffer, SubImage.X, SubImage.Y,

	mov	ecx, DWORD PTR _SubImage$1[ebp+16]
	mov	edx, DWORD PTR _SubImage$1[ebp+8]
	lea	eax, DWORD PTR [edx+ecx-1]
	push	eax
	mov	ecx, DWORD PTR _SubImage$1[ebp+12]
	mov	edx, DWORD PTR _SubImage$1[ebp+4]
	lea	eax, DWORD PTR [edx+ecx-1]
	push	eax
	mov	ecx, DWORD PTR _SubImage$1[ebp+8]
	push	ecx
	mov	edx, DWORD PTR _SubImage$1[ebp+4]
	push	edx
	mov	eax, DWORD PTR _SubImage$1[ebp]
	push	eax
	mov	ecx, DWORD PTR _Height$[ebp]
	push	ecx
	mov	edx, DWORD PTR _Width$[ebp]
	push	edx
	mov	eax, DWORD PTR _Source$[ebp]
	push	eax
	call	_CopyBufferRegionPixel@32
	mov	DWORD PTR _Result$2[ebp], eax

; 24   :             SubImage.X + SubImage.Width - 1, SubImage.Y + SubImage.Height - 1);
; 25   :         if (Result != Success)

	cmp	DWORD PTR _Result$2[ebp], 0
	je	SHORT $LN9@ImageSplit

; 26   :             return Result;

	mov	eax, DWORD PTR _Result$2[ebp]
	jmp	SHORT $LN1@ImageSplit
$LN9@ImageSplit:

; 27   :     }

	jmp	SHORT $LN2@ImageSplit
$LN3@ImageSplit:

; 28   : 
; 29   :     return Success;

	xor	eax, eax
$LN1@ImageSplit:

; 30   : }

	mov	esp, ebp
	pop	ebp
	ret	20					; 00000014H
_ImageSplit@20 ENDP
_TEXT	ENDS
; Function compile flags: /Odtp
; File c:\users\stuart\desktop\projects4\colorblend\colorblender\imagesplitter.cpp
_TEXT	SEGMENT
_SubImage$1 = -40					; size = 24
_Dest$ = -16						; size = 4
_Result$2 = -12						; size = 4
_SourceList$ = -8					; size = 4
_i$3 = -4						; size = 4
_Destination$ = 8					; size = 4
_Width$ = 12						; size = 4
_Height$ = 16						; size = 4
_Sources$ = 20						; size = 4
_SubCount$ = 24						; size = 4
_BGColor$ = 28						; size = 4
_ImageCombine@24 PROC

; 44   : {

	push	ebp
	mov	ebp, esp
	sub	esp, 40					; 00000028H

; 45   :     if (Destination == NULL)

	cmp	DWORD PTR _Destination$[ebp], 0
	jne	SHORT $LN5@ImageCombi

; 46   :         return NullPointer;

	mov	eax, 3
	jmp	$LN1@ImageCombi
$LN5@ImageCombi:

; 47   :     if (SubCount < 1)

	cmp	DWORD PTR _SubCount$[ebp], 1
	jge	SHORT $LN6@ImageCombi

; 48   :         return NoActionTaken;

	mov	eax, 12					; 0000000cH
	jmp	$LN1@ImageCombi
$LN6@ImageCombi:

; 49   :     ImageDefintionStruct *SourceList = (ImageDefintionStruct *)Sources;

	mov	eax, DWORD PTR _Sources$[ebp]
	mov	DWORD PTR _SourceList$[ebp], eax

; 50   :     if (SourceList == NULL)

	cmp	DWORD PTR _SourceList$[ebp], 0
	jne	SHORT $LN7@ImageCombi

; 51   :         return NullPointer;

	mov	eax, 3
	jmp	$LN1@ImageCombi
$LN7@ImageCombi:

; 52   : 
; 53   :     ClearBufferDWord(Destination, Width, Height, BGColor);

	mov	ecx, DWORD PTR _BGColor$[ebp]
	push	ecx
	mov	edx, DWORD PTR _Height$[ebp]
	push	edx
	mov	eax, DWORD PTR _Width$[ebp]
	push	eax
	mov	ecx, DWORD PTR _Destination$[ebp]
	push	ecx
	call	_ClearBufferDWord@16

; 54   :     UINT32 *Dest = (UINT32 *)Destination;

	mov	edx, DWORD PTR _Destination$[ebp]
	mov	DWORD PTR _Dest$[ebp], edx

; 55   :     for (unsigned i = 0; i < (unsigned)SubCount; i++)

	mov	DWORD PTR _i$3[ebp], 0
	jmp	SHORT $LN4@ImageCombi
$LN2@ImageCombi:
	mov	eax, DWORD PTR _i$3[ebp]
	add	eax, 1
	mov	DWORD PTR _i$3[ebp], eax
$LN4@ImageCombi:
	mov	ecx, DWORD PTR _i$3[ebp]
	cmp	ecx, DWORD PTR _SubCount$[ebp]
	jae	$LN3@ImageCombi

; 56   :     {
; 57   :         ImageDefintionStruct SubImage = SourceList[i];

	imul	edx, DWORD PTR _i$3[ebp], 24
	add	edx, DWORD PTR _SourceList$[ebp]
	mov	eax, DWORD PTR [edx]
	mov	DWORD PTR _SubImage$1[ebp], eax
	mov	ecx, DWORD PTR [edx+4]
	mov	DWORD PTR _SubImage$1[ebp+4], ecx
	mov	eax, DWORD PTR [edx+8]
	mov	DWORD PTR _SubImage$1[ebp+8], eax
	mov	ecx, DWORD PTR [edx+12]
	mov	DWORD PTR _SubImage$1[ebp+12], ecx
	mov	eax, DWORD PTR [edx+16]
	mov	DWORD PTR _SubImage$1[ebp+16], eax
	mov	ecx, DWORD PTR [edx+20]
	mov	DWORD PTR _SubImage$1[ebp+20], ecx

; 58   :         if (SubImage.Width + SubImage.X > (unsigned)Width)

	mov	edx, DWORD PTR _SubImage$1[ebp+12]
	add	edx, DWORD PTR _SubImage$1[ebp+4]
	cmp	edx, DWORD PTR _Width$[ebp]
	jbe	SHORT $LN8@ImageCombi

; 59   :             return InvalidOperation;

	mov	eax, 11					; 0000000bH
	jmp	SHORT $LN1@ImageCombi
$LN8@ImageCombi:

; 60   :         if (SubImage.Height + SubImage.Y > (unsigned)Height)

	mov	eax, DWORD PTR _SubImage$1[ebp+16]
	add	eax, DWORD PTR _SubImage$1[ebp+8]
	cmp	eax, DWORD PTR _Height$[ebp]
	jbe	SHORT $LN9@ImageCombi

; 61   :             return InvalidOperation;

	mov	eax, 11					; 0000000bH
	jmp	SHORT $LN1@ImageCombi
$LN9@ImageCombi:

; 62   :         int Result = CopyBufferRegionPixel(SubImage.Buffer, SubImage.Width, SubImage.Height, Dest,

	mov	ecx, DWORD PTR _SubImage$1[ebp+16]
	mov	edx, DWORD PTR _SubImage$1[ebp+8]
	lea	eax, DWORD PTR [edx+ecx-1]
	push	eax
	mov	ecx, DWORD PTR _SubImage$1[ebp+12]
	mov	edx, DWORD PTR _SubImage$1[ebp+4]
	lea	eax, DWORD PTR [edx+ecx-1]
	push	eax
	mov	ecx, DWORD PTR _SubImage$1[ebp+8]
	push	ecx
	mov	edx, DWORD PTR _SubImage$1[ebp+4]
	push	edx
	mov	eax, DWORD PTR _Dest$[ebp]
	push	eax
	mov	ecx, DWORD PTR _SubImage$1[ebp+16]
	push	ecx
	mov	edx, DWORD PTR _SubImage$1[ebp+12]
	push	edx
	mov	eax, DWORD PTR _SubImage$1[ebp]
	push	eax
	call	_CopyBufferRegionPixel@32
	mov	DWORD PTR _Result$2[ebp], eax

; 63   :             SubImage.X, SubImage.Y, SubImage.X + SubImage.Width - 1, SubImage.Y + SubImage.Height - 1);
; 64   :         if (Result != Success)

	cmp	DWORD PTR _Result$2[ebp], 0
	je	SHORT $LN10@ImageCombi

; 65   :             return Result;

	mov	eax, DWORD PTR _Result$2[ebp]
	jmp	SHORT $LN1@ImageCombi
$LN10@ImageCombi:

; 66   :     }

	jmp	$LN2@ImageCombi
$LN3@ImageCombi:

; 67   : 
; 68   :     return Success;

	xor	eax, eax
$LN1@ImageCombi:

; 69   : }

	mov	esp, ebp
	pop	ebp
	ret	24					; 00000018H
_ImageCombine@24 ENDP
_TEXT	ENDS
END