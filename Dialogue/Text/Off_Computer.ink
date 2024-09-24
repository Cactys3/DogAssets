INCLUDE Globals.ink


~played_keyboard = true
{completed_computer:
-> AlreadyLearned
- else:
-> Learn
}


===Learn===
#name:dog #image:dog_growl #sound:dog_growl
Grrrrrr...
#name:dog #image:dog_neutral #sound:dog
..................
   #name:Narrator #image:narrator_neutral #sound:narrator
{has_tastykey:
The dog has just researched online about keys, doors, and molds.
He knew most of the stuff about molds already, but can now operate doors!
~completed_computer = true
    ->END
- else:
    {has_stinkykey:
    The dog has just researched online about keys, doors, and molds.
    He knew most of the stuff about molds already, but can now operate doors!
    ~completed_computer = true
    ->END
    - else:
    #name:Narrator #image:narrator_neutral #sound:narrator
    The dog has just researched online about keys, doors, and molds. <<wait:1> It's now very knolwedgable about such things.
    ~completed_computer = true
    ->END
    }
}



===AlreadyLearned===
#name:Narrator #image:narrator_neutral #sound:narrator
The dog learned about keys, doors, and molds here.
->END