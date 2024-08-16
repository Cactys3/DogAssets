INCLUDE Globals.ink
#layout:down

{completed_microwave:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
 #image:microwave #name:Microwave #sound:microwave
 You have bested us! We begrudingly allow your passage of our ways! Let this not temp your ego!
 -> END
 
  === NotCompleted ===
  ~completed_microwave = true
  #image:microwave #name:Microwave #sound:microwave
  Do you want to skip Microwave or do it?
  + [SKIP]?
  -> END
  + [DO IT]?
  #scene:microwave
  -> END