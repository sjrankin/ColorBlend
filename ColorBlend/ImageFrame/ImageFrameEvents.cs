using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace ColorBlend
{ 
    public partial class ImageFrame
    {
        public delegate void HandleDragOperationStartEvents (object Sender, DragOperationStartEventArgs e);
        public event HandleDragOperationStartEvents DragOperationStartEvent;

        public delegate void HandleObjectsDroppedEvents (object Sender, ObjectsDroppedEventArgs e);
        public event HandleObjectsDroppedEvents ObjectsDroppedEvent;

        public delegate void ImageMousePositionChangedEvent (object Sender, ImageMousePositionChangeArgs e);
        public event ImageMousePositionChangedEvent ImageMousePositionChanged;
    }

    public class DragOperationStartEventArgs : EventArgs
    {
        public DragOperationStartEventArgs () : base()
        {
            CancelDragOperation = false;
            ValidDrop = true;
            DroppedFileNames = new List<string>();
        }

        public bool CancelDragOperation { get; set; }

        public bool ValidDrop { get; set; }

        public List<string> DroppedFileNames { get; internal set; }
    }

    public class ObjectsDroppedEventArgs : EventArgs
    {
        public ObjectsDroppedEventArgs () : base()
        {
            DroppedFileNames = new List<string>();
        }

        public List<string> DroppedFileNames { get; internal set; }
    }

    /// <summary>
    /// Event data for mouse moved over image events.
    /// </summary>
    public class ImageMousePositionChangeArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageMousePositionChangeArgs () : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="MouseX">Horizontal mouse position.</param>
        /// <param name="MouseY">Vertical mouse position.</param>
        /// <param name="ViewportWidth">Viewport width.</param>
        /// <param name="ViewportHeight">Viewport height.</param>
        public ImageMousePositionChangeArgs (double MouseX, double MouseY,
            double ViewportWidth, double ViewportHeight) : base()
        {
            this.MouseX = MouseX;
            this.MouseY = MouseY;
            this.ViewportWidth = ViewportWidth;
            this.ViewportHeight = ViewportHeight;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="MouseX">Horizontal mouse position.</param>
        /// <param name="MouseY">Vertical mouse position.</param>
        /// <param name="ViewportWidth">Viewport width.</param>
        /// <param name="ViewportHeight">Viewport height.</param>
        /// <param name="ActualX">Calculated horizontal mouse position.</param>
        /// <param name="ActualY">Calculated vertical mouse position.</param>
        /// <param name="UnderMouse">The color under the mouse pointer.</param>
        public ImageMousePositionChangeArgs(double MouseX, double MouseY,
            double ViewportWidth, double ViewportHeight,int ActualX,int ActualY,
            Color UnderMouse) : base()
        {
            this.MouseX = MouseX;
            this.MouseY = MouseY;
            this.ViewportWidth = ViewportWidth;
            this.ViewportHeight = ViewportHeight;
            ImageX = ActualX;
            ImageY = ActualY;
            this.UnderMouse = UnderMouse;
        }

        /// <summary>
        /// Get or set the mouse's horizontal position.
        /// </summary>
        public double MouseX { get; set; } = 0.0;

        /// <summary>
        /// Get or set the mouse's vertical position.
        /// </summary>
        public double MouseY { get; set; } = 0.0;

        /// <summary>
        /// Get or set the calculated horizontal position of the mouse.
        /// </summary>
        public int ImageX { get; set; } = 0;

        /// <summary>
        /// Get or set the calculated vertical position of the mouse.
        /// </summary>
        public int ImageY { get; set; } = 0;

        /// <summary>
        /// Get the width of the viewport.
        /// </summary>
        public double ViewportWidth { get; set; } = 0.0;

        /// <summary>
        /// Get the height of the viewport.
        /// </summary>
        public double ViewportHeight { get; set; } = 0.0;

        public Color UnderMouse { get; set; } = Colors.Transparent;
    }
}
