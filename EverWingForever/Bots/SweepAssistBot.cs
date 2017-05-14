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
    /// It is better than the SweepBot for this because it guarantees that it will end on the left side with the cursor near the character.
    /// NOTE:  It is incapable of starting new games or looping!
    /// </summary>
    class SweepAssistBot : EverWingBot
    {
        // This setting determines the position the SweepAssistBot will place the character after it is done.
        private double _endPosition;

        // This setting determines whether the SweepAssistBot will automatically hold down the left mouse button when it is done.
        private bool _leftDown;

        // Default constructor currently doesn't need to do anything.
        public SweepAssistBot() { }

        public SweepAssistBot(double endPosition, bool leftDown)
            : this()
        {
            _endPosition = endPosition;
            _leftDown = leftDown;
        }

        protected override void RunInternal()
        {
            SweepRight(0.1, 5, 10);
            SweepLeft(0.1, 5, 10);
        }

        // Move the character to the position specified, and hold the left mouse button down depending on the setting.
        protected override void FinishInternal()
        {
            Move(_endPosition);
            if (_leftDown)
            {
                InputWrapper.LeftDown();
            }
        }
    }
}
