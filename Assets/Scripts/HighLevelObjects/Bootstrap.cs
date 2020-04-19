using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
	public GameObject GameLoopObj;

    void Awake()
    {
    	foreach (int i in Macro.IDX_ALLSETUP)
    	{
	    	if (!SceneManager.GetSceneByBuildIndex(i).isLoaded)
		        SceneManager.LoadScene(i, LoadSceneMode.Additive);
    	}

    	if (GameLoop.Instance == null)
    	{
    		Debug.Log("No GameLoop detected, instantiate and move to scene");
	    	GameObject GameLoopObj_inst = Instantiate(GameLoopObj);
	    	SceneManager.MoveGameObjectToScene(GameLoopObj_inst, SceneManager.GetSceneByBuildIndex(Macro.IDX_GAMELOOP));
    	}

    }
}
