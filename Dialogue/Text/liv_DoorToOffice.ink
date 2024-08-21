INCLUDE Globals.ink


#layout:up
#name:Narrator #image:narrator_neutral #sound:narrator
{played_doortooffice:
Enter the office?
-else:
Go Through Door?
}
 * [Go]
 -> go
 * [Don't Go]
 -> dont


=== go ===
~played_doortooffice = true
#scene:office
-> END


=== dont ===
:/
-> END
