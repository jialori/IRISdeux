
# 2020-04-18
made FSM work in its Level Selection sub-system:
- Bootstrap prefab in every scene to set up (all the scenes)
- left/right & start Buttons trigger scene load
- start Button trigger FSM state change
	- Observer pattern in GameObjects that need to listen to FSM state change

*Exp:*
think in the specifics of the game workflow, where it starts, where it ends, what changes need to happen, etc., instead of thinking abstractly starting nowhere.

*leftoff:*
trying to separate the GameLoop into a different scene, so it gets rid of Dontdestroyonload and would be static running throughout. 
problem encountered: order of instantiation - the life cycles in different scenes dont match, so it's never guaranteed that GameLoop will be ready when other GameObjects try to subscribe to it.
potential solution: intantiate the GameLoop in the Bootstrap script (if one does not already exist) which will be placed in every scene. Instantiate a GameLoop and then move it into the new scene? (try first; later consider whether it can be non-MonoBehaviour)


thinking of:
- save all meta data in one place for grab (pure C# for now, maybe json & some other better formats for the future)
- 
- abstract a LevelProgress class (pure C#) to track score, timing, level, player.

