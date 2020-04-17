using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtonPress : MonoBehaviour
{
	// public enum Level
	// {
	// 	Default
	// }

	// private Level level;

	// void Awake()
	// {
	// 	level = Level.Default;
	// }

    public void ToGame() 
    {
    	SceneManager.UnloadSceneAsync("StartMenu");
    	if (GameLoop.State != null) GameLoop.State = GameState.ingameState;
    }
}
