using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverWingForever
{
    abstract class PeriodBot : EverWingBot
    {
        // The number of milliseconds in one period.
        private double _periodTimeMs;

        // Stopwatch to keep track of the current time in the period.
        // During the last 6 seconds of the period, the bot will not click the LEVEL_UP_OK button.
        // After each period, the bot will click the LEVEL_UP_OK button once.
        private Stopwatch _swPeriod = new Stopwatch();

        // The stopwatch for keeping track of when to click GAME_OVER_OK.
        private Stopwatch _swGameOver = new Stopwatch();

        // Currently, the the number of games to level up is hard-coded.
        private int _gamesToLevelUp = 100;

        public PeriodBot()
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

        protected override sealed void SetupInternal()
        {
            // Restart the Stopwatches.
            _swPeriod.Restart();
            _swGameOver.Restart();

            // Call child setup 
        }

        protected override sealed void RunInternal()
        {
            // Cache the elapsed millseconds from the Stopwatches.
            long periodMs = _swPeriod.ElapsedMilliseconds;
            long gameOverMs = _swGameOver.ElapsedMilliseconds;

            // Click the GAME_OVER_OK button at most every half-second.
            // Give 6s at the end of the period without clicking the GAME_OVER_OK button so that the bot won't accidentally buy power-ups when clicking LEVEL_UP_OK.
            if (gameOverMs >= 500 && periodMs < _periodTimeMs - 6000)
            {
                ClickGameOverOK();
                _swGameOver.Restart();
            }

            RunPeriod();

            // After a full period, click the LEVEL_UP_OK button and restart the Stopwatch for the next period.
            if (periodMs > _periodTimeMs)
            {
                ClickLevelUpOK();
                _swPeriod.Restart();
            }
        }

        /// <summary>
        /// Performs setup necessary in children classes.  This is analogous to the EverWing.SetupInternal method.
        /// </summary>
        protected virtual void SetupPeriod() { }

        /// <summary>
        /// Child classes must override this to determine the bot's behavior.  This is analogous to the EverWing.RunInternal method.
        /// </summary>
        protected abstract void RunPeriod();
    }
}
