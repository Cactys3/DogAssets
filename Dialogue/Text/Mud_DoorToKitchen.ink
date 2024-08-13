INCLUDE Globals.ink
#layout:up
#name:narrator #image:narrator_neutral #sound:narrator
Would you like to transpose yourself with the other side of this door?
 + [Yes]
 -> go
 + [No]
 -> dont


=== go ===
#scene:kitchen
-> END


=== dont ===
#name:narrator #image:narrator_neutral #sound:narrator
later then.
-> END
