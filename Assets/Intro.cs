using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{    
    public SpriteRenderer Frame;    
    public SpriteRenderer ConfusedCrow;
    public float timerFadeIn;
    public float timeToFadeIn;

    public float timerWritting;
    public float timeToWrite;

    public float timerCrowFadeIn;
    public float timetoCrowFadeIn;

    public TextMesh Instructions;    
    public string CompleteString;

    public float timerFadeOut;
    public float timeToFadeOut;

    public string TextCue;

    public float TextWritting;
    
    public float FrameFadeOut;

    public FrameState FrameAnimationSate;

    public enum FrameState
    {
        None,
        FadeIn,
        Writing,
        WaitingKey,
        FrameFadeOut,
        Finished
    }    

    // Use this for initialization
	void Start ()
	{
	    FrameAnimationSate = FrameState.None;
	    Frame.color = Color.clear;
	    ConfusedCrow.color = Color.clear;
        timerFadeIn = 0;
	    timerWritting = 0;
        CompleteString = Instructions.text;
        Instructions.text = "";	    

	}
	
	// Update is called once per frame
	void Update () {
        #region fadeinframe

	    if (FrameAnimationSate == FrameState.None)
	    {
            Frame.color = Color.clear;
	        ConfusedCrow.color = Color.clear;
            Instructions.color = Color.clear;
	    }
	    if (FrameAnimationSate == FrameState.FadeIn)
	    {
            Instructions.color = Color.black;
            if (timerFadeIn <= timeToFadeIn)
	        {
	            timerFadeIn += Time.deltaTime;
	            Frame.color = Color.Lerp(Color.clear, Color.white, timerFadeIn/timeToFadeIn);
            }
	        else
	        {
	            timerFadeIn = 0;
	            FrameAnimationSate = FrameState.Writing;
	        }
	    }
        #endregion
        #region writing
        if (FrameAnimationSate == FrameState.Writing)
	    {
	        if (timerWritting <= timeToWrite)
	        {
	            timerWritting += Time.deltaTime;	            
	        }
	        else
	        {                
                Instructions.text +=  CompleteString.Substring(Instructions.text.Length, 1);
	            if (Instructions.text.Length < CompleteString.Length)
	            {
	                timerWritting = 0;
	            }
	            else
	            {
	                timerWritting = 0;
	                FrameAnimationSate = FrameState.WaitingKey;
	            }
	        }
	        if (Instructions.text.Contains(TextCue))
	        {
	            if (timerCrowFadeIn <= timetoCrowFadeIn)
	            {                    
                    timerCrowFadeIn += Time.deltaTime;
	                ConfusedCrow.color = Color.Lerp(Color.clear, Color.white, timerCrowFadeIn/timetoCrowFadeIn);
	            }
                
	        }
	    }
        #endregion
        #region fadeout
	    if (FrameAnimationSate == FrameState.FrameFadeOut)
	    {
	        if (timerFadeOut < timeToFadeOut)
	        {
	            timerFadeOut += Time.deltaTime;
                Frame.color = Color.Lerp(Color.white,Color.clear, timerFadeOut / timeToFadeOut);                
                ConfusedCrow.color = Color.Lerp(Color.white, Color.clear, timerFadeOut / timeToFadeOut);
                Instructions.color = Color.Lerp(Color.white, Color.clear, timerCrowFadeIn / timetoCrowFadeIn);
            }
	        else
	        {
	            timerFadeOut = 0;
                FrameAnimationSate = FrameState.Finished;
	        }
	    }
	    #endregion
    }
}
