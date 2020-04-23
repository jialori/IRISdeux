using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class startMenu_AniController : MonoBehaviour, IObserver
{
	public List<Animator> animators = new List<Animator>();
	private static int countCompleted = 0; 
	private static int countTotal;

    void Awake()
    {
        countTotal = animators.Count;
        SyncWithGameloop();
    }


    void Start()
    {
        GameLoop.Instance.Attach(this);
    }


    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);
    }


    public static void NotifyAnimationCompletion(Animator am)
    {
    	countCompleted += 1;
    	if (countCompleted == countTotal) {
    		GameLoop.State = GameState.songmenuState;
    	}
    }


    public void UpdateOnChange(ISubject subject) 
    {
        switch (subject)
        {
            case GameLoop gp:
                SyncWithGameloop();
                break;
        }
    }


    private void SyncWithGameloop()
    {
        switch (GameLoop.State)
        {
            case SongMenuState state_songmenu:
                PlayIdleAnimations();
                break;
            default:
                break;
        }
    }

    private void PlayIdleAnimations()
    {
        foreach (Animator animator in animators)
        {
            animator.Play("Idle");
        }
    }
}
