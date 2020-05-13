

## Gameplay


## System
- [x] GameLoop holds FSM and seperated into its independent scene (to achive constant static presense) 
- [x] bootstrap scene instantiations
- [x] transition into songmenuState upon completion of open animations
- [x] transition into ingameState upon `start` button is hit
- [x] gameover menu and FSM
- [x] settings menu and FSM


## Other
- Macro
- SceneManagerExt


### ToDo
- [x] fix settingsmenu button appearance 
- [x] track Score & GameInfo [tis a system :D]
- [x] clean up / relook at the Bootstrap.cs script
- [ ] have a separate FSM for the *level scene* to separate its logic from the *UI scenes*' logic, since they are parallel rather than exclusive. See comments in `GameLoop.cs` for details

- [] bug: setactivescene() is time-variant.. so it returns error (no harm though)
