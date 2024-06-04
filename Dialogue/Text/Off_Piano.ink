INCLUDE Globals.ink

VAR played = 0
//blank tag 
#name:blank #image:blank layout:blank #sound:blank #typingspeed:0

Play a note?
* [A].#sound:A
-> Play
* [B].#sound:B
-> Play
* [C].#sound:C
-> Play
* [D].#sound:D
~played = 1
-> Play
* [E].#sound:E
-> Play
* [O].#sound:O
-> Play
* [F].#sound:F
-> Play
* [G].#sound:G
-> Play
* nah
->END
===Play===
Play Another?
* [A].#sound:A
-> Play
* [B].#sound:B
-> Play
* [C].#sound:C
-> Play
* [D].#sound:D
~played = 1
-> Play
* [E].#sound:E
-> Play
* [O].#sound:O
{played == 1:
~played = 2
}
-> Play
* [F].#sound:F
-> Play
* [G].#sound:G
{played == 2:
~played = 3
->complete
-else:
-> Play
}
* nah
->END


===complete===
good dog #sound:victory1
->END


