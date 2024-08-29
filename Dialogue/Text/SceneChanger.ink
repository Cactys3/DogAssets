INCLUDE Globals.ink

#layout:down
what u want
*[change scene]
->first
*[obtain item]
->third
*[test]
->test
*[oops]
#name:dog #image:dog_whine #sound:dog_whine
HELLO
im Whine
HELLO
asdfasdfasdfasdfasf asdfasdf asf sadfasdfa asdf 
#name:dog #image:dog_sniff #sound:dog_sniff
HELLO
im sniff
HELLO
asdfasdfasdfasdfasf asdfasdf asf sadfasdfa asdf 
->END

===test===
#name:dog #image:dog_neutral #sound:dog
name:dog image:dog_neutral sound:dog
#name:narrator #image:narrator_neutral #sound:narrator
name:narrator image:narrator_neutral sound:narrator
#image:oven
image:oven
#image:juicer
image:juicer
#image:microwave
image:microwave
->END

===first===
Go to... #typingspeed:0
*[Fridge]
#scene:fridgeoven
->END
*[juicer]
#scene:juicer
->END
*[kitchen dining room]
#scene:kitchen
->END
*[living room]
#scene:living
->END
*[see more]
->second


===second===
Go to... #typingspeed:0

*[office room]
#scene:office
->END
*[bathroom]
#scene:bath
->END
*[Microwave]
#scene:microwave
->END
*[Boss]
#scene:boss
->END
*[Next]
->Six

===Six===
Go to... #typingspeed:0

*[Ending1]
#scene:end1
->END
*[Ending2]
#scene:end2
->END
*[End Choice]
#scene:endchoice
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





