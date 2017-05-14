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
        protected override void RunInternal()
        {
            SweepRight(0.1, 5, 10);
            SweepLeft(0.1, 5, 10);
        }

        // Finish with the character in the middle of the screen and the cursor on the character.
        protected override void FinishInternal()
        {
            MoveRight(0.5);
        }
    }
}
