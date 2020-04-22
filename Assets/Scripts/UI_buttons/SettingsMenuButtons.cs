﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuButtons : MonoBehaviour
{
	// todo: add FSM change
    public void Resume()
    {
    	SceneManagerExt.UnloadSceneAsync(this.gameObject.scene.buildIndex);
    }


    public void PlayAgain()
    {
    	SceneManagerExt.UnloadSceneAsync(this.gameObject.scene.buildIndex);
		ReloadCurLevel();
    }


    public void ToStartMenu()
    {
    	SceneManagerExt.UnloadSceneAsync(this.gameObject.scene.buildIndex);
		ReloadCurLevel();
    	SceneManagerExt.LoadScene_u(Macro.IDX_STARTMENU, LoadSceneMode.Additive);
    }


    void ReloadCurLevel()
    {
    	int curLevel = SceneManagerExt.CurLevel;
    	if (curLevel == -1) {
    		curLevel = Macro.IDX_FIRSTLEVEL;    		
    	} 
    	SceneManagerExt.ReloadScene(curLevel, LoadSceneMode.Additive);   	
    }
}
