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
        private int _iter;

        // Currently, the the number of games to level up is hard-coded.
        private int _gamesToLevelUp = 100;

        // The maximum number of iterations before clicking the LEVEL_UP_OK button.
        private int _maxIters;

        public SweepBot()
        {
            // Calculate the optimal period time given the number of games to level up.
            // Given G games to level up and a period of T seconds:
            //   * The average time wasted per game waiting for clicking GAME_OVER_OK is:  (6 / T) * (6 / 2) = 18 / T.
            //   * The average time wasted per game waiting for clicking LEVEL_UP_OK is:  (1 / G) * (T / 2) = T / 2G.
            // So, let the average total time wasted per game be F(G, T) = (18 / T) + (T / 2G).
            //   * To find when it is minimum, take the first derivative for T:  dF(G, T) / dT = (-18 / T²) + (1 / 2G).
            //   * Set the first derivative equal to 0 and solve for T to get:  T = 6 * sqrt(G).
            double calcTime = 6 * Math.Sqrt(_gamesToLevelUp);

            // Set the period time as the calculated optimal time, but must be within the range [16, 160] seconds.
            double periodTime = Math.Max(Math.Min(calcTime, 160), 16);

            // We will set the total number iterations the total number of strafes (including left and right) to fill up the period time.
            // Note the extra one at the end, which is the final iteration in which the LEVEL_UP_OK button is clicked.
            _maxIters = (int)(2 * (periodTime / 0.100) + 1);
        }

        protected override void SetupInternal()
        {
            // Reset the private iteration counter.
            _iter = 0;
        }

        protected override void RunInternal()
        {
            if (_iter < _maxIters)
            {
                // It is assumed that each iteration will take 50ms, so 120 iterations is 6s.
                if (_iter < _maxIters - 120)
                {
                    ClickGameOverOK();
                }

                // In this loop, each strafe (left or right) takes 50ms.
                int dir = (_iter % 2 == 0) ? 1 : -1;
                Sweep(dir * 0.1, 5, 10);
            }
            else
            {
                ClickLevelUpOK();
            }
            _iter = (_iter + 1) % _maxIters;
        }
    }
}
