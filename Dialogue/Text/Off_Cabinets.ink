INCLUDE Globals.ink

#layout:down
{completed_off_cabinets:
->Completed
- else:
->NotCompleted
}


===Completed===
#name:Narrator #image:narrator_neutral #sound:narrator
It's an empty set of cabinets.
->END


==NotCompleted===
#name:dog #image:dog_sniff #sound:dog_sniff
sniff... sniff....
#name:Narrator #image:narrator_neutral #sound:narrator
The dog wishes to search through the cabinets<<wait:0.3>, will you allow it?
* [yes]
#name:Narrator #image:narrator_neutral #sound:narrator
- The dog searches through the cabinets and finds some hard candy.
~has_candy = true
~completed_off_cabinets = true
->END
* [no]
#name:dog #image:dog_woof #sound:dog_woof
- Woof!
->END