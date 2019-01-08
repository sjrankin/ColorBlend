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
<member name="M:YCbCrtoRGB(System.Double,System.Double,System.Double,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="35">
<summary>
Converts a color in YCbCr color space to RGB color space.
</summary>
<param name="Y">The Y component.</param>
<param name="Cb">The Cb component.</param>
<param name="Cr">The Cr component.</param>
<param name="R">The converted red component.</param>
<param name="G">The converted green component.</param>
<param name="B">The converted blue component.</param>
</member>
<member name="M:RGBtoYCbCr(System.Double,System.Double,System.Double,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="51">
<summary>
Converts a color in the RGB color space to the YCbCr color space.
</summary>
<param name="R">The red component to convert.</param>
<param name="G">The green component to convert.</param>
<param name="B">The blue component to convert.</param>
<param name="Y">Will contain the Y component upon conversion.</param>
<param name="Cb">Will contain the Cb component upon conversion.</param>
<param name="Cr">Will contain the Cr component upon conversion.</param>
</member>
<member name="M:RGBtoHSL(System.Double,System.Double,System.Double,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="67">
<summary>
Convert a color in the RGB color space to the equivalent color in the HSL color space.
</summary>
<param name="R">The red component to convert.</param>
<param name="G">The green component to convert.</param>
<param name="B">The blue component to convert.</param>
<param name="H">On return will contain the hue value.</param>
<param name="S">On return will contain the saturation value.</param>
<param name="L">On return will contain the lumaninance value.</param>
</member>
<member name="M:RGBtoHSL2(System.Byte,System.Byte,System.Byte,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="111">
<summary>
Convert a color in the RGB color space to the equivalent color in the HSL color space.
</summary>
<param name="R">The red component (byte format) to convert.</param>
<param name="G">The green component (byte format) to convert.</param>
<param name="B">The blue component (byte format) to convert.</param>
<param name="H">On return will contain the hue value.</param>
<param name="S">On return will contain the saturation value.</param>
<param name="L">On return will contain the lumaninance value.</param>
</member>
<member name="M:HSLtoRGB(System.Double,System.Double,System.Double,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="137">
<summary>
Converts a color in the HSL color space to the equivalent in the RGB color space.
</summary>
<param name="H">The H component to convert.</param>
<param name="S">The S component to convert.</param>
<param name="L">The L component to convert.</param>
<param name="R">Will contain the red component.</param>
<param name="G">Will contain the green component.</param>
<param name="B">Will contain the blue component.</param>
</member>
<member name="M:HSLtoRGB2(System.Double,System.Double,System.Double,System.Byte*,System.Byte*,System.Byte*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="194">
<summary>
Converts a color in the HSL color space to the equivalent in the RGB color space.
</summary>
<param name="H">The H component to convert.</param>
<param name="S">The S component to convert.</param>
<param name="L">The L component to convert.</param>
<param name="R">Will contain the red component in byte format.</param>
<param name="G">Will contain the green component in byte format.</param>
<param name="B">Will contain the blue component in byte format.</param>
</member>
<member name="M:GetPixelLuminance(System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="223">
<summary>
Return the luminance of the pixel with the passed colors.
</summary>
<remarks>
Converts the RGB color to HSL and returns the Luminance value.
</remarks>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<returns>Luminance of the supplied RGB color.</returns>
</member>
<member name="M:GetPixelSaturation(System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="240">
<summary>
Return the saturation of the pixel with the passed colors.
</summary>
<remarks>
Converts the RGB color to HSL and returns the Saturation value.
</remarks>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<returns>Saturation of the supplied RGB color.</returns>
</member>
<member name="M:GetPixelHue(System.Byte,System.Byte,System.Byte)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="257">
<summary>
Return the hue of the pixel with the passed colors.
</summary>
<remarks>
Converts the RGB color to HSL and returns the Hue value.
</remarks>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<returns>Hue of the supplied RGB color.</returns>
</member>
<member name="M:AdjustHue(System.Byte*,System.Byte*,System.Byte*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="274">
<summary>
Adjust the hue of the supplied RGB color.
</summary>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<param name="Angle">Angle to be added to the hue.</param>
</member>
<member name="M:ChangeHue(System.Byte*,System.Byte*,System.Byte*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="298">
<summary>
Change the hue of the supplied RGB color.
</summary>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<param name="Angle">Angle to be added to the hue.</param>
</member>
<member name="M:ShiftHue(System.Byte*,System.Byte*,System.Byte*,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="332">
<summary>
Adjust the hue of the specified color by adding the supplied offset.
</summary>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<param name="AngleOffset">Offset to be added to the hue.</param>
</member>
<member name="M:RGBtoHSV(System.Double,System.Double,System.Double,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="356">
<summary>
Convert a color in the RGB color space to the equivalent in the HSV color space.
</summary>
<param name="R">The red component to convert. Value ranges from 0.0 to 1.0.</param>
<param name="G">The green component to convert. Value ranges from 0.0 to 1.0.</param>
<param name="B">The blue component to convert. Value ranges from 0.0 to 1.0.</param>
<param name="H">Will contain the H component. Value will range from 0.0 to 360.0.</param>
<param name="S">Will contain the S component. Value will range from 0.0 to 1.0.</param>
<param name="V">Will contain the V component. Value will range from 0.0 to 1.0.</param>
</member>
<member name="M:HSVtoRGB(System.Double,System.Double,System.Double,System.Double*,System.Double*,System.Double*)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="392">
<summary>
Convert a color in the HSV color space to the equivalent in the RGB color space.
</summary>
<remarks>
http://www.cs.rit.edu/~ncs/color/t_convert.html
</remarks>
<param name="H">The H component to convert. Valid range: 0 - 360.</param>
<param name="S">The S component to convert. Valid range: 0.0 - 1.0.</param>
<param name="V">The V component to convert. Valid range: 0.0 - 1.0.</param>
<param name="R">Will contain the R value.</param>
<param name="G">Will contain the G value.</param>
<param name="B">Will contain the B value.</param>
</member>
<member name="M:ColorLuminance2(System.Byte,System.Byte,System.Byte,System.Int32)" decl="false" source="c:\users\stuart\desktop\projects4\colorblend\colorblender\colors.cpp" line="943">
<summary>
Calculate the luminance of the passed color.
</summary>
<param name="R">Red channel value.</param>
<param name="G">Green channel value.</param>
<param name="B">Blue channel value.</param>
<param name="Perceived">Determines if perceptual or objective luminance is calculated.</param>
<returns>Luminance of the passed color.</returns>
</member>
</members>
</doc>