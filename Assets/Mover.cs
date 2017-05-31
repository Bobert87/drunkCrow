using UnityEngine;

public class Mover : MonoBehaviour
{

    public float timer = 0;
    public float timeToMove;
    public bool isMoving;
    public NavigationPoint StartPoint;
    public NavigationPoint EndPoint;
    public Vector3 OffSet;


    // Use this for initialization
    void Start ()
    {
        transform.position = StartPoint.transform.position+OffSet;
    }

    public void Move(NavigationPoint start, NavigationPoint end)
    {
        StartPoint = start;
        EndPoint = end;
        isMoving = true;              
    }

    // Update is called once per frame
	void Update () {
	    if (isMoving)
	    {
	        if (timer < timeToMove)
	        {
	            timer += Time.deltaTime;
	            transform.position = Vector3.Lerp(StartPoint.transform.position, EndPoint.transform.position, timer/timeToMove) + OffSet;
	        }
	        else
	        {
	            isMoving = false;
	            timer = 0;
	        }
	    }

	}
}
