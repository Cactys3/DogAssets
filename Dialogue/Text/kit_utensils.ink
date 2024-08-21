INCLUDE Globals.ink
#layout:up
{completed_utensils:
-> Completed
-else:
-> NotCompleted
}

 === Completed ===
#name:Narrator #image:narrator_neutral #sound:narrator
A kitchen table.
 -> END
 
  === NotCompleted ===
#name:Narrator #image:narrator_neutral #sound:narrator
A kitchen table?
~completed_utensils = true
  -> END
  
