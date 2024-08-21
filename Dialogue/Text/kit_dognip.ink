INCLUDE Globals.ink
#layout:up
#name:Narrator #image:narrator_neutral #sound:narrator
~ ate_dognip = true
It's dognip, quite a strong batch too.
Should the dog eat it?
* [Yes]
    #name:dog #image:dog_sniff #sound:dog_sniff
    Sniff...
    #scene:dognip
    ->END
*[No]
    #name:Narrator #image:narrator_neutral #sound:narrator
    You don't seriously think you can stop the dog,<<wait:0.2> do you?
    #name:dog #image:dog_sniff #sound:dog_sniff
    Sniff...
    #scene:dognip
    ->END