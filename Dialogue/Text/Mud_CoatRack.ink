INCLUDE Globals.ink
#layout:down
#name:dog #image:dog_whine #sound:dog_whine
Whine... 
#name:narrator #image:narrator_neutral #sound:narrator
The dog wonders where everyone else is.
{played_coatrack == false:
They wouldn't leave the dog behind.<<wait:0.3>.<<wait:0.3>. <<wait:0.5>Would they?
~played_coatrack = true
}
