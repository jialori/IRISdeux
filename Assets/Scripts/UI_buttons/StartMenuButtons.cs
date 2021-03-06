﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Interfaces;

public class StartMenuButtons : MonoBehaviour, IObserver
{
	// The current level; -1 means no level scene is currently loaded
	// private static int level = -1;

    private bool inactive;


	void Awake()
	{
        // inactive = true;
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


    public void ToGame() 
    {
        if (inactive) return;

    	GameLoop.State = GameState.ingameState;
        GameInfoTracker.NewGameRecord(SceneManagerExt.CurLevel);
    }
 

    public void ToNextLevel() 
    {
        if (inactive) return;

    	if (SceneManagerExt.CurLevel == -1) return;

        int nxtLevel = SceneManagerExt.GetNextLevelBuildIdx();
    	SceneManagerExt.UnloadSceneAsync(SceneManagerExt.CurLevel);
    	SceneManagerExt.LoadSceneAsync_u(nxtLevel, LoadSceneMode.Additive);
    }
 

    public void ToPreviousLevel() 
    {
        if (inactive) return;

    	if (SceneManagerExt.CurLevel == -1) return;

        int lastLevel = SceneManagerExt.GetPreviousLevelBuildIdx();
    	SceneManagerExt.UnloadSceneAsync(SceneManagerExt.CurLevel);
    	SceneManagerExt.LoadSceneAsync_u(lastLevel, LoadSceneMode.Additive);
    }


    // ========== Subscription ==========
    public void UpdateOnChange(ISubject subject) 
    {
        switch (subject)
        {
            case GameLoop gp:
                SyncWithGameloop();
                break;
        }
    }
    // ========== Subscription END ==========


    private void SyncWithGameloop()
    {
        switch (GameLoop.State)
        {
            case SongMenuState state_songmenu:
                inactive = false; 
                break;
            default:
                inactive = true;
                break;
        }
    }


}
