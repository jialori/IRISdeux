using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenuButtons : MonoBehaviour
{
    // todo: FSM change
    public void PlayAgain()
    {
        Debug.Log("helloooo");
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
