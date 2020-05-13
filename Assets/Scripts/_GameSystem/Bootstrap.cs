using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
    public GameObject GameLoopObj;

    void Awake()
    {
        // DO NOTHING if this script is awake in non-bootstrapping condition
        if (SceneManager.sceneCount > 1) return; 


        // BOOSTSTRAPPING
        // todo: sort out what needs to be bootstrapped when
        // add to SceneManagerExt upon prpgram started        
        // game logic & dev/test logic to be separated

        SceneManagerExt.NotifySceneIsLoaded(gameObject.scene.buildIndex);

        int buildIndex = gameObject.scene.buildIndex;
        if (Macro.IsMenu(buildIndex) && buildIndex != Macro.IDX_STARTMENU) 
        {
            Spawn_GameLoop();
        }
        else
        {
            Spawn_GameLoop();       
            Load_StandardStartScenes();
            Load_FirstLevelScene();
        }
    }


    void Spawn_GameLoop()
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


    void Load_StandardStartScenes()
    {
        foreach (int i in Macro.IDX_All_STANDARDSTART)
        {
            if (!SceneManagerExt.IsLoaded(i))
            {
                Debug.Log("Scene" + i.ToString() + " is missing, additively load");
                SceneManagerExt.LoadScene_u(i, LoadSceneMode.Additive);
            }
        }
    } 


    void Load_FirstLevelScene()
    {
        if (SceneManagerExt.CurLevel == -1)
        {
            SceneManagerExt.LoadScene_u(Macro.IDX_FIRSTLEVEL, LoadSceneMode.Additive);
        }
    }
}
