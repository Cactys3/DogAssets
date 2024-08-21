INCLUDE Globals.ink
#layout:up
VAR trap = false


#name:??? #image:narrator_neutral #sound:narrator
Hey, you! 
You're finally awake.

* [me?]
No, the dog. <<wait:1>Obviously.
Though, speaking of you...
* [...]
I bet you had a nice nap you lazy pup, <<wait:0.5> but it's time for some action!
And you, the non-dog individual.

- The hero of our story, the dog, is stuck in a cage!
And naturally only you can open cages, so will you let them out?
*[let him out]
-> AfterTrapped
*[...]
->Trapped


===Trapped===
I'm going to take your silence as disagreeable silence...
So just what possible reason might you have to keep the dog trapped in their cage?
*[let him out]
->AfterTrapped
*[...]

~ trap = true
- Playing the silent game are we?
I'm quite good at that...
<<wait:5>
Did I win?
*[Yes, let them out]
->AfterTrapped
*[...]
-This is no fun.

Please let them out!
*[let them out]
->AfterTrapped
*[...]
- Seriously..

Pleeasse let them out!!
*[let them out]
->AfterTrapped
*[...]

- Pleeeeaassee!<<wait:0.5>!<<wait:0.5>!

*[let them out]
->AfterTrapped
*[...]
- FINE!

I'LL JUST DO IT MYSELF!
*[let them out]
...
okay
->AfterTrapped
*[...]
In.<<wait:0.4>.<<wait:0.4>. 
And Out.<<wait:0.4>.<<wait:0.4>.
In.<<wait:0.4>.<<wait:0.4>. 
And Out.<<wait:0.4>.<<wait:0.4>.
Anyways...
->AfterTrapped

===AfterTrapped===
{trap:
I think we started off on the wrong foot...
- else:
Great, I think we're off to a good start!
}
*[Who are you?]
Oh I am so glad you asked!
*[...]
Now, you might be wondering who I am!


- I am the Narrator! #name:Narrator #image:narrator_neutral #sound:narrator
And as such I will do many narrator things
{trap:
Such as stopping you from eternally trapping the dog and dooming them to the fires which are slowly encroaching on this very household! 
- else:
Such as informing you of the radioactive fires which are slowly encroaching on this very household! 
}

You must help the dog in their trials and tribulations,
across the carpet and tile they must tread,
through the doorways locked! And paths blocked!
Are you prepared to embark on this quest, knowing of its inordinate importance for our valiant dog?
*[yes]
GREAT!
Now help the dog make their escape!
->END
*[stop talking]
*[...]
- ...
- Just help the dog make their escape.
->END