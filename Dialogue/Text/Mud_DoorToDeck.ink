INCLUDE Globals.ink
#layout:up
#name:narrator #image:narrator_neutral #sound:narrator
Go to the balcony?
 * [Go]
{ unlocked_deck:
 -> go.unlocked //only can go through this door if unlocked deck
}
 -> go.locked
 * [Don't Go]
 -> dont


=== go === 
= unlocked
#scene:deck
-> END
= locked
#name:dog #image:dog_bark #sound:dog_bark
Bark! 
#name:narrator #image:narrator_neutral #sound:narrator
The dog doesn't seem to know how to operate this door.
-> END


=== dont ===
-> END
