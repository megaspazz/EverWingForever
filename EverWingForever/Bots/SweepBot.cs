using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverWingForever
{
    class SweepBot : EverWingBot
    {
        // The number of milliseconds in one period.
        private double _periodTimeMs;

        // The direction that the bot will sweep in the next iteration.
        private int _dir;

        // Stopwatch to keep track of the current time in the period.
        // During the last 6 seconds of the period, the bot will not click the LEVEL_UP_OK button.
        // After each period, the bot will click the LEVEL_UP_OK button once.
        private Stopwatch _sw = new Stopwatch();

        // Currently, the the number of games to level up is hard-coded.
        private int _gamesToLevelUp = 100;

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
            _periodTimeMs = 1000 * Math.Max(Math.Min(calcTime, 160), 16);
        }

        protected override void SetupInternal()
        {
            // Restart the Stopwatch.
            _sw.Restart();

            // Reset the direction.
            _dir = 1;
        }

        protected override void RunInternal()
        {
            // Cache the elapsed millseconds from the Stopwatch.
            long elapsedMs = _sw.ElapsedMilliseconds;

            // Give 6s at the end of the period without clicking the GAME_OVER_OK button so that the bot won't accidentally buy power-ups when clicking LEVEL_UP_OK.
            if (elapsedMs < _periodTimeMs - 6000)
            {
                ClickGameOverOK();
            }

            // Sweep in the current direction and reverse the direction for the next iteration.
            Sweep(_dir * 0.1, 5, 10);
            _dir *= -1;

            // After a full period, click the LEVEL_UP_OK button and restart the Stopwatch for the next period.
            if (elapsedMs > _periodTimeMs)
            {
                ClickLevelUpOK();
                _sw.Restart();
            }
        }
    }
}
