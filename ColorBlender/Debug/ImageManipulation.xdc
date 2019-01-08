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
<member name="M:SolarizeImage(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="6">
<summary>
Solarize the passed image.
</summary>
<remarks>
http://www.cs.umb.edu/~jreyes/csit114-fall-2007/project4/filters.html
</remarks>
<param name="Source">Pointer to the image buffer that will be solarized.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Threshold">Determines when solarization will occur. Normalized value. Clamped as normalized.</param>
<param name="Invert">If TRUE, solarization is inverted with respect to the threshold.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:AlphaSolarizeImage(System.Void*,System.Int32,System.Int32,System.Int32,System.Double,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="62">
<summary>
Solarize the alpha component of the passed image.
</summary>
<remarks>
http://www.cs.umb.edu/~jreyes/csit114-fall-2007/project4/filters.html
</remarks>
<param name="Source">Pointer to the image buffer that will be solarized.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Threshold">Determines when solarization will occur. Normalized value. Clamped as normalized.</param>
<param name="Invert">If TRUE, solarization is inverted with respect to the threshold.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:AlphaSolarizeImage2(System.Void*,System.Int32,System.Int32,System.Int32,System.Double,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="104">
<summary>
Sets the alpha channel based on the luminance of a given pixel.
</summary>
<remarks>
http://www.cs.umb.edu/~jreyes/csit114-fall-2007/project4/filters.html
</remarks>
<param name="Source">Pointer to the image buffer that will be solarized.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Threshold">Determines when solarization will occur. Normalized value. Clamped as normalized.</param>
<param name="SolarAlpha">The alpha channel value if the luminance passes the <paramref name="Threshold"/> value.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:InvertImageRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Boolean,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="145">
<summary>
Invert the colors of the image pointed to by <paramref name="Source"/>.
</summary>
<param name="Source">Pointer to the image to invert.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Destination">Destination of the inversion.</param>
<param name="IncludeAlpha">Determines if alpha is inverted. Usually not a good idea but just in case...</param>
<param name="Left">Left coordinate of the operational region.</param>
<param name="Top">Top coordinate of the operational region.</param>
<param name="Right">Right coordinate of the operational region.</param>
<param name="Bottom">Bottom coordinate of the operational region.</param>
<param name="CopyOutOfRegion">
If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
</param>
<param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:InvertImage(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Boolean)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="226">
<summary>
Invert the colors of the image pointed to by <paramref name="Source"/>.
</summary>
<param name="Source">Pointer to the image to invert.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Destination">Destination of the inversion.</param>
<param name="IncludeAlpha">Determines if alpha is inverted. Usually not a good idea but just in case...</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:CombineChannels32(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="241">
<summary>
Merge four channels into one image. Each channel will be placed in a byte in the final UINT32.
</summary>
<param name="Destination">Pointer to the destination image buffer.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Channel1">
Pointer to the image with channel 1 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
into index 0.
</param>
<param name="Channel2">
Pointer to the image with channel 2 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
into index 1.
</param>
<param name="Channel3">
Pointer to the image with channel 3 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
into index 2.
</param>
<param name="Channel4">
Pointer to the image with channel 4 data. Must have same dimensions as <paramref name="Destination"/>. Data placed
into index 3.
</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:ChannelMerge(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="302">
<summary>
Merge three channels into one image.
</summary>
<param name="Destination">Pointer to the destination image buffer.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="RedChannel">Pointer to the image with the red channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="GreenChannel">Pointer to the image with the green channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="BlueChannel">Pointer to the image with the blue channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:ChannelMergeAlpha(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="353">
<summary>
Merge four channels (red, green, blue and alpha) into one image.
</summary>
<param name="Destination">Pointer to the destination image buffer.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="AlphaChannel">Pointer to the image with the alpha channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="RedChannel">Pointer to the image with the red channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="GreenChannel">Pointer to the image with the green channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="BlueChannel">Pointer to the image with the blue channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:HSLChannelMerge(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="402">
<summary>
Merge three channels into one image. Alpha is set to 0xff.
</summary>
<param name="Destination">Pointer to the destination image buffer.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="HueChannel">Pointer to the image with the hue channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="SaturationChannel">Pointer to the image with the saturation channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<param name="LuminanceChannel">Pointer to the image with the luminance channel data. Must have same dimensions as <paramref name="Destination"/>.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:SplitImageIntoChannels(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="447">
<summary>
Split the source image into component ARGB channels.
</summary>
<param name="Source">Pointer to the image to split.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="AlphaDest">Pointer to the image that will contain the alpha component. If NULL, the alpha component is not split out. Must have same dimensions as <paramref name="Source"/>.</param>
<param name="RedDest">Pointer to the image that will contain the red component. Must have same dimensions as <paramref name="Source"/>.</param>
<param name="GreenDest">Pointer to the image that will contain the green component. Must have same dimensions as <paramref name="Source"/>.</param>
<param name="BlueDest">Pointer to the image that will contain the blue component. Must have same dimensions as <paramref name="Source"/>.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:SplitImageIntoHSLChannels(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="508">
<summary>
Split the source image into component HSL channels.
</summary>
<param name="Source">Pointer to the image to split.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="AlphaDest">Pointer to the image that will contain the alpha component. If NULL, the alpha component is not split out. Must have same dimensions as <paramref name="Source"/>.</param>
<param name="HueDest">Pointer to the image that will contain the hue component. Must have same dimensions as <paramref name="Source"/>.</param>
<param name="SaturationDest">Pointer to the image that will contain the saturation component. Must have same dimensions as <paramref name="Source"/>.</param>
<param name="LuminanceDest">Pointer to the image that will contain the luminance component. Must have same dimensions as <paramref name="Source"/>.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:RollingMeanChannels2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="569">
<summary>
Calculate a running mean for the specified channels.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
<param name="AlphaWindowSize">Size of the alpha window. Size truncated for early pixel indices.</param>
<param name="RedWindowSize">Size of the red window. Size truncated for early pixel indices.</param>
<param name="GreenWindowSize">Size of the green window. Size truncated for early pixel indices.</param>
<param name="BlueWindowSize">Size of the blue window. Size truncated for early pixel indices.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:RollingMeanChannels(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="670">
<summary>
Calculate a running mean for all color channels (with the possible exception of the alpha channel).
</summary>
<remarks>
Calls RollingMeanChannels2 with parameters derived from a call to this function.
</remarks>
<param name="Source">Source image.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
<param name="WindowSize">Size of the rolling mean window. Size truncated for early pixels.</param>
<param name="IncludeAlpha">
Determines if alpha is included in the rolling mean. In general, set this parameter to TRUE if all alpha channel values
are 0xff. Doing this provides better performance.
</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:SortChannels2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="694">
<summary>
Sort the red, green, and blue channels of each pixel according to <paramref name="SortHow"/>. Result return in
<paramref name="Destination"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:SortChannels(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="750">
<summary>
Sort the red, green, and blue channels of each pixel according to <paramref name="SortHow"/>. Result return in
<paramref name="Destination"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the image.</param>
<param name="Height">Height of the image.</param>
<param name="Stride">Stride of the image.</param>
<param name="Destination">Destination image. This buffer must have the same size as the source buffer.</param>
<param name="SortHow">Determines sort order.</param>
<param name="StoreSortHowAsAlpha">If TRUE, the sort order is stored in the destination image as the alpha channel value.</param>
<param name="InvertAlpha">If TRUE, the alpha value is inverted.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:SepiaToneRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="854">
<summary>
Create a sepia toned version of <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the sepia image will be drawn.</param>
<param name="Left">Left coordinate of the operational region.</param>
<param name="Top">Top coordinate of the operational region.</param>
<param name="Right">Right coordinate of the operational region.</param>
<param name="Bottom">Bottom coordinate of the operational region.</param>
<param name="CopyOutOfRegion">
If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
</param>
<param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:SepiaTone(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="935">
<summary>
Create a sepia toned version of <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the sepia image will be drawn.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:ColorThreshold0(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.UInt32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="978">
<summary>
Generate a threshold image based on <paramref name="Source"/> with low, and high regions.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the adjusted image will be drawn.</param>
<param name="Threshold">Luminance value that determines which color will be used.</param>
<param name="PackedColor">Color used when the luminance is less than <paramref name="Threshold"/>.</param>
<param name="InvertThreshold">If TRUE, the threshold usage flag is inverted.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:ColorThreshold(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.UInt32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1038">
<summary>
Generate a threshold image based on <paramref name="Source"/> with low, and high regions.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the adjusted image will be drawn.</param>
<param name="Threshold">Luminance value that determines which color will be used.</param>
<param name="PackedLowColor">Color used when the luminance is below (or equal to) <paramref name="Threshold"/>.</param>
<param name="PackedHighColor">Color used when the luminance is above (or equal to) <paramref name="Threshold"/>.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:ColorThreshold2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.UInt32,System.Double,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1097">
<summary>
Generate a threshold image based on <paramref name="Source"/> with low, middle, and high regions where the middle region consists
of unmodified source data.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the adjusted image will be drawn.</param>
<param name="LowThreshold">Luminance value below which <paramref name="PackedLowColor"/> will be used in the destination image.</param>
<param name="PackedLowColor">Color used when the luminance is below (or equal to) <paramref name="LowThreshold"/>.</param>
<param name="HighThreshold">Luminance value above which <paramref name="PackedHighColor"/> will be used in the destination image.</param>
<param name="PackedHighColor">Color used when the luminance is above (or equal to) <paramref name="HighThreshold"/>.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:ColorThreshold3(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1185">
<summary>
Change colors in the image depending on the original pixel's luminance.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the final image will be drawn.</param>
<param name="ThresholdList">Pointer to a list of thresholds that determine which colors are drawn for which luminances.</param>
<param name="ListCount">Number of items in the threshold list.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:AutoSaturateRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1269">
<summary>
Auto adjust the saturation of <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the saturation adjusted image will be drawn.</param>
<param name="Saturation">Saturation value.</param>
<param name="Left">Left coordinate of the operational region.</param>
<param name="Top">Top coordinate of the operational region.</param>
<param name="Right">Right coordinate of the operational region.</param>
<param name="Bottom">Bottom coordinate of the operational region.</param>
<param name="CopyOutOfRegion">
If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
</param>
<param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:AutoSaturate(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1366">
<summary>
Auto adjust the saturation of <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the saturation adjusted image will be drawn.</param>
<param name="Saturation">Saturation value.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:AutoContrastRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1381">
<summary>
Auto adjust the contrast of <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the adjusted image will be drawn.</param>
<param name="Contrast">Contrast value.</param>
<param name="Left">Left coordinate of the operational region.</param>
<param name="Top">Top coordinate of the operational region.</param>
<param name="Right">Right coordinate of the operational region.</param>
<param name="Bottom">Bottom coordinate of the operational region.</param>
<param name="CopyOutOfRegion">
If TRUE, pixels from the source are copied to the destination if they are not in the operational region. If FALSE,
the color in <paramref name="PackedOut"/> is used for non-operational region pixels.
</param>
<param name="PackedOut">Packed color to use if <paramref name="CopyOutOfRegion"/> is TRUE.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:AutoContrast(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1481">
<summary>
Auto adjust the contrast of <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the adjusted image will be drawn.</param>
<param name="Contrast">Contrast value.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:BrightnessMapRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1496">
<summary>
Return a brightness (luminance) image derived from <paramref name="Source"/> for the given region.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the brightness will be drawn.</param>
<param name="Left">Left coordinate of the operational region.</param>
<param name="Top">Top coordinate of the operational region.</param>
<param name="Right">Right coordinate of the operational region.</param>
<param name="Bottom">Bottom coordinate of the operational region.</param>
<param name="CopyOutOfRegion">
If True, non-operational pixels will be copied to the destination. Otherwise, pixels of the color <paramref name="PackedOut"/>
will be copied.
</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:BrightnessMap(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1577">
<summary>
Return a brightness (luminance) image derived from <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the brightness will be drawn.</param>
<returns>Value indicating operational result.</returns>
</member>
<member name="M:ImageMeanColorRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.UInt32*,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1591">
<summary>
Return the mean color for the image pointed to by <paramref name="Source"/>.
</summary>
<param name="Source">Pointer to the image whose mean color will be returned.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="PackedMeanColor">On success, will contain the image's mean color in packed format.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:ImageMeanColor(System.Void*,System.Int32,System.Int32,System.Int32,System.UInt32*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1645">
<summary>
Return the mean color for the image pointed to by <paramref name="Source"/>.
</summary>
<param name="Source">Pointer to the image whose mean color will be returned.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="PackedMeanColor">On success, will contain the image's mean color in packed format.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:MeanImageColorRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1659">
<summary>
Return an image with the mean color derived from <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the mean color will be drawn.</param>
<param name="Left">Left coordinate of the operational region.</param>
<param name="Top">Top coordinate of the operational region.</param>
<param name="Right">Right coordinate of the operational region.</param>
<param name="Bottom">Bottom coordinate of the operational region.</param>
<param name="CopyOutOfRegion">
If True, non-operational pixels will be copied to the destination. Otherwise, pixels of the color <paramref name="PackedOut"/>
will be copied.
</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:MeanImageColor(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\imagemanipulation.cpp" line="1748">
<summary>
Return an image with the mean color derived from <paramref name="Source"/>.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Destination">Where the mean color will be drawn.</param>
<returns>Value indicating operational result.</returns>
</member>
<!-- Discarding badly formed XML document comment for member 'M:MeanImageColorValue(System.Void*,System.Int32,System.Int32,System.Int32)'. -->
<!-- Discarding badly formed XML document comment for member 'M:MeanImageColor2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.UInt32*)'. -->
</members>
</doc>