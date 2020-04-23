# 2020-04-22
add menus: settings and gameover (w/o FSM change yet)
add menus: settings (w FSM change yet)
refactor UpdateOnChange() structures so subscribers adjust to gameloop state upon start

# 2020-04-21
add SceneManagerExt, an extension to SceneManager that fixes the isLoaded delayed update.

*other:*
- finished SceneManagerExt pretty quick :DDDD
- **exp** stay focused on writing down the code first, and then debug them at once - this worked well!
- so you should never name two things the same name unless it's for overwridding. spent 1hr on an uncared Find-Replace mistake QAQ 

# 2020-04-20
camera in an additively-loaded scene cannot observe canvas from the other scene.
- turns out the issue is not with things are seperated in different scenes, it's actually because the canvas is set to be rendered by their scenes' main cameras only, so definitely no other cameras can observe them.
Canvas: camera (observable only seen by the assigned camera, can scale according to screen), world (observable by all cameras, scale?todo) 

`isLoaded`, counter-intuitively, does not return `true` in the same frame when you load the scene, it is possibly set to `true` in the next fram; so if you load a frame in Awake(), isLoad for that scene will return *false* in Start(). 
- A way out can be to have a script storing the currently loaded scenes and let other scripts update it whenever they load a scene.
<!-- I spent long time debugging the extra scene problem, because I did not realize that a scene does not magically show up. If I trace code that has 'LoadScene' in it, I'm bound to be looking at the lines that wrongly generates the frame, and the bug is bound to be in there. -->

a `const` is always (inherently) a `static` (hence no `const static` syntax).

`order in layer`: the lower the number, the earlier drawn, so the lower in the canvas. 

The Life Cycles (Update, Awake, Start) of objects in all loaded scenes (including additively loaded scenes) will keep running like normal. 

*new term:* 
	`delegate` and `event` seems to make a Subscription pattern together. They are used in Unity to subscribe custom delegate functions to events like `SceneLoaded`, which sends notifications upon occurance.

lesson: debug starting at typos, syntax (brackets), then logic, then into the hidden levels. 
If some behaviors seem to pop out of nowhere, they probably come out from some code containing relevant keywords.

question:
	which camera is used when multiple scenes are loaded?
	static non-monobehaviour classes are useful in Unity games, but does non-static non-monobahaviour classes ever help? do they even instantiate objects? \[YES, can be instantiated and stored/referenced by monobehaviour scripts!\]

# 2020-04-19
decision: GameLoop is gonna stay MonoBehaviour, because of it is handling input through Update() methods. Although I could make other modules call its update method, but that feels out of place and not a natural design for now.
making GameLoop part of a different scene: lifecycle knowledge?

encountered `Missing Reference` error when start playing after switching between levels - need to keep Observer pattern's subscription list clean, subscribers detach onself upon destruction
- OnDestroy() is called when unloading a scene. HOWEVER, any Debug.Log() function will not work in OnDestroy(), meaning you cannot log info when destroying things.
- Second of all, Start() is called when loading a scene.
- `Missing Reference`s are due to the leftover subscribers from previously loaded scenes in GameLoop that should have been detached when they unload.

move global variables / macros into a static Macro class
- [where I learned this](https://stackoverflow.com/questions/14368129/how-to-use-global-variables-in-c)
- well, I sometimes forget that code bundled together compile together, so it does not require any reference to refer to a static class in the same folder (more accurately, in the same namespace)

add Opening animation
- future question: is AniState needed? possibly yes - because there will be multiple animation_end signals to check, and once they all end, all UIs and buttons (say settings menu) need to be notified.

*new term:*
	Unity Threading, c# pointer

*other:*
	spent 1.5 hours on a typo QAQ...

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

