INCLUDE Globals.ink

#layout:down
{ completed_dogfood:
    -> TakenDogFood
- else:
    -> NotTakenDogFood
}




 === NotTakenDogFood ===
#name:narrator #image:narrator_neutral #sound:narrator
Dog food,<<wait:0.2> well past its expiration date 
 * [consume]
     #name:dog #image:dog_woof #sound:dog_woof
    Woof Woof!
#name:narrator #image:narrator_neutral #sound:narrator
    The Dog seems disinclined to eat the rotton dog food.
    They save it for consumption at a later date. 
    -> RecieveDogFood
 * [save it for later]
    -> RecieveDogFood
 
 === RecieveDogFood ===
 ~ completed_dogfood = true
 ~ has_dogfood = true
    -> END

 
 === TakenDogFood ===
#name:narrator #image:narrator_neutral #sound:narrator
 An empty dog food bowl. 
 -> END