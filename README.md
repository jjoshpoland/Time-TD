# Time-TD
Time Game Jam Project

See Trello board for more info on any of these

Component list:

Map:
- Can be added to a scene, should only be one map per scene
- Select size, cell size, and just use the Generate button in the inspector to create map tiles

Cell:
- Data for prefabs spawned by the map

Turret:
- Can be added to a game object, but requires some child objects. See the TurretBase prefab for how the turret parts are arranged in the hierarchy.
- Set range
- Other functions still in progress

SnapToMap:
- Can be added to any game object
- Currently only running in Editor mode
- Moves any object to the nearest valid map coordinate
- Used for quick level design and debugging of object positions
