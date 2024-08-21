INCLUDE Globals.ink
#layout:up

#name:dog #image:dog_bark #sound:dog_bark
Bark! <<wait:0.5> Bark! 

#name:narrator #image:narrator_neutral #sound:narrator
This door leads outside!
Alas, the dog cannot reach the handle..
{played_doortodeck == false:
Nor can we be certain they are capable of operating handles or other doorly devices.
They are,<<wait:0.5> after all,<<wait:0.2> just a dog.

#name:dog #image:dog_bark #sound:dog_bark
Bark! <<wait:0.2> Bark! 
~played_doortodeck = true
}
->END