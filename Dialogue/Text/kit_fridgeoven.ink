INCLUDE Globals.ink
#layout:down
{completed_fridgeoven:
-> Completed
 -else:
-> NotCompleted
}

  ===Completed===
 #image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
   WE will allow you ONE use of our utilities. 
   You may heat and cool ONE item.
   {holding_item:
   ->TryCombine(held_item)
 - else:
 #image:fridge #name:Fridge #sound:fridge
   Alas you hold no item. 
   #image:oven #name:Oven #sound:oven
   Come back when you wish to redeem your reward.
  }
 -> END

  ===NotCompleted===
   #image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
  Do you want to skip fridge minigame or do it?
  + [SKIP]?
  ~completed_fridgeoven = true
  -> END
  + [DO IT]?
#image:oven #name:Oven #sound:oven
  You dare challenge US??? 
       ~completed_fridgeoven = true
     #scene:fridgeoven
  -> END
  
  ===TryCombine(item)===
#image:oven #name:Oven #sound:oven
  {(item == "has_stinkyfilledkeymold"):
  Eww....
 #image:fridge #name:Fridge #sound:fridge
  If it must be done. <<wait: 1> we will do it.
  -> ObtainKey
  - else:
    {item == "has_tastyfilledkeymold":
    #name:oven #image:oven #layout:oven #sound:oven
    Mmmmm...
    What smells SO good!?!
#image:fridge #name:Fridge #sound:fridge
    You wish to use our services on this?
    Very well...
    -> ObtainKey
        - else:
        #name:oven #image:oven #layout:oven #sound:oven
        WHAAAT??? You would have us interact with THAT foul thing?
#image:fridge #name:Fridge #sound:fridge
        Nonsense, come back when you have something worthwhile for us.
        ->END
        }
  }
  
  
  ===ObtainKey===
#name:narrator #image:narrator_neutral #sound:narrator
  The Oven bakes the mold which then goes straight into the Fridge to cool and...
#image:fridgeoven #name:Fridge Oven Duo #sound:fridgeoven
  VOILA!
  A perfectly shapen and hardend key!
  {has_stinkyfilledkeymold:
#name:narrator #image:narrator_neutral #sound:narrator
    This should work for the locked living room door!
  Although it is a bit stinky...
    ~has_stinkykey = true
    ~has_stinkyfilledkeymold = false
  }
  {has_tastyfilledkeymold:
#name:narrator #image:narrator_neutral #sound:narrator
  This should work for the locked living room door!
  Although it is a bit sticky...
    ~has_tastykey = true
    ~has_tastyfilledkeymold = false
  }
  ->END
  
  
  
  