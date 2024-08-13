INCLUDE Globals.ink
#layout:down

{completed_microwave:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
 #image:microwave #name:Microwave
 You have bested us! We begrudingly allow your passage of our ways! Let this not temp your ego!
 YOU STILL SUCK!!!!
 -> END
 
  === NotCompleted ===
  ~completed_microwave = true
  #image:microwave #name:Microwave
  Do you want to skip Microwave or do it?
  + [SKIP]?
  -> END
  + [DO IT]?
  #scene:microwave
  -> END