VAR gender = "" //man, woman, nb, idk
VAR eye_color = "" //blue, brown, green, demon
VAR hair_color = "" //brown, black, light, demon
VAR hair_style = "" //long, short, demon
VAR skin_color = "" //light, dark, cyborg, demon
//unlocked_name is if a particular room or gameplay element is accessible
VAR unlocked_deck = false
VAR unlocked_speech = false
VAR unlocked_dooropening = false
VAR ate_dognip = false
VAR narratortired_bool = false
VAR introduced_dognip = false
//completed_name is if an ink story has already been played out
VAR completed_dogfood = false
VAR completed_plant = false
VAR completed_jucier = false
VAR completed_utensils = false
VAR completed_fridgeoven = false
VAR completed_microwave = false
VAR completed_UFO = false
VAR completed_computer = false
VAR completed_bookshelf = false
VAR completed_off_cabinets = false
VAR completed_flimsykey = false
VAR completed_liv_doortostairs = false
VAR completed_liv_cabinet = false
VAR completed_toilet = false
//played_name is if an ink story has already been played in whatever way the particular story cares about (usually only used within that one story)
VAR played_bath1_toilet = false
VAR played_DoorToStairs = false
VAR played_coatrack = false
VAR played_doortodeck = false
VAR played_movierack = false
VAR played_doortooffice = false
VAR played_doortobath = false
VAR played_doortokitchen = false
VAR played_doortoliving = false
VAR played_intro = false
VAR played_keyboard = false
VAR played_narratortired = false

VAR played_poop = false
//has_itemname is if a particular is currently obtained
VAR has_dogfood = false
VAR has_default = false
VAR has_flimsykey = false
VAR has_clay = false
VAR has_poop = false
VAR has_candy = false
VAR has_moldableclay = false
VAR has_keymold = false
VAR has_stinkyfilledkeymold = false
VAR has_tastyfilledkeymold = false
VAR has_stinkykey = false
VAR has_tastykey = false
//if it's holding an item and what item
VAR holding_item = false
VAR held_item = ""