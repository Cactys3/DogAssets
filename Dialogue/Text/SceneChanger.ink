INCLUDE Globals.ink
what u want
*[change scene]
->first
*[obtain item]
->third


===first===
Go to... #typingspeed:0
*[0 - intro animation]
#scene:0
->END
*[1 - mud room]
#scene:1
->END
*[2 - kitchen dining room]
#scene:2
->END
*[3 - living room]
#scene:3
->END
*[see more]
->second


===second===
Go to... #typingspeed:0

*[4 office room]
#scene:4
->END
*[5 - bathroom]
#scene:5
->END
*[6]
#scene:6
->END
*[7]
#scene:7
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





