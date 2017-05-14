using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Gma.System.MouseKeyHook;

namespace EverWingForever
{
    class BotsHookManager
    {
        private IKeyboardMouseEvents _keyHook = Hook.GlobalEvents();
        private EverWingBot _activeBot;

        public bool EnableConvenienceKeys { get; set; }

        public BotsHookManager()
        {
            _keyHook.KeyDown += ProcessKeyboardHook;
        }

        private void ProcessKeyboardHook(object sender, KeyEventArgs e)
        {
            // Don't do anything if there is no active bot set.
            if (_activeBot == null)
            {
                return;
            }

            if (e.Control && e.Shift && e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.F4:
                    case Keys.A:
                        _activeBot.Stop();
                        break;
                    case Keys.F5:
                    case Keys.S:
                        _activeBot.RunForeverAsync();
                        break;
                    case Keys.W:
                        _activeBot.Run();
                        break;

                    // Debugging cases below.

                    case Keys.D1:
                        _activeBot.ClickRelative(0, 0);
                        break;
                    case Keys.D2:
                        Console.WriteLine(System.Windows.Forms.Cursor.Position);
                        break;
                    case Keys.D3:
                        System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                        System.Windows.Forms.Cursor.Position = new System.Drawing.Point(pt.X, pt.Y + 80);
                        break;
                }
            }

            if (this.EnableConvenienceKeys)
            {
                switch (e.KeyCode)
                {
                    case Keys.Z:
                        _activeBot.Stop();
                        break;
                    case Keys.X:
                        _activeBot.RunForeverAsync();
                        break;
                    case Keys.C:
                        _activeBot.Run();
                        break;
                }
            }
        }

        public void Activate(EverWingBot bot)
        {
            _activeBot = bot;
        }

        public void Deactivate()
        {
            _activeBot = null;
        }
    }
}
