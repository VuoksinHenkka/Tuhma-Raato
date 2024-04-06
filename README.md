TUHMA RAATO a game by Henri Tervapuro

Trailer voice: Christopher Jayawardena. 
Shotgun mesh for renders: PolyElina/Sketchfab. 
SMG mesh for rendering: Vladek/Sketchfab.
Few (herringbore floor tile, concrete, bricks) textures courtesy of https://freepbr.com/


1. Design

      1.1 first version
      Topdown "Survive for X minutes" game with endless enemy swarms. The world is shaped like cd disc, so you´re always
      travelling sideways but end up looping. There are countless items and chances for different item interactions - 
      the limits with that are countless, so you could like, break doors with an axe, or douse something with gasoline
      then set it on fire with a lighter, etc. After the clock runs out, the game ends.
      
      1.2 actual game
      Topdown "Survive for X Minutes" game with pseudo-endless (actually calculated and limited) enemy swarms.
      World is shaped like a cd disc. There are just a handful of items and item interactions - very basic ones,
      but the codebase still allows for a lot more robust interactions. Just for the time and scope, I limited
      the design very quickly. After the clock hits 4AM a helicopter is spawned and you have to climb its ladder
      to escape, where the game ends.
      
      1.3 noteworthy iteration
      I changed the game to mostly black and white - a common speedhack of mine - so I can make assets faster.
      Textures are handled with Substance Painter, which gave me a good speed boost as well.
      Half-way thru the game I limited the rest of the design (amount of content and dynamics) and just focused into making
      the core experience somewhat coherent and smooth. It still has jank and its a pretty hard game, but I didnt
      want to tone it down too much - it felt fitting.
      Joonas, my teacher, spawned the cool idea for the helicopter evacuation point, which pretty much gave the game
      a good climax.



2. Code

      GAMEMANAGER is the mother of gamelogic - it takes care of the crucial gameplay states, 
      delegates, script references (not used by itself, but easy to find this way), and delegate voids.
      STATS holds important player stats (health and so forth)
      
      AUDIOMANAGER is for triggering audio and songs - it also holds references and launches...
      AUDIO_SFX ... these are self-contained audio launchers. They check if their audio is already playing,
      if so, then dont play it yet. I designed audio this way in case there´s a ton of audio triggering happening
      on the screen. There´s also a variation that launches audio from items the player uses, called...
      AUDIO_SFX_FROMITEMDEFINER which is its own script since I didnt want to put this code anywhere else.
      
      
      ZOMBIE is the enemy AI. All it does is walks forward and hurts the player based on timers, but its got some
      quirks related to solving stuck navagents and the way Unity messes up pathfinding. At the moment the solution
      is not perfect, but it atleast works. It also holds....
      INTERFACES (ICanDie, IHaveName, IHaveStatusEffects) are used for interactions with enemy entities, doors, etc.
      
      
      GAMEPLAYERITEMUSING is pretty much the aim/interact code of the player. It communicates with itemdefiners, inventory,
      and reads interfaces.
      ITEMDEFINER is a scriptable asset that I use to communicate and solve any data I need out of a item in the game.
      ITEMPICKUP is a in-game container for an item. It takes a itemdefiner to define all its data, and then it lets players
      pick itself up.
      ITEMRECEIVER is a in-game container as well. Receives itemdefiners.
      
      INVENTORY is the base code for the whole inventory that the player has - the actual inventory slots are seperate
      entities called INVENTORYCELL
      
      UI_ONENABLE(flashcolor,popupgfx,randomrotation) are for doing my UI effects where they fade, pop, and so on.
      
      ++ a ton of undocumented experimentations and microscripts to adjust gameplay. Idk they´re easy to parse.
