using UnityEngine;
using UnityEngine.SceneManagement;
using Interfaces;

public class StartMenuButtons : MonoBehaviour, IObserver
{
	// The current level; -1 means no level scene is currently loaded
	// private static int level = -1;

    private bool inactive;


	void Awake()
	{
        inactive = true;
	}


    void Start()
    {
        GameLoop.Instance.Attach(this);            
        // if (GameLoop.Instance == null)
        // {
            // Debug.Log("no gameloop yet");
        // } else {
        // }
    }


    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);
    }


    public void ToGame() 
    {
        if (inactive) return;

    	SceneManagerExt.UnloadSceneAsync(Macro.IDX_STARTMENU);

    	// Change GameLoop's state
    	if (GameLoop.State != null) GameLoop.State = GameState.ingameState;
    }
 

    public void ToNextLevel() 
    {
        if (inactive) return;

    	if (SceneManagerExt.CurLevel == -1) return;

        int nxtLevel = SceneManagerExt.GetNextLevelBuildIdx();
    	SceneManagerExt.UnloadSceneAsync(SceneManagerExt.CurLevel);
    	SceneManagerExt.LoadSceneAsync(nxtLevel, LoadSceneMode.Additive);
    }
 

    public void ToLastLevel() 
    {
        if (inactive) return;

    	if (SceneManagerExt.CurLevel == -1) return;

        int lastLevel = SceneManagerExt.GetLastLevelBuildIdx();
    	SceneManagerExt.UnloadSceneAsync(SceneManagerExt.CurLevel);
    	SceneManagerExt.LoadSceneAsync(lastLevel, LoadSceneMode.Additive);
    }


    // ========== Subscription ==========
    public void UpdateOnChange(ISubject subject) 
    {
        switch (subject)
        {
            case GameLoop gp:
                switch (GameLoop.State)
                {
                    case SongMenuState state_songmenu:
                        inactive = false; 
                        break;
                    default:
                        inactive = true;
                        break;
                }
                break;
        }
    }
    // ========== Subscription END ==========


}
