using UnityEngine;
using UnityEngine.UI;
using Interfaces;

public class UI_Score : MonoBehaviour, IObserver
{
	public Text scoreText;

    void Start()
    {
    	GameManager.Instance.Player.Attach(this);
        GameLoop.Instance.Attach(this);
    }

    public void UpdateOnChange(ISubject subject) 
    {
       switch (subject)
        {
            case Player p:
            	scoreText.text = p.score.ToString();
                break;

            case GameLoop gp:
                switch (GameLoop.State)
                {
                    case InGameState state_ingame:
                        RenderScore(true);
                        break;
                    default:
                        RenderScore(false);
                        break;
                }
                break;
        }
    }

    void RenderScore(bool b)
    {
        gameObject.SetActive(b);
    }
}
