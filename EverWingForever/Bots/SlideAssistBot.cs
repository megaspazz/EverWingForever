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
        // Keeps track of the current iteration.
        int _iter;

        // Default constructor just calls parent class constructor.
        public SlideAssistBot() : base() { }

        protected override void SetupInternal()
        {
            // Reset the iteration counter.
            _iter = 0;
        }

        // Parameterized constructor just calls parent class constructor.
        public SlideAssistBot(double endPosition, bool leftDown) : base(endPosition, leftDown) { }

        protected override void RunInternal()
        {
            if (_iter > 0)
            {
                MoveRight(0.1);
                Thread.Sleep(10);
            }
            else
            {
                MoveLeft(1);
                MoveLeft(1);    // Move left twice in case it didn't work the first time for higher fault tolerance.
                MoveRight(0.05);
                Thread.Sleep(10);
                _iter = 0;
            }
            _iter = (_iter + 1) % 10;
        }
    }
}
