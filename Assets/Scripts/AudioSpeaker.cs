using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class AudioSpeaker : MonoBehaviour, IObserver
{
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

	void Start()
    {
        GameLoop.Instance.Attach(this);

        _audioSource = GetComponent<AudioSource>();
        _audioSource.Pause();
    }

    void OnDestroy()
    {
        GameLoop.Instance.Detach(this);
    }

    public void UpdateOnChange(ISubject subject) 
    {
       switch (subject)
        {
            case GameLoop gp:
                switch (GameLoop.State)
                {
                    case InGameState state_ingame:
				        _audioSource.Play();
                        break;
                    default:
				        _audioSource.Pause();
                        break;
                }
                break;
        }
    }

}
