using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Iro3.Data.ColorSpaces;

namespace Iro3.Controls.ColorInput
{
    //Event-related code.
    public partial class SimpleColor
    {
        /// <summary>
        /// Handle key presses in the text box. Specifically, we look for return and escape.
        /// Return: Take the contents of the text box and try to make a color out of it. Update as appropriate.
        /// Escape: Functions as an undo key.
        /// </summary>
        /// <param name="Sender">The TextBox where the event occurred.</param>
        /// <param name="e">Event Data.</param>
        private void HandleInputKeyPress (object Sender, KeyEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                Color NewColor = Colors.Transparent;
                bool OK = IroColorSpace.ParseColorInput(TB.Text, ColorSpace, out NewColor);
                if (!OK)
                    return;
                CurrentColor = NewColor;
            }
            if (e.Key == Key.Escape)
            {
                TB.Text = ToString();
                e.Handled = true;
            }
        }

        /// <summary>
        /// The color input text box lost focus. Take the contents of the text box and try to make a color out of them.
        /// </summary>
        /// <param name="Sender">The text box that lost focus.</param>
        /// <param name="e">Event data - not used.</param>
        private void HandleInputLostFocus (object Sender, RoutedEventArgs e)
        {
            TextBox TB = Sender as TextBox;
            if (TB == null)
                return;
            Color NewColor = Colors.Transparent;
            bool ParsedOK = TryParse(TB.Text, out NewColor);
            if (!ParsedOK)
                return;
            CurrentColor = NewColor;
        }

        /// <summary>
        /// Notify all subscribers of a color change.
        /// </summary>
        /// <param name="OldColor">The old, previous color.</param>
        /// <param name="NewColor">The new color.</param>
        /// <returns>The value of the Cancel field in the argument. If false, a subscriber doesn't want the color to change.</returns>
        private bool NotifyColorChange (Color OldColor, Color NewColor)
        {
            if (HandleColorChanged == null)
                return false;
            if (SuppressChangeNotificationEvent)
            {
                SuppressChangeNotificationEvent = false;
                return true;
            }
            ColorInputColorChangedEventArgs args = new ColorInputColorChangedEventArgs(NewColor, OldColor, ColorSpace);
            HandleColorChanged(this, args);
            return args.Cancel;
        }

        /// <summary>
        /// Delegate definition for color change events.
        /// </summary>
        /// <param name="Sender">The instance of the control where the color changed.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandleColorChangedEvents (object Sender, ColorInputColorChangedEventArgs e);
        /// <summary>
        /// Event triggered when the current color is changed.
        /// </summary>
        public event HandleColorChangedEvents HandleColorChanged;
    }

    /// <summary>
    /// Event data for color change events.
    /// </summary>
    public class ColorInputColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColorInputColorChangedEventArgs () : base()
        {
            NewColor = Colors.Transparent;
            OldColor = Colors.Transparent;
            CurrentColorSpace = ColorSpaces.RGB;
            Cancel = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NewColor">The new color.</param>
        /// <param name="OldColor">The old color.</param>
        public ColorInputColorChangedEventArgs (Color NewColor, Color OldColor, ColorSpaces CurrentColorSpace) : base()
        {
            this.NewColor = NewColor;
            this.OldColor = OldColor;
            this.CurrentColorSpace = CurrentColorSpace;
            Cancel = false;
        }

        /// <summary>
        /// The new color.
        /// </summary>
        public Color NewColor { get; internal set; }

        /// <summary>
        /// The old color.
        /// </summary>
        public Color OldColor { get; internal set; }

        /// <summary>
        /// Used to cancel color changes.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Get the current color space.
        /// </summary>
        public ColorSpaces CurrentColorSpace { get; internal set; }
    }
}
