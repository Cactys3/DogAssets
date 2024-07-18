INCLUDE Globals.ink

~ ate_dognip = true
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
It's dognip. 
Should the dog eat it?
* [Yes]
    #name:dog #image:dog_sniff #layout:dog1 #sound:dog_sniff
    Sniff...
    #scene:dognip
    ->END
*[No]
    You don't seriously think you can stop the dog, do you?
    #name:dog #image:dog_bark #layout:dog1 #sound:dog_bark
    Bark!
    #scene:dognip
    ->END