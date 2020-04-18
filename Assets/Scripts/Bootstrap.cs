using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
    	if (!SceneManager.GetSceneByBuildIndex(0).isLoaded)
	        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }
}
