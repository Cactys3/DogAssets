INCLUDE Globals.ink
#layout:up
#name:narrator #image:narrator_neutral #sound:narrator
{played_doortokitchen:
Go to the kitchen?
-else:
Would you like to transpose yourself with the other side of this door?
}
 * [Yes]
 -> go
 * [No]
 -> dont


=== go ===
~played_doortokitchen = true
#scene:kitchen
-> END


=== dont ===
#name:narrator #image:narrator_neutral #sound:narrator
Doors have feelings too, you know.
-> END
