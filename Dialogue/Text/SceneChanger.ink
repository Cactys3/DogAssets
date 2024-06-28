INCLUDE Globals.ink
what u want
*[change scene]
->first
*[obtain item]
->third


===first===
Go to... #typingspeed:0
*[0 - Fridge]
#scene:Fridge Level 1
->END
*[1 - mud room]
#scene:Mud Room
->END
*[2 - kitchen dining room]
#scene:Kitchen Dining Room
->END
*[3 - living room]
#scene:Living Room
->END
*[see more]
->second


===second===
Go to... #typingspeed:0

*[4 office room]
#scene:Office Room
->END
*[5 - bathroom]
#scene:Bathroom 1
->END
*[Microwave]
#scene:Microwave 1
->END
*[Juicer]
#scene:Juicer Minigame
->END
*[Finish]
->END


===third===
obtain item...
*[has_dogfood]
~has_dogfood = true
->third
*[has_moldableclay]
~has_moldableclay = true
->third
*[has_flimsykey]
~has_flimsykey = true
->third
*[has_clay]
~has_clay = true
->third
*[more]
->fourth

===fourth===
obtain item...
*[has_poop]
~has_poop = true
->fourth
*[has_candy]
~has_candy = true
->fourth
*[has_keymold]
~has_keymold = true
->fourth
*[has_stinkyfilledkeymold]
~has_stinkyfilledkeymold = true
->fourth
*[more]
->five

===five===
obtain item...
*[has_tastyfilledkeymold]
~has_tastyfilledkeymold= true
->five
*[has_stinkykey]
~has_stinkykey = true
->five
*[has_tastykey]
~has_tastykey = true
->five
*[Finish]
->END





