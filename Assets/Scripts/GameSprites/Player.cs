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

	private Vector2 targetPos;

    Animator animator;

    void Start()
    {
        GameLoop.Instance.Attach(this);

        animator = this.GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < yMax) {
        	targetPos = new Vector2(transform.position.x, transform.position.y + yIncrement);
	    	transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > yMin) {
        	targetPos = new Vector2(transform.position.x, transform.position.y - yIncrement);
	    	transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    public delegate int PerformCalculation(int x, int y);

    public void DecrementHealth(int value)
    {
    	health -= 1;
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
                Debug.Log("state change notified");
                switch (GameLoop.State)
                {
                    case InGameState state_ingame:
                        animator.enabled = true;
                        break;
                    default:
                        animator.enabled = false;
                        break;
                }
                break;
        }
    }
}
