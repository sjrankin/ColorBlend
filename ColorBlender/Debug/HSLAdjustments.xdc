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
<member name="M:GetHSLImage(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="8">
<summary>
Convert the image in <paramref name="SourceBuffer"/> from RGB to HSL. HSL data are returned in
<paramref name="DoubleBuffer"/>.
</summary>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DoubleBuffer">
Buffer of doubles, three doubles (in HSL order) for each RGB pixel in <paramref name="SourceBuffer"/>.
</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:MakeRGBFromHSL(System.Void*,System.UInt32,System.Void*,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="52">
<summary>
Convert a buffer of double values (representing HSL colors) to a buffer of RGB values.
</summary>
<param name="HSLBuffer">Buffer of HSL double values, one double each for H, S, and L.</param>
<param name="DoubleCount">Number of doubles in <paramref name="HSLBuffer"/>.</param>
<param name="Destination">Where the RGB data will be placed.</param>
<param name="DestinationWidth">Width of the RGB image.</param>
<param name="DestinationHeight">Height of the RGB image.</param>
<param name="DestinationStride">Stride of the RGB image.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:AdjustImageHSLValues(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="103">
<summary>
Adust the HSL values of the image by the specified multipliers.
</summary>
<param name="SurfaceBuffer">Source image.</param>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
<param name="HMultiplier">Value to multiply the hue by.</param>
<param name="SMultiplier">Value to multiply the saturation by.</param>
<param name="LMultiplier">Value to multiply the luminance by.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:ImageHueShift(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="164">
<summary>
Shift the hue of each pixel in the image.
</summary>
<param name="SurfaceBuffer">Source image.</param>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
<param name="HueShiftValue">How much to shift the hue by.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:DoRestrict(System.Double,System.Double,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="207">
<summary>
Restict a value to a range.
</summary>
<param name="SourceValue">
The value to restrict. If the value is greater than <paramref name="MaxRange"/>, the value is
changed to <paramref name="MaxRange"/>.
</param>
<param name="MaxRange">The maximum value in the range.</param>
<param name="Segments">Number of segments to split <paramref name="MaxRange"/> into.</param>
<returns>Restricted value.</returns>.
</member>
<member name="M:RestrictHSL(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="229">
<summary>
Restrict various HSL channel values to certain ranges.
</summary>
<param name="SurfaceBuffer">Source image.</param>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
<param name="HueRangeSize">Determines how to restrict hue values. Should be an even divisor of 360.0.</param>
<param name="SaturationRangeSize">Determines how to restrice saturation values.</param>
<param name="LuminanceRangeSize">Determines how to restrice luminance values.</param>
<param name="RestrictHue">Determines if hues are restricted.</param>
<param name="RestrictSaturation">Determines if saturations are restricted.</param>
<param name="RestrictLuminance">Determines if luminances are restricted.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:Silly_SwapSaturationLuminance(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="294">
<summary>
Swap saturation and luminance values for each pixel.
</summary>
<param name="SurfaceBuffer">Source image.</param>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:RoundToClosest(System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="340">
<summary>
Round <paramref name="ToRound"/> to the nearest multiple of <paramref name="Multiple"/>.
</summary>
<param name="ToRound">The value to round.</param>
<param name="Multiple">Determines how to round off <paramref name="ToRound"/>.</param>
<returns>Rounded value.</returns>
</member>
<member name="M:HSLColorReduction(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Int32,System.Double,System.Int32,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="358">
<summary>
Swap saturation and luminance values for each pixel.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
<param name="HueRanges">Number of hue ranges to reduce the hue channel to.</param>
<param name="ReduceSaturation">Determines if saturation is reduced to a single value.</param>
<param name="SaturationValue">New saturation value if saturation is reduced.</param>
<param name="ReduceLuminance">Determines if luminance is reduced to a single value.</param>
<param name="LuminanceValue">New luminance value if luminance is reduced.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:HSLBulkSet(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Int32,System.Double,System.Int32,System.Double,System.Int32,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="432">
<summary>
Set all HSL values as specified.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
<param name="SetHue">Determines if the hue is changed.</param>
<param name="NewHue">New hue value.</param>
<param name="SetSaturation">Determines if the saturation is changed.</param>
<param name="NewSaturation">New saturation value.</param>
<param name="SetLuminance">Determines if the luminance is changed.</param>
<param name="NewLuminance">New luminance value.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="T:ConditionalHSLAdjustment" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="501">
<summary>
Instructions on how to conditionally change colors in HSL mode.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.RangeLow" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="506">
<summary>
Determines the hue low range for conditional changes.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.RangeHigh" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="510">
<summary>
Determines the hue high range for conditional changes.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.ModifyHue" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="514">
<summary>
Determines if the hue is modified.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.HueOperand" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="518">
<summary>
Operand to apply to the source hue value.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.HueOperation" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="522">
<summary>
Operation to apply the operand to the hue.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.ModifySaturation" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="526">
<summary>
Determines if the saturation is modified.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.SaturationOperand" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="530">
<summary>
Operand to apply to the source saturation value.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.SaturationOperation" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="534">
<summary>
Operation to apply the operand to the saturation.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.ModifyLuminance" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="538">
<summary>
Determines if the luminance is modified.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.LuminanceOperand" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="542">
<summary>
Operand to apply to the source luminance value.
</summary>
</member>
<member name="F:ConditionalHSLAdjustment.LuminanceOperation" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="546">
<summary>
Operation to apply the operand to the luminance.
</summary>
</member>
<member name="M:ModifyHSLValue(System.Double,System.Double,System.Int32,System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="552">
<summary>
Modify an HSL value in <paramref name="Source"/> according to <paramref name="Operator"/> and
<paramref name="Operand"/>.
</summary>
<param name="Source">Value to modify.</param>
<param name="Operand">Operand used to modify <paramref name="Source"/>.</param>
<param name="Operator">How to apply <paramref name="Operand"/> to <paramref name="Source"/>.</param>
<param name="ClampLow">Low clamping value.</param>
<param name="ClampHigh">High clamping value.</param>
<returns>Modified value.</returns>
</member>
<member name="M:HSLConditionalModify(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Void*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="599">
<summary>
Conditionally modify H, S, or L values based on the conditions in <paramref name="Conditions"/>. Whether
or not modification takes place depends on the hue matching the conditional range.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
<param name="Conditions">Pointer to an array of ConditionalHSLAdjustment structures.</param>
<param name="ConditionalCount">
Number of ConditionalHSLAdustment structures are in the array pointed to by
<paramref name="Conditions"/>.
</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:RGBtoHSLtoRGB(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="677">
<summary>
Convert each pixel in <pararef name="Source"/> from RGB to HSL and back to RGB.
</summary>
<param name="Source">Source image.</param>
<param name="Width">Width of the source image.</param>
<param name="Height">Height of the source image.</param>
<param name="Stride">Stride of the source image.</param>
<param name="Destination">Destination of the changes. Must be same dimensionally as <paramref name="Source"/>.</param>
<returns>Value indicating operational results.</returns>
</member>
<member name="M:RestrictHues2(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="868">
<summary>
Restrict various HSL channel values to certain ranges.
</summary>
<param name="SurfaceBuffer">Source image.</param>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
<param name="HueCount">Number of hues to restrict the image to.</param>
<returns>Value indicating operational success.</returns>
</member>
<member name="M:RestrictHueRange(System.Void*,System.Int32,System.Int32,System.Int32,System.Void*,System.Double,System.Double)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\hsladjustments.cpp" line="922">
<summary>
Restrict various HSL channel values to certain ranges.
</summary>
<param name="SurfaceBuffer">Source image.</param>
<param name="SourceWidth">Width of the source image.</param>
<param name="SourceHeight">Height of the source image.</param>
<param name="SourceStride">Stride of the source image.</param>
<param name="DestinationBuffer">Destination of the changes. Must be same dimensionally as <paramref name="SourceBuffer"/>.</param>
<param name="LowHue">The low hue range value.</param>
<param name="HighHue">the high hue range value.</param>
<returns>Value indicating operational success.</returns>
</member>
</members>
</doc>