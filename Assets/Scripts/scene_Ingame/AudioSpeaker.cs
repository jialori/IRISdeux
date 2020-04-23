using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class AudioSpeaker : MonoBehaviour, IObserver
{
    AudioSource _audioSource;

    // todo: can be singleton? one game only has one background music

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

	void Start()
    {
        GameLoop.Instance.Attach(this);
        SyncWithGameloop();        
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
                SyncWithGameloop();
                break;
        }
    }


    private void SyncWithGameloop()
    {
        switch (GameLoop.State)
        {
            case InGameState state_ingame:
                _audioSource.Play();
                // if (_audioSource.time == 0f) {
                //     _audioSource.Play();
                // } else {
                //     _audioSource.UnPause();
                // }
                break;
            case SettingsMenuState state_settingsmenu:
                _audioSource.Pause();
                break;
            default:
                _audioSource.Pause();
                break;
        }
    }
}
