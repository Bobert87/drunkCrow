using UnityEngine;
using System.Collections;

public class UpDownBounce : MonoBehaviour
{

    private Vector3 _originalPos;
    private float _timer;
    private const float MaxTimer = Mathf.PI*2;
    public float timerMod;
    public float heightMod;
    // Use this for initialization
    void Start ()
	{
	    _originalPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_timer > MaxTimer)
	        _timer = 0;
	    else
	    {
	        _timer += Time.deltaTime*timerMod;
	    }
	    transform.localPosition = _originalPos + Vector3.up*Mathf.Sin(_timer)*heightMod;
	}
}
