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

namespace EverWingForever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TEMPORARY: Initialize the type of bot to run here.
        private EverWingBot _bot = new SweepBot();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTop_Click(object sender, RoutedEventArgs e)
        {
            string[] result = InputBox.Show("Top");
            if (result != null)
            {
                int top;
                if (int.TryParse(result[0], out top))
                {
                    _bot.SetTop(top);
                    this.btnTop.Content = top;
                }
                else
                {
                    MessageBox.Show("Invalid input: must be numeric.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            string[] result = InputBox.Show("Left");
            if (result != null)
            {
                int left;
                if (int.TryParse(result[0], out left))
                {
                    _bot.SetLeft(left);
                    this.btnLeft.Content = left;
                }
                else
                {
                    MessageBox.Show("Invalid input: must be numeric.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            string[] result = InputBox.Show("Right");
            if (result != null)
            {
                int right;
                if (int.TryParse(result[0], out right))
                {
                    _bot.SetRight(right);
                    this.btnRight.Content = right;
                }
                else
                {
                    MessageBox.Show("Invalid input: must be numeric.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnBottom_Click(object sender, RoutedEventArgs e)
        {
            string[] result = InputBox.Show("Bottom");
            if (result != null)
            {
                int bottom;
                if (int.TryParse(result[0], out bottom))
                {
                    _bot.SetBottom(bottom);
                    this.btnBottom.Content = bottom;
                }
                else
                {
                    MessageBox.Show("Invalid input: must be numeric.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnStationaryBot_Click(object sender, RoutedEventArgs e)
        {
            // NYI
        }
    }
}
