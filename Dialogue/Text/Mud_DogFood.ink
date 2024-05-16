INCLUDE Globals.ink

{ completed_dogfood:
    -> TakenDogFood
- else:
    -> NotTakenDogFood
}




 === NotTakenDogFood ===
Dog food,<<wait:0.5> well past its expiration date //narrator says
 + [consume]
    Woof Woof! //dog 
    The Dog seems disinclined to eat the rotton dog food. //narrator
    They save it for later. 
    -> RecieveDogFood
 + [save for later]
    -> RecieveDogFood
 
 === RecieveDogFood ===
 ~ completed_dogfood = true
 ~ has_dogfood = true
 ~ has_default = true
\+1 Rotton Dog Food!
    -> END

 
 === TakenDogFood ===
 An empty dog food bowl. //narrator
 -> END