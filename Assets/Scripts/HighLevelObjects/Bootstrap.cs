using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
	public GameObject GameLoopObj;

    void Awake()
    {
        // do nothing if not bootstrapping
        if (SceneManager.sceneCount > 1) return; 


        // add to SceneManagerExt upon prpgram started
        SceneManagerExt.NotifySceneIsLoaded(gameObject.scene.buildIndex);

        int buildIndex = gameObject.scene.buildIndex;
        if (Macro.IsMenu(buildIndex)) {

        } 
        else if (Macro.IsLevel(buildIndex))
        {
            LoadFreshStartScenes();
        }

        AddGameLoop();

        switch (gameObject.scene.buildIndex)
        {
            case (Macro.IDX_STARTMENU):
                SceneManagerExt.LoadScene_u(Macro.IDX_FIRSTLEVEL, LoadSceneMode.Additive);
                break;

            default:
                break;
        }

    }

    void LoadFreshStartScenes()
    {
        foreach (int i in Macro.IDX_All_FRESHSTART)
        {
            if (!SceneManagerExt.IsLoaded(i))
            {
                Debug.Log("Scene" + i.ToString() + " is missing, additively load");
                SceneManagerExt.LoadScene_u(i, LoadSceneMode.Additive);
            }
        }
    } 

    void AddGameLoop()
    {
        if (GameLoop.Instance == null)
        {
            Debug.Log("No GameLoop detected, instantiate and move to scene");
            
            if (!SceneManagerExt.IsLoaded(Macro.IDX_GAMELOOP))
            {
                Debug.Log("GameLoop Scene is missing, additively load");
                SceneManagerExt.LoadScene_u(Macro.IDX_GAMELOOP, LoadSceneMode.Additive);
            }

            GameObject GameLoopObj_inst = Instantiate(GameLoopObj);
            SceneManager.MoveGameObjectToScene(GameLoopObj_inst, SceneManager.GetSceneByBuildIndex(Macro.IDX_GAMELOOP));
        }        
    }
}
