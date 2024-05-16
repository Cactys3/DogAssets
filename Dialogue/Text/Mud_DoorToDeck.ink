INCLUDE Globals.ink

Go Through Door?
 * [Go]
{ unlocked_deck:
 -> go.unlocked //only can go through this door if unlocked deck
}
 -> go.locked
 * [Don't Go]
 -> dont


=== go === //later add substitches for if dog can speak
= unlocked
#scene:deck
-> END
= locked
{ unlocked_speech:
    it's door... //dog
- else: 
    Bark! //dog
}
{ not unlocked_speech:
    Bark! //dog
}
The dog doesn't seem to know how to operate this door. //narrator
-> END


=== dont ===
This saddens the door.
-> END
