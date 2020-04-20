using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMenu_AniController : MonoBehaviour
{
	public List<Animator> openAnimations = new List<Animator>();
	private static int countCompleted = 0; 
	private static int countTotal;

    void Awake()
    {
        startMenu_AniController.countTotal = openAnimations.Count;
    }

    public static void NotifyAnimationCompletion(Animator am)
    {
    	countCompleted += 1;
    	if (startMenu_AniController.countCompleted == startMenu_AniController.countTotal) {
    		GameLoop.State = GameState.songmenuState;
    	}
    }

}
