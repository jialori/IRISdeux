using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
	public GameObject GameLoopObj;

    void Awake()
    {

        switch (gameObject.scene.buildIndex)
        {
            case (Macro.IDX_STARTMENU):
                break;
            case (Macro.IDX_GAMELOOP):
                break;
            default: // a game level scene
                foreach (int i in Macro.IDX_ALL_SETUP)
                {
                    if (!SceneManager.GetSceneByBuildIndex(i).isLoaded)
                        Debug.Log("Scene" + i.ToString() + " is missing, additively load");
                        SceneManager.LoadScene(i, LoadSceneMode.Additive);
                }

                if (GameLoop.Instance == null)
                {
                    Debug.Log("No GameLoop detected, instantiate and move to scene");
                    GameObject GameLoopObj_inst = Instantiate(GameLoopObj);
                    SceneManager.MoveGameObjectToScene(GameLoopObj_inst, SceneManager.GetSceneByBuildIndex(Macro.IDX_GAMELOOP));
                }

                break;

        }

    }
}
