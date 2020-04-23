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


        SceneManagerExt.NotifySceneIsLoaded(gameObject.scene.buildIndex);

        // todo: sort out what needs to be bootstrapped when
        // add to SceneManagerExt upon prpgram started        

        // game logic & dev/test logic

        LoadAndInstantiateGameLoop();

        if (!Macro.IsMenu(gameObject.scene.buildIndex)) 
        {
            LoadFreshStartScenes();
            LoadLevelScene();
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

    void LoadAndInstantiateGameLoop()
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


    void LoadLevelScene()
    {
        if (SceneManagerExt.CurLevel == -1)
        {
            SceneManagerExt.LoadScene_u(Macro.IDX_FIRSTLEVEL, LoadSceneMode.Additive);
        }
    }
}
