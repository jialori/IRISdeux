using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System;

public class AudioData : MonoBehaviour, ISubject
{
    AudioSource _audioSource;
    public float[] _samples = new float[512];
    public float[] _freqBands = new float[8];
    public float[] _freqBandsHighest = new float[8];
    public float[] _bufferedBands = new float[8];
    public static float[] _audioBands = new float[8];
    public static float[] _audioBandsBuffered = new float[8];
    float[] _bufferDecrease = new float[8];

    public static AudioData instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreatAudioBands();
        foreach (var x in _audioBandsBuffered)
        {
            // Debug.Log(x);            
        }
        Notify();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        int count_prev = 0;
        for (int i = 0; i < 8; i++) {
            float average = 0;
            int sampleCount = (int)Math.Pow(2, i) * 2;

            if (i == 7) sampleCount += 2;

            for (int j = 0; j < sampleCount; j++) {
                // average += _samples[count];
                average += _samples[count] * (count + 1);
                count++;
            }

            // Debug.Log("freqband: " + count.ToString());
            // Debug.Log("freqband: " + average.ToString());
            // average /= (count - count_prev);
            average /= count;
            count_prev = count;

            // _freqBands[i] = average;
            _freqBands[i] = average * 10;
        }

    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBands[i] > _bufferedBands[i])
            {
                _bufferedBands[i] = _freqBands[i];
                _bufferDecrease[i] = 0.005f;
            }

            if (_freqBands[i] < _bufferedBands[i])
            {
                _bufferedBands[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void CreatAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBands[i] > _freqBandsHighest[i]) _freqBandsHighest[i] = _freqBands[i];
            _audioBands [i] = (_freqBands[i] / _freqBandsHighest[i]);
            _audioBandsBuffered[i] = (_bufferedBands[i] / _freqBandsHighest[i]);
        }
    }


    // Subscription Pattern ============
    private List<IObserver> _observers = new List<IObserver>();

    // The subscription management methods.
    public void Attach(IObserver observer)
    {
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
    }

    // Trigger an update in each subscriber.
    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.UpdateOnChange(this);
        }
    }
}
