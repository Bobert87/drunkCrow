using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public Color CurrentColor;
    public Color Red;
    public Color Blue;
    public Color NoColor;    
    public NavigationPoint StartingPoint;
    public NavigationPoint CurrentPoint;
    public NavigationPoint FinishPoint;
    public Intro IntroFrame;
    public Intro WinFrame;
    public Intro LooseFrame;
    public Stats Stats;
    

    public GameObject centerCrow;
    public Mover PlayCrow;

    public AudioClip Move;
    public AudioClip Bump;

    public AudioSource audio;

    private bool _axisInUse;    

    public NavigationPoint trail;

    public GameState CurrentState = GameState.Intro;

    Vector3 v ;
    Vector3 o;

    public enum GameState
    {
        Intro,
        Game,
        Loose,
        Win
    }

	// Use this for initialization
	void Start () {	    
	    CurrentPoint = StartingPoint;
	    CurrentColor = Blue;
        CurrentState = GameState.Intro;
	    audio = GetComponent<AudioSource>();
        v = PlayCrow.transform.localScale;
	    o = PlayCrow.OffSet;
	}
	
	// Update is called once per frame
	void Update () {
	    if (CurrentState == GameState.Intro)
	    {
	        if (IntroFrame.FrameAnimationSate == Intro.FrameState.None)
	        {
                IntroFrame.FrameAnimationSate = Intro.FrameState.FadeIn;
	        }
	        if (IntroFrame.FrameAnimationSate == Intro.FrameState.WaitingKey)
	        {
	            if (Input.anyKey)
	                IntroFrame.FrameAnimationSate = Intro.FrameState.FrameFadeOut;
	        }
            if (IntroFrame.FrameAnimationSate == Intro.FrameState.Finished)
            {
                CurrentState = GameState.Game;
            }
	    }

	    if (CurrentState == GameState.Game)
	    {
	        if (!PlayCrow.isMoving)
	            GetInput();
	        if (CurrentPoint == FinishPoint)
	        {
	            CurrentState = GameState.Win;
                WinFrame.FrameAnimationSate = Intro.FrameState.FadeIn;
	        }
	        if ((Stats.MoveCount >= 115) && (CurrentPoint != FinishPoint))
	        {
	            CurrentState = GameState.Loose;
                LooseFrame.FrameAnimationSate = Intro.FrameState.FadeIn;
            }
	    }

	    if (CurrentState == GameState.Win)
	    {
	        if (Input.anyKey && WinFrame.FrameAnimationSate == Intro.FrameState.WaitingKey)
	        {                
	            WinFrame.FrameAnimationSate = Intro.FrameState.FrameFadeOut;
                PlayCrow.Move(CurrentPoint,StartingPoint);
	            CurrentPoint = StartingPoint;
	            Stats.Restart();
	            trail = null;
                CurrentState = GameState.Game;
	        }
	    }

	    if (CurrentState == GameState.Loose)
	    {
            if (Input.anyKey && LooseFrame.FrameAnimationSate == Intro.FrameState.WaitingKey)
            {
	            LooseFrame.FrameAnimationSate = Intro.FrameState.FrameFadeOut;
                PlayCrow.Move(CurrentPoint, StartingPoint);
                CurrentPoint = StartingPoint;
                Stats.Restart();
                trail = null;
                CurrentState = GameState.Game;
            }
	    }
	}
    
    private bool Navigate(NavigationPoint navPoint)
    {
        _axisInUse = true;
        if (CanNavigate(navPoint))
        {
            PlayCrow.Move(CurrentPoint, navPoint);
            trail = CurrentPoint;
            CurrentPoint = navPoint;
            
            if (navPoint.Color == Blue || navPoint.Color == Red)
                ChangeColor();
            audio.clip = Move;
            Stats.AddMove();
            return true;
        }
        Stats.AddMove();
        audio.clip = Bump;
        audio.Play();
        return false;
    }

    private void GetInput()
    {
        if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
        {
            _axisInUse = false;
        }

        
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (!_axisInUse)
            {
                _axisInUse = true;
                if (Navigate(CurrentPoint.right))
                {
                    PlayCrow.transform.localScale = new Vector3(v.x, v.y, v.z);
                    PlayCrow.OffSet = new Vector3(o.x, o.y, o.z);
                }
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (!_axisInUse)
            {
                _axisInUse = true;
                if (Navigate(CurrentPoint.left))
                {
                    PlayCrow.transform.localScale = new Vector3(-v.x, v.y, v.z);
                    PlayCrow.OffSet = new Vector3(-o.x, o.y, o.z);
                }
            }
        }   
        if (Input.GetAxis("Vertical") > 0)
        {
            if (!_axisInUse)
            {
                _axisInUse = true;
                Navigate(CurrentPoint.top);                                
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if (!_axisInUse)
            {
                _axisInUse = true;
                Navigate(CurrentPoint.down);                                
            }
        }
    }

    private void ChangeColor()
    {
        CurrentColor = CurrentColor == Blue ? Red : Blue;
    }

    public bool CanNavigate(NavigationPoint navPoint)
    {
        
        if (navPoint == null)
            return false;
        if (CurrentPoint.Color == Red || CurrentPoint.Color == Blue)
        {
            return navPoint != trail;
        }
        if (CurrentColor == Red)
        {
            return navPoint.Color == Blue || navPoint.Color == NoColor;
        }

        if (CurrentColor == Blue)
        {
            return navPoint.Color == Red || navPoint.Color == NoColor;
        }
        return false;
    }
}
