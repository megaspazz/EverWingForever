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

using Gma.System.MouseKeyHook;

using Keys = System.Windows.Forms.Keys;

namespace EverWingForever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The currently selected bot.
        private EverWingBot _bot;

        // Manage the keyboard hooks for the bots.
        private BotsHookManager _hookManager = new BotsHookManager();

        // Hooks for the main window UI.
        private IKeyboardMouseEvents _uiHook = Hook.GlobalEvents();

        // A map of Radio Buttons to bots, which is used in the Radio Button events to update the currently selected bot.
        private Dictionary<object, EverWingBot> _map = new Dictionary<object, EverWingBot>();

        public MainWindow()
        {
            InitializeComponent();

            // Map the Radio Buttons to the bots that they are associated with.
            // Also, set the default for the first bot.  This should match the Radio Button that is checked by default.
            _map[this.radStationaryBot] = new StationaryBot();
            _map[this.radSideStrategyBot] = new SideStrategyBot();
            _map[this.radRandomStrafeBot] = new RandomStrafeBot();
            _map[this.radSweepBot] = _bot = new SweepBot();    // ← Set the default bot like so.
            _map[this.radSweepAssistBot] = new SweepAssistBot(0.5, true);    // Change the parameters to the SweepAssistBot to suit your preferences.

            // Activate the default bot.
            _hookManager.Activate(_bot);

            // Add events for the UI hook.
            _uiHook.KeyDown += ProcessGlobalKeyDown;
        }

        private void ProcessGlobalKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.F6:
                        this.chkEnableConveienceKeys.IsChecked ^= true;
                        break;
                }
            }
        }

        private void UpdateSelelectedBot(object sender, RoutedEventArgs e)
        {
            EverWingBot activeBot;
            if (_map.TryGetValue(sender, out activeBot))
            {
                _bot = activeBot;
                _hookManager.Activate(_bot);
            }
        }

        private void chkEnableConveienceKeys_Checked(object sender, RoutedEventArgs e)
        {
            _hookManager.EnableConvenienceKeys = true;
        }

        private void chkEnableConveienceKeys_Unchecked(object sender, RoutedEventArgs e)
        {
            _hookManager.EnableConvenienceKeys = false;
        }

        #region Setting Coordinates (possibly deprecated code)

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

        #endregion
    }
}
