# EverWingForever
EverWing on Facebook Messenger is a pretty fun game if you don't have to grind!

## In-Game Settings

1. Make sure that you have your DPI at 100% in your Windows settings.  This might be automatically set to something else if you have a high resolution display on a small laptop or a 4K monitor.  If you change the DPI, you may have to restart your computer for the coordinate computations to take effect.  Since it was not extensively tested on other DPI settings, it is probably easiest to just set the DPI to 100% for now.
2. Make sure that you turn the Movement Speed to LOW.  This sets the character movement to be 1:1 with the mouse movement in pixels.  Otherwise, the movement calculations will not be correct.
3. If the game is lagging, try running with LOW graphics settings in Chrome or Firefox.  From preliminary testing, running the bot had poor performance in Edge.

## Instructions

1. Set up the top/left/right/bottom positions of the game screen that you plan to bot in.  You must hard-code the defaults for your computer/settings in EverWingBot.cs, but please don't commit the changes to the default coordinates.
2. In the UI, select a bot to use.  It is recommended to just use the default Sweep Bot because it is probably the fastest for farming coins.
3. Press Ctrl + Alt + Shift + F5 to start the bot.  Ctrl + Alt + Shift + S was also added as a convenience shortcut for starting the bot.
4. Press Ctrl + Alt + Shift + F4 to stop the bot.  Ctrl + Alt + Shift + A was also added as a convenience shortcut for stopping the bot.  Note that it will need to finish the current iteration of RunInternal function before it will fully terminate the bot, so this may take some time depending on how that function is implemented.
