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
        SyncWithGameloop();
    }


    void OnDestroy()
    {
        Player.Instance.Detach(this);
        GameLoop.Instance.Detach(this);    

        _hearts.Clear();    
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
                SyncWithGameloop();
                break;
        }
    }


    private void SyncWithGameloop()
    {
        switch (GameLoop.State)
        {
            case SettingsMenuState state_settingsmenu:
            case InGameState state_ingame:
                RenderHearts(true);
                break;
            default:
                RenderHearts(false);
                break;
        }
    }


    void RenderHearts(bool b)
    {
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

}
