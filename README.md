# SizeCalculator Playnite Extension
A Game Installation Directory Size Calculator for Playnite library manager.

Get Playnite at [Playnite.link](https://playnite.link/)

[Forum Page](https://playnite.link/forum/thread-280.html)

## Features:
- Scans library for all installed games with Installation Direcotry set up
- Calculates size of the directory
- Saves it into the Age rating field
- Enables sorting and filtering by size

## Installation:

- Download the archive
  - Latest version: [SizeCalculator.zip](https://github.com/Gerren/SizeCalculator/blob/master/SizeCalculator.zip)
- Unpack the archive into your Playinite/Expansion folder
- Restart Playnite

## Usage:

In the menu select Extensions/Calculate sizes. After that you can sort your library by Age rating, descending to find the game that takes up the most space.

The size is formated as "005 GB", rounded to whole number. The leading zeros are used to easier sorting and filtering.

The games, that do not have the Installation direcotery set up, or the calculation leads to error, the field is not set up.

## Known issues:
- The size is stored in Age rating field. If you alerady use the Age rating field, be prepared to lose it.
- If you use Age rating, the filter will show all the values.
- Playnite does not exactly like having field values edited at runtime. Sometimes the values can seem to be doubled or sorted incorrectly. This corrects itself on the next Playnite session.
- The sorting does not refresh on recalculation. Just change sorting and change it back.
- Currently, the extension does not take in mind the drive the game is installed on. If have games installed on multiple drives and seek to free up space, you shall pick and choose from results.
- This extension calculates all files recursively, but beware - this version does not include Image, ROM and ISO.
- As of Playnite version 7.7, there is no posibility to filter a value out (exclude it). There is no way to select all too. Eg. the only way to filter "all but None" values, one must check all individual values, with the excepiton of None. Which can be tedious.


## More info:
- As of Playnite version 7.7, there is no way to find out game sizes.
- The usual ways to find game sizes lead nowhere, because of all the clients and libraries. The directory tree is not making it any easier.
- The Age rating field was used, because:
  - it was underutilized
  - IGDB does not provide this info anyway
  - it supports fitering and sorting

# Images:

![Calculate Sizes](/Playnite_calculate.png)

![Sort](/Playnite_sort.png)

![Filter](/Playnite_filter.png)
