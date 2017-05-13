# EverWingForever
EverWing on Facebook Messenger is a pretty fun game if you don't have to grind!

## Instructions

1. Set up the top/left/right/bottom positions of the game screen that you plan to bot in.  These will reset to the defaults every time you restart the program, so if it's annoying to input them every time, you can just hard-code the defaults for your computer/settings in EverWingBot.cs.
2. Change the type of bot you want to run in MainWindow.xaml.cs (currently there is only the StationaryBot).  Please don't commit the changes to the default bot type.
3. Press Ctrl + Alt + Shift + F5 to start the bot.
4. Press Ctrl + Alt + Shift + F4 to stop the bot.  Note that it will need to finish the current iteration of RunInternal function before it will fully terminate the bot, so this may take some time depending on how that function is implemented.
