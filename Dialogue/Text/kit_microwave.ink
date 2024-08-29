INCLUDE Globals.ink
#image:microwave #name:Microwave #sound:microwave  #layout:down
{completed_microwave:
-> Completed
 -else:
-> NotCompleted
}

 === Completed ===
 You have bested us! We begrudingly allow your passage of our ways! Let this not temp your ego!
 -> END
 
  === NotCompleted ===
  Have you come here to challenge our might!?!
  To test your mettle against our metal?!?
  To see if YOU have what it takes to overcome OUR most difficult challenge?
  *[yes]
    ~completed_microwave = true
  THEN LET THE CHALLENGE COMMENCE!
  #scene:microwave
  -> END
  *[no]
  Well...
  We won't let you pass this room until you beat our challenge.
  ->END