using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class RandomStrafeBot : EverWingBot
    {
        private static readonly Random RNG = new Random();

        private int _iter;
        
        protected override void SetupInternal()
        {
            // Reset the private iteration counter.
            _iter = 0;
        }

        protected override void RunInternal()
        {
            if (_iter < 159)
            {
                if (_iter < 100)
                {
                    ClickGameOverOK();
                }
                Move(RNG.NextDouble() * 0.4 - 0.2);
            }
            else
            {
                ClickLevelUpOK();
            }
            _iter = (_iter + 1) % 160;
            Thread.Sleep(100);
        }
    }
}
