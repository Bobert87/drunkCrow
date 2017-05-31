using UnityEngine;
using System.Collections;

public class WriteText : MonoBehaviour
{
    public float timerWritting;
    public float timeToWrite;
    public TextMesh Instructions;
    public string CompleteString;
    public bool Animate;
	// Use this for initialization
	void Start ()
	{
	    Instructions = GetComponent<TextMesh>();
	    CompleteString = Instructions.text;
	    Instructions.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	    if (Animate)
	    {
	        if (timerWritting <= timeToWrite)
	        {
	            timerWritting += Time.deltaTime;
	        }
	        else
	        {
	            Instructions.text += CompleteString.Substring(Instructions.text.Length, 1);
	            if (Instructions.text.Length < CompleteString.Length)
	            {
	                timerWritting = 0;
	            }
	            else
	            {
	                timerWritting = 0;
	                Animate = false;
	            }
	        }
	    }
	}
}
