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

        // Values that define the region of the game on the screen.
        private int _left;
        private int _right;
        private int _top;
        private int _bottom;

        // Manage the keyboard hooks for the bots.
        private BotsHookManager _hookManager = new BotsHookManager();

        // Hooks for the main window UI.
        private IKeyboardMouseEvents _uiHook = Hook.GlobalEvents();

        // A map of string identifiers to bots, which is used in the Radio Button events to update the currently selected bot.
        // The string keys here are also used to save a setting for which bot was last selected.
        private Dictionary<object, EverWingBot> _map = new Dictionary<object, EverWingBot>();

        public MainWindow()
        {
            InitializeComponent();

            // Add events for the UI hook.
            _uiHook.KeyDown += ProcessGlobalKeyDown;
            _uiHook.MouseMove += ProcessMouseMove;

            // Set the coordinates based on saved settings.
            // Note that these need to be set before initializing and activating the bots.
            _left = Properties.Settings.Default.Left;
            _right = Properties.Settings.Default.Right;
            _top = Properties.Settings.Default.Top;
            _bottom = Properties.Settings.Default.Bottom;

            // Map the Radio Buttons to the bots that they are associated with.
            AssociateRadioButtonWithBot(this.radStationaryBot, new StationaryBot());
            AssociateRadioButtonWithBot(this.radSideStrategyBot, new SideStrategyBot());
            AssociateRadioButtonWithBot(this.radRandomStrafeBot, new RandomStrafeBot());
            AssociateRadioButtonWithBot(this.radSweepBot, new SweepBot());
            AssociateRadioButtonWithBot(this.radSlideBot, new SlideBot());
            AssociateRadioButtonWithBot(this.radSweepAssistBot, new SweepAssistBot(0.5, true));    // Change the parameters to the SlideAssistBot to suit your preferences.
            AssociateRadioButtonWithBot(this.radSlideAssistBot, new SlideAssistBot(0.5, true));    // Change the parameters to the SlideAssistBot to suit your preferences.

            // Get the radio button to be selected.
            string lastRadBtnName = Properties.Settings.Default.LastSelectedBotRadioButtonName;
            RadioButton lastRadBtn = this.grpBots.FindName(lastRadBtnName) as RadioButton;
            if (lastRadBtn == null)
            {
                lastRadBtn = this.radSlideBot;    // ← Set the default selected bot type radio button here.
            }

            // Set the selected radio button and the associated bot.  Note that the RadioButton event will automatically activate the bot.
            lastRadBtn.IsChecked = true;
            _bot = GetBotFromRadioButton(lastRadBtn);

            // Set whether or not to use convenience keys.  Note that the CheckBox event will automatically update the hook manager.
            this.chkEnableConveienceKeys.IsChecked = Properties.Settings.Default.EnableConvenienceKeys;

            // Update the TextBoxes in the UI with the coordinates.
            this.txtLeft.Text = _left.ToString();
            this.txtRight.Text = _right.ToString();
            this.txtTop.Text = _top.ToString();
            this.txtBottom.Text = _bottom.ToString();
        }

        private void ProcessMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
            this.lblCursorX.Text = pt.X.ToString();
            this.lblCursorY.Text = pt.Y.ToString();
        }

        private void ProcessGlobalKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt)
            {
                switch (e.KeyCode)
                {
                    // Shortcuts for changing bot settings.
                    case Keys.F6:
                        this.chkEnableConveienceKeys.IsChecked ^= true;
                        break;

                    // Shortcuts for setting the bounds of the region.
                    case Keys.I:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            SetTop(pt.Y);
                        }
                        break;
                    case Keys.J:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            SetLeft(pt.X);
                        }
                        break;
                    case Keys.K:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            SetRight(pt.X);
                        }
                        break;
                    case Keys.M:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            SetBottom(pt.Y);
                        }
                        break;

                    // Shortcuts for defininig the top-left and bottom-right points of the region.
                    case Keys.U:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            SetLeft(pt.X);
                            SetTop(pt.Y);
                        }
                        break;
                    case Keys.Oemcomma:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            SetRight(pt.X);
                            SetBottom(pt.Y);
                        }
                        break;

                    // Debugging shortcuts
                    case Keys.L:
                        {
                            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(pt.X, pt.Y, 1, 1);
                            using (System.Drawing.Bitmap bmp = WindowWrapper.ScreenCapture(rect))
                            {
                                using (Bitmap24 bmp24 = Bitmap24.FromImage(bmp))
                                {
                                    bmp24.Lock();
                                    int[] px = bmp24.GetPixel(0, 0);
                                    double lum = Bitmap24.CalculateLuminance(px);
                                    Console.WriteLine("({0}, {1}) -> ({1}) -> {2}", pt.X, pt.Y, string.Join(", ", px), lum);
                                }
                            }
                        }
                        break;
                    case Keys.O:
                        {
                            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(
                                _left,
                                _top,
                                _right - _left + 1,
                                _bottom - _top + 1
                            );
                            using (System.Drawing.Bitmap bmp = WindowWrapper.ScreenCapture(bounds))
                            {
                                bmp.Save(@"tmp.bmp");
                            }
                        }
                        break;
                }
            }
        }

        private void AssociateRadioButtonWithBot(RadioButton radBtn, EverWingBot bot)
        {
            radBtn.Tag = bot;
        }

        private EverWingBot GetBotFromRadioButton(RadioButton radBtn)
        {
            return (EverWingBot)radBtn.Tag;
        }

        private void UpdateSelelectedBot(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a RadioButton, since only those bot selection Radio Buttons should use this event handler.
            RadioButton radBtn = (RadioButton)sender;

            // Activate the selected bot.
            EverWingBot activeBot = GetBotFromRadioButton(radBtn);
            _bot = activeBot;
            _hookManager.Activate(_bot);

            // Update the bot with the coordinates in case they changed while the bot was inactive.
            _bot.SetLeft(_left);
            _bot.SetRight(_right);
            _bot.SetTop(_top);
            _bot.SetBottom(_bottom);

            // Update and save the setting for the last bot.
            Properties.Settings.Default.LastSelectedBotRadioButtonName = radBtn.Name;
            Properties.Settings.Default.Save();
        }

        private void EnableConvenienceKeysChanged(object sender, RoutedEventArgs e)
        {
            // Get whether or not to enable convenience keys from the CheckBox.
            bool enable = this.chkEnableConveienceKeys.IsChecked.Value;

            // Update the hoook manager settings.
            _hookManager.EnableConvenienceKeys = enable;

            // Update and save the user settings.
            Properties.Settings.Default.EnableConvenienceKeys = enable;
            Properties.Settings.Default.Save();
        }

        private void SetLeft(int left)
        {
            // Update private member.
            _left = left;

            // Update text box.
            this.txtLeft.Text = left.ToString();

            // Update the bot.
            _bot.SetLeft(left);

            // Update and set the user settings.
            Properties.Settings.Default.Left = left;
            Properties.Settings.Default.Save();
        }

        private void SetRight(int right)
        {
            // Update private member.
            _right = right;

            // Update text box.
            this.txtRight.Text = right.ToString();

            // Update the bot.
            _bot.SetRight(right);

            // Update and set the user settings.
            Properties.Settings.Default.Right = right;
            Properties.Settings.Default.Save();
        }

        private void SetTop(int top)
        {
            // Update private member.
            _top = top;

            // Update text box.
            this.txtTop.Text = top.ToString();

            // Update the bot.
            _bot.SetTop(top);

            // Update and set the user settings.
            Properties.Settings.Default.Top = top;
            Properties.Settings.Default.Save();
        }

        private void SetBottom(int bottom)
        {
            // Update private member.
            _bottom = bottom;

            // Update text box.
            this.txtBottom.Text = bottom.ToString();

            // Update the bot.
            _bot.SetBottom(bottom);

            // Update and set the user settings.
            Properties.Settings.Default.Bottom = bottom;
            Properties.Settings.Default.Save();
        }

        // Define some constants to be used in finding the game region.
        private static readonly int CHECK_AROUND = 2;
        private static readonly int CHECK_SIZE = 1 + CHECK_AROUND + CHECK_AROUND;

        private void btnShrinkToFit_Click(object sender, RoutedEventArgs e)
        {
            // Create a Rectangle defining the region.
            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(
                _left - 1,
                _top - 1,
                _right - _left + 3,
                _bottom - _top + 3
            );

            // Get the width and height of the region.
            int width = bounds.Width;
            int height = bounds.Height;

            // Quit if the width and the height are too small.
            if (width < 20 || height < 20)
            {
                MessageBox.Show("The defined region is too small.  Please update the region and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get a screenshot of the defined region to process.
            using (System.Drawing.Bitmap bmp = WindowWrapper.ScreenCapture(bounds))
            {
                using (Bitmap24 bmp24 = Bitmap24.FromImage(bmp))
                {
                    // Lock the bits in the Bitmap24 to directly access the raw data.
                    bmp24.Lock();

                    // Compute how much to shift the left coordinate by.
                    int leftShift = -1;
                    int leftDark = 0;
                    for (int x = 0; x < width; ++x)
                    {
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x, (height / 2) - CHECK_AROUND, 1, CHECK_SIZE);
                        int currDark = CountDark(bmp24, rect);
                        if (leftDark == CHECK_SIZE && currDark == 0)
                        {
                            leftShift = x - 1;
                            break;
                        }
                        leftDark = currDark;
                    }

                    // Compute how much to shift the right coordinate by.
                    int rightShift = -1;
                    int rightDark = 0;
                    for (int x = width - 1; x >= 0; --x)
                    {
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x, (height / 2) - CHECK_AROUND, 1, CHECK_SIZE);
                        int currDark = CountDark(bmp24, rect);
                        if (rightDark == CHECK_SIZE && currDark == 0)
                        {
                            rightShift = width - x - 2;
                            break;
                        }
                        rightDark = currDark;
                    }

                    // Compute how much to shift the top coordinate by.
                    int topShift = -1;
                    int topDark = 0;
                    for (int y = 0; y < height; ++y)
                    {
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle((width / 2) - CHECK_AROUND, y, CHECK_SIZE, 1);
                        int currDark = CountDark(bmp24, rect);
                        if (topDark == CHECK_SIZE && currDark == 0)
                        {
                            topShift = y - 1;
                            break;
                        }
                        topDark = currDark;
                    }

                    // Compute how much to shift the bottom coordinate by.
                    int bottomShift = -1;
                    int bottomDark = 0;
                    for (int y = height - 1; y >= 0 ; --y)
                    {
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle((width / 2) - CHECK_AROUND, y, CHECK_SIZE, 1);
                        int currDark = CountDark(bmp24, rect);
                        if (bottomDark == CHECK_SIZE && currDark == 0)
                        {
                            bottomShift = height - y - 2;
                            break;
                        }
                        bottomDark = currDark;
                    }

                    // Define a list containing the ones that were not found.
                    List<string> notFound = new List<string>();

                    // Check if a valid left shift was found.
                    if (leftShift > 0)
                    {
                        SetLeft(_left + leftShift);
                    }
                    else if (leftShift < 0)
                    {
                        notFound.Add("Left");
                    }

                    // Check if a valid right shift was found.
                    if (rightShift > 0)
                    {
                        SetRight(_right - rightShift);
                    }
                    else if (rightShift < 0)
                    {
                        notFound.Add("Right");
                    }

                    // Check if a valid top shift was found.
                    if (topShift > 0)
                    {
                        SetTop(_top + topShift);
                    }
                    else if (topShift < 0)
                    {
                        notFound.Add("Top");
                    }

                    // Check if a valid bottom shift was found.
                    if (bottomShift > 0)
                    {
                        SetBottom(_bottom - bottomShift);
                    }
                    else if (bottomShift < 0)
                    {
                        notFound.Add("Bottom");
                    }

                    if (notFound.Count > 0)
                    {
                        string errorMessage = "Unable to auto-detect the bounds.  Please make sure that the defined region completely contains the game window, and that the game is displaying a bright image, such as the main screen.\n\nThe following dimensions were not found:\n\n" + string.Join("\n", notFound) + "\n\n";
                        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        // This helper function assumes that the input Bitmap24 has already been locked bits.
        private int CountDark(Bitmap24 bmp24, System.Drawing.Rectangle rect)
        {
            int cnt = 0;
            for (int x = rect.Left; x < rect.Right; ++x)
            {
                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    int[] px = bmp24.GetPixel(x, y);
                    if (IsDarkPixel(px))
                    {
                        ++cnt;
                    }
                }
            }
            return cnt;
        }

        private bool IsDarkPixel(int[] px)
        {
            return Bitmap24.CalculateLuminance(px) <= 40.96;
        }
    }
}
