INCLUDE Globals.ink

{completed_plant:
    -> alreadyTasted
- else:
    -> notTasted
}


 === notTasted ===
Growl... //dog
It seems this particular plant tastes foul. //narrator
~ completed_plant = true
-> END

 === alreadyTasted ===
 Growl... //dog
 -> END