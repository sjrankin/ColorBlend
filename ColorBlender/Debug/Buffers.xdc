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
<member name="M:SwapPixel(System.UInt32*,System.UInt32*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="6">
<summary>
Swap two pixels.
</summary>
<param name="Pixel1">First pixel.</param>
<param name="Pixel2">Second pixel.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:InPlaceReverseScanLine(System.UInt32*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="24">
<summary>
Reverse, in place, a scanline.
</summary>
<param name="ScanLine">The scanline to reverse.</param>
<param name="PixelCount">Number of pixels in the scanline.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:CopyBuffer2(System.Void*,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="45">
<summary>
Copies the image in <paramref name="Source"/> to <paramref name="Destination"/>.
</summary>
<param name="Source">Source buffer.</param>
<param name="Width">Width of both <paramref name="Source"/> and <paramref name="Destination"/>.</param>
<param name="Height">Height of both <paramref name="Source"/> and <paramref name="Destination"/>.</param>
<param name="Destination">Destination buffer.</param>
</member>
<member name="M:CopyBuffer3(System.Void*,System.UInt32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="74">
<summary>
Copies the image (or any memory buffer) in <paramref name="Source"/> to <paramref name="Destination"/>.
</summary>
<param name="Source">Source buffer/image to copy.</param>
<param name="BufferSize">
Size in bytes of the buffer to copy. Both <paramref name="Source"/> and <paramref name="Destination"/>
must be the same size.
</param>
<param name="Destination">Destination of the copy operation.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:CopyHorizontalLine(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="97">
<summary>
Copy a single line to a buffer. The line must have the same stride as the buffer.
</summary>
<param name="Destination">Where the rectangle will be rendered.</param>
<param name="Width">Width of the destination in pixels.</param>
<param name="Height">Height of the destination in scan lines.</param>
<param name="Stride">Stride of the destination.</param>
<param name="LineBuffer">Pointer to the line to copy.</param>
<param name="LineCount">
Number of times to copy the line to the destination. Vertical starting point in the destination is updated after each copy.
</param>
<param name="LineStart">Where to start copying to the buffer, e.g., which line.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:CopyVerticalLine(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="138">
<summary>
Copy a column of data (one pixel wide) from 1 to <paramref name="ColumnCount"/> times to the destination buffer.
</summary>
<param name="Destination">Where the rectangle will be rendered.</param>
<param name="Width">Width of the destination in pixels.</param>
<param name="Height">Height of the destination in scan lines.</param>
<param name="Stride">Stride of the destination.</param>
<param name="ColumnBuffer">Pointer to the column to copy.</param>
<param name="ColumnCount">
Number of times to copy the column to the destination. Vertical starting point in the destination is updated after each copy.
</param>
<param name="ColumnStart">Where to start copying to the buffer, e.g., which destination column.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:CopyRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="186">
<summary>
Copy a subset region from the source buffer and place it into the destination buffer. The region to be copied must have
coordinates valid for the source buffer. The coordinates must be semantically correct (upper-left less than lower-right).
</summary>
<param name="Source">The source image.</param>
<param name="SourceWidth">Width of the source in pixels.</param>
<param name="SourceHeight">Height of the source in scan lines.</param>
<param name="SourceStride">Stride of the source.</param>
<param name="Destination">Where the rectangle will be rendered.</param>
<param name="DestinationWidth">Width of the destination in pixels.</param>
<param name="DestinationHeight">Height of the destination in scan lines.</param>
<param name="DestinationStride">Stride of the destination.</param>
<param name="UpperLeft">Pointer to the upper-left coordinate of the region to copy.</param>
<param name="LowerRight">Pointer to the lower-right coordinate of the region to copy.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:PasteRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="257">
<summary>
Paste a buffer into the destination buffer.
</summary>
<param name="Destination">The target buffer for the paste.</param>
<param name="DestWidth">Width of the destination in pixels.</param>
<param name="DestHeight">Height of the destination in scan lines.</param>
<param name="DestStride">Stride of the destination.</param>
<param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
<param name="SourceWidth">Width of the source in pixels.</param>
<param name="SourceHeight">Height of the source in scan lines.</param>
<param name="SourceStride">Stride of the source.</param>
<param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
<param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:PasteRegion2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="328">
<summary>
Paste a buffer into the destination buffer. Source regions outside the destination will be cropped.
</summary>
<param name="Destination">The target buffer for the paste.</param>
<param name="DestWidth">Width of the destination in pixels.</param>
<param name="DestHeight">Height of the destination in scan lines.</param>
<param name="DestStride">Stride of the destination.</param>
<param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
<param name="SourceWidth">Width of the source in pixels.</param>
<param name="SourceHeight">Height of the source in scan lines.</param>
<param name="SourceStride">Stride of the source.</param>
<param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
<param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:PasteRegion3(System.Void*,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="399">
<summary>
Paste a buffer into the destination buffer. Source regions outside the destination will be cropped.
</summary>
<param name="Destination">The target buffer for the paste.</param>
<param name="DestWidth">Width of the destination in pixels.</param>
<param name="DestHeight">Height of the destination in scan lines.</param>
<param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
<param name="SourceWidth">Width of the source in pixels.</param>
<param name="SourceHeight">Height of the source in scan lines.</param>
<param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
<param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:PasteRegion4(System.Void*,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="505">
<summary>
Paste a buffer into the destination buffer. Source regions outside the destination will be cropped.
</summary>
<param name="Destination">The target buffer for the paste.</param>
<param name="DestWidth">Width of the destination in pixels.</param>
<param name="DestHeight">Height of the destination in scan lines.</param>
<param name="Source">The buffer that will be pasted into <paramref name="Destination"/>.</param>
<param name="SourceWidth">Width of the source in pixels.</param>
<param name="SourceHeight">Height of the source in scan lines.</param>
<param name="UpperLeft">Pointer to the upper-left coordinate in the destination where the paste will occur.</param>
<param name="LowerRight">Pointer to the lower-right coordinate in the destination where the paste will occur.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:CopyBufferToBuffer(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="597">
<summary>
Copies the contents of <paramref name="Source"/> to <paramref name="Destination"/>.
</summary>
<param name="Source">Source buffer.</param>
<param name="Width">Width of the source and destination.</param>
<param name="Height">Height of the source and destination.</param>
<param name="Stride">Stride of the source and destination.</param>
<param name="Destination">Where the contents of <paramref name="Source"/> will be copied.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:ClearBuffer2(System.Void*,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="633">
<summary>
Clears the buffer pointed to by <paramref name="Destination"/> with the color in <paramref name="FillColor"/>.
</summary>
<param name="Destination">The buffer to be cleared.</param>
<param name="Width">Width of the buffer.</param>
<param name="Height">Height of the buffer.</param>
<param name="Stride">Stride of the buffer.</param>
<param name="FillColor">Packed color used to clear the buffer.</param>
<returns>Value indication operational success.</returns>
</member>
<member name="M:CopyCircularBuffer(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="647">
<summary>
Copy a circular region from <paramref name="Source"/>. to <paramref name="Destination"/>. Non-copied region of the destination
is filled by the color specified in <paramref name="PackedBG"/>.
</summary>
<param name="Source">Pointer to the source image.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="Destination">Where the circular region will be copied to.</param>
<param name="DestWidth">Width of the destination buffer.</param>
<param name="DestHeight">Height of the destination buffer.</param>
<param name="DestStride">Stride of the destination buffer.</param>
<param name="X">Horizontal coordinate of the circle to copy.</param>
<param name="Y">Vertical coordinate of the circle to copy.</param>
<param name="Radius">Radius of the circle to copy.</param>
<param name="PackedBG">Packed background color used to fill non-circular pixels.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:SwapImageBuffers(System.Void*,System.Void*,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\buffers.cpp" line="693">
<summary>
Swap the contents of two buffers with the same dimensions.
</summary>
<param name="Buffer1">The first buffer.</param>
<param name="Buffer2">The second buffer.</param>
<param name="Width">Width of both buffers.</param>
<param name="Height">Height of both buffers.</param>
<param name="Stride">Stride of both buffers.</param>
</member>
</members>
</doc>