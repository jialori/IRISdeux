using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Spawner : MonoBehaviour, IObserver
{
	public GameObject[] obstaclePatterns;

	public float timeBtwSpawn;
	private float timeBtwSpawnInst;
	public float timeBtwSpawnDecrement;
	public float minTimeBtwSpawn = 0.65f;
	// public float speed;

    private bool pause = false;

    void Start()
    {
        GameLoop.Instance.Attach(this);
    }

    void Update()
    {
        if (pause) return;

        if (timeBtwSpawnInst <= 0)
        {
        	int rand = Random.Range(0, obstaclePatterns.Length);
        	Instantiate(obstaclePatterns[rand], transform.position, Quaternion.identity);
        	timeBtwSpawnInst = timeBtwSpawn;
        	if (timeBtwSpawn >= minTimeBtwSpawn) {
        		timeBtwSpawn -= timeBtwSpawnDecrement;
        	}
        } 
        else {
	        timeBtwSpawnInst -= Time.deltaTime;
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
                        pause = false;
                        break;
                    default:
                        pause = true;
                        break;
                }
                break;
        }
    }

}
