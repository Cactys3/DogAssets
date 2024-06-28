INCLUDE Globals.ink
Go Through Door?
 * [Go]
 -> go
 * [Don't Go]
 -> dont


=== go ===
{completed_microwave:
    {completed_fridgeoven:
        {completed_jucier:
        #scene:living
        - else:
        #name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
        The dog MUST beat ALL kitchen appliances before moving on!
        ->END
        }
    - else:
    #name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
    The dog MUST beat ALL kitchen appliances before moving on!
    ->END
    }
- else:
#name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
The dog MUST beat ALL kitchen appliances before moving on!
->END
}

#scene:living
-> END


=== dont ===
come again soon.
-> END
