using UnityEngine;
using UnityEngine.SceneManagement;
using Interfaces;

public class StartMenuButtons : MonoBehaviour, IObserver
{
	// The current level; -1 means no level scene is currently loaded
	private static int level = -1;

    private bool inactive;

	void Awake()
	{
        inactive = true;

        // todo: move into switch in bootstrap
        // Bootstrap logic moved into Boostrap.cs
        int curLevel = GetCurrentLevelBuildIdx();
        if (curLevel == -1) {
         SceneManager.LoadSceneAsync(Macro.IDX_FIRSTLEVEL, LoadSceneMode.Additive);
         StartMenuButtons.level = Macro.IDX_FIRSTLEVEL;          
        } else {
         StartMenuButtons.level = curLevel;          
        }

	}

    void Start()
    {
        GameLoop.Instance.Attach(this);
        StartMenuButtons.level = GetCurrentLevelBuildIdx();
    }

    void Update()
    {
        // Debug.Log("helloooo from startmenu");
    }

    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);
    }


    public void ToGame() 
    {
        // todo: change GameLoop state?
        if (inactive) return;

    	SceneManager.UnloadSceneAsync(Macro.IDX_STARTMENU);

    	// Change GameLoop's state
    	if (GameLoop.State != null) GameLoop.State = GameState.ingameState;
    }

    public void ToNextLevel() 
    {
        if (inactive) return;

    	if (StartMenuButtons.level == -1) return;

    	SceneManager.UnloadSceneAsync(StartMenuButtons.level);
    	int nxtLevel = GetNextLevelBuildIdx();
    	StartMenuButtons.level = nxtLevel;
    	SceneManager.LoadSceneAsync(nxtLevel, LoadSceneMode.Additive);
    }

    public void ToLastLevel() 
    {
        if (inactive) return;

    	if (StartMenuButtons.level == -1) return;

    	SceneManager.UnloadSceneAsync(StartMenuButtons.level);
    	int lastLevel = GetLastLevelBuildIdx();
    	StartMenuButtons.level = lastLevel;
    	SceneManager.LoadSceneAsync(lastLevel, LoadSceneMode.Additive);
    }

    private int GetCurrentLevelBuildIdx()
    {
    	int curLevel = -1;
    	if (SceneManager.sceneCount > 0)
        {
            for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.buildIndex >= Macro.IDX_FIRSTLEVEL) {
                	curLevel = scene.buildIndex;
                }
            }
        }

        return curLevel;
    }

    private int GetNextLevelBuildIdx()
    {
    	if (StartMenuButtons.level == -1) 
    		return -1;
    	
    	int n = SceneManager.sceneCountInBuildSettings;
    	return (StartMenuButtons.level + 1 < n) ? StartMenuButtons.level + 1 : Macro.IDX_FIRSTLEVEL;
    }

    private int GetLastLevelBuildIdx()
    {
    	if (StartMenuButtons.level == -1) 
    		return -1;
    	
    	int n = SceneManager.sceneCountInBuildSettings;
    	return (StartMenuButtons.level - 1 >= Macro.IDX_FIRSTLEVEL) ? StartMenuButtons.level - 1 : n - 1;
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
