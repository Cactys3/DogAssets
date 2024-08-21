INCLUDE Globals.ink

#layout:down
#name:Narrator #image:narrator_neutral #sound:narrator
It's a cabinet stock full of bits and odds and pieces and ends and knicks and knacks!
Do you want to take something from it?
*[yes]
{completed_liv_cabinet:
You reach in,<<wait:0.2> grab,<<wait:0.2> and feel something.<<wait:0.2>.<<wait:0.2>.
so incredibly useless that you shove it so far back into the dark depths of the cabinet that it will never again feel the warmth of the incandescent bulbs lighting the room.
-else:
~has_clay = true
~completed_liv_cabinet = true
You reach in,<<wait:0.2> grab,<<wait:0.2> and feel something surround your fingers.<<wait:0.2>.<<wait:0.2>.
It's clay! The kind that's supposed to be wet and moldable.
}
->END
*[no]
->END
