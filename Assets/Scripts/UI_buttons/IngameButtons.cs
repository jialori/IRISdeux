using UnityEngine;
using UnityEngine.SceneManagement;
using Interfaces;

public class IngameButtons : MonoBehaviour, IObserver
{
	public void ToSettingsMenu()
	{
		SceneManagerExt.LoadScene_u(Macro.IDX_SETTINGSMENU, LoadSceneMode.Additive);
		GameLoop.State = GameState.settingsmenuState;
	}


	void Start()
    {
        GameLoop.Instance.Attach(this);
        SyncWithGameloop();        
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
            	RenderButtons(true);
                break;
            default:
            	RenderButtons(false);
                break;
        }
    }

    private void RenderButtons(bool b)
    {
    	for (int i = 0; i < transform.childCount; i++) {
    		transform.GetChild(i).gameObject.SetActive(b);
    	}
    }
}
