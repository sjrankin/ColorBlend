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
using System.Windows.Shapes;

namespace ColorBlend
{
    /// <summary>
    /// Interaction logic for DebugMessageBox.xaml
    /// </summary>
    public partial class DebugMessageBox : Window
    {
        public DebugMessageBox ()
        {
            CommonInitialization();
            TitleText = "Error";
            MessageText = "An error occurred.";
        }

        public DebugMessageBox (string MessageText, string TitleText = "")
        {
            CommonInitialization();
            if (string.IsNullOrEmpty(TitleText))
                TitleText = "Error";
            this.TitleText = TitleText;
            this.MessageText = MessageText;
            DoBreak = false;
        }

        private void CommonInitialization ()
        {
            InitializeComponent();
            ShowVariableList = true;
        }

        public string TitleText
        {
            get
            {
                return this.Title;
            }
            set
            {
                this.Title = value;
            }
        }

        public string MessageText
        {
            get
            {
                return this.MessageBlock.Text;
            }
            set
            {
                this.MessageBlock.Text = value;
            }
        }

        private bool _ShowVariableList = false;
        public bool ShowVariableList
        {
            get
            {
                return _ShowVariableList;
            }
            set
            {
                _ShowVariableList = value;
                DumpState(_ShowVariableList);
            }
        }

        private void DumpState (bool ShowStates)
        {
            VarDump.Items.Clear();
            if (ShowStates)
            {
                ContentGrid.ColumnDefinitions[1].Width = new GridLength(1.0, GridUnitType.Auto);
                ContentSplitter.Opacity = 1.0;
            }
            else
            {
                ContentGrid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Pixel);
                ContentSplitter.Opacity = 0.0;
                return;
            }
            int Index = 0;
            foreach (Tuple<string, string> OneState in VariableList)
            {
                TextBlock StateUI = new TextBlock
                {
                    Margin = new Thickness(2),
                    Background = Index % 2 == 0 ? Brushes.White : Brushes.WhiteSmoke
                };
                Index++;
                Run VarName = new Run(OneState.Item1)
                {
                    FontFamily = new FontFamily("Segoe UI"),
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                };
                StateUI.Inlines.Add(VarName);
                Run VarValue = new Run("  " + OneState.Item2)
                {
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 12
                };
                StateUI.Inlines.Add(VarValue);
                VarDump.Items.Add(StateUI);
            }
        }

        private List<Tuple<string, string>> _VarList = new List<Tuple<string, string>>();
        public List<Tuple<string, string>> VariableList
        {
            get
            {
                return _VarList;
            }
            set
            {
                _VarList = value;
                DumpState(true);
            }
        }

        public bool DoBreak { get; internal set; }

        private void HandleButtonClicks (object Sender, RoutedEventArgs e)
        {
            Button B = Sender as Button;
            if (B == null)
                return;
            if (B.Name == "CloseButton")
            {
                DoBreak = false;
            }
            else
                if (B.Name == "BreakButton")
                {
                    DoBreak = true;
                }
            this.Close();
        }
    }
}
