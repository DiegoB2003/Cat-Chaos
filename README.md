# Cat-Chaos
This game was developed for an Undergraduate project at the University of Illinois at Chicago in a CS 426 course.

[Design Document](https://docs.google.com/document/d/1CFu6U85XrUEFJIyIWUjvCOGbUwl8SkGE5plIKW824DA/edit?usp=sharing)

Team Members: Arlette Diaz, Nick Filipov, **Diego Bravo**

# How To Play + Controls
- You must be a mischievous cat by destroying fragile objects (currently only ceramic objects like cups) around the house without getting caught by the owner.
- Many actions, such as breaking items, moving, and getting near the dog, will contribute to a noise meter, triggering the owner to chase you if you are too loud.
- Use **WASD** for forward, right, back, and left movement.
- Press **Space** to jump.
- Use the **Mouse** to look around.
- **Left Mouse Click** (recently implemented for public demo) will push objects in front of you.
- Press **E** to interact with power-ups around the level.

### My contributions:
- Adjusted the breaking sound volume so it isn’t too loud.
- Adjusted music volume to be a little louder on speakers.
- Added more jump power-ups to allow players to reach new areas.
- Added more breakable cups to allow players the opportunity to get higher scores and encourage them to explore new areas.
- Added toon-style shader (aka Cel Shading), which has been applied to some power-up objects to make them pop out a bit more. This shader may be further utilized in the next release when we redesign power-ups.
- Cat Sounds: Added cat walking sound which plays when the player moves, and jumping sound which plays when the player jumps.
- Background Ambiance: Added quiet room ambiance to immerse the player in the house environment.
- Enhanced Score Tracker UI: The score tracker UI element has been greatly improved to have better visibility, style, and reactivity to player actions.
- Additional Sound Enhancements: Made some sounds 3D to make them less disturbing and more immersive for the player. e.g., the dog walking and barking sounds.
- Placed More Objective Items: The gameplay has been extended by adding more breakable items for the player to cause chaos with.
- Implemented chase logic for the Owner NPC through the NavMesh.
- Set up Cat idle and walk animations through Mecanim.
- Set up hinge door prefab (the owner's bedroom has a hinge door that swings open when the owner walks through it).
