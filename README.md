# SizeCalculator Playnite Extension
A Game Installation Directory Size Calculator for Playnite library manager.

Get Playnite at [Playnite.link](https://playnite.link/)

[Forum Page](https://playnite.link/forum/thread-280.html)

## Features:
- Scans library for all installed games with Installation Directory set up
- Calculates size of the directory
- Saves it into the Age rating field
- Enables sorting and filtering by size
- Option to round size up or down
- Option to calculate "Size on disk", when you use compression

## Installation:

- Download the archive
  - Latest version: [SizeCalculator.zip](https://github.com/Gerren/SizeCalculator/blob/master/SizeCalculator.zip)
- Unpack the archive into your Playinite/Expansion folder
- Restart Playnite

## Usage:

In the menu select Extensions/Calculate sizes. After that you can sort your library by Age rating, descending to find the game that takes up the most space.

The size is formatted as "005 GB", rounded to whole number, by default. The leading zeros are used to easier sorting and filtering.

The games, that do not have the Installation direcotery set up, or the calculation leads to error, the field is not set up.

Options are available in Settings/Plugins/Size Calculator.

## Known issues:
- The size is stored in Age rating field. If you alerady use the Age rating field, be prepared to lose it.
- If you use Age rating, the filter will show all the values.
- Currently, the extension does not take in mind the drive the game is installed on. If have games installed on multiple drives and seek to free up space, you shall pick and choose from results.
- As of Playnite version 7.7, there is no posibility to filter a value out (exclude it). There is no way to select all too. Eg. the only way to filter "all but None" values, one must check all individual values, with the excepiton of None. Which can be tedious.
- Size on Disk option slows the process and can cause the Playnite to freeze for a short period. This is due to kernel32.dll calls, which are used to determine size on disk.

## More info:
- As of Playnite version 7.7, there is no way to find out game sizes.
- The usual ways to find game sizes lead nowhere, because of all the clients and libraries. The directory tree is not making it any easier.
- The Age rating field was used, because:
  - it was underutilized
  - IGDB does not provide this info anyway
  - it supports fitering and sorting
  
# Changes:
- Version 1.1
  - Sizes are stored in the database
  - Ability to calculate Size on Disk
  - Custom rounding
- Version 1.2
  - Now is a GenericPlugin (will not show amongst libraries)
  - Ability to Calculate only games with no size calculated.
  - Support for Emulated games.

# Images:

![Calculate Sizes](/Playnite_calculate.png)

![Sort](/Playnite_sort.png)

![Filter](/Playnite_filter.png)
