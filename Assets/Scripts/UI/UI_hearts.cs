using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class UI_hearts : MonoBehaviour, IObserver
{
    [SerializeField] private List<GameObject> _hearts = new List<GameObject>();

    void Awake()
    {
        foreach (Transform child in transform)
        {
            _hearts.Add(child.gameObject);
        }
    }

    void Start()
    {
    	Player.Instance.Attach(this);
        GameLoop.Instance.Attach(this);

        RenderHearts(false);
    }

    public void UpdateOnChange(ISubject subject) 
    {
        switch (subject)
        {
            case Player p when (p.health < _hearts.Count && _hearts.Count != 0):
                Destroy(_hearts[_hearts.Count - 1]);
                _hearts.RemoveAt(_hearts.Count - 1);
                break;

            case GameLoop gp:
                switch (GameLoop.State)
                {
                    case InGameState state_ingame:
                        RenderHearts(true);
                        break;
                    default:
                        RenderHearts(false);
                        break;
                }
                break;
        }
    }

    void RenderHearts(bool b)
    {
        Debug.Log(_hearts.Count);
        foreach (var heart in _hearts)
        {
            if (heart != null) {
                SpriteRenderer mr = heart.GetComponent<SpriteRenderer>();
                mr.enabled = b;                
            } else {
                Debug.Log("a heart is null.");
            }
        }
    }

    void OnDestroy()
    {
        Player.Instance.Detach(this);
        GameLoop.Instance.Detach(this);    

        _hearts.Clear();    
    }
}
