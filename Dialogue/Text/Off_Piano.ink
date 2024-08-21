INCLUDE Globals.ink

#layout:up
->Play


===Play===
Play a note? #image:piano_silent  #sound:piano #name:Piano

* D #image:piano_d  #sound:piano_d #name:Piano
->PlayRoundTwo

* C #image:piano_c  #sound:piano_c #name:Piano
->Play

* A #image:piano_a  #sound:piano_a #name:Piano
->Play

* E #image:piano_e  #sound:piano_e #name:Piano
->Play

* [nah]
->END


===PlayRoundTwo===
Play Another? #image:piano_silent  #sound:piano #name:Piano

* G #image:piano_g  #sound:piano_g #name:Piano
->Play

* A #image:piano_a  #sound:piano_a #name:Piano
->Play

* F #image:piano_f  #sound:piano_f #name:Piano
->Play

* O #image:piano_c  #sound:piano_c #name:Piano
->PlayRoundThree

* [nah]
->END


===PlayRoundThree===
One more? #image:piano_silent  #sound:piano #name:Piano

* F #image:piano_f  #sound:piano_f #name:Piano
->Play

* C #image:piano_c  #sound:piano_c #name:Piano
->Play

* G #image:piano_g  #sound:piano_g #name:Piano
->complete

* E #image:piano_e  #sound:piano_e #name:Piano
->Play

* [nah]
->END

===complete===
good dog #sound:piano_victory #image:piano_silent #name:Piano
->END


