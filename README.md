# connectron

Welcome to Connectron, a very over the top Connect 4 game!

## Install

1. Download [Connectron.zip](Connectron.zip)
2. Unzip it
3. Run Connectron.exe

## Setting up

-   Grid Size: The number of spaces on the board, X by Y, ranging from 3x3 to 100x100.
-   Number of players: The number of people playing the game, from 0 to 10.
-   Length of winning line: The number of counters you must get in a row to win, from 4 to the largest dimension of the grid.
-   Number of rounds: The number of Connectron games you wish to play continuously.
-   Players section: CPU checkbox: When ticked, a computer will take the turn of this player.
-   Colour label: Shows the colour of each player's counters. Click it to change the colour for this player.
-   Players textbox: Enter the player's name here.

## How to play

The bold player name on the right of the screen indicates whose turn it is.
Click anywhere in a column to place a counter in that column; it will fall as far down as it can. The aim is to get the previously specified number of counters (default is 4) in a continuous line, horizontally, vertically or diagonally.
The undo button, bomb counter button and start and end buttons are in the top right of the screen.

## Special rules

Corner counter count as more:

-   If the winning length is less than 7, counters placed in any of the four corners count as 2 counters.
-   If the winning length is 7 or more, counters placed in any of the four corners count as 3 counters.

Solitaire counters:

-   If a counter is completely surrounded by counters from one other player (or players who are allied together), the surrounded counter is destroyed.

Bomb counters:

-   When played, a bomb counter destroys itself and any counters it is touching when it falls.
-   Each player gets one counter per round.

Overflow full columns:

-   Can only be enabled for a grid which is at least 8 cells high.
-   If you place a counter which would fill up its column it is overflowed into columns on either side, unless this would fill up that column.

Alliances:

-   Each player can form alliances with other players. Allied counters count together, so allied players can win together.
-   Alliances can be changed at the start of each round.
