INCLUDE Globals.ink

#layout:down
#name:Narrator #image:narrator_neutral #sound:narrator
Go Back to the kitchen?
 + [Go]
 -> go
 + [Don't Go]
 -> dont


=== go ===
#scene:kitchen
-> END


=== dont ===
-> END
