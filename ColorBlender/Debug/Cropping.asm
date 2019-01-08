; Listing generated by Microsoft (R) Optimizing Compiler Version 19.12.25831.0 

	TITLE	C:\Users\Stuart\Desktop\Projects4\ColorBlend\ColorBlender\Cropping.cpp
	.686P
	.XMM
	include listing.inc
	.model	flat

INCLUDELIB MSVCRTD
INCLUDELIB OLDNAMES

CONST	SEGMENT
$SG110238 DB	'Source is NULL.', 00H
$SG110239 DB	'ImageCrop', 00H
	ORG $+2
$SG110241 DB	'Destination is NULL.', 00H
	ORG $+3
$SG110242 DB	'ImageCrop', 00H
	ORG $+2
$SG110244 DB	'Left < 0', 00H
	ORG $+3
$SG110245 DB	'ImageCrop', 00H
	ORG $+2
$SG110247 DB	'Top < 0', 00H
$SG110248 DB	'ImageCrop', 00H
	ORG $+2
$SG110250 DB	'Right < 0', 00H
	ORG $+2
$SG110251 DB	'ImageCrop', 00H
	ORG $+2
$SG110253 DB	'Bottom < 0', 00H
	ORG $+1
$SG110254 DB	'ImageCrop', 00H
	ORG $+2
$SG110256 DB	'Nothing to do.', 00H
	ORG $+1
$SG110257 DB	'ImageCrop', 00H
	ORG $+2
$SG110259 DB	'Left + Right crop values > width.', 00H
	ORG $+2
$SG110260 DB	'ImageCrop', 00H
	ORG $+2
$SG110262 DB	'Top + Bottom crop values > height.', 00H
	ORG $+1
$SG110263 DB	'ImageCrop', 00H
	ORG $+2
$SG110264 DB	'ImageCrop', 00H
CONST	ENDS
PUBLIC	_ImageCrop@36
EXTRN	_ErrorStackPushReturn@8:PROC
EXTRN	_ErrorStackPushReturn2@12:PROC
; Function compile flags: /Odtp
; File c:\users\stuart\desktop\projects4\colorblend\colorblender\cropping.cpp
_TEXT	SEGMENT
_DestHeight$ = -56					; size = 4
_DestRowOffset$1 = -52					; size = 4
_SourceRowOffset$2 = -48				; size = 4
_DestStride$ = -44					; size = 4
_DestWidth$ = -40					; size = 4
_PixelSize$ = -36					; size = 4
_DestRow$ = -32						; size = 4
_DestColumn$ = -28					; size = 4
_SourceIndex$3 = -24					; size = 4
_Src$ = -20						; size = 4
_DestIndex$4 = -16					; size = 4
_Dest$ = -12						; size = 4
_Column$5 = -8						; size = 4
_Row$6 = -4						; size = 4
_Source$ = 8						; size = 4
_Width$ = 12						; size = 4
_Height$ = 16						; size = 4
_Stride$ = 20						; size = 4
_Left$ = 24						; size = 4
_Top$ = 28						; size = 4
_Right$ = 32						; size = 4
_Bottom$ = 36						; size = 4
_Destination$ = 40					; size = 4
_ImageCrop@36 PROC

; 14   : {

	push	ebp
	mov	ebp, esp
	sub	esp, 56					; 00000038H

; 15   :     if (Source == NULL)

	cmp	DWORD PTR _Source$[ebp], 0
	jne	SHORT $LN8@ImageCrop

; 16   :         return ErrorStackPushReturn2(NullPointer, "ImageCrop", "Source is NULL.");

	push	OFFSET $SG110238
	push	OFFSET $SG110239
	push	3
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN8@ImageCrop:

; 17   :     if (Destination == NULL)

	cmp	DWORD PTR _Destination$[ebp], 0
	jne	SHORT $LN9@ImageCrop

; 18   :         return ErrorStackPushReturn2(NullPointer, "ImageCrop", "Destination is NULL.");

	push	OFFSET $SG110241
	push	OFFSET $SG110242
	push	3
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN9@ImageCrop:

; 19   :     if (Left < 0)

	cmp	DWORD PTR _Left$[ebp], 0
	jge	SHORT $LN10@ImageCrop

; 20   :         return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Left < 0");

	push	OFFSET $SG110244
	push	OFFSET $SG110245
	push	25					; 00000019H
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN10@ImageCrop:

; 21   :     if (Top < 0)

	cmp	DWORD PTR _Top$[ebp], 0
	jge	SHORT $LN11@ImageCrop

; 22   :         return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Top < 0");

	push	OFFSET $SG110247
	push	OFFSET $SG110248
	push	25					; 00000019H
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN11@ImageCrop:

; 23   :     if (Right < 0)

	cmp	DWORD PTR _Right$[ebp], 0
	jge	SHORT $LN12@ImageCrop

; 24   :         return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Right < 0");

	push	OFFSET $SG110250
	push	OFFSET $SG110251
	push	25					; 00000019H
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN12@ImageCrop:

; 25   :     if (Bottom < 0)

	cmp	DWORD PTR _Bottom$[ebp], 0
	jge	SHORT $LN13@ImageCrop

; 26   :         return ErrorStackPushReturn2(ValueTooSmall, "ImageCrop", "Bottom < 0");

	push	OFFSET $SG110253
	push	OFFSET $SG110254
	push	25					; 00000019H
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN13@ImageCrop:

; 27   :     if (Left == 0 && Top == 0 && Right == 0 && Bottom == 0)

	cmp	DWORD PTR _Left$[ebp], 0
	jne	SHORT $LN14@ImageCrop
	cmp	DWORD PTR _Top$[ebp], 0
	jne	SHORT $LN14@ImageCrop
	cmp	DWORD PTR _Right$[ebp], 0
	jne	SHORT $LN14@ImageCrop
	cmp	DWORD PTR _Bottom$[ebp], 0
	jne	SHORT $LN14@ImageCrop

; 28   :         return ErrorStackPushReturn2(NOP, "ImageCrop", "Nothing to do.");

	push	OFFSET $SG110256
	push	OFFSET $SG110257
	push	0
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN14@ImageCrop:

; 29   :     if (Left + Right > Width)

	mov	eax, DWORD PTR _Left$[ebp]
	add	eax, DWORD PTR _Right$[ebp]
	cmp	eax, DWORD PTR _Width$[ebp]
	jle	SHORT $LN15@ImageCrop

; 30   :         return ErrorStackPushReturn2(ValueTooBig, "ImageCrop", "Left + Right crop values > width.");

	push	OFFSET $SG110259
	push	OFFSET $SG110260
	push	26					; 0000001aH
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN15@ImageCrop:

; 31   :     if (Top + Bottom > Height)

	mov	ecx, DWORD PTR _Top$[ebp]
	add	ecx, DWORD PTR _Bottom$[ebp]
	cmp	ecx, DWORD PTR _Height$[ebp]
	jle	SHORT $LN16@ImageCrop

; 32   :         return ErrorStackPushReturn2(ValueTooBig, "ImageCrop", "Top + Bottom crop values > height.");

	push	OFFSET $SG110262
	push	OFFSET $SG110263
	push	26					; 0000001aH
	call	_ErrorStackPushReturn2@12
	jmp	$LN1@ImageCrop
$LN16@ImageCrop:

; 33   : 
; 34   :     int PixelSize = 4;

	mov	DWORD PTR _PixelSize$[ebp], 4

; 35   :     BYTE *Dest = (BYTE *)Destination;

	mov	edx, DWORD PTR _Destination$[ebp]
	mov	DWORD PTR _Dest$[ebp], edx

; 36   :     BYTE *Src = (BYTE *)Source;

	mov	eax, DWORD PTR _Source$[ebp]
	mov	DWORD PTR _Src$[ebp], eax

; 37   :     __int32 DestWidth = Width - (Left + Right);

	mov	ecx, DWORD PTR _Left$[ebp]
	add	ecx, DWORD PTR _Right$[ebp]
	mov	edx, DWORD PTR _Width$[ebp]
	sub	edx, ecx
	mov	DWORD PTR _DestWidth$[ebp], edx

; 38   :     __int32 DestHeight = Height - (Top + Bottom);

	mov	eax, DWORD PTR _Top$[ebp]
	add	eax, DWORD PTR _Bottom$[ebp]
	mov	ecx, DWORD PTR _Height$[ebp]
	sub	ecx, eax
	mov	DWORD PTR _DestHeight$[ebp], ecx

; 39   :     __int32 DestStride = DestWidth * 4;

	mov	edx, DWORD PTR _DestWidth$[ebp]
	shl	edx, 2
	mov	DWORD PTR _DestStride$[ebp], edx

; 40   :     int DestRow = 0;

	mov	DWORD PTR _DestRow$[ebp], 0

; 41   :     int DestColumn = 0;

	mov	DWORD PTR _DestColumn$[ebp], 0

; 42   : 
; 43   :     for (int Row = Top; Row < Height - Bottom; Row++)

	mov	eax, DWORD PTR _Top$[ebp]
	mov	DWORD PTR _Row$6[ebp], eax
	jmp	SHORT $LN4@ImageCrop
$LN2@ImageCrop:
	mov	ecx, DWORD PTR _Row$6[ebp]
	add	ecx, 1
	mov	DWORD PTR _Row$6[ebp], ecx
$LN4@ImageCrop:
	mov	edx, DWORD PTR _Height$[ebp]
	sub	edx, DWORD PTR _Bottom$[ebp]
	cmp	DWORD PTR _Row$6[ebp], edx
	jge	$LN3@ImageCrop

; 44   :     {
; 45   :         int SourceRowOffset = Row * Stride;

	mov	eax, DWORD PTR _Row$6[ebp]
	imul	eax, DWORD PTR _Stride$[ebp]
	mov	DWORD PTR _SourceRowOffset$2[ebp], eax

; 46   :         int DestRowOffset = DestRow * DestStride;

	mov	ecx, DWORD PTR _DestRow$[ebp]
	imul	ecx, DWORD PTR _DestStride$[ebp]
	mov	DWORD PTR _DestRowOffset$1[ebp], ecx

; 47   :         for (int Column = Left; Column < Width - Right; Column++)

	mov	edx, DWORD PTR _Left$[ebp]
	mov	DWORD PTR _Column$5[ebp], edx
	jmp	SHORT $LN7@ImageCrop
$LN5@ImageCrop:
	mov	eax, DWORD PTR _Column$5[ebp]
	add	eax, 1
	mov	DWORD PTR _Column$5[ebp], eax
$LN7@ImageCrop:
	mov	ecx, DWORD PTR _Width$[ebp]
	sub	ecx, DWORD PTR _Right$[ebp]
	cmp	DWORD PTR _Column$5[ebp], ecx
	jge	SHORT $LN6@ImageCrop

; 48   :         {
; 49   :             int SourceIndex = (Column * PixelSize) + SourceRowOffset;

	mov	edx, DWORD PTR _Column$5[ebp]
	imul	edx, DWORD PTR _PixelSize$[ebp]
	add	edx, DWORD PTR _SourceRowOffset$2[ebp]
	mov	DWORD PTR _SourceIndex$3[ebp], edx

; 50   :             int DestIndex = (DestColumn * PixelSize) + DestRowOffset;

	mov	eax, DWORD PTR _DestColumn$[ebp]
	imul	eax, DWORD PTR _PixelSize$[ebp]
	add	eax, DWORD PTR _DestRowOffset$1[ebp]
	mov	DWORD PTR _DestIndex$4[ebp], eax

; 51   :             Dest[DestIndex + 3] = Src[SourceIndex + 3];

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _DestIndex$4[ebp]
	mov	edx, DWORD PTR _Src$[ebp]
	add	edx, DWORD PTR _SourceIndex$3[ebp]
	mov	al, BYTE PTR [edx+3]
	mov	BYTE PTR [ecx+3], al

; 52   :             Dest[DestIndex + 2] = Src[SourceIndex + 2];

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _DestIndex$4[ebp]
	mov	edx, DWORD PTR _Src$[ebp]
	add	edx, DWORD PTR _SourceIndex$3[ebp]
	mov	al, BYTE PTR [edx+2]
	mov	BYTE PTR [ecx+2], al

; 53   :             Dest[DestIndex + 1] = Src[SourceIndex + 1];

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _DestIndex$4[ebp]
	mov	edx, DWORD PTR _Src$[ebp]
	add	edx, DWORD PTR _SourceIndex$3[ebp]
	mov	al, BYTE PTR [edx+1]
	mov	BYTE PTR [ecx+1], al

; 54   :             Dest[DestIndex + 0] = Src[SourceIndex + 0];

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _DestIndex$4[ebp]
	mov	edx, DWORD PTR _Src$[ebp]
	add	edx, DWORD PTR _SourceIndex$3[ebp]
	mov	al, BYTE PTR [edx]
	mov	BYTE PTR [ecx], al

; 55   :             DestColumn++;

	mov	ecx, DWORD PTR _DestColumn$[ebp]
	add	ecx, 1
	mov	DWORD PTR _DestColumn$[ebp], ecx

; 56   :         }

	jmp	SHORT $LN5@ImageCrop
$LN6@ImageCrop:

; 57   :         DestRow++;

	mov	edx, DWORD PTR _DestRow$[ebp]
	add	edx, 1
	mov	DWORD PTR _DestRow$[ebp], edx

; 58   :         DestColumn = 0;

	mov	DWORD PTR _DestColumn$[ebp], 0

; 59   :     }

	jmp	$LN2@ImageCrop
$LN3@ImageCrop:

; 60   : 
; 61   :     return ErrorStackPushReturn(Success, "ImageCrop");

	push	OFFSET $SG110264
	push	0
	call	_ErrorStackPushReturn@8
$LN1@ImageCrop:

; 62   : }

	mov	esp, ebp
	pop	ebp
	ret	36					; 00000024H
_ImageCrop@36 ENDP
_TEXT	ENDS
END
