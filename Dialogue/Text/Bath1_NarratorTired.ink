INCLUDE Globals.ink
#layout:down
#name:Narrator #image:narrator_neutral #sound:narrator

{played_narratortired:
-> played
- else:
I'm kinda tired.
You know what?<<wait:1> For the rest of this room,<<wait:0.3> I'm not going to narrate if you're interacting with something unimportant
~played_narratortired = true
->END
}

===played===
{narratortired_bool:
Don't worry about that.
~narratortired_bool = false
- else:
That's unimportant.
~narratortired_bool = true
->END
}



->END

