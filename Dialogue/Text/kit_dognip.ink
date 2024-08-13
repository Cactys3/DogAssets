INCLUDE Globals.ink
#layout:up
#name:narrator #image:narrator_neutral #sound:narrator
~ ate_dognip = true
It's dognip. 
Should the dog eat it?
* [Yes]
    #name:dog #image:dog_sniff #sound:dog_sniff
    Sniff...
    #scene:dognip
    ->END
*[No]
    You don't seriously think you can stop the dog, do you?
    #name:dog #image:dog_bark #sound:dog_bark
    Bark!
    #scene:dognip
    ->END