INCLUDE Globals.ink
#layout:down

{completed_jucier:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
#name:Juicer #image:juicer
 YOU STRONG NOW!!!
 -> END
 
  ===NotCompleted===
  #name:Juicer #image:juicer
  Do you want to skip juicer minigame or do it?
  + [SKIP]?
  ~completed_jucier = true
  -> END
  + [DO IT]?
  ~completed_jucier = true
 #scene:juicer
  -> END