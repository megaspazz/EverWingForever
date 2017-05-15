using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    /// <summary>
    /// This bot will just strafe from left to right, and then from right to left.
    /// Its purpose is to assist the player by allowing the player to toggle auto-strafe.
    /// </summary>
    class SweepAssistBot : AssistBot
    {
        // The direction to move in the next iteration.
        private int _dir;

        // The current position of the bot.
        private int _pos;

        // Default constructor just calls parent class constructor.
        public SweepAssistBot() : base() { }

        protected override void SetupInternal()
        {
            _dir = 1;
            _pos = 0;
        }

        // Parameterized constructor just calls parent class constructor.
        public SweepAssistBot(double endPosition, bool leftDown) : base(endPosition, leftDown) { }
        
        protected override void RunInternal()
        {
            // Move in the current direction.
            Move(_dir * 0.1);
            Thread.Sleep(5);

            // Update the position and reverse the direction if the bot is at the left or right bounds.
            _pos += _dir;
            if (_pos == 0 || _pos == 10)
            {
                _dir *= -1;
            }
        }
    }
}
