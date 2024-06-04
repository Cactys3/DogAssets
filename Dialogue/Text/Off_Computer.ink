INCLUDE Globals.ink

{completed_computer:
-> AlreadyLearned
- else:
-> Learn
}


===Learn===
#name:dog #image:dog_growl #layout:dog1 sound:dog_growl
Grrrrrr...
#name:dog #image:dog_neutral #layout:dog1 sound:keyboard
..................
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator1
The dog has just researched online about keys, doors, and molds. <<wait:1> It's now very knolwedgable about such things.
~completed_computer = true
->END


===AlreadyLearned===
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator1
The dog learned about keys, doors, and molds here.
->END