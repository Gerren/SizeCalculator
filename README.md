# SizeCalculator
A Game Installation Directory Size Calculator

## Features:
- Scans library for all installed games with Installation Direcotry set up
- Calculates size of the directory
- Saves it into the Age rating field
- Enables sorting and filtering by size

## Installation:

- Download the archive
  - Latest version: https://github.com/Gerren/SizeCalculator/blob/master/SizeCalculator.zip
- Unpack the archive into your Playinite/Expansion folder
- Restart Playnite

## Usage:

In the menu select Extensions/Calculate sizes. After that you can sort your library by Age rating, descending to find the game that takes the most space.

The size is formated as "005 GB", rounded to whole number. The leading zeros are used to easier sorting and filtering.

## More info:
- As of version 7.7, there is no way to find out game sizes.
- The usual ways to find game sizes lead nowhere, because of all the clients and libraries. The directory tree is not making it any easier.
- The Age rating field was used, because:
  - it was underutilized
  - IGDB does not provide this info anyway
  - it supports fitering and sorting
- This extension calculates all files recursively, but beware - this version does not include Image, ROM and ISO.
- Of course, if you alerady use the Age rating field, be prepared to lose it.
- The games, that do not have the Installation direcotery set up, or it leads to error, the field is not set up.
- Currently, the extension does not take in mind the drive the game is installed on. If have games installed on multiple drives and seek to free up space, you shall pick and choose from results.
