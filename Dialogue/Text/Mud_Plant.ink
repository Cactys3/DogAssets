INCLUDE Globals.ink
#layout:up
{completed_plant:
    -> alreadyTasted
- else:
    -> notTasted
}


 === notTasted ===
#name:dog #image:dog_growl #sound:dog_growl
Growl... 
#name:narrator #image:narrator_neutral #sound:narrator
It seems this particular plant tastes foul. 
~ completed_plant = true
-> END

 === alreadyTasted ===
 #name:dog #image:dog_growl #sound:dog_growl
 Growl... 
 -> END