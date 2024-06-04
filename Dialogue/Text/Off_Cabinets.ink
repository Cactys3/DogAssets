INCLUDE Globals.ink

{completed_off_cabinets:
->Completed
- else:
->NotCompleted
}

===Completed===
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
It's an empty set of cabinets.
->END


==NotCompleted===
#name:dog #image:dog_sniff #layout:dog1 #sound:dog_sniff
sniff... sniff....
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
The dog wishes to search through the cabinets <<wait:0.5>, will you allow it?
* [yes]
* [no]
#name:dog #image:dog_woof #layout:dog1 #sound:dog_woof
- Woof!
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
The dog searches through the cabinets and finds some hard candy.
~has_candy = true
~completed_off_cabinets = true
->END