using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace Iro3.Selectors
{
    public partial class Rotational : ContentControl
    {
        public Rotational () : base()
        {
            CreateUI();
            DrawUI();
            AttachEvents();
        }

        private void AttachEvents ()
        {
            Rotator.MouseEnter += HandleMouseEnteredRotator;
            Rotator.MouseLeave += HandleMouseExitedRotator;
            Rotator.MouseMove += HandleMouseMoveInRotator;
        }

        private void CreateUI ()
        {
            Infrastructure = new Grid();
            Infrastructure.VerticalAlignment = VerticalAlignment.Stretch;
            Infrastructure.HorizontalAlignment = HorizontalAlignment.Stretch;
            Content = Infrastructure;
            Rotator = new Ellipse();
            Rotator.Height = double.NaN;
            Rotator.Width = double.NaN;
            Infrastructure.Children.Add(Rotator);
            Indicator = new Ellipse();
            Indicator.Height = 5.0;
            Indicator.Width = 5.0;
            ValueBlock = new TextBlock();
            Infrastructure.Children.Add(ValueBlock);
        }

        private Grid Infrastructure;
        private Ellipse Rotator;
        private Ellipse Indicator;
        private TextBlock ValueBlock;

        private void DrawUI ()
        {
            RotateControl(Value);
            Rotator.Fill = RotatorBrush;
            Rotator.Stroke = RotatorBorderBrush;
            Rotator.StrokeThickness = RotatorBorderBrushWidth;
            Rotator.ToolTip = ToolTip;
            ValueBlock.Text = ((int)(Value)).ToString();
            ValueBlock.VerticalAlignment = VerticalAlignment.Center;
            ValueBlock.HorizontalAlignment = HorizontalAlignment.Center;
            ValueBlock.FontWeight = ValueFontWeight;
            ValueBlock.FontSize = ValueFontSize;
            ValueBlock.FontFamily = ValueFontFamily;
            ValueBlock.Foreground = ValueForeground;
        }

        private void RotateControl(double RotationalValue)
        {
            RotationalValue = (double)((int)RotationalValue % 360);
            RotateTransform RT = new RotateTransform();
            RT.Angle = RotationalValue;
            TransformGroup TG = new TransformGroup();
            TG.Children.Add(TG);
            Rotator.LayoutTransform = RT;
        }
    }
}
