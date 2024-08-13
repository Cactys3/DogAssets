INCLUDE Globals.ink
#layout:up
#name:narrator #image:narrator_neutral #sound:narrator
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
        The dog MUST beat ALL kitchen appliances before moving on!
        ->END
        }
    - else:
    The dog MUST beat ALL kitchen appliances before moving on!
    ->END
    }
- else:
The dog MUST beat ALL kitchen appliances before moving on!
->END
}

#scene:living
-> END


=== dont ===
come again soon.
-> END
