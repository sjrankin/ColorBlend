; Listing generated by Microsoft (R) Optimizing Compiler Version 19.12.25831.0 

	TITLE	C:\Users\Stuart\Desktop\Projects4\ColorBlend\ColorBlender\IsolateHues.cpp
	.686P
	.XMM
	include listing.inc
	.model	flat

INCLUDELIB MSVCRTD
INCLUDELIB OLDNAMES

PUBLIC	_IsolateHues@44
PUBLIC	__real@3fd6666666666666
PUBLIC	__real@4008000000000000
PUBLIC	__real@4076800000000000
EXTRN	_RGB8ToHSL@24:PROC
EXTRN	_HSLtoRGB8@36:PROC
EXTRN	__fltused:DWORD
;	COMDAT __real@4076800000000000
CONST	SEGMENT
__real@4076800000000000 DQ 04076800000000000r	; 360
CONST	ENDS
;	COMDAT __real@4008000000000000
CONST	SEGMENT
__real@4008000000000000 DQ 04008000000000000r	; 3
CONST	ENDS
;	COMDAT __real@3fd6666666666666
CONST	SEGMENT
__real@3fd6666666666666 DQ 03fd6666666666666r	; 0.35
CONST	ENDS
; Function compile flags: /Odtp
; File c:\users\stuart\desktop\projects4\colorblend\colorblender\isolatehues.cpp
_TEXT	SEGMENT
_S$1 = -88						; size = 8
_DL$2 = -80						; size = 8
_scratch$3 = -72					; size = 8
_L$4 = -64						; size = 8
_H$5 = -56						; size = 8
tv128 = -48						; size = 4
_RowOffset$6 = -44					; size = 4
_PixelSize$ = -40					; size = 4
tv141 = -36						; size = 4
_Src$ = -32						; size = 4
_Column$7 = -28						; size = 4
_Row$8 = -24						; size = 4
_Dest$ = -20						; size = 4
_Index$9 = -16						; size = 4
_DB$10 = -11						; size = 1
_DG$11 = -10						; size = 1
_DR$12 = -9						; size = 1
_DB$13 = -8						; size = 1
_DG$14 = -7						; size = 1
_DR$15 = -6						; size = 1
_A$16 = -5						; size = 1
_Gr$17 = -4						; size = 1
_B$18 = -3						; size = 1
_G$19 = -2						; size = 1
_R$20 = -1						; size = 1
_Source$ = 8						; size = 4
_Width$ = 12						; size = 4
_Height$ = 16						; size = 4
_Stride$ = 20						; size = 4
_Destination$ = 24					; size = 4
_HueRangeStart$ = 28					; size = 8
_HueRangeEnd$ = 36					; size = 8
_IsolateForegroundOp$ = 44				; size = 4
_IsolateBackgroundOp$ = 48				; size = 4
_IsolateHues@44 PROC

; 23   : {

	push	ebp
	mov	ebp, esp
	sub	esp, 88					; 00000058H

; 24   : 	if (Source == NULL)

	cmp	DWORD PTR _Source$[ebp], 0
	jne	SHORT $LN12@IsolateHue

; 25   : 		return NullPointer;

	mov	eax, 3
	jmp	$LN1@IsolateHue
$LN12@IsolateHue:

; 26   : 	if (Destination == NULL)

	cmp	DWORD PTR _Destination$[ebp], 0
	jne	SHORT $LN13@IsolateHue

; 27   : 		return NullPointer;

	mov	eax, 3
	jmp	$LN1@IsolateHue
$LN13@IsolateHue:

; 28   : 
; 29   : 	if (HueRangeStart < 0.0)

	xorps	xmm0, xmm0
	comisd	xmm0, QWORD PTR _HueRangeStart$[ebp]
	jbe	SHORT $LN14@IsolateHue

; 30   : 		HueRangeStart = 0.0;

	xorps	xmm0, xmm0
	movsd	QWORD PTR _HueRangeStart$[ebp], xmm0
$LN14@IsolateHue:

; 31   : 	if (HueRangeEnd > 360.0)

	movsd	xmm0, QWORD PTR _HueRangeEnd$[ebp]
	comisd	xmm0, QWORD PTR __real@4076800000000000
	jbe	SHORT $LN15@IsolateHue

; 32   : 		HueRangeEnd = 360.0;

	movsd	xmm0, QWORD PTR __real@4076800000000000
	movsd	QWORD PTR _HueRangeEnd$[ebp], xmm0
$LN15@IsolateHue:

; 33   : 	if (HueRangeStart > HueRangeEnd)

	movsd	xmm0, QWORD PTR _HueRangeStart$[ebp]
	comisd	xmm0, QWORD PTR _HueRangeEnd$[ebp]
	jbe	SHORT $LN16@IsolateHue

; 34   : 	{
; 35   : 		double scratch = HueRangeStart;

	movsd	xmm0, QWORD PTR _HueRangeStart$[ebp]
	movsd	QWORD PTR _scratch$3[ebp], xmm0

; 36   : 		HueRangeStart = HueRangeEnd;

	movsd	xmm0, QWORD PTR _HueRangeEnd$[ebp]
	movsd	QWORD PTR _HueRangeStart$[ebp], xmm0

; 37   : 		HueRangeEnd = scratch;

	movsd	xmm0, QWORD PTR _scratch$3[ebp]
	movsd	QWORD PTR _HueRangeEnd$[ebp], xmm0
$LN16@IsolateHue:

; 38   : 	}
; 39   : 
; 40   : 	BYTE *Src = (BYTE *)Source;

	mov	eax, DWORD PTR _Source$[ebp]
	mov	DWORD PTR _Src$[ebp], eax

; 41   : 	BYTE *Dest = (BYTE *)Destination;

	mov	ecx, DWORD PTR _Destination$[ebp]
	mov	DWORD PTR _Dest$[ebp], ecx

; 42   : 	int PixelSize = 4;

	mov	DWORD PTR _PixelSize$[ebp], 4

; 43   : 
; 44   : 	for (int Row = 0; Row < Height; Row++)

	mov	DWORD PTR _Row$8[ebp], 0
	jmp	SHORT $LN4@IsolateHue
$LN2@IsolateHue:
	mov	edx, DWORD PTR _Row$8[ebp]
	add	edx, 1
	mov	DWORD PTR _Row$8[ebp], edx
$LN4@IsolateHue:
	mov	eax, DWORD PTR _Row$8[ebp]
	cmp	eax, DWORD PTR _Height$[ebp]
	jge	$LN3@IsolateHue

; 45   : 	{
; 46   : 		int RowOffset = Row * Stride;

	mov	ecx, DWORD PTR _Row$8[ebp]
	imul	ecx, DWORD PTR _Stride$[ebp]
	mov	DWORD PTR _RowOffset$6[ebp], ecx

; 47   : 		for (int Column = 0; Column < Width; Column++)

	mov	DWORD PTR _Column$7[ebp], 0
	jmp	SHORT $LN7@IsolateHue
$LN5@IsolateHue:
	mov	edx, DWORD PTR _Column$7[ebp]
	add	edx, 1
	mov	DWORD PTR _Column$7[ebp], edx
$LN7@IsolateHue:
	mov	eax, DWORD PTR _Column$7[ebp]
	cmp	eax, DWORD PTR _Width$[ebp]
	jge	$LN6@IsolateHue

; 48   : 		{
; 49   : 			int Index = (Column * PixelSize) + RowOffset;

	mov	ecx, DWORD PTR _Column$7[ebp]
	imul	ecx, DWORD PTR _PixelSize$[ebp]
	add	ecx, DWORD PTR _RowOffset$6[ebp]
	mov	DWORD PTR _Index$9[ebp], ecx

; 50   : 			BYTE A = Src[Index + 3];

	mov	edx, DWORD PTR _Src$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR [edx+3]
	mov	BYTE PTR _A$16[ebp], al

; 51   : 			BYTE R = Src[Index + 2];

	mov	ecx, DWORD PTR _Src$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR [ecx+2]
	mov	BYTE PTR _R$20[ebp], dl

; 52   : 			BYTE G = Src[Index + 1];

	mov	eax, DWORD PTR _Src$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR [eax+1]
	mov	BYTE PTR _G$19[ebp], cl

; 53   : 			BYTE B = Src[Index + 0];

	mov	edx, DWORD PTR _Src$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR [edx]
	mov	BYTE PTR _B$18[ebp], al

; 54   : 			double H, S, L;
; 55   : 			RGB8ToHSL(R, G, B, &H, &S, &L);

	lea	ecx, DWORD PTR _L$4[ebp]
	push	ecx
	lea	edx, DWORD PTR _S$1[ebp]
	push	edx
	lea	eax, DWORD PTR _H$5[ebp]
	push	eax
	movzx	ecx, BYTE PTR _B$18[ebp]
	push	ecx
	movzx	edx, BYTE PTR _G$19[ebp]
	push	edx
	movzx	eax, BYTE PTR _R$20[ebp]
	push	eax
	call	_RGB8ToHSL@24

; 56   : 			if ((H >= HueRangeStart) && (H <= HueRangeEnd))

	movsd	xmm0, QWORD PTR _H$5[ebp]
	comisd	xmm0, QWORD PTR _HueRangeStart$[ebp]
	jb	SHORT $LN17@IsolateHue
	movsd	xmm0, QWORD PTR _HueRangeEnd$[ebp]
	comisd	xmm0, QWORD PTR _H$5[ebp]
	jb	SHORT $LN17@IsolateHue

; 57   : 			{
; 58   : 				switch (IsolateForegroundOp)

	mov	ecx, DWORD PTR _IsolateForegroundOp$[ebp]
	mov	DWORD PTR tv128[ebp], ecx

; 59   : 				{
; 60   : 
; 61   : 				}
; 62   : 				Dest[Index + 3] = A;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _A$16[ebp]
	mov	BYTE PTR [edx+3], al

; 63   : 				Dest[Index + 2] = R;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _R$20[ebp]
	mov	BYTE PTR [ecx+2], dl

; 64   : 				Dest[Index + 1] = G;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR _G$19[ebp]
	mov	BYTE PTR [eax+1], cl

; 65   : 				Dest[Index + 0] = B;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _B$18[ebp]
	mov	BYTE PTR [edx], al

; 66   : 			}
; 67   : 			else

	jmp	$LN10@IsolateHue
$LN17@IsolateHue:

; 68   : 			{
; 69   : 				switch (IsolateBackgroundOp)

	mov	ecx, DWORD PTR _IsolateBackgroundOp$[ebp]
	mov	DWORD PTR tv141[ebp], ecx
	cmp	DWORD PTR tv141[ebp], 7
	ja	$LN27@IsolateHue
	mov	edx, DWORD PTR tv141[ebp]
	jmp	DWORD PTR $LN29@IsolateHue[edx*4]
$LN19@IsolateHue:

; 70   : 				{
; 71   : 				case HueIsolationOp_Clear:
; 72   : 					Dest[Index + 3] = 0x0;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax+3], 0

; 73   : 					Dest[Index + 2] = 0x0;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [ecx+2], 0

; 74   : 					Dest[Index + 1] = 0x0;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [edx+1], 0

; 75   : 					Dest[Index + 0] = 0x0;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax], 0

; 76   : 					break;

	jmp	$LN10@IsolateHue
$LN20@IsolateHue:

; 77   : 
; 78   : 				case HueIsolationOp_Gray:
; 79   : 					Dest[Index + 3] = 0xff;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [ecx+3], 255			; 000000ffH

; 80   : 					Dest[Index + 2] = 0xc0;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [edx+2], 192			; 000000c0H

; 81   : 					Dest[Index + 1] = 0xc0;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax+1], 192			; 000000c0H

; 82   : 					Dest[Index + 0] = 0xc0;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [ecx], 192			; 000000c0H

; 83   : 					break;

	jmp	$LN10@IsolateHue
$LN21@IsolateHue:

; 84   : 
; 85   : 				case HueIsolationOp_White:
; 86   : 					Dest[Index + 3] = 0xff;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [edx+3], 255			; 000000ffH

; 87   : 					Dest[Index + 2] = 0xff;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax+2], 255			; 000000ffH

; 88   : 					Dest[Index + 1] = 0xff;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [ecx+1], 255			; 000000ffH

; 89   : 					Dest[Index + 0] = 0xff;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [edx], 255			; 000000ffH

; 90   : 					break;

	jmp	$LN10@IsolateHue
$LN22@IsolateHue:

; 91   : 
; 92   : 				case HueIsolationOp_Black:
; 93   : 					Dest[Index + 3] = 0xff;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax+3], 255			; 000000ffH

; 94   : 					Dest[Index + 2] = 0x0;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [ecx+2], 0

; 95   : 					Dest[Index + 1] = 0x0;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [edx+1], 0

; 96   : 					Dest[Index + 0] = 0x0;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax], 0

; 97   : 					break;

	jmp	$LN10@IsolateHue
$LN23@IsolateHue:

; 98   : 
; 99   : 				case HueIsolationOp_Grayscale:
; 100  : 				{
; 101  : 					BYTE Gr = (BYTE)(((double)R + (double)G + (double)B) / 3.0);

	movzx	ecx, BYTE PTR _R$20[ebp]
	cvtsi2sd xmm0, ecx
	movzx	edx, BYTE PTR _G$19[ebp]
	cvtsi2sd xmm1, edx
	addsd	xmm0, xmm1
	movzx	eax, BYTE PTR _B$18[ebp]
	cvtsi2sd xmm1, eax
	addsd	xmm0, xmm1
	divsd	xmm0, QWORD PTR __real@4008000000000000
	cvttsd2si ecx, xmm0
	mov	BYTE PTR _Gr$17[ebp], cl

; 102  : 					Dest[Index + 3] = 0xff;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [edx+3], 255			; 000000ffH

; 103  : 					Dest[Index + 2] = Gr;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR _Gr$17[ebp]
	mov	BYTE PTR [eax+2], cl

; 104  : 					Dest[Index + 1] = Gr;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _Gr$17[ebp]
	mov	BYTE PTR [edx+1], al

; 105  : 					Dest[Index + 0] = Gr;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _Gr$17[ebp]
	mov	BYTE PTR [ecx], dl

; 106  : 				}
; 107  : 				break;

	jmp	$LN10@IsolateHue
$LN24@IsolateHue:

; 108  : 
; 109  : 				case HueIsolationOp_Copy:
; 110  : 					Dest[Index + 3] = R;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR _R$20[ebp]
	mov	BYTE PTR [eax+3], cl

; 111  : 					Dest[Index + 2] = R;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _R$20[ebp]
	mov	BYTE PTR [edx+2], al

; 112  : 					Dest[Index + 1] = G;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _G$19[ebp]
	mov	BYTE PTR [ecx+1], dl

; 113  : 					Dest[Index + 0] = B;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR _B$18[ebp]
	mov	BYTE PTR [eax], cl

; 114  : 					break;

	jmp	$LN10@IsolateHue
$LN25@IsolateHue:

; 115  : 
; 116  : 				case HueIsolationOp_Desaturate:
; 117  : 				{
; 118  : 					BYTE DR, DG, DB;
; 119  : 					HSLtoRGB8(H, 0.0, L, &DR, &DG, &DB);

	lea	edx, DWORD PTR _DB$13[ebp]
	push	edx
	lea	eax, DWORD PTR _DG$14[ebp]
	push	eax
	lea	ecx, DWORD PTR _DR$15[ebp]
	push	ecx
	sub	esp, 8
	movsd	xmm0, QWORD PTR _L$4[ebp]
	movsd	QWORD PTR [esp], xmm0
	sub	esp, 8
	xorps	xmm0, xmm0
	movsd	QWORD PTR [esp], xmm0
	sub	esp, 8
	movsd	xmm0, QWORD PTR _H$5[ebp]
	movsd	QWORD PTR [esp], xmm0
	call	_HSLtoRGB8@36

; 120  : 					Dest[Index + 3] = A;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _A$16[ebp]
	mov	BYTE PTR [edx+3], al

; 121  : 					Dest[Index + 2] = DR;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _DR$15[ebp]
	mov	BYTE PTR [ecx+2], dl

; 122  : 					Dest[Index + 1] = DG;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR _DG$14[ebp]
	mov	BYTE PTR [eax+1], cl

; 123  : 					Dest[Index + 0] = DB;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _DB$13[ebp]
	mov	BYTE PTR [edx], al

; 124  : 				}
; 125  : 				break;

	jmp	$LN10@IsolateHue
$LN26@IsolateHue:

; 126  : 
; 127  : 				case HueIsolationOp_Deluminate:
; 128  : 				{
; 129  : 					BYTE DR, DG, DB;
; 130  : 					double DL = L * 0.35;

	movsd	xmm0, QWORD PTR _L$4[ebp]
	mulsd	xmm0, QWORD PTR __real@3fd6666666666666
	movsd	QWORD PTR _DL$2[ebp], xmm0

; 131  : 					HSLtoRGB8(H, S, DL, &DR, &DG, &DB);

	lea	ecx, DWORD PTR _DB$10[ebp]
	push	ecx
	lea	edx, DWORD PTR _DG$11[ebp]
	push	edx
	lea	eax, DWORD PTR _DR$12[ebp]
	push	eax
	sub	esp, 8
	movsd	xmm0, QWORD PTR _DL$2[ebp]
	movsd	QWORD PTR [esp], xmm0
	sub	esp, 8
	movsd	xmm0, QWORD PTR _S$1[ebp]
	movsd	QWORD PTR [esp], xmm0
	sub	esp, 8
	movsd	xmm0, QWORD PTR _H$5[ebp]
	movsd	QWORD PTR [esp], xmm0
	call	_HSLtoRGB8@36

; 132  : 					Dest[Index + 3] = A;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _A$16[ebp]
	mov	BYTE PTR [ecx+3], dl

; 133  : 					Dest[Index + 2] = DR;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	cl, BYTE PTR _DR$12[ebp]
	mov	BYTE PTR [eax+2], cl

; 134  : 					Dest[Index + 1] = DG;

	mov	edx, DWORD PTR _Dest$[ebp]
	add	edx, DWORD PTR _Index$9[ebp]
	mov	al, BYTE PTR _DG$11[ebp]
	mov	BYTE PTR [edx+1], al

; 135  : 					Dest[Index + 0] = DB;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _DB$10[ebp]
	mov	BYTE PTR [ecx], dl

; 136  : 				}
; 137  : 				break;

	jmp	SHORT $LN10@IsolateHue
$LN27@IsolateHue:

; 138  : 
; 139  : 				default:
; 140  : 					Dest[Index + 3] = 0xff;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax+3], 255			; 000000ffH

; 141  : 					Dest[Index + 2] = R;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	dl, BYTE PTR _R$20[ebp]
	mov	BYTE PTR [ecx+2], dl

; 142  : 					Dest[Index + 1] = 0x0;

	mov	eax, DWORD PTR _Dest$[ebp]
	add	eax, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [eax+1], 0

; 143  : 					Dest[Index + 0] = 0x0;

	mov	ecx, DWORD PTR _Dest$[ebp]
	add	ecx, DWORD PTR _Index$9[ebp]
	mov	BYTE PTR [ecx], 0
$LN10@IsolateHue:

; 144  : 					break;
; 145  : 				}
; 146  : 			}
; 147  : 		}

	jmp	$LN5@IsolateHue
$LN6@IsolateHue:

; 148  : 	}

	jmp	$LN2@IsolateHue
$LN3@IsolateHue:

; 149  : 
; 150  : 	return Success;

	xor	eax, eax
$LN1@IsolateHue:

; 151  : }

	mov	esp, ebp
	pop	ebp
	ret	44					; 0000002cH
$LN29@IsolateHue:
	DD	$LN19@IsolateHue
	DD	$LN20@IsolateHue
	DD	$LN22@IsolateHue
	DD	$LN21@IsolateHue
	DD	$LN23@IsolateHue
	DD	$LN24@IsolateHue
	DD	$LN25@IsolateHue
	DD	$LN26@IsolateHue
_IsolateHues@44 ENDP
_TEXT	ENDS
END
