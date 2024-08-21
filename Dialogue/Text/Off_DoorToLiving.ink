INCLUDE Globals.ink
#layout:up
#name:Narrator #image:narrator_neutral #sound:narrator
Go Through Door?
 * [Go]
 -> go
 * [Don't Go]
 -> dont


=== go ===
#scene:living
-> END


=== dont ===
alright, stay then.
-> END
