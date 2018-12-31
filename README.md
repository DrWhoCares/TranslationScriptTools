# TranslationScriptTools
A small set of utilities for creating Translation Scripts

An example tutorial with images can be found here: https://imgur.com/a/BAyoz8E

Translation Script Tools is currently just an executable and a Notepad++ language styler file.
Later, I might make better tools that automate more of the process. We'll see as time goes on.

The executable can be placed and run anywhere. Requires Windows, as it uses C# and the Microsoft .NET framework.
Each Chapter needs it's own folder, named "Chapter X" where X is replaced with the chapter number.
X can be any value 0 to 9999 with the additonal option of ".Y" where Y is any value 0 to 99.
Inside of the Chapter X folder should be a folder named "Raws". The final script will be output to the Chapter X directory.

You need to create the folder before running this program, but the file will be created by the program.
The program will parse the total number of pages and display the raws for you to fill out the information.
Simply enter the number of panels (subjective to your preference on what is a panel), and optionally select the SFX boxes if you want to add an "SFX Section".

You can use the arrow keys to go forward and back. My personal preference is to use the num pad and arrow keys with my left hand, and mouse with right.

If you open the file with Notepad++ for the first time, you wont see any styling. I might be able to fix that later, but for now, do it manually.
Click on "Language" at the top of Notepad++ and click "Define your language..." at the bottom. Then click "Import" on the left.
Import the file that comes with the executable and you'll be set. Anytime you want a file to be styled using this syntax do the following:

1. Click "Language" at the top of Notepad++
2. Click "TLS" at the bottom of the menu
3. You're done, until you close the file using the X box on the file tab, it will save your language choice.
You can freely close Notepad++ without having to set the language again, unless you close the actual tab.

Any questions, contact me.