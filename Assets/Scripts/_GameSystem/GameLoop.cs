using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Interfaces;

public class GameState
{
	public static AnimationState aniState = new AnimationState();
	public static SongMenuState songmenuState = new SongMenuState();
	public static SettingsMenuState settingsmenuState = new SettingsMenuState();
	public static GameoverMenuState gameovermenuState = new GameoverMenuState();
	public static InGameState ingameState = new InGameState();

	// Returns True in case of state switch 
	public virtual bool HandleInput() {return false;}

	public virtual void RegUpdate() {}

	public virtual void Enter() {}

	public virtual void Exit() {}
}

public class AnimationState : GameState
{
	// public override bool HandleInput() {}
	// public override void RegUpdate() {}
}

public class SongMenuState : GameState
{
	public override void Exit()
	{
        SceneManagerExt.UnloadSceneAsync(Macro.IDX_STARTMENU);
	}

	public override void Enter() 
	{
    	SceneManagerExt.LoadScene_u(Macro.IDX_STARTMENU, LoadSceneMode.Additive);
    	SceneManagerExt.LoadScene_u(SceneManagerExt.CurLevel, LoadSceneMode.Additive);
	}	
}

public class SettingsMenuState : GameState
{
	public override void Exit()
	{
    	SceneManagerExt.UnloadSceneAsync(Macro.IDX_SETTINGSMENU);
	}

	public override void Enter() 
	{
		SceneManagerExt.LoadScene_u(Macro.IDX_SETTINGSMENU, LoadSceneMode.Additive);
	}	
}

public class GameoverMenuState : GameState
{
	public override void Exit()
	{
    	SceneManagerExt.UnloadSceneAsync(Macro.IDX_GAMEOVERMENU);
	}

	public override void Enter() 
	{
		SceneManagerExt.LoadScene_u(Macro.IDX_GAMEOVERMENU, LoadSceneMode.Additive);
	}	
}


/*TODO: 
	Maybe have another FSM for the *level scene* to separate its logic from the *UI scenes*'s' logic,
	since they are parallel rather than exclusive.

	E.G.

	level scene is on during ingame, 
	on and paused during settings, 
	display only (off, maybe have GIFs for each level) during song menu 

  */
public class InGameState : GameState
{
	// public LevelRecord;
	// public override bool HandleInput() {}
	// public override void RegUpdate() {}	
	// public override void Enter() {}
}


public class GameLoop : MonoBehaviour, ISubject
{
	private static bool isDirty;

	// Properties
	private static GameState _state;
	public static GameState State 
	{
		get => _state;

		set 
		{
			if (value != _state)
			{
				_state?.Exit();
				_state = value;
				isDirty = true;
			}
		}
	}

	// Singleton Variables
	static GameLoop _instance;
	static public GameLoop Instance { get => _instance; }


	void Awake()
	{
		if (_instance != null && _instance != this) 
		{
			Destroy(this.gameObject);
		}

		_instance = this;


		isDirty = false;
		State = GameState.aniState;
    	// Keeps focus on the level scene.
		SceneManager.sceneLoaded += FocusOnLevelSceneInGame;
	}


	void OnDestroy()
	{
		SceneManager.sceneLoaded -= FocusOnLevelSceneInGame;
	}


    void Update()
    {
    	// Game State routine
    	_state.HandleInput();
    	if (isDirty) 
    	{
    		_state.Enter();
    		Notify();
    		isDirty = false;
    	}
    	_state.RegUpdate();

    	// Custom code...
    }


	void FocusOnLevelSceneInGame(Scene scene, LoadSceneMode mode)
	{
		if (GameLoop.State == GameState.ingameState && Macro.IsLevel(scene.buildIndex))
		{
			SceneManagerExt.SetActiveScene(SceneManagerExt.CurLevel);
		}
	}


	// ============ Subcriber Pattern ============
    private List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
    	// Debug.Log("something attached to gameloop!");
        this._observers.Add(observer);
    }

    // Detach an observer from the subject.
    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
    }

    // Notify all observers about an event.
    public void Notify()
    {
    	Debug.Log(_state.ToString());
        foreach (var observer in _observers)
        {
            observer.UpdateOnChange(this);
        }
    }
	// ============ Subcriber Pattern END ============
}
