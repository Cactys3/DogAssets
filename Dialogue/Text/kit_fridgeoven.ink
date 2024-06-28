INCLUDE Globals.ink

{completed_fridgeoven:
-> Completed
 -else:
-> NotCompleted
}

  ===Completed===
   #name:fridgeoven #image:fridgeoven #layout:fridgeoven #sound:fridgeoven
   WE will allow you ONE use of our utilities. 
   You may heat and cool ONE item.
   {holding_item:
   ->TryCombine(held_item)
 - else:
   Alas you hold no item. 
   Come back when you wish to redeem your reward.
  }
 -> END

  ===NotCompleted===
  Do you want to skip fridge minigame or do it?
  + [SKIP]?
  ~completed_fridgeoven = true
  -> END
  + [DO IT]?
  #name:fridgeoven #image:fridgeoven #layout:fridgeoven #sound:fridgeoven
  You dare challenge US??? 
       ~completed_fridgeoven = true
     #scene:Fridge Level 1
  -> END
  
  ===TryCombine(item)===

  {(item == "has_stinkyfilledkeymold"):
  #name:oven #image:oven #layout:oven #sound:oven
  Eww....
  #name:fridge #image:fridge #layout:fridge #sound:fridge
  If it must be done. <<wait: 1> we will do it.
  -> ObtainKey
  - else:
    {item == "has_tastyfilledkeymold":
    #name:oven #image:oven #layout:oven #sound:oven
    Mmmmm...
    What smells SO good!?!
    #name:fridge #image:fridge #layout:fridge #sound:fridge
    You wish to use our services on this?
    Very well...
    -> ObtainKey
        - else:
        #name:oven #image:oven #layout:oven #sound:oven
        WHAAAT??? You would have us interact with THAT foul thing?
        #name:fridge #image:fridge #layout:fridge #sound:fridge
        Nonsense, come back when you have something worthwhile for us.
        ->END
        }
  }
  
  
  ===ObtainKey===
  #name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
  The Oven bakes the mold which then goes straight into the Fridge to cool and...
  #name:fridgeoven #image:fridgeoven #layout:fridgeoven #sound:fridgeoven
  VOILA!
  A perfectly shapen and hardend key!
  {has_stinkyfilledkeymold:
  #name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
    This should work for the locked living room door!
  Although it is a bit stinky...
    ~has_stinkykey = true
    ~has_stinkyfilledkeymold = false
  }
  {has_tastyfilledkeymold:
  #name:narrator #image:narrator_default #layout:narrator1 #sound:narrator_default
  This should work for the locked living room door!
  Although it is a bit sticky...
    ~has_tastykey = true
    ~has_tastyfilledkeymold = false
  }
  ->END
  
  
  
  