using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Provide scene-related functions and tracks currently opened scenes.
// Other:
// static, non-monobehaviour
// (static because we won't need multiple instantiations of this)
public class SceneManagerExt
{

	// problem: the first scene loaded is not registered here. 
	// - use Bootstrap to register! a little repetitive but ok for now

	// loaded condition for all scenes (represented by their build index)
	public static Dictionary<int, bool> scenesToLoaded;

	// The current level; -1 means no level scene is currently loaded
	// private static int curLevel = -1;
	public static int CurLevel 
	{
		get => GetCurrentLevelBuildIdx(); // most updated info
	}


	static SceneManagerExt() 
	{
		scenesToLoaded = new Dictionary<int, bool>();
		for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			scenesToLoaded.Add(i, false);
		}
	}


	// ============ Functions correponding to SceneManager functions ============

	public static bool IsLoaded(int buildIndex)
	{
		return scenesToLoaded[buildIndex];
	}


	public static void LoadScene(int buildIndex, LoadSceneMode mode = LoadSceneMode.Single)
	{
		// scenesToLoaded[buildIndex] = true;
		scenesToLoaded.Remove(buildIndex);
		scenesToLoaded.Add(buildIndex,true);
    	// Debug.Log("loaded scene" + buildIndex.ToString() + " " + scenesToLoaded[buildIndex]);
		SceneManager.LoadScene(buildIndex, mode);
	}

	public static void LoadSceneAsync(int buildIndex, LoadSceneMode mode = LoadSceneMode.Single)
	{
		// scenesToLoaded[buildIndex] = true;		
		scenesToLoaded.Remove(buildIndex);
		scenesToLoaded.Add(buildIndex,true);
    	// Debug.Log("loaded scene" + buildIndex.ToString() + " " + scenesToLoaded[buildIndex]);
		SceneManager.LoadSceneAsync(buildIndex, mode);
	}


	public static void UnloadSceneAsync(int buildIndex)
	{
		// scenesToLoaded[buildIndex] = false;		
		scenesToLoaded.Remove(buildIndex);
		scenesToLoaded.Add(buildIndex,false);
		SceneManager.UnloadSceneAsync(buildIndex);
	}

	// ============ Functions correponsing to SceneManager functions END ============


	public static void NotifySceneIsLoaded(int buildIndex)
	{
		// scenesToLoaded[buildIndex] = true;
		scenesToLoaded.Remove(buildIndex);
		scenesToLoaded.Add(buildIndex,true);
    	// Debug.Log("notify scene load" + buildIndex.ToString() + " " + scenesToLoaded[buildIndex]);


	}


    public static int GetCurrentLevelBuildIdx()
    {
        for (int i = Macro.IDX_FIRSTLEVEL; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
        	// Debug.Log("scene" + i.ToString() + " " + scenesToLoaded[i]);
        	if (scenesToLoaded[i]) {
        		return i;
        	}
        }

        return -1;
    }


    public static int GetNextLevelBuildIdx()
    {
    	int curLevel = CurLevel;
    	if (curLevel == -1){ 
    		return -1;
    	} else {    	
	    	int n = SceneManager.sceneCountInBuildSettings;
	    	return (curLevel + 1 < n) ? curLevel + 1 : Macro.IDX_FIRSTLEVEL;
    	}
    }


    public static int GetLastLevelBuildIdx()
    {
    	int curLevel = CurLevel;
    	if (curLevel == -1) 
    		return -1;
		else {
	    	int n = SceneManager.sceneCountInBuildSettings;
	    	return (curLevel - 1 >= Macro.IDX_FIRSTLEVEL) ? curLevel - 1 : n - 1;
		}    	
    }	
}
