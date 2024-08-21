INCLUDE Globals.ink

#layout:up
#name:Narrator #image:narrator_neutral #sound:narrator
{played_doortobath:
Enter the bathroom?
-else:
Go Through Door?
}
 * [Go]
 -> go
 * [Don't Go]
 -> dont


=== go ===
~played_doortobath = true
#scene:bath
-> END


=== dont ===
-> END
