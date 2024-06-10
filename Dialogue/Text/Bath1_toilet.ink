INCLUDE Globals.ink

-> start

== start ==
#name:narrator #image:narrator_default #layout:narrator1
Water, it's wet.

{played_bath1_toilet == false:
#name:dog #image:dog_sniff #layout:dog1
Sniff...
#name:narrator #image:narrator_default #layout:narrator1
The dog happily drinks its fill.
~ played_bath1_toilet = true
}

{holding_item:
#name:narrator #image:{held_item} #layout:{held_item}
Would you like to splash the {held_item} in the toilet water?
* [why yes, i would]
->holding(held_item)
*[no]
#name:narrator #image:narrator_default #layout:narrator1
Good decision, that would probably be a bad idea.
->END
-else:
->END
}
===holding(item)===
{item == "has_clay":
~has_moldableclay = true
~has_clay = false
~holding_item = false
~held_item = ""
It worked
->END
-else:
It didn't work
->END
}

