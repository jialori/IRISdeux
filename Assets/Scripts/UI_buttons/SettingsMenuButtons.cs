using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuButtons : MonoBehaviour
{
	// todo: add FSM change
    public void Resume()
    {
        GameLoop.State = GameState.ingameState;
    }


    public void PlayAgain()
    {
		ReloadCurLevel();
        GameLoop.State = GameState.ingameState;
    }


    public void ToStartMenu()
    {
		ReloadCurLevel();
        GameLoop.State = GameState.songmenuState;
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
