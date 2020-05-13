using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenuButtons : MonoBehaviour
{
    // todo: FSM change
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
