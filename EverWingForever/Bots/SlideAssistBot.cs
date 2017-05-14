using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    /// <summary>
    /// This bot will just strafe from left to right repeatedly.
    /// Its purpose is to assist the player by allowing the player to toggle auto-strafe.
    class SlideAssistBot : AssistBot
    {
        // Default constructor just calls parent class constructor.
        public SlideAssistBot() : base() { }

        // Parameterized constructor just calls parent class constructor.
        public SlideAssistBot(double endPosition, bool leftDown) : base(endPosition, leftDown) { }

        protected override void RunInternal()
        {
            Sweep(0.1, 10, 9);
            MoveLeft(1);
            MoveRight(0.05);
            Thread.Sleep(10);
        }
    }
}
