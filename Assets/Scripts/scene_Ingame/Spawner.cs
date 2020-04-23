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

    private bool pause;

    void Start()
    {
        GameLoop.Instance.Attach(this);
        SyncWithGameloop();
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


    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);        
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
