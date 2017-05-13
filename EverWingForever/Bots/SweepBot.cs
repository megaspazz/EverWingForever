using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class SweepBot : EverWingBot
    {
        private int _iter = 0;

        private int _gameTime = 60000;

        protected override void RunInternal()
        {
            if (_iter < 320)
            {
                if (_iter < 200)
                {
                    ClickGameOverOK();
                }
                int dir = (_iter % 2 == 0) ? 1 : -1;
                for (int i = 0; i < 10; ++i)
                {
                    Move(dir * 0.1);
                    Thread.Sleep(5);
                }
            }
            else
            {
                ClickLevelUpOK();
            }
            _iter = (_iter + 1) % 321;
        }
    }
}
