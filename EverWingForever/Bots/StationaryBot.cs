using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class StationaryBot : EverWingBot
    {
        protected override void RunInternal()
        {
            for (int i = 0; i < 50; ++i)
            {
                ClickNewGame();
                Thread.Sleep(100);
                ClickGameOverOK();
                Thread.Sleep(100);
            }
            Thread.Sleep(5000);
            ClickLevelUpOK();
        }
    }
}
