using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverWingForever
{
    /// <summary>
    /// These bots are designed to help the player play the game instead of looping forever and starting new rounds.
    /// They have two primary behaviors when they are stopped:
    ///   * They put the character at a certain position every time so that the player knows where to find it to resume manual play.
    ///   * They can hold down the left mouse button upon returning control to the player.
    /// </summary>
    abstract class AssistBot : EverWingBot
    {
        // This setting determines the position the SweepAssistBot will place the character after it is done.
        private double _endPosition;

        // This setting determines whether the SweepAssistBot will automatically hold down the left mouse button when it is done.
        private bool _leftDown;

        // Default constructor currently doesn't need to do anything.
        public AssistBot() { }

        public AssistBot(double endPosition, bool leftDown)
            : this()
        {
            _endPosition = endPosition;
            _leftDown = leftDown;
        }

        protected override sealed void FinishInternal()
        {
            // Move the character to the position specified.
            MoveLeft(1);
            MoveRight(_endPosition);

            // Hold the left mouse button down depending on the setting.
            if (_leftDown)
            {
                InputWrapper.LeftDown();
            }

            // Perform other Finish actions defined by the child classes.
            FinishAssist();
        }

        /// <summary>
        /// Child classes should override this to define additional behavior for when the bot is finished running.  This is analogous to the EverWingBot.FinishInternal method.
        /// </summary>
        protected virtual void FinishAssist() { }
    }
}
