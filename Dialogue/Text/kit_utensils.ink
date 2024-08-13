INCLUDE Globals.ink
#layout:up
{completed_utensils:
-> Completed
-else:
-> NotCompleted
}

 === Completed ===
   #name:narrator #image:narrator_neutral 
  nothing to see here...
 -> END
 
  === NotCompleted ===
  #name:narrator #image:narrator_neutral 
  nothing to see here...
  -> END