INCLUDE Globals.ink

#layout:down
{ completed_dogfood:
    -> TakenDogFood
- else:
    -> NotTakenDogFood
}




 === NotTakenDogFood ===
 #name:narrator #sound:narrator #image:narrator_neutral
Dog food,<<wait:0.5> well past its expiration date 
 * [consume]
     #name:dog #image:dog_woof #sound:dog_woof
    Woof Woof!
     #name:narrator #sound:narrator #image:narrator_neutral
    The Dog seems disinclined to eat the rotton dog food.
    They save it for later. 
    -> RecieveDogFood
 * [save for later]
    -> RecieveDogFood
 
 === RecieveDogFood ===
 ~ completed_dogfood = true
 ~ has_dogfood = true
    -> END

 
 === TakenDogFood ===
  #name:narrator #sound:narrator #image:narrator_neutral
 An empty dog food bowl. 
 -> END