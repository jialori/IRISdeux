using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenuButtons : MonoBehaviour
{
    // todo: FSM change
    public void PlayAgain()
    {
        SceneManagerExt.UnloadSceneAsync(this.gameObject.scene.buildIndex);
        ReloadCurLevel();
        GameLoop.State = GameState.ingameState;
    }


    public void ToStartMenu()
    {
        SceneManagerExt.UnloadSceneAsync(this.gameObject.scene.buildIndex);
        ReloadCurLevel();
        SceneManagerExt.LoadScene_u(Macro.IDX_STARTMENU, LoadSceneMode.Additive);
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
