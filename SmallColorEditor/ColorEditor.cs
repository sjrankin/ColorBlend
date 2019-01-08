using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Editor.Color
{
    public class ColorEditor :ContentControl
    {
        public ColorEditor () : base()
        {
            CreateUI();
            DrawUI();
        }

        private void CreateUI ()
        {
            Infrastructure = new Grid();
            this.Content = Infrastructure;
            ColumnDefinition CD0 = new ColumnDefinition();
            CD0.Width = new GridLength(0.35, GridUnitType.Star);
            ColumnDefinition CD1 = new ColumnDefinition();
            CD1.Width = new GridLength(0.25, GridUnitType.Star);
            ColumnDefinition CD2 = new ColumnDefinition();
            CD2.Width = new GridLength(0.20, GridUnitType.Star);
            CD2.MinWidth = 24.0;
            ColumnDefinition CD3 = new ColumnDefinition();
            CD3.Width = new GridLength(0.30, GridUnitType.Star);

            ColorNameBlock = new TextBlock();
            Grid.SetColumn(ColorNameBlock, 0);
            ColorNameBlock.Margin = new Thickness(2);
            ColorNameBlock.VerticalAlignment = VerticalAlignment.Center;
            ColorNameBlock.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private Grid Infrastructure=null;
        private TextBlock ColorNameBlock = null;

        private void DrawUI ()
        {
        }
    }
}
