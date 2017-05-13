using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class SideStrategyBot : EverWingBot
    {
        protected override void RunInternal()
        {
            for (int i = 0; i < 100; ++i)
            {
                ClickGameOverOK();
                MoveRight(1);
                Thread.Sleep(50);
                MoveLeft(1);
                Thread.Sleep(50);
            }
            for (int i = 0; i < 60; ++i)
            {
                MoveRight(1);
                Thread.Sleep(50);
                MoveLeft(1);
                Thread.Sleep(50);
            }
            ClickLevelUpOK();
        }
    }
}
