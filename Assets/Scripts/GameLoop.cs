using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class GameState
{
	public static AnimationState aniState = new AnimationState();
	public static SongMenuState songmenuState = new SongMenuState();
	public static SettingsMenuState settingsmenuState = new SettingsMenuState();
	public static GameoverMenuState gameovermenuState = new GameoverMenuState();
	public static InGameState ingameState = new InGameState();

	// Returns True in case of state switch 
	public virtual bool HandleInput(GameLoop game) {return false;}

	public virtual void Update(GameLoop game) {}

	public virtual void Enter(GameLoop game) {}
}

public class AnimationState : GameState
{
	// public override bool HandleInput() {}
	// public override void Update() {}

}

public class SongMenuState : GameState
{

}


public class SettingsMenuState : GameState
{
}

public class GameoverMenuState : GameState
{
}

public class InGameState : GameState
{
}

public class GameLoop : MonoBehaviour, ISubject
{
	public static bool dirty;

	// There are two ways to set the _state
	// 1. Within GameState's HandleInput()
	// 2. Use of State's setter from outside of this class
	private static GameState _state;
	public static GameState State 
	{
		get => _state;
		set 
		{
			if (value != _state)
			{
				_state = value;
				dirty = true;
			}
		}
	}

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
		DontDestroyOnLoad(_instance.gameObject);

		dirty = false;

		_state = GameState.aniState; // will set dirty to true
    	_state.HandleInput(this);

	}

    void Update()
    {
    	// Game State control
    	_state.HandleInput(this);
    	if (dirty) 
    	{
    		_state.Enter(this);
    		Notify();
    		dirty = false;
    	}
    	_state.Update(this);

    	// Other...

    }


	// ============ Subcriber Pattern ============
    private List<IObserver> _observers = new List<IObserver>();
    // Attach an observer to the subject.
    public void Attach(IObserver observer)
    {
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
        foreach (var observer in _observers)
        {
            observer.UpdateOnChange(this);
        }
    }
}
