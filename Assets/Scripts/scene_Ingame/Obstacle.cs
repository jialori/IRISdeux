using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Obstacle : MonoBehaviour, IObserver
{
	public float speed;
	public int damageToHealth;

    private bool pause;

    
    void Start()
    {
        GameLoop.Instance.Attach(this);
        SyncWithGameloop();
    }


    void Update()
    {
        if (pause) return;

    	transform.Translate(Vector2.left * speed * Time.deltaTime);    
    }


    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.gameObject.CompareTag("Player")) {
    		other.GetComponent<Player>().DecrementHealth(damageToHealth);
    		if (transform.parent.childCount == 1) 
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
    	}
        else if (other.gameObject.CompareTag("Boundary")) {
            Player.Instance.IncrementScore(1);
            Destroy(transform.parent.gameObject);
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
            default:
                Pause(true);
                break;
        }
    }


    private void Pause(bool b)
    {
        pause = b;
    }    
}
