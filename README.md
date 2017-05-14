# EverWingForever
EverWing on Facebook Messenger is a pretty fun game if you don't have to grind!

## Getting the Code

1.  Download Git.  Since this program only works on Windows as far as I know, you might as well get Git for Windows.  You can just Google it and download and install the latest version.
2.  Navigate to the folder where you want to save the source code.  Git for Windows has a convenient integration with the right-click context menu option "Open Git Bash here" to make things easier.
3.  To get the repository the first time:
	a.  `git clone https://github.com/megaspazz/EverWingForever.git` Note that this will create a new folder in your current directory.
	b.  `cd EverWingForever` Navigate into the newly created folder.
3.  To get updates, make sure that you're already in the "EverWingForever" folder.
	a.  `git reset --hard HEAD` Remove all your local changes.  You might be do a `git stash` instead to save your local modifications for later.
	b.  `git pull origin master` Get the most recent version.

## In-Game Settings

1.  Make sure that you have your DPI at 100% in your Windows settings.  This might be automatically set to something else if you have a high resolution display on a small laptop or a 4K monitor.  If you change the DPI, you may have to restart your computer for the coordinate computations to take effect.  Since it was not extensively tested on other DPI settings, it is probably easiest to just set the DPI to 100% for now.
2.  Make sure that you turn the Movement Speed to LOW.  This sets the character movement to be 1:1 with the mouse movement in pixels.  Otherwise, the movement calculations will not be correct.
3.  If the game is lagging, try running with LOW graphics settings in Chrome or Firefox.  From preliminary testing, running the bot had poor performance in Edge.

## Instructions

1.  Set up the top/left/right/bottom positions of the game screen that you plan to bot in.  You must hard-code the defaults for your computer/settings in EverWingBot.cs, but please don't commit the changes to the default coordinates.
2.  In the UI, select a bot to use.  It is recommended to just use the default Sweep Bot because it is probably the fastest for farming coins.
3.  Press [Ctrl + Alt + Shift + F5] or [Ctrl + Alt + Shift + S] to start the bot.
4.  Press [Ctrl + Alt + Shift + F4] or [Ctrl + Alt + Shift + A] to stop the bot.  Note that it will need to finish the current iteration of RunInternal function before it will fully terminate the bot, so this may take some time depending on how that function is implemented.
5.  Press [Ctrl + Alt + Shift + D] to toggle the bot.  That is to say, it should start the bot if it is ready; otherwise, it will stop the bot.

## Tips

1.  Convenience Shortcuts are enabled by default.  These are keys that make it easier to use the program, but might interfere with normal usage of the desktop if you choose to leave the program open.
	a.  Press [X] to start the bot.
	b.  Press [Z] to stop the bot.
	c.  Press [C] to toggle the bot.
2.  The hotkey for toggling Convenience Shortcuts on and off is [Ctrl + Alt + Shift + F6].

## How to use the SweepAssistBot

This is basically just the sweeping part of the SweepBot that only sweeps and does not do anything to start a new round or close the Level Up popup window.

First, you can customize the SweepAssistBot using the constructor in MainWindow.xaml.cs for a few settings:
1.  The ending position determines where the SweepAssistBot will put the character when it stops.  A value of 0 is the far-left side and a value of 1 is the far-right side.  For example, I like using a value of 0.5 to put it right in the middle when it's done.
2.  There is a setting for whether or not to hold left button down at the end.  If set to false, then it will simply move your cursor to where the character is without holding down the buttons, so you will have to click and drag on your character.  If set to true, it will also hold down the left mouse button for you so that you can continue playing without having to click.  Personally, I prefer to have it automatically hold down the left mouse button because I find it to be more seamless with the bot; it also feels a little easier to play if you don't have to hold down the left mouse button the entire time.

There are two ways to drive the bot.  It's highly recommended that you use Convenience Shortcuts, since it makes it way easier to quickly control the bot.  The instructions below will assume that you use Convenience Shortcuts, although it is possible to use the other key combos if you wish.
1.  Use [X] and [Z] to start and stop the bot, respectively.
2.  Use [C] to toggle the bot on and off.  I find this method easier, since you only need to press one key.
