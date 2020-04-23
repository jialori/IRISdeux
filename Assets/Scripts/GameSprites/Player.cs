using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Player : MonoBehaviour, ISubject, IObserver
{
	public float health;
	public float speed;
	public float yIncrement;
	public float yMin;
	public float yMax;

    // todo: This should not be recorded here - have a LevelRecord that stores player, score, progress, etc.
    public float score;

    private Animator animator;
    private bool pause;

    // ======== Singleton ==========
    static Player _instance;
    static public Player Instance { get => _instance; }
    void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }

        _instance = this;

        animator = this.GetComponent<Animator>();
    }
    // ======== Singleton End =========

    void Start()
    {
        GameLoop.Instance.Attach(this);
        SyncWithGameloop();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause) return;

        Vector2 targetPos;
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < yMax) {
        	targetPos = new Vector2(transform.position.x, transform.position.y + yIncrement);
	    	// transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.position = targetPos;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > yMin) {
        	targetPos = new Vector2(transform.position.x, transform.position.y - yIncrement);
            // transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
	    	transform.position = targetPos;
        }
    }

    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);
    }


    public void DecrementHealth(int value)
    {
    	health -= 1;
    	Notify();

        if (health <= 0) {
            GameLoop.State = GameState.gameovermenuState;
        }
    }


    public void IncrementScore(int value)
    {
        score += 1;
        Notify();
    }

	// ============ Subcriber Pattern ============
    // For the sake of simplicity, the Subject's state, essential to all
    // subscribers, is stored in this variable.
    // public int State { get; set; } = -0;

    // List of subscribers. In real life, the list of subscribers can be
    // stored more comprehensively (categorized by event type, etc.).
    private List<IObserver> _observers = new List<IObserver>();

    // The subscription management methods.
    public void Attach(IObserver observer)
    {
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
    }

    // Trigger an update in each subscriber.
    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.UpdateOnChange(this);
        }
    }

    public void UpdateOnChange(ISubject subject) 
    {
        switch (subject)
        {
            case GameLoop gp:
                SyncWithGameloop();
                break;
        }
    }


    private void SyncWithGameloop()
    {
        switch (GameLoop.State)
        {
            case InGameState state_ingame:
                Pause(false);
                break;
            case SettingsMenuState state_settingsmenu:
                Pause(true, true, false);
                break;
            default:
                Pause(true);
                break;
        }
    }


    private void Pause(bool b)
    {
        pause = b;
        animator.enabled = !b;
        SpriteRenderer mr = GetComponent<SpriteRenderer>();
        mr.enabled = !b;
    }


    private void Pause(bool b, bool animatorPause, bool rendererPause)
    {
        pause = b;
        animator.enabled = !animatorPause;
        GetComponent<SpriteRenderer>().enabled = !rendererPause;
        // mr.enabled = !rendererPause;
    }

}
