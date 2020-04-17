using UnityEngine;
using UnityEngine.SceneManagement;

// This script sets up initial states that are independent 
// of other components. 
public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }
}
