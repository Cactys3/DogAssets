INCLUDE Globals.ink
#image:juicer #name:Juicer #sound:juicer  #layout:up

{introduced_dognip:
    {completed_jucier:
    -> Completed
     -else:
    -> NotCompleted
    }
-else:
    #name:narrator #image:narrator_neutral #sound:narrator
    The dog is high.
    Their hallucinations seem to be most intense around kitchen appliances. 
    They're not going to go to the next room until they "defeat" all of the kitchen appliances.
    Whatever that means...
    ~introduced_dognip = true
    #image:juicer #name:Juicer #sound:juicer
    ->NotCompleted
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