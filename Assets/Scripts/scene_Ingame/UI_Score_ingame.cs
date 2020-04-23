using UnityEngine;
using UnityEngine.UI;
using Interfaces;

public class UI_Score_ingame : MonoBehaviour, IObserver
{
	private Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
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
    }


    public void UpdateOnChange(ISubject subject) 
    {
        switch (subject)
        {
            case Player p:
            	scoreText.text = p.score.ToString();
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
                RenderScore(true);
                break;
            default:
                RenderScore(false);
                break;
        }
    }


    void RenderScore(bool b)
    {
        scoreText.enabled = b;
    }

}
