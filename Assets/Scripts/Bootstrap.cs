using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
	// todo: data to be abstracted to an independent info-storing file
	int IDX_GAMELOOP = 1;
	int[] IDX_ALLSETUP = {0, 1};

	public GameObject GameLoopObj;

    void Awake()
    {
    	foreach (int i in IDX_ALLSETUP)
    	{
	    	if (!SceneManager.GetSceneByBuildIndex(i).isLoaded)
		        SceneManager.LoadScene(i, LoadSceneMode.Additive);
    	}

    	if (GameLoop.Instance == null)
    	{
    		Debug.Log("No GameLoop detected, instantiate and move to scene");
	    	GameObject GameLoopObj_inst = Instantiate(GameLoopObj);
	    	SceneManager.MoveGameObjectToScene(GameLoopObj_inst, SceneManager.GetSceneByBuildIndex(IDX_GAMELOOP));
    	}

    }
}
