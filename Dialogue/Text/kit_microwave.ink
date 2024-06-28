INCLUDE Globals.ink

{completed_microwave:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
 You have bested us! We begrudingly allow your passage of our ways! Let this not temp your ego!
 YOU STILL SUCK!!!!
 -> END
 
  === NotCompleted ===
  ~completed_microwave = true
  Do you want to skip Microwave or do it?
  + [SKIP]?
  -> END
  + [DO IT]?
  #scene:Microwave 1
  -> END