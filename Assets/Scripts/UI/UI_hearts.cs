using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class UI_hearts : MonoBehaviour, IObserver
{
    [SerializeField] private List<GameObject> _hearts = new List<GameObject>();

    void Start()
    {
    	GameManager.Instance.Player.Attach(this);
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
        foreach (var heart in _hearts)
        {
            SpriteRenderer mr = heart.GetComponent<SpriteRenderer>();
            mr.enabled = b;
        }
    }
}
