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
<member name="M:ConvertBlockToDouble(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="29">
<summary>
Convert a block from bytes to doubles, one double per pixel channel.
</summary>
<param name="Source">The source byte block to convert.</param>
<param name="SourceWidth">The width of the source block.</param>
<param name="SourceHeight">The height of the source block.</param>
<param name="SourceStride">The stride of the source block.</param>
<param name="Destination">Pointer to the double buffer - must be same size and dimensions as <paramref name="Source"/>.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:AccumulateDoubleBlock(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="66">
<summary>
Accumulate blocks of doubles.
</summary>
<param name="Source">The source byte block to convert.</param>
<param name="SourceWidth">The width of the source block.</param>
<param name="SourceHeight">The height of the source block.</param>
<param name="SourceStride">The stride of the source block.</param>
<param name="Accumulator">Stores accumulated values - must be same size and dimensions as <paramref name="Source"/>.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:DoubleBlockOperation(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Double,System.Boolean)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="138">
<summary>
Apply an operand to each double in <paramref name="Source"/>.
</summary>
<param name="Source">The source byte block to convert.</param>
<param name="SourceWidth">The width of the source block.</param>
<param name="SourceHeight">The height of the source block.</param>
<param name="SourceStride">The stride of the source block.</param>
<param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
<param name="Operand">The value applied via the <paramref name="Operator"/>.</param>
<param name="IncludeAlpha">Determines if alpha is operated upon.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:ByteBlockOperationByChannel(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="214">
<summary>
Apply an operand to each selected channel byte in <paramref name="Source"/>.
</summary>
<param name="Source">The source byte block to convert.</param>
<param name="SourceWidth">The width of the source block.</param>
<param name="SourceHeight">The height of the source block.</param>
<param name="SourceStride">The stride of the source block.</param>
<param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
<param name="OperandValue">The value applied via the <paramref name="Operator"/>.</param>
<param name="DoAlpha">Determines if alpha is operated upon.</param>
<param name="DoRed">Determines if red is operated upon.</param>
<param name="DoGreen">Determines if green is operated upon.</param>
<param name="DoBlue">Determines if blue is operated upon.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:ByteBlockOperation(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Byte,System.Boolean)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="481">
<summary>
Apply an operand to each byte in <paramref name="Source"/>.
</summary>
<param name="Source">The source byte block to convert.</param>
<param name="SourceWidth">The width of the source block.</param>
<param name="SourceHeight">The height of the source block.</param>
<param name="SourceStride">The stride of the source block.</param>
<param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
<param name="Operand">The value applied via the <paramref name="Operator"/>.</param>
<param name="IncludeAlpha">Determines if alpha is operated upon.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:ByteBlocksOperation(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="623">
<summary>
Apply two buffers to each other and store the result in a third buffer. All buffers must be the same size and dimensions.
</summary>
<param name="Destination">The destination of the operation.</param>
<param name="DestinationWidth">The width of the destination block.</param>
<param name="DestinationHeight">The height of the destination block.</param>
<param name="DestinationStride">The stride of the destination block.</param>
<param name="BufferA">First operand buffer.</param>
<param name="BufferB">Second operand buffer.</param>
<param name="Operator">Determines the operation that will be used with <paramref name="Operand"/>.</param>
<param name="IncludeAlpha">Determines if alpha is operated upon.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:MeanPixel(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1253">
<summary>
Return the the mean pixel based on luminance.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The mean pixel.</returns>
</member>
<member name="M:MedianPixel(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1289">
<summary>
Return the the median pixel based on luminance.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The median pixel.</returns>
</member>
<member name="M:BrightestPixel(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1345">
<summary>
Return the the pixel with the brightest luminance.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The brightest pixel.</returns>
</member>
<member name="M:DarkestPixel(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1372">
<summary>
Return the the pixel with the darkest luminance.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The darkest pixel.</returns>
</member>
<member name="M:PixelSum(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1399">
<summary>
Return the the sum of all of the passed pixels. Each channel clamped to 0xff.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The sum of the passed pixels.</returns>
</member>
<member name="M:PixelSumDouble(System.Double*,System.Int32,System.Double*,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1433">
<summary>
Return the sum of each channel in a buffer consisting of four doubles per pixel.
</summary>
<param name="DoublePixels">Points to the buffer with four doubles per pixel.</param>
<param name="PixelCount">Number of pixels in <paramref name="DoublePixels"/>.</param>
<param name="AlphaSum">On return, the sum of all alpha channel values.</param>
<param name="RedSum">On return, the sum of all red channel values.</param>
<param name="GreenSum">On return, the sum of all green channel values.</param>
<param name="BlueSum">On return, the sum of all blue channel values.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:ClosestPixelLuminance(System.UInt32*,System.Int32,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1468">
<summary>
Return the pixel whose luminance is the closest to <paramref name="LuminanceTarget"/>.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<param name="LuminanceTarget">Determines which pixel is returned.</param>
<returns>The pixel that has a luminance closest to <paramref name="LuminanceTarget"/>.</returns>
</member>
<member name="M:LeastClosestPixelLuminance(System.UInt32*,System.Int32,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1496">
<summary>
Return the pixel whose luminance is the farthest away from <paramref name="LuminanceTarget"/>.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<param name="LuminanceTarget">Determines which pixel is returned.</param>
<returns>The pixel that is the farthest away from <paramref name="LuminanceTarget"/>.</returns>
</member>
<member name="M:SmallestAlphaPixel(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1524">
<summary>
Return the pixel whose alpha value is smallest of the passed set of pixels.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The pixel that has the smallest alpha value.</returns>
</member>
<member name="M:GreatestAlphaPixel(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1551">
<summary>
Return the pixel whose alpha value is greatest of the passed set of pixels.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<returns>The pixel that has the greatest alpha value.</returns>
</member>
<member name="M:ClosestAlphaPixel(System.UInt32*,System.Int32,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1578">
<summary>
Return the pixel whose alpha value is the closest to a specific value.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<param name="FarthestFrom">
Determines which pixel is returned. The pixel whose alpha value is the closest to this returned.
</param>
<returns>The pixel that is closest to <paramref name="FarthestFrom"/>.</returns>
</member>
<member name="M:FarthestAlphaPixel(System.UInt32*,System.Int32,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1608">
<summary>
Return the pixel whose alpha value is the farther away from the specific value.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<param name="FarthestFrom">
Determines which pixel is returned. The pixel whose alpha value is the farthest away from this returned.
</param>
<returns>The pixel that is farthest away from <paramref name="FarthestFrom"/>.</returns>
</member>
<member name="M:PixelLogicalOperation(System.UInt32*,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1638">
<summary>
Perform an arithmetical logical operation on all channels of the passed pixels.
</summary>
<param name="PackedPixels">Array of pixels to process.</param>
<param name="PixelCount">Number of pixels in <paramref name="PackedPixels"/>.</param>
<param name="Operation">The arithmetic logical operation to perform on all of the pixels.</param>
<returns>Pixel based on the arithmetic logical operation.</returns>
</member>
<member name="M:MassImageArithmetic(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\fullbufferfunctions.cpp" line="1694">
<summary>
Perform an arithmetic type operation on the pixels in the image.
</summary>
<remarks>
All images must have the same dimensions and stride.
</remarks>
<param name="Destination">Will contain the result of the arithmetic processing.</param>
<param name="Width">Width of the image buffer.</param>
<param name="Height">Height of the image buffer.</param>
<param name="Stride">Stride of the image buffer.</param>
<param name="ImageSet">Pointer to an array of images to process.</param>
<param name="ImageCount">Number of images in <paramref name="ImageSet"/>.</param>
<param name="Operation">The operation to perform.</param>
<param name="ExtraData">Context-sensitive data - not all operations require this data.</param>
<returns>Value indication operational success.</returns>
</member>
</members>
</doc>