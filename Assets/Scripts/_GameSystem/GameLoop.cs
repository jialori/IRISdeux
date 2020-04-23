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
}

public class AnimationState : GameState
{
	// public override bool HandleInput() {}
	// public override void RegUpdate() {}
}

public class SongMenuState : GameState
{}

public class SettingsMenuState : GameState
{}

public class GameoverMenuState : GameState
{
	public override void Enter() 
	{
		SceneManagerExt.LoadScene_u(Macro.IDX_GAMEOVERMENU, LoadSceneMode.Additive);
	}	

}

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

	// used to set the _state
	private static GameState _state;
	public static GameState State 
	{
		get => _state;

		set 
		{
			if (value != _state)
			{
				_state = value;
				isDirty = true;
			}
		}
	}


    // ======== Singleton ==========
	static GameLoop _instance;
	static public GameLoop Instance
    {
        get => _instance;
    }

	void Awake()
	{
		if (_instance != null && _instance != this) 
		{
			Destroy(this.gameObject);
		}

		_instance = this;

		isDirty = false;

		State = GameState.aniState; // will set isDirty to true
    	State.HandleInput();

	}
    // ======== Singleton END ==========


    void Update()
    {
    	// Game State control
    	_state.HandleInput();
    	if (isDirty) 
    	{
    		_state.Enter();
    		Notify();
    		isDirty = false;
    	}
    	_state.RegUpdate();

    	// Other...

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
