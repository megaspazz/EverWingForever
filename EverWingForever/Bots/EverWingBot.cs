using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace EverWingForever
{
    abstract class EverWingBot
    {
        // Relative Y position of the horizontal line on which to perform the movement click-and-drag operations.
        private static readonly double MOVEMENT_Y = 0.8;

        // Relative X & Y coordinates that represent the location to click on the main screen to start a new round.
        private static readonly double NEW_GAME_X = 0.5;
        private static readonly double NEW_GAME_Y = 0.75;

        // Relative X & Y coordinates that represent the location of the "OKAY" button when a round ends.
        private static readonly double GAME_OVER_OK_X = 0.75;
        private static readonly double GAME_OVER_OK_Y = 0.85;

        // Relative X & Y coordinates that represent the location of the "OKAY!" button in the "LEVEL UP!" popup.
        // Note that this coordinate will also click and purchase pre-game power-ups when it starts, so be careful!
        private static readonly double LEVEL_UP_OK_X = 0.5;
        private static readonly double LEVEL_UP_OK_Y = 0.65;

        // Rectangle representing the boundaries of the game window:
        //   * The top-left of the Rectangle should be the top-left point in the game window.
        //   * The bottom-right point should be the bottom-rigth point in the game window.
        // Note that this means that the the representation of the Rectangle will be off by one pixel in height and width,
        // since GAME_BOUNDS is inclusive of the bottom-right, but the Rectangle's bottom-right is supposed to be exclusive.
        // Example:
        //   #---+
        //   |   |
        //   |   |
        //   +---#
        // The # characters define the points that define the rectangle.
        private Rectangle GAME_BOUNDS = new Rectangle(-971, 9, 661, 1177);

        private bool _running = false;
        private IKeyboardMouseEvents _keyHook = Hook.GlobalEvents();

        public EverWingBot()
        {
            _keyHook.KeyDown += ProcessKeyboardHook;
        }

        private void ProcessKeyboardHook(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.F4:
                        Console.WriteLine("STOPPED");
                        Stop();
                        break;
                    case Keys.F5:
                        RunForeverAsync();
                        break;
                    case Keys.D1:
                        ClickRelative(0, 0);
                        break;
                }
            }
        }

        public void SetLeft(int left)
        {
            GAME_BOUNDS = new Rectangle(left, GAME_BOUNDS.Top, GAME_BOUNDS.Right - left, GAME_BOUNDS.Height);
        }

        public void SetRight(int right)
        {
            GAME_BOUNDS = new Rectangle(GAME_BOUNDS.Left, GAME_BOUNDS.Top, right - GAME_BOUNDS.Left, GAME_BOUNDS.Height);
        }

        public void SetTop(int top)
        {
            GAME_BOUNDS = new Rectangle(GAME_BOUNDS.Left, top, GAME_BOUNDS.Bottom - top, GAME_BOUNDS.Width);
        }

        public void SetBottom(int bottom)
        {
            GAME_BOUNDS = new Rectangle(GAME_BOUNDS.Left, GAME_BOUNDS.Top, bottom - GAME_BOUNDS.Top, GAME_BOUNDS.Width);
        }

        /// <summary>
        /// Child classes should implement this method to define what the bot actually does.  Make sure the implementation allows the bot to loop forever.
        /// The general strategy is to spam click the GAME_OVER_OK button and spam clicking to start the game.
        /// You need to guarantee that the bot will not click LEVEL_UP_OK within ~5 seconds of starting a new game, or it will waste resources buying power-ups.
        /// </summary>
        protected abstract void RunInternal();

        /// <summary>
        /// Runs a single iteration of the bot.
        /// <returns>Whether the bot started successfully.</returns>
        /// </summary>
        public virtual bool Run()
        {
            if (!_running)
            {
                _running = true;
                RunInternal();
                _running = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Runs the bot forever.
        /// <returns>Whether the bot started successfully.</returns>
        /// </summary>
        public virtual bool RunForever()
        {
            if (!_running)
            {
                _running = true;
                while (_running)
                {
                    RunInternal();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Runs the bot forever in another thread, so that it won't block the UI.
        /// <returns>Whether the bot started successfully.</returns>
        /// </summary>
        public virtual bool RunForeverAsync()
        {
            if (!_running)
            {
                Task.Factory.StartNew(() =>
                {
                    RunForever();
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Stop()
        {
            _running = false;
        }

        public int UpscaleX(double x)
        {
            return (int)Math.Round(x * GAME_BOUNDS.Width);
        }

        public double DownscaleX(int x)
        {
            return (double)x / GAME_BOUNDS.Width;
        }

        public int ToScreenX(double x)
        {
            return GAME_BOUNDS.Left + UpscaleX(x);
        }

        public int UpscaleY(double y)
        {
            return (int)Math.Round(y * GAME_BOUNDS.Height);
        }

        public double DownscaleY(int y)
        {
            return (double)y / GAME_BOUNDS.Height;
        }

        public int ToScreenY(double y)
        {
            return GAME_BOUNDS.Top + UpscaleY(y);
        }

        public Point ToScreenPoint(double x, double y)
        {
            return new Point(ToScreenX(x), ToScreenY(y));
        }

        public void ClickRelative(double x, double y)
        {
            InputWrapper.LeftClick(ToScreenPoint(x, y));
        }

        public void ClickAndDragRelative(double x0, double y0, double xf, double yf)
        {
            InputWrapper.ClickAndDrag(ToScreenPoint(x0, y0), ToScreenPoint(xf, yf));
        }

        public void MoveRight(double dx)
        {
            ClickAndDragRelative(0, MOVEMENT_Y, dx, MOVEMENT_Y);
        }

        public void MoveLeft(double dx)
        {
            int px = UpscaleX(dx);
            ClickAndDragRelative(dx, MOVEMENT_Y, 0, MOVEMENT_Y);
        }

        public void Move(double dx)
        {
            if (dx > 0)
            {
                MoveRight(dx);
            }
            else
            {
                MoveLeft(-dx);
            }
        }

        public void ClickNewGame()
        {
            ClickRelative(NEW_GAME_X, NEW_GAME_Y);
        }

        public void ClickGameOverOK()
        {
            ClickRelative(GAME_OVER_OK_X, GAME_OVER_OK_Y);
        }

        public void ClickLevelUpOK()
        {
            ClickRelative(LEVEL_UP_OK_X, LEVEL_UP_OK_Y);
        }
    }
}
