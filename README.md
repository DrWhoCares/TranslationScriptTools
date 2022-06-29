# TranslationScriptTools
A small set of utilities for creating Translation Scripts.
Program will automatically check for new releases on Github and prompt the user to install on startup.

## Table of Contents
* [Translation Script Maker](#translation-script-maker)
  * [Technical Details](#technical-details)
  * [Tutorial](#tutorial)
    * [Chapter Selection Window](#chapter-selection-window)
      * [Raws Location](#raws-location)
      * [Output Location](#output-location)
        * [Chapter Folder](#chapter-folder)
        * [With Raws](#with-raws)
        * [Custom Location](#custom-location)
      * [Translator Name](#translator-name)
      * [Series Selection](#series-selection)
      * [Chapter Selection](#chapter-selection)
    * [Script Editor Window](#script-editor-window)
      * [Input Controls](#input-controls)
        * [Page Selection](#page-selection)
          * [Saving on Page Change](#saving-on-page-change)
        * [Total Panels](#total-panels)
        * [Panels With SFX](#panels-with-sfx)
        * [Spread Checkbox](#spread-checkbox)
      * [Raws Viewer](#raws-viewer)
      * [Script Editor](#script-editor)
* [Notepad++ Theme](#notepad-theme)

## Translation Script Maker
### Technical Details
The Translation Script Maker is a utility built using C# and the .NET Framework (v4.7.2).

It also makes use of these packages:
- [CyotekImageBox v1.3.1](https://github.com/cyotek/Cyotek.Windows.Forms.ImageBox)
- [jacobslusser.ScintillaNET v3.6.3](https://github.com/jacobslusser/ScintillaNET)
- [Newtonsoft.Json v13.0.1](https://www.newtonsoft.com/json)
- [Onova v2.6.2](https://github.com/Tyrrrz/Onova)
- [WeCantSpell.Hunspell](https://github.com/aarondandy/WeCantSpell.Hunspell/)
- [WindowsAPICodePack-Core v1.1.2](https://github.com/aybe/Windows-API-Code-Pack-1.1)
- [WindowsAPICodePack-Shell v1.1.1](https://github.com/aybe/Windows-API-Code-Pack-1.1)

The dark theme of the ScriptViewer is based off of the Notepad++ theme [VS2015-Dark](https://github.com/Ludomancer/VS2015-Dark-Npp).

### Tutorial
#### Chapter Selection Window
![image](https://user-images.githubusercontent.com/12800223/66271532-9d7a1080-e824-11e9-8424-13871f4fb2e7.png "Chapter Selection Window Example")

This is the first window you'll see when you start the program. If you've already opened it before, a configuration file will be created and your previous choices will be remembered.

#### Raws Location
![image](https://user-images.githubusercontent.com/12800223/66271610-b2a36f00-e825-11e9-83ec-5f28a384ab7a.png "Raws Location Example")

The Raws Location should be some folder that contains all of your titles. The folders' names can be anything, but the contents of those folders have a few restrictions. More on that in the [Series Selection](#series-selection) and [Chapter Selection](#chapter-selection) sections.

#### Output Location
**Note:** The Output Location is also the location used to open an existing file.
##### Chapter Folder
This is the default Output Location. It will output the script to within the Chapter folder. If you place your Raws directly within the Chapter folder (not within a subfolder), then this will have the same effect as the With Raws option.
##### With Raws
This option will output the script to the same location as the Raws. If you place your Raws directly within the Chapter folder (not within a subfolder), then this will have the same effect as the With Raws option.
##### Custom Location
This option will clear the Output Location box and allow you to click the button next to it to select a folder to output the script to.

#### Translator Name
The Translator Name is the name that will be added to the script file's name when output. A script file will be output with a name like so: `Ch X - TL TRANSLATOR_NAME.txt`, where 'X' will be replaced by the selected Chapter number, parsed from the Chapter Folder name.

#### Series Selection
The Series Selection ComboBox will automatically parse the folders within the [Raws Location](#raws-location), allowing you to select any valid folder. A folder is considered valid if it meets the following requirements:
* *(Optional)* Contains one or more folders matching the Regex Expression: `Vol(ume)?.? *([0-9]+$)`.
  * e.g., "Volume 12", "Vol12", "Volume_12", etc.
  * If a Volume Folder is included, the valid Chapter Folders must be within them.
* A Title Folder must contain one or more valid Chapter Folders which have the following requirements:
  * Chapter folders must match the following Regex Expression: `Ch(apter)?.? *([0-9]+([.,][0-9]+)?$)`.
    * e.g., "Chapter 1", "Ch 1", "Ch_1.5", etc.
  * Chapter folders must either contain a folder for raws, or contain the raws themselves.
    * Raws must be in one of the following formats: `png, jpg, jpeg`.
    * A folder containing raws must match the following Regex Expression: `Raw(s)?`.
      * e.g., "Raw" or "Raws"

**NOTE:** *It's strongly recommended to have your files named properly to have them sorted correctly. File names such as `001.png` are best. For Spreads, if the name is `X-Y.png` where X is replaced by the first filename, and Y is replaced by the second filename (before you merged them), then it will automatically be marked as a spread. For example, `15-16.png`.*
    
#### Chapter Selection
The Chapter Selection ComboBox will automatically be filled using the valid chapter folders that were parsed. In the case of a chapter folder being within a Volume folder, the name displayed will be shown as `VOLUME_FOLDER\CHAPTER_FOLDER`.

#### Script Editor Window
![image](https://user-images.githubusercontent.com/12800223/66272518-52fe9100-e830-11e9-9394-cfd83cad0b9f.png "Script Editor Window Example")

This is the window that is used for both creating and editing a Translation Script. The top is the [Input Controls](#input-controls), the left is the [Raws Viewer](#raws-viewer), and the right is the [Script Editor](#script-editor).

#### Input Controls
The Input Controls are how you'll be automatically generating the formatting syntax for the script, or navigating the different pages.

##### Page Selection
![image](https://user-images.githubusercontent.com/12800223/66272589-429ae600-e831-11e9-872f-a42c668177dd.png "Page Selection Example")

When first creating a script, pages that have not been saved yet will be bolded and marked with an asterisk in the Page Selection ComboBox. You can select any page to instantly jump to that page.
You can use the left and right arrow keys to move forward and back a page, so long as you aren't currently selected on the Script Editor. I personally recommend creating the file first, by using the numpad to fill out the amount of panels (and mouse to do SFX), and the arrow keys to move forward, before going back through and filling out the rest of the script.

###### Saving on Page Change
**Note:** Any time you switch pages, the script file will be saved and output in it's current entirety. You can also save manually using the keyboard shortcut `Ctrl + S`. Any time there are unsaved changes, the window caption will have an asterisk.
![image](https://user-images.githubusercontent.com/12800223/66272738-2730da80-e833-11e9-9fd0-98a68e5fb010.png "Unsaved Changes Example")

##### Total Panels
![image](https://user-images.githubusercontent.com/12800223/66272603-84c42780-e831-11e9-814e-8a0be9f68be6.png "Total Panels Example")

When the Total Panels TextBox is changed, it will automatically add or remove Panels from the Script Editor. Currently you can have up to 12 Panels on a single page (to be changed in a future refactor). This is because it will also add or remove the Panels with SFX checkboxes accordingly.
**Note:** If the Total Panels is less than the current amount, it will remove the extra Panels, and __all__ of their contents.

##### Panels With SFX
![image](https://user-images.githubusercontent.com/12800223/66272618-aa513100-e831-11e9-949b-5699d1145662.png "Panels with SFX Example")

If you select a checkbox for a given Panel, it will automatically insert the SFX subsection formatting syntax into that Panel, at the end of the Panel, preserving any existing text in the Panel. Unchecking a box will remove the SFX subsection **__and everything inside of it__**.

##### Spread Checkbox
![image](https://user-images.githubusercontent.com/12800223/66272625-d79ddf00-e831-11e9-8edc-c77e1331625f.png "Spread Checkbox")

In the case that your file was not [automatically parsed as a spread](#saving-on-page-change), you can manually toggle the page's status as a spread with this checkbox. Doing so will change the formatting syntax for the page headers, offsetting pages that come after it accordingly.

#### Raws Viewer
The Raws Viewer defaults to being zoomed out as far as possible, while retaining the correct size ratio. You can use the mousewheel to scroll in or out. While zoomed in, you can click-and-drag to move around, or use the scroll bars.

#### Script Editor
This is where you will actually edit your script, adding in any relevant transcriptions or translations.
You are unable to edit any of the formatting syntax lines. Eventually the handling of the formatting syntax lines will be refactored to be handled better.
You can add in templated formatting syntax by using the right-click menu and selecting the option you want.
![image](https://user-images.githubusercontent.com/12800223/66276382-1e063480-e858-11e9-93ef-ad07da707c6a.png "Context Menu Example")

The Script Editor uses ScintillaNET, and as such, has a number of built-in keyboard shortcuts by default. Such as using Ctrl + Mousewheel to zoom in and out.

## Notepad++ Theme
The Notepad++ Dark Theme that I used is located here: https://github.com/Ludomancer/VS2015-Dark-Npp and provided in this repo with the same name, "VS2015-Dark.xml".
To install, see that link, or otherwise drop into the location at: `%APPDATA% -> Notepad++ -> themes` then switch to it via `Settings -> Style Configurator... -> Select theme: VS2015-Dark`.

If you open the file with Notepad++ for the first time, you might not see any styling. If this is the case, you haven't imported the TLS language into Notepad++ yet.
Click on "Language" at the top of Notepad++ and click "Define your language..." at the bottom. Then click "Import" on the left.
Import the file that comes with the executable and you'll be set.

Anytime you want a file to be styled using this syntax (and it's not done automatically) do the following:
1. Click "Language" at the top of Notepad++
2. Click "TLS" at the bottom of the menu
3. You're done, until you close the file using the X box on the file tab, it will save your language choice.
You can freely close Notepad++ without having to set the language again, unless you close the actual tab.

**NOTE:** Files opened with the extension of ".txt" will automatically use the TLS formatting. To change this, go to 'Define your language...' and change the language to TLS, then change the 'Ext' box to something else, or make it empty.

**Keywords to note:**
```
{           ==> Opening Code Block (For collapsing sections)
}           ==> Closing Code Block (For collapsing sections)
Page        ==> Page Keyword (For highlighting)
Panel       ==> Panel Keyword (For highlighting)
----------# ==> Opening Page Block (For highlighting)
#---------- ==> Closing Page Block (For highlighting)
---#        ==> Opening Panel Block (For highlighting)
#---        ==> Closing Panel Block (For highlighting)
[SFX]       ==> SFX Keyword (For highlighting)
[T/N]       ==> Translation Note (For highlighting)
``          ==> Single Line Comment/In-line Comment (For highlighting, second backtick is optional)
(T/P:)      ==> Single/Multi-line Comment (For highlighting, meant for notes to the Proofreader)
```

---
Any questions, feel free to contact me or create an issue (which is really helpful for keeping track of things to change or add).
