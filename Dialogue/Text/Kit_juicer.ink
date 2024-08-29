INCLUDE Globals.ink
#image:juicer #name:Juicer #sound:juicer  #layout:up
{completed_jucier:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
 YOU STRONG NOW!!!
 GROWN YOU HAVE!
 ME ALLOW LEAVE!!
 LEAVE!
 -> END
 
  ===NotCompleted===
CHALLENGE YOU WILL COMPLETE!!!
NOW!!!
DO!!!
*[yes]
  ~completed_jucier = true
 #scene:juicer
  -> END
*[no]
->yes


===yes===
NO! YES!!!
*[yes]
  ~completed_jucier = true
 #scene:juicer
  -> END
*[no]
->yes