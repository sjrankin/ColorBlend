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
<member name="M:ChannelShift(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="6">
<summary>
Rotates the value of pixels in the buffer. Values are constrained to individual pixels.
</summary>
<param name="Target">Target buffer that will have its pixels rotated.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
<param name="ShiftBy">How the pixel bits are shifted.</param>
<returns>Value indicating sucess.</returns>
</member>
<member name="M:ChannelMigrate(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="50">
<summary>
Migrate color data from pixel to pixel.
</summary>
<param name="Target">Target buffer that will have its pixels rotated.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
<param name="MigrateBy">How the pixel bits are shifted.</param>
<param name="MigrateAlpha">If true, alpha bits will be migrated.</param>
<param name="MigrateRed">If true, red bits will be migrated.</param>
<param name="MigrateGreen">If true, blue bits will be migrated.</param>
<param name="MigrateBlue">If true, green bits will be migrated.</param>
<returns>Value indicating sucess.</returns>
</member>
<member name="M:PixelMigrate(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="132">
<summary>
Migrate pixels through the buffer. Atomic unit is a pixel, not a channel.
</summary>
<param name="Target">Target buffer that will have its pixels migrated.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
</member>
<member name="M:ChannelSwap(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="144">
<summary>
Swap channels in a given pixel.
</summary>
<param name="Target">Target buffer that will have its pixel channels swapped.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
<param name="SourceIndices">Indicates the source of a sequential channel.</param>
<param name="DestIndices">Incidates the destination of a squential channel.</param>
<param name="IndexCount">Number of indices in both SourceIndices and DestIndices.</param>
<returns>Value indicating success.</returns>
</member>
<member name="M:ChannelSwap3(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="200">
<summary>
Swap channels in a given pixel.
</summary>
<param name="Source">Image source for the operation.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
<param name="SourceIndices">Indicates the source of a sequential channel.</param>
<param name="DestIndices">Incidates the destination of a squential channel.</param>
<param name="IndexCount">Number of indices in both SourceIndices and DestIndices.</param>
<returns>Value indicating success.</returns>
</member>
<member name="M:ChannelSwap4(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Double,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="281">
<summary>
Swap channels in a given pixel.
</summary>
<param name="Source">Image source for the operation.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
<param name="SourceIndices">Indicates the source of a sequential channel.</param>
<param name="DestIndices">Incidates the destination of a squential channel.</param>
<param name="IndexCount">Number of indices in both SourceIndices and DestIndices.</param>
<returns>Value indicating success.</returns>
</member>
<member name="M:ChannelSwap2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="409">
<summary>
Swap channels with optional thresholds.
</summary>
<param name="Source">Source image for swapping data.</param>
<param name="Width">Width of the source and destination.</param>
<param name="Height">Height of the source and destination.</param>
<param name="Strie">Stride of the source and destination.</param>
<param name="SwapOrder">How to swap the channels. No RGB order available as that would be silly.</param>
<param name="ExecOptions">Optional threshold instructions for when to swap.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:shuffle(System.Byte*,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="520">
<summary>
Shuffle the array.
</summary>
<param name="array">The array to shuffle.</param>
<param name="n">Size of <paramref name="array"/>.</param>
</member>
<member name="M:Random3(System.Byte*,System.Byte*,System.Byte*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="540">
<summary>
Shuffle the parameters randomly.
</summary>
<param name="Red">Red channel data.</param>
<param name="Green">Green channel data.</param>
<param name="Blue">Blue channel data.</param>
</member>
<member name="M:Random4(System.Byte*,System.Byte*,System.Byte*,System.Byte*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="559">
<summary>
Shuffle the parameters randomly.
</summary>
<param name="Alpha">Alpha channel data.</param>
<param name="Red">Red channel data.</param>
<param name="Green">Green channel data.</param>
<param name="Blue">Blue channel data.</param>
</member>
<member name="M:RandomChannelSwap(System.Void*,System.Int32,System.Int32,System.Int32,System.UInt32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="581">
<summary>
Swap the channels of the buffer randomly.
</summary>
<param name="Target">Target buffer that will have its pixel channels randomized.</param>
<param name="BufferWidth">Width of the target buffer.</param>
<param name="BufferHeight">Height of the target buffer.</param>
<param name="BufferStride">Stride of the buffer.</param>
<param name="Seed">Random number seed.</param>
<param name="IncludeAlpha">If true, the alpha channel will be included in the random swaps.</param>
</member>
<member name="M:SelectRGBChannels(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="629">
<summary>
Select which RGB channels are shown.
</summary>
<param name="Source">Image source for the operation.</param>
<param name="BufferWidth">Width of the source and destination buffer.</param>
<param name="BufferHeight">Height of the source and destination buffer.</param>
<param name="BufferStride">Stride of the source and destination buffer.</param>
<param name="SelectRed">If TRUE, the red channel will be selected.</param>
<param name="SelectGreen">If TRUE, the green channel will be selected.</param>
<param name="SelectBlue">If TRUE, the blue channel will be selected.</param>
<returns>Value indicating operational status.</returns>
</member>
<member name="M:SelectHSLChannels(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Boolean,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="695">
<summary>
Select which HSL channels are shown.
</summary>
<param name="Source">Image source for the operation.</param>
<param name="BufferWidth">Width of the source and destination buffer.</param>
<param name="BufferHeight">Height of the source and destination buffer.</param>
<param name="BufferStride">Stride of the source and destination buffer.</param>
<param name="SelectHue">If TRUE, the hue channel will be selected.</param>
<param name="SelectSaturation">If TRUE, the saturation channel will be selected.</param>
<param name="SelectLuminance">If TRUE, the luminance channel will be selected.</param>
<returns>Value indicating operational status.</returns>
</member>
<member name="M:RGBCombine(System.Void*,System.Void*,System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="802">
<summary>
Combine three images, each representing a red, green, or blue channel, and return the result.
</summary>
<param name="RedSource">Pointer to the red channel image buffer.</param>
<param name="GreenSource">Pointer to the green channel image buffer.</param>
<param name="BlueSource">Pointer to the blue channel image buffer.</param>
<param name="Width">Width of all four buffers.</param>
<param name="Height">Height of all four buffers.</param>
<param name="Stride">Stride of all four buffers.</param>
<param name="Destination">Where the combined image will be written.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:ApplyBrightnessMap(System.Void*,System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="847">
<summary>
Apply a brightness map to the source image and return it in the destination image.
</summary>
<remarks>
http://en.literateprograms.org/RGB_to_HSV_color_space_conversion_%28C%29
</remarks>
<param name="Source">Source image.</param>
<param name="IlluminationMap">The brightness map to apply. Only the red channel is used.</param>
<param name="Width">Width of the source, destination, and brightness map images.</param>
<param name="Height">Height of the source, destination, and brightness map images.</param>
<param name="Stride">Stride of the source, destination, and brightness map images.</param>
<param name="Destination">Where the result will be written.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:GammaCorrection(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\channels.cpp" line="890">
<summary>
Apply a gamma correction to the source image and return it in the destination image.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the result will be written.</param>
<param name="Gamma">Normalized gamma value to apply. This value is clamped to 0.0 to 1.0.</param>
<param name="IncludeAlpha">If TRUE, alpha is also gamma corrected.</param>
<returns>Value indicating operational results.</returns>
</member>
</members>
</doc>