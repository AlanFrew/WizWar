WizWar
======

This is a computer simulation of the actual board game WizWar. Rules can be found online.

I wrote this program in college (4 years ago). Pretty much every part of the code is terrible and the game barely runs,
but it may comfort you to see proof that

-I have experience with OOP and large projects
-I can run a large project by myself and put a ton of hours into it
-I produced an almost-viable product despite massive inexperience and absurd expectations.

Basic Instructions:

-The game has a board and control panel for each player. It is in hotseat mode for debugging purposes. You start as Blue.
-Move using the arrow keys.
-You can see the current UI state for each player on their board, which will clue you into what the game is expecting.
-It is possible to finish a game, although you can easily get stuck.


Please excuse whatever bugs and profanity you may encounter. Some areas of interest in the code:

1. The event dispatcher supports covariance in C# 3.0. Events come in a class hierarchy and listen for a parent-type event
will subscribe you to children of that type as well. A simple concept logically, but very difficult to convince the compiler
to accept. C# solved this problem in 4.0 by introducing "in" and "out" for co-/contra-variance--equivalent to wildcards
in Java. The event dispatcher as a whole is an example of the Observer pattern.
2. The UI uses the State pattern with definied transitions to enable various buttons at the right time in a somewhat
organized way.
3. The Board class has some code for reading in a map based on an associated text file. This allows players to alter the
placement of walls and so forth.

Good luck!
