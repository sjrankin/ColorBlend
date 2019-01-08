using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Iro3.Data.Image;

namespace ColorBlend
{
    /// <summary>
    /// Base class for actions pertaining to image manipulation.
    /// </summary>
    public class StageBase
    {
        /// <summary>
        /// Defines a state changer delegate. This is used to enable the base class to communicate with the child class.
        /// </summary>
        /// <param name="NewState">New state of some type.</param>
        public delegate void StateChanger (StageAction NewState);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ShortName">Short name for the action.</param>
        /// <param name="Description">Description of the action.</param>
        /// <param name="Action">Defines the action.</param>
        public StageBase (string ShortName, string Description, ImageActions Action)
        {
            CommonInitialization();
            this.ShortName = ShortName;
            this.Description = Description;
            this.Action = Action;
            this.ChangeDelegate = ChangeDelegate;
            UseRegion = false;
        }

        /// <summary>
        /// Get or set the stage ID.
        /// </summary>
        public Guid StageID { get; set; }

        /// <summary>
        /// Get or set the delegate that reacts to state changes. If this property is null, state changes are not
        /// sent to child classes.
        /// </summary>
        internal StateChanger ChangeDelegate { get; set; }

        /// <summary>
        /// Returns true if the child class has set a change delegate, false if not.
        /// </summary>
        public bool HasStateDelegate
        {
            get
            {
                return ChangeDelegate == null ? false : true;
            }
        }

        /// <summary>
        /// Common initialization.
        /// </summary>
        private void CommonInitialization ()
        {
            StageID = Guid.NewGuid();
            CBI = new ColorBlenderInterface();
            ChangeDelegate = null;
            Description = "";
            ShortName = "";
            Action = ImageActions.NOP;
            IsEnabled = true;
            UserVisible = true;
            Input = new ImageList(1);
            Output = new ImageList(1);
            UseRegion = false;
            Region = new RectRegion(new PointEx(0.0, 0.0), new PointEx(1.0, 1.0), true, true);
        }

        /// <summary>
        /// Refresh the image (by re-executing the image action). If no change delegate has been assigned, calling this
        /// method has no effect.
        /// </summary>
        /// <returns>True if the action was passed to the child class, false if no child delegate was found.</returns>
        public bool Refresh ()
        {
            if (ChangeDelegate != null)
            {
                ChangeDelegate(new StageAction(StageActions.Refresh));
                return true;
            }
            return false;
        }

        public bool Execute ()
        {
            return false;
        }

        /// <summary>
        /// Get or set the action performed by the child class.
        /// </summary>
        public ImageActions Action { get; set; }

        /// <summary>
        /// Get or set the description of the action done by the child class.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the short name of the action.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Get the color blender interface class.
        /// </summary>
        public ColorBlenderInterface CBI { get; private set; }

        private bool _IsSink;
        /// <summary>
        /// Determines if the action is a sink - e.g., nothing past the current action is done.
        /// </summary>
        public bool IsSink
        {
            get
            {
                return _IsSink;
            }
            set
            {
                _IsSink = value;
                if (ChangeDelegate != null)
                    ChangeDelegate(new StageAction(StageActions.SinkChange));
            }
        }

        private bool _IsEnabled;
        /// <summary>
        /// Get or set the enabled state of the action. If set to false, no image manipulation is done and the input is
        /// passed as-is to the output.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                _IsEnabled = value;
                if (ChangeDelegate != null)
                    ChangeDelegate(new StageAction(StageActions.EnableChange));
            }
        }

        /// <summary>
        /// Determines if the node should be displayed in any user-visible UIs.
        /// </summary>
        public bool UserVisible { get; set; }

        /// <summary>
        /// Get the list of input images.
        /// </summary>
        public ImageList Input { get; private set; }

        /// <summary>
        /// Get the list of output images.
        /// </summary>
        public ImageList Output { get; private set; }

        /// <summary>
        /// Get or set the flag that determines if regions are used. If set to False (default), the entire image is used.
        /// </summary>
        public bool UseRegion { get; set; }

        /// <summary>
        /// Get or set the region affected by the stage.
        /// </summary>
        public RectRegion Region { get; internal set; }

        /// <summary>
        /// Create and return an image action.
        /// </summary>
        /// <param name="ActionType">The image action type.</param>
        /// <returns>Image action. If <paramref name="ActionType"/> is not understood, NOPAction is returned.</returns>
        public static StageBase ActionFactory (ImageActions ActionType)
        {
            switch (ActionType)
            {
                case ImageActions.Grayscale:
                    return new GrayscaleStage();

                case ImageActions.Splitter:
                    return new SplitterStage();

                case ImageActions.Selector:
                    return new SelectorStage();

                case ImageActions.Display:
                    return new DisplayStage(null);
            }
            return new NOPStage();
        }

        /// <summary>
        /// Link the input of this stage to the input of the next stage (effectively skipping this stage).
        /// </summary>
        /// <param name="Successor">Where to link the input.</param>
        /// <returns>True on success.</returns>
        public bool LinkToSuccessor (StageBase Successor)
        {
            if (Successor == null)
                throw new ArgumentNullException("Successor");
            Successor.Input.Clear();
            Successor.Input.Add(Input.First);
            return true;
        }

        /// <summary>
        /// Notify subscribers that rendering completed.
        /// </summary>
        /// <param name="Returned">The render return code.</param>
        public void NotifyCompleted (ColorBlenderInterface.ReturnCode Returned)
        {
            if (PipelineStageCompleted != null)
            {
                PipelineStageCompletedEventArgs args = new PipelineStageCompletedEventArgs
                {
                    ReturnCode = Returned
                };
                PipelineStageCompleted(this, args);
            }
        }

        /// <summary>
        /// Delegate definition for stage complete events.
        /// </summary>
        /// <param name="Sender">The stage where the completion took place.</param>
        /// <param name="e">Event data.</param>
        public delegate void HandlePipelineStageCompletedEvent (StageBase Sender, PipelineStageCompletedEventArgs e);
        /// <summary>
        /// Triggered when a stage is completed.
        /// </summary>
        public event HandlePipelineStageCompletedEvent PipelineStageCompleted;
    }

    /// <summary>
    /// Event data for stage complete events.
    /// </summary>
    public class PipelineStageCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PipelineStageCompletedEventArgs () : base()
        {
            ReturnCode = ColorBlenderInterface.ReturnCode.NotSet;
        }

        /// <summary>
        /// Returns success results based on the value of <seealso cref="ReturnCode"/>.
        /// </summary>
        public bool Success
        {
            get
            {
                return ReturnCode == ColorBlenderInterface.ReturnCode.Success;
            }
        }

        /// <summary>
        /// Get or set the render return code value.
        /// </summary>
        public ColorBlenderInterface.ReturnCode ReturnCode { get; set; }

        /// <summary>
        /// Return a copy of the image data.
        /// </summary>
        /// <returns>Image data.</returns>
        public ImageData GetImageData ()
        {
            return ImageBuffer;
        }

        /// <summary>
        /// Copy WriteableBitmap data to a local variable.
        /// </summary>
        /// <param name="Data">Data to copy.</param>
        public void SetImageData (ref WriteableBitmap Data)
        {
            if (ImageBuffer == null)
                throw new ArgumentNullException("Data");
            ImageBuffer = new ImageData(ref Data);
        }

        /// <summary>
        /// Contains non-WriteableBitmap image data.
        /// </summary>
        private ImageData ImageBuffer = null;
    }

    /// <summary>
    /// Defines valid image actions.
    /// </summary>
    public enum ImageActions
    {
        /// <summary>
        /// No action. Image sink.
        /// </summary>
        NOP,
        /// <summary>
        /// Sends the input unchanged to one or more other actions.
        /// </summary>
        Splitter,
        /// <summary>
        /// Selects one input from two or more actions depending on criteria specfic to the given selector (e.g., brightness, size).
        /// </summary>
        Selector,
        /// <summary>
        /// Open a file.
        /// </summary>
        OpenFile,
        /// <summary>
        /// Save a file (image sink).
        /// </summary>
        SaveFile,
        /// <summary>
        /// Grayscale action.
        /// </summary>
        Grayscale,
        /// <summary>
        /// Threshold action.
        /// </summary>
        Threshold,
        /// <summary>
        /// Solarize action.
        /// </summary>
        Solarize,
        /// <summary>
        /// Color inverse action.
        /// </summary>
        Negative,
        /// <summary>
        /// Image distortion action.
        /// </summary>
        Distortion,
        /// <summary>
        /// Image convolution action.
        /// </summary>
        Convolve,
        /// <summary>
        /// Image action is the terminal action in the pipeline.
        /// </summary>
        Terminal,
        /// <summary>
        /// Display action.
        /// </summary>
        Display,
    }
}
