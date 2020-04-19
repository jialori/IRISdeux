using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtons : MonoBehaviour
{
	// Assume that the last series of build scenes are Level scenes,
	// and the first Level scene starts at this index.
	private int firstLevelBuildIndex = 1; // ToBeSet

	// The current level; -1 means no level scene is currently loaded
	private static int level = -1;

	void Awake()
	{
		int curLevel = GetCurrentLevelBuildIdx();
		if (curLevel == -1) {
			SceneManager.LoadSceneAsync(firstLevelBuildIndex, LoadSceneMode.Additive);
			StartMenuButtons.level = firstLevelBuildIndex;			
		} else {
			StartMenuButtons.level = curLevel;			
		}

	}

    public void ToGame() 
    {
    	SceneManager.UnloadSceneAsync("StartMenu");

    	// Change GameLoop's state
    	if (GameLoop.State != null) GameLoop.State = GameState.ingameState;
    }

    public void ToNextLevel() 
    {
    	if (StartMenuButtons.level == -1) return;

    	SceneManager.UnloadSceneAsync(StartMenuButtons.level);
    	int nxtLevel = GetNextLevelBuildIdx();
    	StartMenuButtons.level = nxtLevel;
    	SceneManager.LoadSceneAsync(nxtLevel, LoadSceneMode.Additive);
    }

    public void ToLastLevel() 
    {
    	if (StartMenuButtons.level == -1) return;

    	SceneManager.UnloadSceneAsync(StartMenuButtons.level);
    	int lastLevel = GetLastLevelBuildIdx();
    	StartMenuButtons.level = lastLevel;
    	SceneManager.LoadSceneAsync(lastLevel, LoadSceneMode.Additive);
    }

    private int GetCurrentLevelBuildIdx()
    {
    	int curLevel = -1;
    	if (SceneManager.sceneCount > 0)
        {
            for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.buildIndex >= firstLevelBuildIndex) {
                	curLevel = scene.buildIndex;
                }
            }
        }

        return curLevel;
    }

    private int GetNextLevelBuildIdx()
    {
    	if (StartMenuButtons.level == -1) 
    		return -1;
    	
    	int n = SceneManager.sceneCountInBuildSettings;
    	return (StartMenuButtons.level + 1 < n) ? StartMenuButtons.level + 1 : firstLevelBuildIndex;
    }

    private int GetLastLevelBuildIdx()
    {
    	if (StartMenuButtons.level == -1) 
    		return -1;
    	
    	int n = SceneManager.sceneCountInBuildSettings;
    	return (StartMenuButtons.level - 1 >= firstLevelBuildIndex) ? StartMenuButtons.level - 1 : n - 1;
    }


}
