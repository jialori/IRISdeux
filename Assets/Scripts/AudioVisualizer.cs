using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class AudioVisualizer : MonoBehaviour, IObserver
{
    public float minScale = 0.8f;
    public float maxScale = 2f;
    public int sampleSubscribedTo;
    private Vector2 initLocalScale;

    // used in buffering
    public bool useBuffer;
    public float bufferDegree;
    public float initBufferAmount = 0.005f;
    public float culmulativeBufferAmount = 1.2f;
    float _bufferPrevSample;
    float _bufferDecrease;
    float _bufferIncrease;
    bool _wasDecrease;

    void Awake()
    {
    	initLocalScale = transform.localScale;
    }

    void Start()
    {
    	AudioData.instance.Attach(this);
    }

    public void UpdateOnChange(ISubject subject) 
    {
    	// todo: add control to degree of buffer
    	float scale;
    	if (useBuffer)
    	{
    		float bufferedSample = CreateBuffer(AudioData._audioBands[sampleSubscribedTo]);
    		scale = LerpInScale(bufferedSample);
    		// variational buffering
   		}
    	else
    		scale = LerpInScale(AudioData._audioBands[sampleSubscribedTo]);
    	transform.localScale = scale * initLocalScale;
    }

    private float LerpInScale(float x)
    {
    	return (1 - x) * minScale + x * maxScale;
    }

    float CreateBuffer(float newSample)
    {
	    if (newSample > _bufferPrevSample)
	    {
	    	if (!_wasDecrease) // still increasing
	    	{
		        if (newSample - _bufferIncrease > _bufferPrevSample) 
		        	newSample = newSample - _bufferIncrease;
		        else
		        	newSample = _bufferPrevSample;
		        _bufferIncrease *= (culmulativeBufferAmount * bufferDegree);
	    	} 
	    	else 
	    	{ // new increase
	    		_bufferIncrease = initBufferAmount;
	    		_wasDecrease = false;
	    	}
	        _bufferDecrease = initBufferAmount * bufferDegree;
	    }
		else if (newSample < _bufferPrevSample)
	    {
	    	if (_wasDecrease) // still decresing
	    	{
		        if (newSample + _bufferIncrease < _bufferPrevSample) 
		        	newSample = newSample + _bufferIncrease;
		        else
		        	newSample = _bufferPrevSample;
		        // newSample += _bufferDecrease;
		        _bufferDecrease *= (culmulativeBufferAmount * bufferDegree);
	    	} 
	    	else 
	    	{ // new decrease
	    		_bufferDecrease = initBufferAmount;
	    		_wasDecrease = true;
	    	}
	    }

	    _bufferPrevSample = newSample;

        return newSample;
    }

}
