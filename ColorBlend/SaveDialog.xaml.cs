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
using System.IO;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for SaveDialog.xaml
    /// </summary>
    public partial class SaveDialog : Window
    {
        public SaveDialog ()
        {
            InitializeComponent();
            SaveAs = new SaveRecord();
        }

        private void CommonInitialization ()
        {
            FileNameBox.Text = SaveAs.SaveName;
            WidthBox.Text = SaveAs.Width.ToString("f0");
            HeightBox.Text = SaveAs.Height.ToString("f0");
            DPIXBox.Text = SaveAs.DPIX.ToString("f0");
            DPIYBox.Text = SaveAs.DPIY.ToString("f0");
        }

        private SaveRecord _SaveAs = null;
        public SaveRecord SaveAs
        {
            get
            {
                return _SaveAs;
            }
            set
            {
                _SaveAs = value;
                CommonInitialization();
            }
        }

        private void HandleSaveButtonClick (object Sender, RoutedEventArgs e)
        {
            SaveAs.Canceled = false;
            this.Close();
        }

        private void HandleCancelButtonClick (object Sender, RoutedEventArgs e)
        {
            SaveAs.Canceled = true;
            this.Close();
        }

        private void HandleBrowseClick (object Sender, RoutedEventArgs e)
        {

        }
    }

    public class SaveRecord
    {
        public SaveRecord ()
        {
            SaveName = Path.Combine(Environment.CurrentDirectory, "ColorBlend.png"); ;
            DPIX = 96.0;
            DPIY = 96.0;
            Width = 100;
            Height = 100;
            Canceled = false;
        }

        public string SaveName { get; set; }

        public double DPIX { get; set; }

        public double DPIY { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool Canceled { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(SaveName))
                return false;
            if (DPIX < 50)
                return false;
            if (DPIY < 50)
                return false;
            if (Width < 8)
                return false;
            if (Height < 8)
                return false;
            if (Canceled)
                return false;
            return true;
        }
    }
}
