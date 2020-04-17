using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBounceVFX : MonoBehaviour
{
	public float alphaLow;
	public float alphaHigh;
	public float durationInSeconds;
	private float durationSum = 0;
	private Text text;
	private Color color;
    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
    	text = (Text) gameObject.GetComponent(typeof(Text));
    	color = text.color;
    	alpha = color.a;
    }

    // Update is called once per frame
    void Update()
    {

        alpha = (durationSum % durationInSeconds);
    }

}
