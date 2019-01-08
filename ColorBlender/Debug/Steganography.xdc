<?xml version="1.0"?><doc>
<members>
<member name="T:CommonObject" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="16">
<summary>
Defines a common structure for rendering of all objects.
</summary>
</member>
<member name="F:CommonObject.ObjectAction" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="21">
<summary>
Tells the renderer what action to take with this object.
</summary>
</member>
<member name="F:CommonObject.ObjectBuffer" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="25">
<summary>
The buffer that will be blended/rendered.
</summary>
</member>
<member name="F:CommonObject.ObjectWidth" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="29">
<summary>
The width of the buffer.
</summary>
</member>
<member name="F:CommonObject.ObjectHeight" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="33">
<summary>
The height of the buffer.
</summary>
</member>
<member name="F:CommonObject.ObjectStride" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="37">
<summary>
The stride of the buffer.
</summary>
</member>
<member name="F:CommonObject.Left" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="41">
<summary>
Left side of the object.
</summary>
</member>
<member name="F:CommonObject.Top" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="45">
<summary>
Top side of the object.
</summary>
</member>
<member name="F:CommonObject.Right" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="49">
<summary>
Right side of the object.
</summary>
</member>
<member name="F:CommonObject.Bottom" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="53">
<summary>
Bottom side of the object.
</summary>
</member>
<member name="T:DisplayInstructionList2" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="59">
<summary>
Display list structure.
</summary>
</member>
<member name="F:DisplayInstructionList2.Operand" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="64">
<summary>
The type of object to display.
</summary>
</member>
<member name="F:DisplayInstructionList2.ReturnOnFailure" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="68">
<summary>
Determines if control is returned to the caller if this particular display list item fails.
</summary>
</member>
<member name="F:DisplayInstructionList2.Parameters" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="72">
<summary>
Pointer to a parameter block specific to Operand.
</summary>
</member>
<member name="F:DisplayInstructionList2.Object" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="76">
<summary>
Not currently used.
</summary>
</member>
<member name="T:GradientStop" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="82">
<summary>
One gradient stop.
</summary>
</member>
<member name="F:GradientStop.StartColor" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="87">
<summary>
Starting color.
</summary>
</member>
<member name="F:GradientStop.EndColor" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="91">
<summary>
Ending color.
</summary>
</member>
<member name="F:GradientStop.AbsStart" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="95">
<summary>
Absolute start of the gradient range.
</summary>
</member>
<member name="F:GradientStop.AbsEnd" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="99">
<summary>
Absolute end of the gradient range.
</summary>
</member>
<member name="F:GradientStop.AbsGap" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="103">
<summary>
Absolute gap size.
</summary>
</member>
<member name="T:RelativePointStruct" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="199">
<summary>
Defines a relative `. Assumes values clamped from 0.0 to 1.0.
</summary>
</member>
<member name="F:RelativePointStruct.X" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="204">
<summary>
Relative horizontal coordinate.
</summary>
</member>
<member name="F:RelativePointStruct.Y" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="208">
<summary>
Relative vertical coordinate.
</summary>
</member>
<member name="T:AbsolutePointStruct" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="214">
<summary>
Determines an absolute point.
</summary>
</member>
<member name="F:AbsolutePointStruct.X" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="219">
<summary>
Horizontal coordinate.
</summary>
</member>
<member name="F:AbsolutePointStruct.Y" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="223">
<summary>
Vertical coordinate.
</summary>
</member>
<member name="T:PureColorType" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="237">
<summary>
Defines a pure color type.
</summary>
</member>
<member name="F:PureColorType.X" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="242">
<summary>
Horizontal coordinate.
</summary>
</member>
<member name="F:PureColorType.Y" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="246">
<summary>
Vertical coordinate.
</summary>
</member>
<member name="F:PureColorType.Alpha" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="250">
<summary>
The alpha channel value.
</summary>
</member>
<member name="F:PureColorType.Red" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="254">
<summary>
The red channel value.
</summary>
</member>
<member name="F:PureColorType.Green" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="258">
<summary>
The green channel value.
</summary>
</member>
<member name="F:PureColorType.Blue" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="262">
<summary>
The blue channel value.
</summary>
</member>
<member name="F:PureColorType.Hypotenuse" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="266">
<summary>
The value of the hypotenuse used to calculate color percents. Ignored if UseRadius is true.
</summary>
</member>
<member name="F:PureColorType.Radius" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="270">
<summary>
The radius that determines the percent value for the colors. Used only if UseRadius is true.
</summary>
</member>
<member name="F:PureColorType.AlphaStart" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="274">
<summary>
Starting alpha value (e.g. the alpha value at X,Y). Ignored if UseAlpha is false.
</summary>
</member>
<member name="F:PureColorType.AlphaEnd" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="278">
<summary>
Ending alpha value (at 100% of either the Hypotenuse or Radius). Ignored if UseAlpha is false.
</summary>
</member>
<member name="F:PureColorType.UseRadius" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="282">
<summary>
Determines if the Radius value is used.
</summary>
</member>
<member name="F:PureColorType.UseAlpha" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="286">
<summary>
Determines if alpha values are calculated.
</summary>
</member>
<member name="F:PureColorType.DrawHorizontalIndicator" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="290">
<summary>
Determines if horizontal indicators are drawn.
</summary>
</member>
<member name="F:PureColorType.DrawVerticalIndicator" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="294">
<summary>
Determines if vertical indicators are drawn.
</summary>
</member>
<member name="F:PureColorType.DrawPointIndicator" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="298">
<summary>
Determines if the point indicator is drawn.
</summary>
</member>
<member name="T:LineDefinitionStruct" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="304">
<summary>
Defines a point from which vertical and horizontal lines will be drawn.
</summary>
</member>
<member name="F:LineDefinitionStruct.X" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="309">
<summary>
Horizontal coordinate.
</summary>
</member>
<member name="F:LineDefinitionStruct.Y" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="313">
<summary>
Vertical coordinate.
</summary>
</member>
<member name="F:LineDefinitionStruct.Alpha" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="317">
<summary>
The alpha channel value.
</summary>
</member>
<member name="F:LineDefinitionStruct.Red" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="321">
<summary>
The red channel value.
</summary>
</member>
<member name="F:LineDefinitionStruct.Green" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="325">
<summary>
The green channel value.
</summary>
</member>
<member name="F:LineDefinitionStruct.Blue" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="329">
<summary>
The blue channel value.
</summary>
</member>
<member name="F:LineDefinitionStruct.DrawPointIndicator" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="333">
<summary>
Determines if the point indicator is drawn.
</summary>
</member>
<member name="F:LineDefinitionStruct.DrawVerticalLines" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="337">
<summary>
Determines if vertical lines are drawn.
</summary>
</member>
<member name="F:LineDefinitionStruct.DrawHorizontalLines" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="341">
<summary>
Determines if horiztonal lines are drawn.
</summary>
</member>
<member name="T:PureColorStruct" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="347">
<summary>
Defines a color value.
</summary>
</member>
<member name="F:PureColorStruct.UseAlpha" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="352">
<summary>
Determines if the alpha channel should be used.
</summary>
</member>
<member name="F:PureColorStruct.Alpha" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="356">
<summary>
The alpha channel value.
</summary>
</member>
<member name="F:PureColorStruct.Red" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="360">
<summary>
The red channel value.
</summary>
</member>
<member name="F:PureColorStruct.Green" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="364">
<summary>
The green channel value.
</summary>
</member>
<member name="F:PureColorStruct.Blue" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="368">
<summary>
The blue channel value.
</summary>
</member>
<member name="T:ImageDefintionStruct" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="395">
<summary>
Defines a region in an image or an entire image depending on usage. Not all fields are necessarily used.
</summary>
</member>
<member name="F:ImageDefintionStruct.Buffer" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="400">
<summary>
Pointer to the bits in the region.
</summary>
</member>
<member name="F:ImageDefintionStruct.X" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="404">
<summary>
Left of the upper-left corner.
</summary>
</member>
<member name="F:ImageDefintionStruct.Y" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="408">
<summary>
Upper of the upper-left corner.
</summary>
</member>
<member name="F:ImageDefintionStruct.Width" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="412">
<summary>
Width of the region.
</summary>
</member>
<member name="F:ImageDefintionStruct.Height" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="416">
<summary>
Height of the region.
</summary>
</member>
<member name="F:ImageDefintionStruct.Stride" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="420">
<summary>
Stride of the region.
</summary>
</member>
<member name="T:RegionStruct" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="426">
<summary>
Defines a region.
</summary>
</member>
<member name="F:RegionStruct.Top" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="431">
<summary>
The top of the region.
</summary>
</member>
<member name="F:RegionStruct.Left" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="435">
<summary>
The left side of the region.
</summary>
</member>
<member name="F:RegionStruct.Bottom" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="439">
<summary>
The bottom of the region.
</summary>
</member>
<member name="F:RegionStruct.Right" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\structures.h" line="443">
<summary>
The right side of the region.
</summary>
</member>
<member name="T:OctreeNode" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\octree.h" line="6">
<summary>
One octree node.
</summary>
</member>
<member name="F:OctreeNode.Color" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\octree.h" line="11">
<summary>
The color of the node.
</summary>
</member>
<member name="F:OctreeNode.Count" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\octree.h" line="15">
<summary>
Color count.
</summary>
</member>
<member name="M:RandomizeImageBitsRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.UInt32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="16">
<summary>
Randomize bits in the specicified channels specified by the supplied bit mask for the specified region in the source.
</summary>
<remarks>
If no channels are selected or if the random mask is 0x0, no action will be taken and the destinaion buffer will not be filled.
</remarks>
<param name="Buffer">Source image to whose bits will be randomized.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Destination">Destination buffer - where the results are written.</param>
<param name="Left">The left coordinate of the region to convert.</param>
<param name="Top">The top coordinate of the region to convert.</param>
<param name="Right">The right coordinate of the region to convert.</param>
<param name="Bottom">The bottom coordinate of the region to convert.</param>
<param name="RandomizeMask">The mask used to determine which bits are randomized. If this value is 0x0, no action is taken.</param>
<param name="RandomSeed">Random number generator seed.</param>
<param name="IncludeAlpha">Randomize the alpha channel.</param>
<param name="IncludeRed">Randomize the red channel.</param>
<param name="IncludeGreen">Randomize the green channel.</param>
<param name="IncludeBlue">Randomize the blue channel.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:RandomizeImageBits1(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Byte,System.UInt32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="90">
<summary>
Randomize bits in the specicified channels specified by the supplied bit mask.
</summary>
<remarks>
If no channels are selected or if the random mask is 0x0, no action will be taken and the destinaion buffer will not be filled.
</remarks>
<param name="Buffer">Source image to whose bits will be randomized.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Destination">Destination buffer - where the results are written.</param>
<param name="RandomizeMask">The mask used to determine which bits are randomized. If this value is 0x0, no action is taken.</param>
<param name="RandomSeed">Random number generator seed.</param>
<param name="IncludeAlpha">Randomize the alpha channel.</param>
<param name="IncludeRed">Randomize the red channel.</param>
<param name="IncludeGreen">Randomize the green channel.</param>
<param name="IncludeBlue">Randomize the blue channel.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:RandomizeImageBits2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Byte,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="114">
<summary>
Randomize bits in all channels specified by the supplied bit mask.
</summary>
<param name="Buffer">Source image to whose bits will be randomized.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Destination">Destination buffer - where the results are written.</param>
<param name="RandomizeMask">The mask used to determine which bits are randomized. If this value is 0x0, no action is taken.</param>
<param name="RandomSeed">Random number generator seed.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:BytePerCharacter(System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="131">
<summary>
Determines the number of bytes needed per character given the available bits to use.
</summary>
<param name="ChannelMask">Available bits per byte.</param>
<returns>Number of bytes needed to store one character. Returns -1 on error.</returns>
</member>
<member name="M:BytesRequiredToFit(System.Byte,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="145">
<summary>
Determines the number of bytes needed to store the string given bits available in <paramref name="ChannelMask"/>.
</summary>
<param name="ChannelMask">The number of bits to use.</param>
<param name="TextLength">Number of characters in the string.</param>
<returns>Number of required bytes needed to store a string of characters. Returns -1 on error.</returns>
</member>
<member name="M:CanFit(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="159">
<summary>
Determines if the specified string can fit into available space.
</summary>
<param name="Left">Left side of the region.</param>
<param name="Top">Top of the region.</param>
<param name="Right">Right side of the region.</param>
<param name="Bottom">Bottom of the region.</param>
<param name="IncludeAlpha">Determines if the alpha channel is available for use.</param>
<param name="IncludeRed">Determines if the red channel is available for use.</param>
<param name="IncludeGreen">Determines if the green channel is available for use.</param>
<param name="IncludeBlue">Determines if the blue channel is available for use.</param>
<param name="ChannelMask">The number of bits to use in each available channel.</param>
<param name="TextLength">Number of characters in the string.</param>
<returns>TRUE if the text can fit, FALSE if not.</returns>
</member>
<member name="M:MaskOffset(System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="205">
<summary>
Return the number of zeros on the right size of <paramref name="Mask"/>.
</summary>
<param name="Mask">The value to count zeros.</param>
<returns>The number of zeros on the right size of <paramref name="Mask"/>.</returns>
</member>
<member name="M:GetBits(System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="228">
<summary>
Return the bits in <paramref name="Source"/> as defined by <paramref name="Mask"/>. Returned bits are shifted right the
appropriate number (as defined by <paramref name="Mask"/> bits.
</summary>
<param name="Source">The value whose bits are desired.</param>
<param name="Mask">Defines the bits to return.</param>
<returns>The bits in <paramref name="Source"/> as specified by <paramref name="Mask"/>.</returns>
</member>
<member name="M:AddStringToRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Int32,System.Int32,System.Int32,System.Int32,System.SByte!System.Runtime.CompilerServices.IsSignUnspecifiedByte*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="242">
<summary>
Add a string of 8-bit characters to the supplied buffer by setting bits in the specified bit mask.
</summary>
<param name="Buffer">Source image to whose bits will be randomized.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Destination">Destination buffer - where the results are written.</param>
<param name="Left">The left coordinate of the region to use.</param>
<param name="Top">The top coordinate of the region to use.</param>
<param name="Right">The right coordinate of the region to use.</param>
<param name="Bottom">The bottom coordinate of the region to use.</param>
<param name="RelativeXOffset">Horizontal offset.</param>
<param name="RelativeYOffset">Vertical offset.</param>
<param name="ChannelMask">Determines where character bits will be written.</param>
<param name="IncludeAlpha">Write bits in the alpha channel.</param>
<param name="IncludeRed">Write bits in the red channel.</param>
<param name="IncludeGreen">Write bits in the green channel.</param>
<param name="IncludeBlue">Write bits in the blue channel.</param>
<param name="Text">The text to add to the string.</param>
<param name="TextLength">Length of the text to add to the string.</param>
</member>
<member name="M:AddDataToRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Int32,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="333">
<summary>
Merge data from a buffer with the source and place the result in the destination buffer.
</summary>
<param name="Buffer">Source image to whose bits will be randomized.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Destination">Destination buffer - where the results are written.</param>
<param name="Left">The left coordinate of the region to use.</param>
<param name="Top">The top coordinate of the region to use.</param>
<param name="Right">The right coordinate of the region to use.</param>
<param name="Bottom">The bottom coordinate of the region to use.</param>
<param name="RelativeXOffset">Horizontal offset.</param>
<param name="RelativeYOffset">Vertical offset.</param>
<param name="ChannelMask">Determines where character bits will be written.</param>
<param name="IncludeAlpha">Write bits in the alpha channel.</param>
<param name="IncludeRed">Write bits in the red channel.</param>
<param name="IncludeGreen">Write bits in the green channel.</param>
<param name="IncludeBlue">Write bits in the blue channel.</param>
<param name="DataSource">Pointer to the buffer of data to add.</param>
<param name="DataSourceLength">Number of bytes in <paramref name="DataSource"/>.</param>
</member>
<member name="M:DataMerge(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.UInt32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\steganography.cpp" line="480">
<summary>
Merge data (in the form of a BYTE array) with <paramref name="Source"/> into <paramref name="Destination"/>.
</summary>
<remarks>
A header prefixes the data that is merged. The format of the header is Mask Size:Byte Count. Mask size if a byte value and
Byte Count is a UINT32 value. The bits used for the header depends on <paramref name="ByTwo"/>.
</remarks>
<param name="Buffer">Source image to whose bits will be randomized.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Destination">Destination buffer - where the results are written.</param>
<param name="DataBuffer">The data to merge with <paramref name="Source"/>.</param>
<param name="DataCount">Number of byte entries in <paramref name="DataBuffer"/>.</param>
<param name="ByTwo">Determines the number of bits used to merge.</param>
<param name="IncludeAlpha">Write bits in the alpha channel.</param>
<param name="IncludeRed">Write bits in the red channel.</param>
<param name="IncludeGreen">Write bits in the green channel.</param>
<param name="IncludeBlue">Write bits in the blue channel.</param>
<returns>Value indicating operational results.</returns>
</member>
</members>
</doc>