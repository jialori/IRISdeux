using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public float speed;
	public int damageToHealth;

    // Update is called once per frame
    void Update()
    {
    	transform.Translate(Vector2.left * speed * Time.deltaTime);    
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
            GameManager.Instance.Player.IncrementScore(1);
            Debug.Log("self-destroyred");
            Destroy(transform.parent.gameObject);
        }
    }
}
