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

namespace EverWingForever
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        private TextBox[] _boxes;

        private InputBox()
        {
            InitializeComponent();
        }

        private InputBox(params string[] prompts)
            : this()
        {
            int n = prompts.Length;

            _boxes = new TextBox[n];
            for (int i = 0; i < n; ++i)
            {
                _boxes[i] = new TextBox();
                this.spBoxes.Children.Add(new TextBlock() { Text = prompts[i] });
                this.spBoxes.Children.Add(_boxes[i]);
            }

            if (n > 0)
            {
                _boxes[0].Focus();
            }
            //foreach (string prompt in prompts)
            //{
            //    this.spMain.Children.Add
            //}
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Accept();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void Accept()
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }

        public string[] GetResponses()
        {
            return _boxes.Select(x => x.Text).ToArray();
        }

        public static string[] Show(params string[] prompts)
        {
            InputBox ib = new InputBox(prompts);
            bool? result = ib.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return ib.GetResponses();
            }
            else
            {
                return null;
            }
        }
    }
}