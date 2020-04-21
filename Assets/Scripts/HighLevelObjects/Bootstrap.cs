using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
	public GameObject GameLoopObj;

    void Awake()
    {
        // do nothing if this is not the only scene open
        if (SceneManager.sceneCount > 1) return;

        LoadAllSetUpScenes();
        AddGameLoop();

        switch (gameObject.scene.buildIndex)
        {
            case (Macro.IDX_STARTMENU):
                SceneManager.LoadScene(Macro.IDX_FIRSTLEVEL, LoadSceneMode.Additive);
                break;

            default:
                break;
        }

    }

    void LoadAllSetUpScenes()
    {
        foreach (int i in Macro.IDX_ALL_SETUP)
        {
            if ((!SceneManager.GetSceneByBuildIndex(i).isLoaded)
                && (SceneManager.GetSceneByBuildIndex(i).name != gameObject.scene.name))
            {
                Debug.Log("Scene" + i.ToString() + " is missing, additively load");
                SceneManager.LoadScene(i, LoadSceneMode.Additive);
            }
        }
    } 

    void AddGameLoop()
    {
        if (GameLoop.Instance == null)
        {
            Debug.Log("No GameLoop detected, instantiate and move to scene");

            // isLoaded changes in the next frame, so referencing it in the same frame does not work
            // Workaround: manage a static List of loaded scenes (a new script) on the GameLoop object.

            // if (!SceneManager.GetSceneByBuildIndex(Macro.IDX_GAMELOOP).isLoaded)
            // {
            //     Debug.Log("GameLoop Scene is missing, additively load");
            //     SceneManager.LoadScene(Macro.IDX_GAMELOOP, LoadSceneMode.Additive);
            // }

            GameObject GameLoopObj_inst = Instantiate(GameLoopObj);
            SceneManager.MoveGameObjectToScene(GameLoopObj_inst, SceneManager.GetSceneByBuildIndex(Macro.IDX_GAMELOOP));
        }        
    }
}
