INCLUDE Globals.ink
#name:dog #image:dog_woof #layout:dog1
Woof! Woof!
#name:dog #image:dog_sniff #layout:dog1
Sniff... Sniff...
#name:narrator #image:narrator_default #layout:narrator1
The dog can't tell if this poop is its own or of a foreign origin.
* [It looks foreign]
#name:dog #image:dog_woof #layout:dog1
Woof! //TODO: make poop sound here
#name:narrator #image:narrator_default #layout:narrator1
The dog has decided to replace the poop with one of their own.
~ has_poop = true
-> END
* [It doesn't look foreign]
#name:dog #image:dog_woof #layout:dog1
Woof! //TODO: make poop sound here
#name:narrator #image:narrator_default #layout:narrator1
The dog has decided to replace the poop with a fresh one.
~ has_poop = true
-> END
* [I can't tell]
#name:dog #image:dog_woof #layout:dog1
Woof! //TODO: make poop sound here
#name:narrator #image:narrator_default #layout:narrator1
The dog has decided to replace the poop with one of their own.
~ has_poop = true
-> END