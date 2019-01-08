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
<member name="M:DrawBlocks(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="196">
<summary>
Draws a set of color blocks using alpha blending.
</summary>
<param name="Target">Where the drawing will take place.</param>
<param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
<param name="TargetHeight">The height of the target in scanlines.</param>
<param name="TargetStride">The stride of the target.</param>
<param name="ColorBlockList">Array of information on how to draw the color blocks.</param>
<param name="ColorBlockCount">Number of color blocks in the ColorBlockList.</param>
<param name="DefaultColor">The background color when there are no blocks. Format is BGRA.</param>
<returns>True on success, false on error.</returns>
</member>
<member name="M:DrawLine(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="314">
<summary>
Draw a horizontal or vertical line.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="IsHorizontal">Determines if a horizontal or vertical line will be drawn.</param>
<param name="Coordinate">The column where the line will be drawn.</param>
<param name="A">The alpha value of the line to draw.</param>
<param name="R">The red value of the line to draw.</param>
<param name="G">The green value of the line to draw.</param>
<param name="B">The blue value of the line to draw.</param>
</member>
<member name="M:DrawAnyLine(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="376">
<summary>
Draw a line from the two specfied points using the specified color. Parts of lines that extend beyond the bounds set by
<paramref name="TargetWidth"/> and <paramref name="TargetHeight"/> are not drawn.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="X1">First horizontal coordinate.</param>
<param name="Y1">First vertical coordinate.</param>
<param name="X2">Second horizontal coordinate.</param>
<param name="Y2">Second vertical coordinate.</param>
<param name="A">The alpha value of the line to draw.</param>
<param name="R">The red value of the line to draw.</param>
<param name="G">The green value of the line to draw.</param>
<param name="B">The blue value of the line to draw.</param>
<returns>True on success, false on error.</returns>
</member>
<member name="M:DrawHorizontalLine(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="401">
<summary>
Draw a horizontal line. Does alpha blending.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="Y">The row where the line will be drawn.</param>
<param name="A">The alpha value of the line to draw.</param>
<param name="R">The red value of the line to draw.</param>
<param name="G">The green value of the line to draw.</param>
<param name="B">The blue value of the line to draw.</param>
</member>
<member name="M:DrawVerticalLine(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="450">
<summary>
Draw a vertical line. Does alpha blending.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="X">The column where the line will be drawn.</param>
<param name="A">The alpha value of the line to draw.</param>
<param name="R">The red value of the line to draw.</param>
<param name="G">The green value of the line to draw.</param>
<param name="B">The blue value of the line to draw.</param>
</member>
<member name="M:DrawVerticalLine(System.Byte*,System.Int32,System.Int32,System.Int32,System.Int32,PureColorType*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="499">
<summary>
Draw a vertical line in <paramref name="Buffer"/>.
</summary>
<param name="Buffer">The buffer where the line will be drawn.</param>
<param name="Width">The width of the buffer.</param>
<param name="Height">The height of the buffer.</param>
<param name="Stride">The stride of the buffer.</param>
<param name="ColorIndex">Determines where and what color the line will be.</param>
<param name="ColorSet">Source for line location and color.</param>
</member>
<member name="M:DrawHorizontalLine(System.Byte*,System.Int32,System.Int32,System.Int32,System.Int32,PureColorType*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="523">
<summary>
Draw a horizontal line in <paramref name="Buffer"/>.
</summary>
<param name="Buffer">The buffer where the line will be drawn.</param>
<param name="Width">The width of the buffer.</param>
<param name="Height">The height of the buffer.</param>
<param name="Stride">The stride of the buffer.</param>
<param name="ColorIndex">Determines where and what color the line will be.</param>
<param name="ColorSet">Source for line location and color.</param>
</member>
<member name="M:RenderColorBlob(System.Void*,System.Int32,System.Int32,System.Int32,System.UInt32,System.Byte,System.Byte,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\render.cpp" line="547">
<summary>
Render a color blob in the provided buffer. The buffer defines the size of the blob to render.
</summary>
<param name="Target">Where the blob will be rendered.</param>
<param name="ImageWidth">The width of the blob/target buffer.</param>
<param name="ImageHeight">The height of the blob/target buffer.</param>
<param name="ImageStride">The stride of the blob/target buffer.</param>
<param name="BlobColor">Packed blob color.</param>
<param name="CenterAlpha">The alpha value at the center of the blob.</param>
<param name="EdgeAlpha">The alpha value at the edge of the blob.</param>
<param name="EdgeColor">
The color of the border to draw around the edge of the enclosing rectangle. If this color's alpha is 0x0, no edge drawing is done.
</param>
<returns>TRUE on success, FALSE on error.</returns>
</member>
</members>
</doc>