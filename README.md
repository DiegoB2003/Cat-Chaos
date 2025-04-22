# Cat-Chaos
CS 426 Final Project

[Design Document](https://docs.google.com/document/d/1CFu6U85XrUEFJIyIWUjvCOGbUwl8SkGE5plIKW824DA/edit?usp=sharing)

Team Members: Arlette Diaz, Nick Filipov, Diego Bravo

# Assignment 8
### Arlette Diaz:
-

### Nick Filipov:
-

### Diego Bravo:
- Adjusted the breaking sound volume so it isn’t too loud.
- Adjusted music volume to be a little louder on speakers.
- Added more jump power-ups to allow players to reach new areas.
- Added more breakable cups to allow players the opportunity to get higher scores and encourage them to explore new areas.
- Added toon style shader (aka Cel Shading), which has been applied to some power-up objects to make them pop out a bit more. This shader may be further utilized in the next release when we redesign power-ups.

# Assignment 7
### Arlette Diaz:
- Attack Sound: Added a woosh audio that plays whenever the owner grabs the player.
- Chase Music: Added music that plays whenever the owner is in chase mode. This audio makes the game feel more intense. 
- Glass Breaking Sound: Added a glass shattering sound whenever the player knocks over breakable cups. This audio increases the noise meter since the player is causing the chaos.
- Noise Meter UI: Added functionality and color to improve visual indicators so the player knows how much noise they are causeing.
- Glow Effect: Added a new glowing effect to the powerups scattered around the house that the play can easily spot them in our dark enviorment. 

### Nick Filipov:
- Dog Sounds: Added barking sounds for when the player walks to close to Dog because the dog is supposed to make a noise to
increase the noise meter. Also added walking sounds for the Dog as it walks to its next location.
- Background Music: Added background music throughout the game so it isn't silent and feels a little sneaky while you
sneak around as a cat and try to avoid the owner.
- PowerUp Sounds: Added a sound for each of the 2 PowerUps to indicate to the player that they picked up the powerup and
what powerup they picked up depending on the sound.
- PowerUp Effect: I also created a sparkle animation to when the user picks up a PowerUp just for visual feedback.
- PowerUp UI: I also added 2 PowerUp icons at the bottom left of the screen to show when and which powerup is active
and also added "Press (E)" text on the screen when looking at a PowerUp to indicate to the players that it is able to
be picked up/used.

### Diego Bravo:
- Cat Sounds: Added cat walking sound which plays when the player moves and jumping sound which plays when the player jumps.
- Background Ambiance: Added quiet room ambiance to immerse the player in the house environment.
- Enhanced Score Tracker UI: The score tracker UI element has been greatly improved to have better visibility, style, and reactivity to player actions.
- Additional Sound Enhancements: Made some sounds 3D to make them less disturbing and more immersive for the player. e.g., the dog walking and barking sounds.
- Placed More Objective Items: The gameplay has been extended by adding more breakable items for the player to cause chaos with.

# Assignment 6
**Design and Rationale:** In our game "Cat Chaos", the cat (player) must avoid being caught while causing chaos in the house.
We used Mecanim and AI techniques, such as pathfinding and FSMs, to make the cat's owner patrol the house and chase the cat.
We also used waypoints to make a dog that patrols an area of the house. This will make the level difficult for the player to
cause chaos, which encourages them to be strategic.

### AI implementations:
- FSM on the Owner NPC - Nick Filipov
- Pathfinding on the Owner NPC - Diego Bravo
- Waypoints on the Dog NPC - Arlette Diaz

### Mecanim implementations:
- Owner NPC idle+walk - Nick Filipov
- Cat Player idle+walk - Diego Bravo
- Dog NPC idle+walk - Arlette Diaz

### Physics:
- Speed Boost Power Up (gives player cat more directional force for a duration on collision with power up object)
- Jump Boost Power Up (gives player cat more jump force for a duration on collision with power up object)
- Hinge Door (the owner's bedroom has a hinge door that swings open when the owner walks through it)

### Textures:
There are multiple textures used across the level, such as floor, door, and wall textures.

### Lighting:
There are multiple light sources used across the level, such as lamps.

# Assignment 5
[Assignment 5: Design Document, Tools, Sw Prototype and Level Design](https://docs.google.com/document/d/1FLa1F97W0JR0hCHbJc4M_DVPr4UKmouwLH6_qRgPJr4/edit?usp=sharing)
