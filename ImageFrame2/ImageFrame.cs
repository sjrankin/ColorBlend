using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Iro3.Controls.Images
{
    [ContentProperty("Source")]
    public partial class ImageFrame : ContentControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageFrame () : base()
        {
            CreateUI();
            DrawUI();
        }

        /// <summary>
        /// Create the base UI.
        /// </summary>
        private void CreateUI ()
        {
            Container = new Border();
            FrameStructure = new Grid();
            Container.Child = FrameStructure;
            this.Content = Container;
            HeaderRow = new RowDefinition();
            HeaderRow.Height = new GridLength(1.0, GridUnitType.Auto);
            FrameStructure.RowDefinitions.Add(HeaderRow);
            RowDefinition ContentRow = new RowDefinition();
            ContentRow.Height = new GridLength(1.0, GridUnitType.Star);
            FrameStructure.RowDefinitions.Add(ContentRow);
            FooterRow = new RowDefinition();
            FooterRow.Height = new GridLength(1.0, GridUnitType.Auto);
            FrameStructure.RowDefinitions.Add(FooterRow);
            HeaderBlock = new TextBlock();
            Grid.SetRow(HeaderBlock, 0);
            HeaderBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
            HeaderBlock.VerticalAlignment = VerticalAlignment.Stretch;
            FrameStructure.Children.Add(HeaderBlock);
            FooterBlock = new TextBlock();
            Grid.SetRow(FooterBlock, 2);
            FooterBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
            FooterBlock.VerticalAlignment = VerticalAlignment.Stretch;
            FrameStructure.Children.Add(FooterBlock);
            ImageContainer = new Border();
            Grid.SetRow(ImageContainer, 1);
            ImageContainer.AllowDrop = true;
            ImageContainer.DragEnter += HandleDragIntoImageContainer;
            ImageContainer.DragLeave += HandleDragExitedImageContainer;
            ImageContainer.DragOver += HandleDragOverImageContainer;
            ImageContainer.Drop += HandleSomethingDroppedOnImageContainer;
            ImageContainer.QueryContinueDrag += QueryKeepOnDragging;
            FrameStructure.Children.Add(ImageContainer);
            ImageBlock = new Image();
            ImageBlock.VerticalAlignment = VerticalAlignment.Stretch;
            ImageBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
            ImageBlock.AllowDrop = true;
            ImageContainer.Child = ImageBlock;
            OverlayBlock = new Border();
            Grid.SetRow(OverlayBlock, 1);
            OverlayBlock.IsHitTestVisible = false;
            OverlayBlock.Opacity = 0.5;
            OverlayBlock.Background = Brushes.Yellow;
            FrameStructure.Children.Add(OverlayBlock);
        }

        private void QueryKeepOnDragging (object Sender, QueryContinueDragEventArgs e)
        {
        }

        private void HandleSomethingDroppedOnImageContainer (object Sender, DragEventArgs e)
        {
        }

        private void HandleDragOverImageContainer (object Sender, DragEventArgs e)
        {
        }

        private void HandleDragExitedImageContainer (object Sender, DragEventArgs e)
        {
        }

        private void HandleDragIntoImageContainer (object Sender, DragEventArgs e)
        {
        }

        private Border Container;
        private Grid FrameStructure;
        private RowDefinition HeaderRow;
        private RowDefinition FooterRow;
        private TextBlock HeaderBlock;
        private TextBlock FooterBlock;
        private Border ImageContainer;
        private Image ImageBlock;
        private Border OverlayBlock;

        /// <summary>
        /// Draw the user interface.
        /// </summary>
        private void DrawUI ()
        {

        }
    }
}
