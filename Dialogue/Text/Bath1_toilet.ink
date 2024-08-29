INCLUDE Globals.ink
#layout:down
#name:Narrator #image:narrator_neutral #sound:narrator

Water, it's wet.

{played_bath1_toilet == false:
#name:dog #image:dog_bark #layout:dog1
Sniff...
#name:Narrator #image:narrator_neutral #sound:narrator
The dog happily drinks its fill.
~ played_bath1_toilet = true
}

{holding_item:
->IsHoldingItem
-else:
->NotHoldingItem
}



===NotHoldingItem===
You could dip an item into the toilet, if you were holding one.
->END

===IsHoldingItem
#name:Narrator #image:narrator_neutral #sound:narrator
Would you like to splash your item in the toilet water?
* [why yes, i would]
->TestItem(held_item)
*[no]
Good decision, that would probably be a bad idea.
->END

===TestItem(item)===
{item == "has_clay":
~has_moldableclay = true
~has_clay = false
~holding_item = false
~held_item = ""
The clay, once dipped in the water, becomes wet and moldable.
->END
-else:
Water doesn't seem to have a lasting impact on that particular item.
->END
}

