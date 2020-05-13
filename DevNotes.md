## Phase 2
### 2020-05-13
Rewrote parts of the game loop's code.
Main changes are: 
	logic of `UI scenes`' transition are moved from UI buttons to GameLoop's Enter and Exit methods, since the underlying logics are identical.

todo:
	have another FSM for the *level scene* to separate its logic from the *UI scenes*'s' logic,
	since they are parallel rather than exclusive.


## Phase 1
### 2020-04-23
- fixed SetActive on a level scene load upon transferring into ingameState. 
	previous: SectActive() in ingameState.Enter() \[natural, but sequence of execution in Unity lifecycles does not allow this to happen - Scene is not loaded right after LoadScene is called, but by the end of the frame or something like that, so SetActive() on the same frame cannot work\]
	now: SetActive() in sceneLoaded in the SceneManager + conditions filter. so it will be called upon every scene loaded.
	alternative solution: use async load callback \[sounds a bit more natural\]

**I'm pausing development to focus on the gameplay design for a bit.** It is also a great time to re-look at the code wirtten for casual improvement.

**list of things learnt (C#)**
static constructor: if not used, static variables will be initialized with default values.
dictionary operation: unlike python's syntax (dict['a'] = 2, the same syntax is used for adding and re-assigning values), .Add() adds an entry, and Add() & Remove() are mixed to reassign values, since no built-in re-assignment method is provided.
C# pointer:
C# parameter passing \[read the MS document, really helpful\]: normally the primitive value types are passed by value, and the reference types (objects, arrays, etc.) are passed by reference.
- so changing elements of reference types will affect on the caller; but re-assigning the variable to a new object/array/... would not  
- `ref` modifier: abstractly bahaviour is that *any* change (including re-assignment) on the parameter reflects on the argument. the argument's memory address will be replaced by the parameter's memory address upon completion of the method call.  
- `out` modifier
- `in` modifier: passed by reference (for reference types, what happens is a reference of the memory address is passed), cannot be modified (but operations & method-calling on it allowed); pretty much only useful for `struct` (belongs to value types)
- c# types can be divided into "reference types" and "values types"
C# object copy: assignment operator for objects performs pass by value (shallow copy?)

**todo:**
iterator methods &/ design pattern;


### 2020-04-22
- add menus: settings and gameover (w/o FSM change yet)
- add menus: settings (w FSM change yet)
- refactor UpdateOnChange() structures so subscribers adjust to gameloop state upon start
*sequeces of executions really can have multiple orders with different scene changes, thus code should ideally be order-invariant.*
- align canvas sizes and camera settings across scenes
- add menus: gameover (w FSM change yet)
- add GameInfoTracker [non-monobehaviour]
- add score display in gameover menu

抽象思考进行不下去的时候，就从底层开始直接开始写/想，这样可以找到思路。

坑： even Unity needs restarting to fix itself sometimes... oh well I guess it is in the core of every software & hardware...

### 2020-04-21
add SceneManagerExt, an extension to SceneManager that fixes the isLoaded delayed update.

*other:*
- finished SceneManagerExt pretty quick :DDDD
- **exp** stay focused on writing down the code first, and then debug them at once - this worked well!
- so you should never name two things the same name unless it's for overwridding. spent 1hr on an uncared Find-Replace mistake QAQ 

### 2020-04-20
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

### 2020-04-19
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

### 2020-04-18
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

