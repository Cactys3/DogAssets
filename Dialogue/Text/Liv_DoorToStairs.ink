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
-> END
}

===TryKey===
{has_tastykey || has_stinkykey:
Temporary text: unlock door with key?
* [try key]
~has_tastykey = false
~has_stinkykey = false
~completed_liv_doortostairs = true //TODO: this variable should unlock the door and make display the opening door animation
->END
* [leave]
->END
- else:
You don't have a key!
->END
}

===Completed===
Open door?
*[yes]
#scene:next
->END
*[no]
->END