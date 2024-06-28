INCLUDE Globals.ink

#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
{completed_liv_doortostairs:
->Completed
- else:
-> TestKnowledge
}


===TestKnowledge===
{completed_computer:
->TryKey
- else:
-> FailedKnowledge
}

===FailedKnowledge===
#name:dog #image:dog_bark #layout:dog1 #sound:dog_bark_confused
Woof?
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
{has_tastykey || has_stinkykey:
Although you made a key, the dog doesn't know how to open doors.
Maybe try doing some research?
->END
}
The dog doesn't have a key for this door.
Not that they know how to open closed doors in the first place.
->END


===TryKey===
{has_tastykey || has_stinkykey:
Temporary text: unlock door with key?
* [try key]
~has_tastykey = false
~has_stinkykey = false
~completed_liv_doortostairs = true //TODO: this variable should unlock the door and make display the opening door animation
->Completed
* [leave]
->END
- else:
#name:dog #image:dog_woof #layout:dog1 #sound:dog_woof_2
Woof! Woof!
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
The dog is excited to use his knewly gained knowledge on door-opening.
#name:dog #image:dog_growl #layout:dog1 #sound:dog_growl
Growl...
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
But you don't have a key.
{played_DoorToStairs:
#name:dog #image:dog_woof #layout:dog1 #sound:dog_woof
Woof!
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
The dog wishes to make one with his molding skills.
- else:
~played_DoorToStairs = true
}
->END
}

===Completed===
Go through door?
*[yes]
#scene:Demo End
->END
*[no]
->END