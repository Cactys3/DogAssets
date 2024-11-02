INCLUDE Globals.ink


#layout:up
#name:Narrator #image:narrator_neutral #sound:narrator
{completed_liv_doortostairs:
->Completed
- else:
-> TestKnowledge
}


===TestKnowledge===
{completed_computer:
->TryKey
- else:
-> FailedKnowledge
}

===FailedKnowledge===
#name:Narrator #image:narrator_neutral #sound:narrator
{has_tastykey || has_stinkykey:
    Although you made a key, the dog doesn't know how to open doors.
    Maybe try doing some research?
        {played_doortooffice:
        That office room might have more to give.
        - else:
        I know there's an office around here.
        }
-else:
    The dog doesn't have a key for this door,<<wait:0.3> not that they know how to open closed doors in the first place.
        {played_doortooffice:
        Sounds like the dog needs to use that computer room again.
        - else:
        I know there's an office around here somewhere.
        }
}
->END


===TryKey===
{has_tastykey || has_stinkykey:
    ~has_tastykey = false
    ~has_stinkykey = false
    ~completed_liv_doortostairs = true
    ->UnlockDoor
- else:
    #name:dog #image:dog_woof #sound:dog_woof
    Woof! Woof!
    #name:Narrator #image:narrator_neutral #sound:narrator
    The dog is excited to use their newly gained knowledge on locks and doors!
    {has_flimsykey:
        Though they are missing a solid enough key to turn the lock
    -else:
        Though they are missing a key.
    }
    {played_DoorToStairs:
        #name:dog #image:dog_woof #sound:dog_woof
        Woof!
        #name:Narrator #image:narrator_neutral #sound:narrator
        The dog wishes to make one with his molding skills.
    - else:
        ~played_DoorToStairs = true
    }
    ->END
}


===UnlockDoor===
The dog has applied their rigourus studies and crafted a key fit to rule each and every lock within 2000 square feet!
Though it is a struggle for them not to eat it...
Do you now wish to use this hand crafted key and complete this quest?
*[yes]
~completed_liv_doortostairs = true
->Completed
*[no]
->END

===Completed===
After battling their way through each kitchen appliance come to life, gaining copious quantities of knowledge, 
applying that knowledge to craft a work of art, finest ever made by dogkind, and finally using that work of art to unlock a locked door, as a dog.
They are finally ready to move on.
This trip, while exilerating, has also been enlighting.
In conquering the trials this journey through at them, the dog has come to accept a cold truth. 
Those that were once called family chose to abandon the dog and the house they turned to home.
As the effects of the dognip start to shift, the dog realizes they must make their way through this final doorway...
And face the cause of this destruction.<<wait:0.3>.<<wait:0.3>.<<wait:0.3> a<<wait:0.1>l<<wait:0.1>o<<wait:0.1>n<<wait:0.1>e<<wait:1>
#scene:boss
->END



