using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class SlideBot : PeriodBot
    {
        // Slide all the way to the right and then reset back to the left.
        protected override void RunPeriod()
        {
            Sweep(0.1, 8, 9);
            MoveLeft(1);
            MoveRight(0.05);
            Thread.Sleep(8);
        }
    }
}
