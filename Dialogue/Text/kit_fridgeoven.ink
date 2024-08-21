INCLUDE Globals.ink
#layout:down
#image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
{completed_fridgeoven:
-> Completed
 -else:
-> NotCompleted
}

  ===Completed===
#image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
   WE will allow you ONE use of our utilities. 
#image:fridge #name:Fridge #sound:fridge
   You may heat and cool one item.
   {holding_item:
   ->TryCombine(held_item)
 - else:
#image:oven #name:Oven #sound:oven
   Alas thou holdith no item. 
#image:fridge #name:Fridge #sound:fridge
   Come back when you want to use your reward.
  }
 -> END

  ===NotCompleted===
#image:oven #name:Oven #sound:oven
Who dost traverse our floors?
Thine paws tread heavy and lack grace
Fridge, share thine wisdom.
#image:fridge #name:Fridge #sound:fridge
You can challenge us.
If you don't, you can not leave.
#image:oven #name:Oven #sound:oven
.<<wait:0.1>.<<wait:0.1>.<<wait:0.1>.
#image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
Shall we battle now?
*[yes]
~completed_fridgeoven = true
#scene:fridgeoven
  -> END
  *[no]
#image:oven #name:Oven #sound:oven
Thoust shall not leave this place!
->END
  
  ===TryCombine(item)===
{(item == "has_stinkyfilledkeymold"):
    #image:oven #name:Oven #sound:oven
    Thou bringith an offer in jest, surely...
    #image:fridge #name:Fridge #sound:fridge
    If it must be done.<<wait:0.8> we will do it.
  -> ObtainKey
  - else:
    {item == "has_tastyfilledkeymold":
    #image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
    Mmmmm...
    #image:oven #name:Oven #sound:oven
    What smellith SO eminently flavorful!?!
    #image:fridge #name:Fridge #sound:fridge
    You wish to use our services on this?
    Very well.
    -> ObtainKey
        - else:
    #image:oven #name:Oven #sound:oven
    Thou bringith an offer in jest, surely...
    #image:fridge #name:Fridge #sound:fridge
    We cannot work on {item}.
    Come back when you have something suited for baking and cooling.
        ->END
        }
  }
  
  
  ===ObtainKey===
#name:Narrator #image:narrator_neutral #sound:narrator
  The Oven bakes the mold which then goes straight into the Fridge to cool and<<wait:0.5>.<<wait:0.5>.<<wait:0.5>.<<wait:0.5>
#image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
  VOILA!
  A perfectly shapen and hardend key!
  {has_stinkyfilledkeymold:
#name:Narrator #image:narrator_neutral #sound:narrator
    This should work for the locked living room door!
  Although it is a bit stinky...
    ~has_stinkykey = true
    ~has_stinkyfilledkeymold = false
  }
  {has_tastyfilledkeymold:
#name:Narrator #image:narrator_neutral #sound:narrator
  This should work for the locked living room door!
  Although it is a bit sticky...
    ~has_tastykey = true
    ~has_tastyfilledkeymold = false
  }
  ->END
  
  
  
  