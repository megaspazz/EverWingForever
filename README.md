# EverWingForever
EverWing on Facebook Messenger is a pretty fun game if you don't have to grind!

## Getting the Code

1.  Download Git.  Since this program only works on Windows as far as I know, you might as well get Git for Windows.  You can just Google it and download and install the latest version.
2.  Navigate to the folder where you want to save the source code.  Git for Windows has a convenient integration with the right-click context menu option "Open Git Bash here" to make things easier.
3.  To get the repository the first time:
    1.  `git clone https://github.com/megaspazz/EverWingForever.git` Note that this will create a new folder in your current directory.
    2.  `cd EverWingForever` Navigate into the newly created folder.
4.  To get updates, make sure that you're already in the "EverWingForever" folder.
    1.  `git reset --hard HEAD` Remove all your local changes.  You might be do a `git stash` instead to save your local modifications for later.
    2.  `git pull origin master` Get the most recent version.

## Just the Executable

Want to just run the program instead of pulling the code for the entire repository?  You can [download the 64-bit version here](https://app.box.com/s/552uuufa8f9dobmugv88u2mii33aq4je).  If you have a 32-bit computer, you can [download the 32-bit version here](https://app.box.com/s/jnxmrt71vvrv8x8fryi36trkxv4wv4nn).  You will have to extract the ZIP file, and make sure that you keep the EXE and DLL files together in the same folder when you run the program.

These pre-built binaries should be sufficient for most people, but do note that these binaries may be slightly out of date with the repository, so if you want the latest and the greatest, you should get the code and build from source yourself.  

To update your executable, just download from the links again, but there aren't any official version numbers.

## Updates

Currently, there are no automatic updates, so if the program doesn't seem to work anymore, please update using the instructions above.  The game itself sometimes receives updates, so that means that the bot might stop working if the UI changes enough.  I will try to keep it up-to-date for now, but if I'm slow to come out with a patch, feel free to submit an issue on the Github repo.

## In-Game Settings

1.  Make sure that you have your DPI at 100% in your Windows settings.  This might be automatically set to something else if you have a high resolution display on a small laptop or a 4K monitor.  If you change the DPI, you may have to restart your computer for the coordinate computations to take effect.  Since it was not extensively tested on other DPI settings, it is probably easiest to just set the DPI to 100% for now.
2.  Make sure that you turn the Movement Speed to LOW.  This sets the character movement to be 1:1 with the mouse movement in pixels.  Otherwise, the movement calculations will not be correct.
3.  If the game is lagging, try running with LOW graphics settings in Chrome or Firefox.  From preliminary testing, Edge had poor performance running the bot.

## Set the Coordinates of the Game Region

It is required to configure the coordinates the first time you use the program, and also if you move the game window.  It's easiest to just configure it once in a maximized browser window and just play the game in that exact same position every time so you can just set and forget it, since the bot will remember the last-used settings.

1.  Use the following to set preliminary bounds for game.  Make sure that these are close to the game, but it's OK if they are a little outside the game.  However, they *cannot* be inside the game region itself, or the auto-adjust shrink later will fail.
    * [Ctrl + Alt + Shift + I] will set the top coordinate to the current Y position of your mouse cursor.
	* [Ctrl + Alt + Shift + J] will set the left coordinate to the current X position of your mouse cursor.
	* [Ctrl + Alt + Shift + K] will set the right coordinate to the current X position of your mouse cursor.
	* [Ctrl + Alt + Shift + M] will set the bottom coordinate to the current Y position of your mouse cursor.
	* [Ctrl + Alt + Shift + U] will set the top and left coordinates to the XY position of your mouse cursor.
	* [Ctrl + Alt + Shift + ,] will set the bottom and right coordinates to the XY position of your mouse cursor.
2.  When you have set an initial estimate for the bounds, click the "Shrink to Fit" button to automatically adjust the game region to exactly fit the game.  Note that this requires that the bounds you set earlier fully contain the game, such that none of the coordinates are within the game region.  Additionally, the game must be displaying something bright, without any other windows covering up the game area.  For best results, just do it from the main game screen.

NOTE:  There is currently a known issue that sometimes the character won't move or will be stuck on one side after using "Shrink to Fit".  This is because one of the border column of pixels is actually not responsive to user input.  To fix this, you can manually adjust the left and right boundaries to be barely within the game region using the shortcuts above.  After you manually set the boundaries, don't click on "Shrink to Fit" or it may mess up your settings.

NOTE:  The input for the coordinates are intentionally not editable.  This is because I didn't want to write any validation code for user input.  Perhaps this will change in a future release, but I think it's easier just to use the shortcuts anyway.

## Instructions

1.  In the UI, select a bot to use.  It is recommended to just use the default Slide Bot because it is probably the fastest for farming coins.
2.  Press [Ctrl + Alt + Shift + F5] or [Ctrl + Alt + Shift + S] to start the bot.
3.  Press [Ctrl + Alt + Shift + F4] or [Ctrl + Alt + Shift + A] to stop the bot.  Note that it will need to finish the current iteration of RunInternal function before it will fully terminate the bot, so this may take some time depending on how that function is implemented.
4.  Press [Ctrl + Alt + Shift + D] to toggle the bot.  That is to say, it should start the bot if it is ready; otherwise, it will stop the bot.

## Tips

1.  Convenience Shortcuts are enabled by default.  These are keys that make it easier to use the program, but might interfere with normal usage of the desktop if you choose to leave the program open.
    1.  Press [X] to start the bot.
    2.  Press [Z] to stop the bot.
    3.  Press [C] to toggle the bot.
2.  The hotkey for toggling Convenience Shortcuts on and off is [Ctrl + Alt + Shift + F6].
3.  If you are already at the level cap of 50, you can change the `_gamesToLevelUp` variable to some arbitrarily high number so that it won't ever waste time trying to click the OK button in the Level Up popup.  This is a very small micro-optimization that probably doesn't save that much time, though.

## Farming Help

1.  It probably goes without saying, but you should use Lily (2x Coins) for farming.
2.  Since the bot usually dies to the first meteor, most of the enemies encountered will be very weak, so it's recommended that you just upgrade your gun really high so you can easily clear everything.
3.  Certain dragons are more efficient for farming than others.  Since the enemies are weak, it doesn't really matter what dragons you use if your gun level is high enough.  I think these effects are pretty good:
    * Item Drop Rate:  More items is obviously better.  This bonus also always comes with the Item Duration bonus.
	* Rush Flower Drop Rate:  The bot makes really good use of Rush Flowers because it clears everything quickly and collects all the drops.  It is also the bot's only real chance of avoiding meteors.
    * Double Gems:  Gems actually make up a decent amount of the income, perhaps around 20%.  However, I'm not sure if this one is as good as the Rush Flower.
	* Item Duration:  Items lasting longer helps increase your income, especially the Magnet and the Rush Flower.  It's better if you can just get this one with the Item Drop Rate if you have the proper Legendaries.
4.  The maximum number of coins you can hold at any given time is 999,999.  You will want to make sure you stop the bot and spend your coins before you reach this amount.
5.  To maximize the chances of hatching legendaries, you should try to spend as many coins at one time as possible, restarting the game every time you get a legendary to click on the "Claim Bonus" link that is automatically generated in the chat.  Every time you click that, it gives you a +5% chance to get Legendaries for a short time, so you can stack them the multiplier by yourself by hatching a large number of dragons at one time.  You can actually even hatch a couple more by having completed Adventure quests without claiming the rewards.

There is a [spreadsheet](https://docs.google.com/spreadsheets/d/1P8jmvRxwRBXb2tFHHe7KKVPEBCCd-pJBxGmQ7BIB410/edit?usp=sharing) for seeing what combination is the best for farming.  It seems like the combination of the Fea line (+40% Rush Flowers) and the Xiaolong/Skout lines (+40% Item Drop Rate and Item Duration) have the highest income.  The Avi/Umbra lines (2x gems) seem to be decent replacements for the Fea line sidekick.

## How to use the AssistBots

This is basically just the movement part of the SweepBot and SlideBot.  They only move around and do not do anything to start a new round or close the Level Up popup window.  Stopping the AssistBots should also be a little more responsive than their farming counterparts, although it may be unnoticeable.

First, you can customize the AssistBots using the constructors in MainWindow.xaml.cs for a few settings:
1.  The ending position determines where the Assist will put the character when it stops.  A value of 0 is the far-left side and a value of 1 is the far-right side.  For example, I like using a value of 0.5 to put it right in the middle when it's done.
2.  There is a setting for whether or not to hold left button down at the end.  If set to false, then it will simply move your cursor to where the character is without holding down the buttons, so you will have to click and drag on your character.  If set to true, it will also hold down the left mouse button for you so that you can continue playing without having to click.  Personally, I prefer to have it automatically hold down the left mouse button because I find it to be more seamless with the bot; it also feels a little easier to play if you don't have to hold down the left mouse button the entire time.

There are two ways to drive the bot.  It's highly recommended that you use Convenience Shortcuts, since it makes it way easier to quickly control the bot.  The instructions below will assume that you use Convenience Shortcuts, although it is possible to use the other key combos if you wish.
1.  Use [X] and [Z] to start and stop the bot, respectively.
2.  Use [C] to toggle the bot on and off.  I find this method easier, since you only need to press one key.

## Live Demo

This isn't going to be online 24/7, but if you're lucky, you might be able to see what the bot does on [my stream](https://www.twitch.tv/megaspazz).
