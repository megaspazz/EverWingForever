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
            for (int i = 0; i < 10; ++i)
            {
                MoveRight(0.1);
                Thread.Sleep(5);
            }

            for (int i = 0; i < 10; ++i)
            {
                MoveLeft(0.1);
                Thread.Sleep(5);
            }
        }
    }
}
