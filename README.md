# Time-TD
Time Game Jam Project

See Trello board for more info on any of these

Component list:

Map:
- Can be added to a scene, should only be one map per scene
- Select size, cell size, and just use the Generate button in the inspector to create map tiles

Cell:
- Data for prefabs spawned by the map

Tower:
- Assign a turret that will be the turret for the tower
- Can assign functions to the On Upgrade and On Sell events

Turret:
- Can be added to a game object, but requires some child objects. See the TurretBase prefab for how the turret parts are arranged in the hierarchy.
- Set range
- Assign other turrets to the "Upgrade Paths" list to define what turrets this turret can upgrade to
- Initialized should be false if the object is a prefab. If the object is placed in the scene somewhere, it should be set to initialized. Note that the player controller sets this to true if it placed the turret from the UI.

SnapToMap:
- Can be added to any game object
- Currently only running in Editor mode
- Moves any object to the nearest valid map coordinate
- Used for quick level design and debugging of object positions
- Add the Environment tag to environment objects being placed

LevelManager:
- Should be placed on a single empty game object in the scene
- Add waves and set the spawn timers
- Add a spawner object from the scene

PlayerController:
- Should be placed on a single empty game object in the scene
- Requires a map object and an event system

Wave:
- A data only scriptable object that can be created from the Create>TD>Wave menu
- Assign mobs and the quantities of each mob

Mob:
- Assign to a game object with a navmeshagent component attached. Create a prefab to use it as part of a Wave.

Spawner:
- Attach to a game object and select another game object to act as the spot where the spawns will be instantiated
- Can add functions to the On Spawn event

Exit:
- Attach to a game object with a box collider
- Can add functions to the On Mob Enter event


