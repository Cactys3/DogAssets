INCLUDE Globals.ink
#layout:up
#name:Narrator #image:narrator_neutral #sound:narrator
{played_doortoliving:
Go to the living room?
-else:
Go Through Door?
}
 * [Go]
 -> go
 * [Don't Go]
 -> dont


=== go ===
VAR count = 0

{completed_microwave:
~count = count + 1
}

{completed_fridgeoven:
~count = count + 1
}

{completed_jucier:
~count = count + 1
}

{count == 3:
~played_doortoliving = true
#scene:living
- else:
The dog MUST beat ALL kitchen appliances before moving on!
There are {count} appliances left!
}
->END 



=== dont ===
-> END
