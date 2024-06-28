INCLUDE Globals.ink

{completed_jucier:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
 YOU STRONG NOW!!!
 -> END
 
  ===NotCompleted===
  Do you want to skip juicer minigame or do it?
  + [SKIP]?
  ~completed_jucier = true
  -> END
  + [DO IT]?
  ~completed_jucier = true
 #scene:Juicer Minigame
  -> END