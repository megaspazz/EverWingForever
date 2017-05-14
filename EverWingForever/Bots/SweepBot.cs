using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class SweepBot : PeriodBot
    {
        // The direction that the bot will sweep in the next iteration.
        private int _dir;

        protected override void SetupPeriod()
        {
            // Reset the direction.
            _dir = 1;
        }

        // Sweep in the current direction and reverse the direction for the next iteration.
        protected override void RunPeriod()
        {
            Sweep(_dir * 0.1, 5, 10);
            _dir *= -1;
        }
    }
}
