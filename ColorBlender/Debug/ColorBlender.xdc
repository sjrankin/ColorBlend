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
<member name="M:DoNothing" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="14">
<summary>
Expensive version of a NOP.
</summary>
</member>
<member name="M:PixelLuminance(System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="21">
<summary>
Calculate and return a brightness/luminance value for the passed color channel values.
</summary>
<param name="R">The red channel value.</param>
<param name="G">The green channel value.</param>
<param name="B">The blue channel value.</param>
<returns>Luninance of the passed color channels</returns>
</member>
<member name="M:PixelLuminanceSc(System.Double,System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="33">
<summary>
Calculate and return a brightness/luminance value for the passed color channel values.
</summary>
<param name="R">The normalized red channel value.</param>
<param name="G">The normalized green channel value.</param>
<param name="B">The normalized blue channel value.</param>
<returns>Luninance of the passed color channels</returns>
</member>
<member name="M:ColorLuminance(System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="45">
<summary>
Calculate and return a brightness/luminance value for the passed color channel values. This function is exposed to
managed programs but uses the same function as do other functions - that way the same algorithm is used for everything.
</summary>
<param name="R">The red channel value.</param>
<param name="G">The green channel value.</param>
<param name="B">The blue channel value.</param>
<returns>Luninance of the passed color channels</returns>
</member>
<member name="M:ColorLuminanceSc(System.Double,System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="62">
<summary>
Calculate and return a brightness/luminance value for the passed color channel values. This function is exposed to
managed programs but uses the same function as do other functions - that way the same algorithm is used for everything.
</summary>
<param name="R">The normalized red channel value.</param>
<param name="G">The normalized green channel value.</param>
<param name="B">The normalzied blue channel value.</param>
<returns>Luninance of the passed color channels</returns>
</member>
<member name="M:delta(System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="75">
<summary>
Return the delta between the two parameters.
</summary>
<param name="Op1">First operand.</param>
<param name="Op2">Second operation.</param>
<returns>The delta between the two operands.</returns>
</member>
<member name="M:Distance(System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="86">
<summary>
Calculates the distance between (X1,Y1) and (X2,Y2).
</summary>
<param name="X1">First horizontal coordinate.</param>
<param name="Y1">First vertical coordinate.</param>
<param name="X2">Second horizontal coordinate.</param>
<param name="Y2">Second vertical coordinate.</param>
<returns>The distance between (X1,Y1) and (X2,Y2).</returns>
</member>
<member name="M:ColorPointIndex(System.Int32,System.Int32,AbsolutePointStruct*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="99">
<summary>
Determines if (X,Y) is a pure color point.
</summary>
<param name="X">The horizontal coordinate.</param>
<param name="Y">The vertical coordinate.</param>
<param name="Points">Array pointer to the set of pure color points.</param>
<param name="PointCount">Number of pure color points in Points.</param>
<returns>The index of the pure color at (X,Y) if at a pure color point, -1 if not at a pure color point.</returns>
</member>
<member name="M:BlendColors(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="117">
<summary>
Given a set of colors and their locations, create data that can be used in an Image (BGRA32 format) that has all of the
colors blended appropriately.
</summary>
<param name="Target">The location where the colors will be placed - must be allocated prior to calling this function.</param>
<param name="Width">The width of the image/target buffer.</param>
<param name="Height">The height of the image/target buffer.</param>
<param name="Stride">The stride of the image/target buffer.</param>
<param name="PureColorCount">The number of colors used as primary colors for blending.</param>
<param name="ColorLocations">
Pointer to an array of AbsolutePointStructs that determines the location of the pure colors. No error checking is done.
</param>
<param name="PureColors">Pointer to an array of PureColorStructs that contain the primary colors from which the blending is generated.</param>
<returns>TRUE on success, FALSE on parametric fail.</returns>
</member>
<member name="F:Alpha" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="190">
<summary>
The alpha channel.
</summary>
</member>
<member name="F:Red" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="194">
<summary>
The red channel.
</summary>
</member>
<member name="F:Green" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="198">
<summary>
The green channel.
</summary>
</member>
<member name="F:Blue" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="202">
<summary>
The blue channel.
</summary>
</member>
<member name="T:ChannelIDs" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="181">
<summary>
IDs of the various channels in an ARGB pixel.
</summary>
<remarks>
The values assigned to the enums are used to shift the source pixel to the proper location in order to
extract the channel as efficiently as possible (e.g., in a calculation, not with a bunch of IF statements).
</remarks>
</member>
<member name="M:GetChannel(System.Int32,ChannelIDs)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="208">
<summary>
Extract and return the specified channel value from <paramref name="AllChannels"/>.
</summary>
<param name="AllChannels">The source for channel information. Must be in the form of AARRGGBB.</param>
<param name="ChannelID">Determines which channel is extracted and returned.</param>
<returns>The specified channel from <paramref name="AllChannels"/>.</returns>
</member>
<member name="M:MergeChannel(System.Int32,System.Byte,ChannelIDs)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="219">
<summary>
Merges a given channel's value into the source value and returns the results.
</summary>
<remarks>
This function does not clear channels before merging them so it is best to start with a <paramref name="Source"/> value of 0x0.
</remarks>
<param name="Source">
The pixel value where <paramref name="ChannelValue"/> will be merged. For the purposes of this function, it is
assumed that the format of this parameter is AARRGGBB.
</param>
<param name="ChannelValue">The value to merge with the source, e.g., the channel value.</param>
<param name="ChannelID">Determines which channel is being merged (the location of the merge) into <paramref name="Source"/>.</param>
<returns><paramref name="Source"/> merged with <paramref name="ChannelValue"/>.</returns>
</member>
<member name="M:SetPixel(System.Byte*,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="237">
<summary>
Sets one pixel.
</summary>
<param name="Buffer">The start of the buffer where the pixel will be set.</param>
<param name="Index">The offset into the buffer where to set the pixel values.</param>
<param name="A">The alpha value.</param>
<param name="R">The red value.</param>
<param name="G">The green value.</param>
<param name="B">The blue value.</param>
</member>
<member name="M:InverseChannelMask(ChannelIDs)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="254">
<summary>
Return an inverse mask for the given channel. Assumes pixel structure of AARRGGBB.
</summary>
<returns>Inverse mask for the specified channel.</returns>
</member>
<member name="M:SetChannel(System.Int32,System.Byte,ChannelIDs)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="279">
<summary>
Clears then sets the specified channel to the specified channel value.
</summary>
<param name="Source">The source pixel value.</param>
<param name="ChannelValue">The value to set - replaces old value.</param>
<param name="ChannelID">Determines the location of the channel.</param>
<returns>New pixel value with the appropriate channel cleared then set with passed data.</returns>
</member>
<member name="M:ColorPercent2(System.Double,System.Byte,System.Byte,System.Byte,System.Byte,System.Byte*,System.Byte*,System.Byte*,System.Byte*,System.Double,System.Double,System.Boolean)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="291">
<summary>
Returns a percentage of a color.
</summary>
<param name="Percent">The precent of the returned color.</param>
<param name="inA">Alpha channel input. Not currently used.</param>
<param name="inR">Red channel input.</param>
<param name="inG">Green channel input.</param>
<param name="inB">Blue channel input.</param>
<param name="outA">Alpha channel output.</param>
<param name="outR">Red channel output.</param>
<param name="outG">Green channel output.</param>
<param name="outB">Blue channel output.</param>
<param name="AlphaStart">Starting (at the pure color point location) alpha level.</param>
<param name="AlphaEnd">Ending alpha level.</param>
<param name="UseAlpha">Determines if alpha calculations are made. If not, alpha is always 0xff.</param>
</member>
<member name="M:MakeColor2(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,PureColorType*,System.Byte*,System.Byte*,System.Byte*,System.Byte*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="347">
<summary>
Create the blended color at the specified point in the buffer.
</summary>
<param name="Target">The location where the colors will be placed - must be allocated prior to calling this function.</param>
<param name="Width">The width of the image/target buffer.</param>
<param name="Height">The height of the image/target buffer.</param>
<param name="PurePointCount">Number of pure colors in the set of pure colors.</param>
<param name="Colors">Points to an array of pure colors.</param>
<param name="FinalA">Pointer to the final alpha level.</param>
<param name="FinalR">Pointer to the final red level.</param>
<param name="FinalG">Pointer to the final green level.</param>
<param name="FinalB">Pointer to the final blue level.</param>
<returns>TRUE on success, FALSE on parametric fail.</returns>
</member>
<member name="M:ColorPointIndex2(System.Int32,System.Int32,PureColorType*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="411">
<summary>
Determines if the point X,Y is a pure color point.
</summary>
<param name="X">Horizontal location of the point to test.</param>
<param name="Y">Vertical location of the point to test.</param>
<param name="ColorSet">Pointer to the set of pure colors.</param>
<param name="PurePointCount">Number of pure colors in ColorSet.</param>
<returns>The index of the pure color at X,Y, -1 if not a pure color point.</returns>
</member>
<member name="M:DrawPointIndicator(System.Byte*,System.Int32,System.Int32,System.Int32,System.Int32,PureColorType*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="429">
<summary>
Draw a block/point in <paramref name="Buffer"/>.
</summary>
<param name="Buffer">The buffer where the line will be drawn.</param>
<param name="Width">The width of the buffer.</param>
<param name="Height">The height of the buffer.</param>
<param name="Stride">The stride of the buffer.</param>
<param name="ColorIndex">Determines where and what color the line will be.</param>
<param name="ColorSet">Source for line location and color.</param>
</member>
<member name="M:BlendColors2(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="476">
<summary>
Given a set of colors and their locations, create data that can be used in an Image (BGRA32 format) that has all of the
colors blended appropriately.
</summary>
<param name="Target">The location where the colors will be placed - must be allocated prior to calling this function.</param>
<param name="Width">The width of the image/target buffer.</param>
<param name="Height">The height of the image/target buffer.</param>
<param name="Stride">The stride of the image/target buffer.</param>
<param name="PureColorCount">The number of colors used as primary colors for blending.</param>
<param name="PureColors">
Pointer to an array of PureColorTypes that contain the primary colors and locations from which the 
blending is generated/calculated.
</param>
<returns>TRUE on success, FALSE on parametric fail.</returns>
</member>
<member name="M:MakeDotIndices(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,AbsolutePointStruct*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="542">
<summary>
Return a set of points given the top,left and bottom,right coordinates.
</summary>
<param name="Left">The left side of the region.</param>
<param name="Top">The top of the region.</param>
<param name="Right">The right side of the region.</param>
<param name="Bottom">The bottom of the region.</param>
<param name="Width">The width of the buffer.</param>
<param name="Height">The height of the buffer.</param>
<param name="Stride">The stride of the buffer.</param>
<param name="PointList">Pointer to an array of points generated by this function.</param>
<param name="DotCount">The maximum number of dots given the region defintion.</param>
<returns>Quantity of points actually generated.</returns>
</member>
<member name="M:PointInPlane(System.Int32,System.Int32,PlaneSetStruct*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="582">
<summary>
Determines if the logical point (<paramref name="X"/>,<paramref name="Y"/>) is in <paramref name="Plane"/>.
</summary>
<param name="X">Logical horizontal coordinate.</param>
<param name="Y">Logical vertical coordinate.</param>
<param name="Plane">The plane that will be tested.</param>
<returns>TRUE if (<paramref name="X"/>,<paramref name="Y"/>) is in the plane, FALSE if not.</returns>
</member>
<member name="M:MergePlanes(System.Void*,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="600">
<summary>
Merges a set of sub-planes into the final plane. Assumes that all sub-planes have had their coordinates validated and
corrected as necessary.
</summary>
<remarks>
This function assumes all planes have had their coordinates normalized to all non-negative values.
</remarks>
<param name="Target">The target buffer where there planes will be merged to.</param>
<param name="PlaneSet">Array of planes that will be merged.</param>
<param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
<param name="Width">Width of the target buffer.</param>
<param name="Height">Height of the target buffer.</param>
<param name="Stride">Stride of the target buffer.</param>
<returns>TRUE on success, FALSE on error.</returns>
</member>
<member name="M:MergePlanes2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="662">
<summary>
Merge a set of color blobs (in <paramref name="PlaneSet"/>) to <paramref name="Target"/>.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="PlaneSet">The list of color blobs to merge to <paramref name="Target"/>.</param>
<param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
<returns>Operational success indicator.</returns>
</member>
<member name="M:MergePlanes4(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="764">
<summary>
Merge a set of color blobs (in <paramref name="PlaneSet"/>) to <paramref name="Target"/>. This is a composite action - the
order of the planes in the list is relevant - first items are composited first. The background in <paramref name="Target"/>
will be merged with the planes and is assumed to be drawn before calling this function.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="PlaneSet">The list of color blobs to merge to <paramref name="Target"/>.</param>
<param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
<returns>Operational success indicator.</returns>
</member>
<member name="M:MergePlanes3(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="862">
<summary>
Merge a set of color blobs (in <paramref name="PlaneSet"/>) to <paramref name="Target"/>.
</summary>
<param name="Target">Where the drawing will be done.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="PlaneSet">The list of color blobs to merge to <paramref name="Target"/>.</param>
<param name="PlaneCount">Number of planes in <paramref name="PlaneSet"/>.</param>
<returns>Operational success indicator.</returns>
</member>
<member name="M:CreateBitMask(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="916">
<summary>
Create a bit mask.
</summary>
<param name="Target">The buffer of bytes that make up the bit mask.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="Left">The left coordinate of the start of the masked region.</param>
<param name="Top">The top coordinate of the start of the masked region.</param>
<param name="Width">The width of the masked region.</param>
<param name="Height">The height of the masked region.</param>
<param name="BitOnValue">The value written to the masked region.</param>
<param name="BitOffValue">The value written to the unmasked region.</param>
</member>
<member name="M:CreateMask(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.UInt32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="957">
<summary>
Create a mask from image data in <paramref name="ImageSource"/> and return the masked image in <paramref name="Target"/>.
</summary>
<param name="Target">Will contain the masked image on success.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="ImageSource">
The source image that determines the resultant mask. Must have the same dimensions and stride as <paramref name="Target"/>.
</param>
<param name="Threshold">
Determines if a pixel from <paramref name="ImageSource"/> or the mask values is written to <paramref name="Target"/>. If any
color channel value is less than the corresponding color channel value in this parameter, the mask value is written. This parameter
is in BGRA format.
</param>
<param name="AlphaToo">Determines if alpha values are used in determination of target mask values.</param>
<param name="MaskA">
The alpha value to write if the source pixel is less than <paramref name="Threshold"/>. If <paramref name="AlphaToo"/> is false,
0x0 will be used as the alpha value.
</param>
<param name="MaskR">The red channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
<param name="MaskG">The green channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
<param name="MaskB">The blue channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
<returns>True on success, false on error.</returns>
</member>
<member name="M:CreateMaskFromLuminance(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1032">
<summary>
Create a mask from image data in <paramref name="ImageSource"/> and return the masked image in <paramref name="Target"/>.
</summary>
<param name="Target">Will contain the masked image on success.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="ImageSource">
The source image that determines the resultant mask. Must have the same dimensions and stride as <paramref name="Target"/>.
</param>
<param name="Threshold">
The luninance threshold. Source pixels with a luminance less than this value will not be included in the returned buffer.
</param>
<param name="AlphaToo">Determines if alpha values are used in determination of target mask values.</param>
<param name="MaskA">
The alpha value to write if the source pixel is less than <paramref name="Threshold"/>. If <paramref name="AlphaToo"/> is false,
0x0 will be used as the alpha value.
</param>
<param name="MaskR">The red channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
<param name="MaskG">The green channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
<param name="MaskB">The blue channel value to write if the source pixel is less than <paramref name="Threshold"/>.</param>
<returns>True on success, false on error.</returns>
</member>
<member name="M:CreateAlphaMaskFromLuminance(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Int32,System.UInt32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1097">
<summary>
Create a mask based on the luminance of a given pixel. Masked pixel alpha value not changed.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source and destination images.</param>
<param name="Height">Height of the source and destination images.</param>
<param name="Stride">Stride of the source and destination images.</param>
<param name="Luminance">Determines if a pixel is part of the mask or not. If not, the pixel is saved as transparent in the destination.</param>
<param name="UseMaskedPixel">
If TRUE, <paramref name="MaskedPixelColor"/> is used for pixels that meet the luminance requirement, otherwise
the original pixel is used.
</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:ClearBuffer(System.Void*,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte,System.Int32,System.Int32,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1162">
<summary>
Clears the specified buffer with the specified color value. Optionally draws a grid.
</summary>
<param name="Target">The buffer to clear.</param>
<param name="TargetWidth">Width of the buffer where drawing will be done.</param>
<param name="TargetHeight">Height of the buffer where drawing will be done.</param>
<param name="TargetStride">Stride of the buffer where drawing will be done.</param>
<param name="ClearA">The alpha channel value for a cleared color.</param>
<param name="ClearR">The red channel value for a cleared color.</param>
<param name="ClearG">The green channel value for a cleared color.</param>
<param name="ClearB">The blue channel value for a cleared color.</param>
<param name="DrawGrid">Determines if a grid is drawn over the cleared buffer.</param>
<param name="GridA">The alpha channel value the grid color.</param>
<param name="GridR">The red channel value the grid color.</param>
<param name="GridG">The green channel value the grid color.</param>
<param name="GridB">The blue channel value the grid color.</param>
<param name="GridCellWidth">Horizontal distance between grid lines.</param>
<param name="GridCellHeight">Vertical distance between grid lines.</param>
<returns>True on success, false on error.</returns>
</member>
<member name="M:CropBuffer(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1304">
<summary>
Crops the source buffer as per the size in the region and returns the result in the target buffer. The cropped region will
be aligned to the upper-left corder in the target buffer on completion.
</summary>
<param name="Target">Will contain the cropped buffer.</param>
<param name="TargetWidth">The width of the target buffer.</param>
<param name="TargetHeight">The height of the target buffer.</param>
<param name="TargetStride">The stride of the target buffer.</param>
<param name="Source">The source buffer.</param>
<param name="SourceWidth">The width of the source buffer.</param>
<param name="SourceHeight">The height of the source buffer.</param>
<param name="SourceStride">The stride of the source buffer.</param>
<param name="RegionPtr">
Determines the final region after the crop. This function assumes the values in Region have been validated by the caller.
</param>
<returns>TRUE on success, FALSE on failure.</returns>
</member>
<member name="M:SetAlpha(System.Void*,System.Int32,System.Int32,System.Int32,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1359">
<summary>
Sets the alpha level of all pixels in <paramref name="Target"/> to <paramref name="NewAlpha"/>.
</summary>
<param name="Target">The buffer where the alpha levels will be set.</param>
<param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
<param name="TargetHeight">The height of the target in scanlines.</param>
<param name="TargetStride">The stride of the target.</param>
<param name="NewAlpha">The new alpha value.</param>
<returns>Value that indicates the result of the operation.</returns>
</member>
<member name="M:SetAlphaByBrightness(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1389">
<summary>
Sets the alpha level of all pixels in <paramref name="Target"/> to <paramref name="NewAlpha"/>.
</summary>
<param name="Target">The buffer where the alpha levels will be set.</param>
<param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
<param name="TargetHeight">The height of the target in scanlines.</param>
<param name="TargetStride">The stride of the target.</param>
<param name="Invert">Determines if the brightness ratio is inverted.</param>
<param name="UseExistingAlpha">If true, the current alpha level is used as the base, if false, 0xff is used as the base.</param>
<returns>Value that indicates the result of the operation.</returns>
</member>
<member name="M:ApplyChannelMasks2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Byte,System.Int32,System.Byte,System.Int32,System.Byte,System.Int32,System.Byte,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1438">
<summary>
Apply a mask to each specified channel in <paramref name="Source"/> and save the result to <paramref name="Destination"/>.
</summary>
<param name="Source">Source image that will be manipulated.</param>
<param name="Width">Width of the source and destination buffers.</param>
<param name="Height">Height of the source and destination buffers.</param>
<param name="Stride">Stride of the source and destination buffers.</param>
<param name="LogicalOperator">Determines the logical operation to apply.</param>
<param name="AlphaMask">The mask to apply to the alpha channel.</param>
<param name="UseAlpha">If TRUE, <paramref name="AlphaMask"/> is applied to the alpha channel. If not, alpha is not modified.</param>
<param name="RedMask">The mask to apply to the red channel.</param>
<param name="UseRed">If TRUE, <paramref name="RedMask"/> is applied to the red channel. If not, red is not modified.</param>
<param name="GreenMask">The mask to apply to the green channel.</param>
<param name="UseGreen">If TRUE, <paramref name="GreenMask"/> is applied to the green channel. If not, green is not modified.</param>
<param name="BlueMask">The mask to apply to the blue channel.</param>
<param name="UseBlue">If TRUE, <paramref name="BlueMask"/> is applied to the blue channel. If not, blue is not modified.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:ApplyChannelMasks(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Byte,System.Byte,System.Byte,System.Byte,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1527">
<summary>
Apply a mask to each specified channel in <paramref name="Source"/> and save the result to <paramref name="Destination"/>.
The same mask is applied to each channel.
</summary>
<param name="Source">Source image that will be manipulated.</param>
<param name="Width">Width of the source and destination buffers.</param>
<param name="Height">Height of the source and destination buffers.</param>
<param name="Stride">Stride of the source and destination buffers.</param>
<param name="LogicalOperator">Determines the logical operation to apply.</param>
<param name="AlphaMask">The mask to apply to the alpha channel.</param>
<param name="RedMask">The mask to apply to the red channel.</param>
<param name="GreenMask">The mask to apply to the green channel.</param>
<param name="BlueMask">The mask to apply to the blue channel.</param>
<param name="IncludeAlpha">If TRUE, alpha is modified with the mask, otherwise, it is left alone.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:PixelChannelRollingLogicalOperation(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Byte,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1553">
<summary>
Perform rolling logical operations on channels inside given pixels.
</summary>
<param name="Source">Source image that will be manipulated.</param>
<param name="Width">Width of the source and destination buffers.</param>
<param name="Height">Height of the source and destination buffers.</param>
<param name="Stride">Stride of the source and destination buffers.</param>
<param name="LogicalOperator">Determines the logical operation to apply.</param>
<param name="RightToLeft">Determines the channel order. If TRUE, channel order is ARGB, otherwise the channel order is BGRA.</param>
<param name="Mask">Mask value applied to keep some bits unchanged.</param>
<param name="IncludeAlpha">If TRUE, alpha is manipulated as well.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:BufferInverter4(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Byte,System.Int32,System.Byte,System.Int32,System.Byte,System.Int32,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1713">
<summary>
Does a variable color inversion of an image.
</summary>
<param name="Source">Source image to invert.</param>
<param name="Width">Width of the source and destination buffers.</param>
<param name="Height">Height of the source and destination buffers.</param>
<param name="Stride">Stride of the source and destination buffers.</param>
<param name="Destination">Where the new image will be written.</param>
<param name="InvertAlpha">If TRUE, the alpha channel is always inverted.</param>
<param name="InvertRed">If TRUE, the red channel is always inverted.</param>
<param name="InvertGreen">If TRUE, the green channel is always inverted.</param>
<param name="InvertBlue">If TRUE, the blue channel is always inverted.</param>
<param name="UseAlphaThreshold">
If TRUE (and if <paramref name="InvertAlpha"/> is TRUE), the alpha channel will be inverted if
the source alpha channel value is greater than <paramref name="AlphaThreshold"/>.
</param>
<param name="AlphaThreshold">The value that determines if the alpha channel is inverted if <paramref name="UseAlphaThreshold"/> is TRUE.</param>
<param name="UseRedThreshold">
If TRUE (and if <paramref name="InvertRed"/> is TRUE), the red channel will be inverted if
the source red channel value is greater than <paramref name="RedThreshold"/>.
</param>
<param name="RedThreshold">The value that determines if the red channel is inverted if <paramref name="UseRedThreshold"/> is TRUE.</param>
<param name="UseGreenThreshold">
If TRUE (and if <paramref name="InvertGreen"/> is TRUE), the green channel will be inverted if
the green red channel value is greater than <paramref name="GreenThreshold"/>.
</param>
<param name="GreenThreshold">The value that determines if the green channel is inverted if <paramref name="UseGreenThreshold"/> is TRUE.</param>
<param name="UseBlueThreshold">
If TRUE (and if <paramref name="InvertBlue"/> is TRUE), the blue channel will be inverted if
the source blue channel value is greater than <paramref name="BlueThreshold"/>.
</param>
<param name="BlueThreshold">The value that determines if the blue channel is inverted if <paramref name="UseBlueThreshold"/> is TRUE.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:BufferInverter3(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1820">
<summary>
Does a simple color inversion of an image.
</summary>
<param name="Source">Source image to invert.</param>
<param name="Width">Width of the source and destination buffers.</param>
<param name="Height">Height of the source and destination buffers.</param>
<param name="Stride">Stride of the source and destination buffers.</param>
<param name="Destination">Where the new image will be written.</param>
<param name="InvertAlpha">If TRUE, the alpha channel is inverted as well.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:BufferInverter2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Double,System.Boolean,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1865">
<summary>
Invert the pixels in the buffer.
</summary>
<param name="Target">The buffer whose pixels will be inverted.</param>
<param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
<param name="TargetHeight">The height of the target in scanlines.</param>
<param name="TargetStride">The stride of the target.</param>
<param name="InversionOperation">The type of inversion.</param>
<param name="LuminanceThreshold">If a pixel's luminance is greater than this value, the pixel will be inverted.</param>
<param name="InvertThreshold">If true, <paramref name="LuminanceThreshold"/> will be inverted prior to use.</param>
<param name="AllowInvertAlpha">Determines if alpha is inverted.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:BufferInverter(System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Double,System.Boolean,System.Boolean,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="1951">
<summary>
Invert the pixels in the buffer.
</summary>
<param name="Target">The buffer whose pixels will be inverted.</param>
<param name="TargetWidth">The width of the target in pixels. Each pixel is four bytes wide.</param>
<param name="TargetHeight">The height of the target in scanlines.</param>
<param name="TargetStride">The stride of the target.</param>
<param name="InversionOperation">The type of inversion.</param>
<param name="LuminanceThreshold">If a pixel's luminance is greater than this value, the pixel will be inverted.</param>
<param name="InvertThreshold">If true, <paramref name="LuminanceThreshold"/> will be inverted prior to use.</param>
<param name="AllowInvertAlpha">Determines if alpha is inverted.</param>
<returns>Value indicating result of operation.</returns>
</member>
<member name="M:CropBuffer2(System.Void*,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="2034">
<summary>
Crop <paramref name="SourceBuffer"/> with the supplied region and place the result in <paramref name="DestinationBuffer"/>.
</summary>
<param name="SourceBuffer">Contains the source to be cropped. This buffer is not changed.</param>
<param name="DestinationBuffer">
Will contain the cropped part of <paramref name="SourceBuffer"/> on exit. The caller must create this buffer which must
be the proper size.
</param>
<param name="BufferWidth">Width of <paramref name="SourceBuffer"/> in pixels.</param>
<param name="BufferHeight">Height of <paramref name="SourceBuffer"/> in scan lines.</param>
<param name="BufferStride">Width of <paramref name="SourceBuffer"/> in bytes.</param>
<param name="X1">Left side of the crop region.</param>
<param name="Y1">Top of the crop region.</param>
<param name="X2">Right side of the crop region.</param>
<param name="Y2">Bottom of the crop region.</param>
<returns>Value indicating success.</returns>
</member>
<member name="M:CopySubRegion(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colorblender.cpp" line="2116">
<summary>
Copy part of an image in <paramref name="Source"/> to <paramref name="Destination"/>.
</summary>
<param name="Source">Source of the region to copy.</param>
<param name="SourceWidth">Width of the source.</param>
<param name="SourceHeight">Height of the source.</param>
<param name="SourceStride">Stride of the source.</param>
<param name="Destination">Where the copy will be placed. Must be large enough to hold the copy.</param>
<param name="DestinationWidth">Width of the destination.</param>
<param name="DestinationHeight">Height of the destination.</param>
<param name="DestinationStride">Stride of the destination.</param>
<param name="X1">
Horizontal coordinate in <paramref name="Source"/> where copying will start. Width of the sub-region to copy
is based on <paramref name="DestinationWidth"/>.
</param>
<param name="Y1">
Vertical coordinate in <paramref name="Source"/> where copying will start. Height of the sub-region to copy
is based on <paramref name="DestinationHeight"/>.
</param>
<returns>Value indication operational results.</returns>
</member>
</members>
</doc>