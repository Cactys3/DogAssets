INCLUDE Globals.ink
#layout:up
#name:dog #image:dog_sniff #sound:dog_sniff
Sniff... Sniff...

{played_poop == false:
    ->FirstInteract
-else:
    #name:Narrator #image:narrator_neutral #sound:narrator
    It's the dog's poop
    {has_poop:
        You have a similar poop in your inventory.
    }
-> END
}



===FirstInteract===
#name:dog #image:dog_woof #sound:dog_woof
Woof! Woof!
#name:Narrator #image:narrator_neutral #sound:narrator
The dog can't tell if this poop is its own or of a foreign origin.
* [It looks foreign]
#name:dog #image:dog_woof #layout:dog1
Woof!
#name:narrator #image:narrator_default #layout:narrator1
The dog has decided to replace the poop with one of their own.
~ has_poop = true
-> END
* [It doesn't look foreign]
#name:dog #image:dog_woof #layout:dog1
Woof!
#name:narrator #image:narrator_default #layout:narrator1
The dog has decided to replace the poop with a fresh one.
~ has_poop = true
-> END
* [I can't tell]
#name:dog #image:dog_woof #layout:dog1
Woof!
#name:narrator #image:narrator_default #layout:narrator1
The dog has decided to replace the poop with one of their own.
~ has_poop = true
~ played_poop = true
-> END