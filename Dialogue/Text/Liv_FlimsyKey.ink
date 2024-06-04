INCLUDE Globals.ink

{completed_flimsykey:
->END
}

//once the key is picked up, this ink story should be unreachable anyways as the gameobject should be deleted
#name:dog #image:dog_sniff #layout:dog1 #sound:dog_sniff
sniff... sniff...
#name:dog #image:dog_whine #layout:dog1 #sound:dog_whine
whinnnee...

#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
The dog has found a flimsy toy key.
~has_flimsykey = true
~completed_flimsykey = true