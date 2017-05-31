using UnityEngine;
using System.Collections;

public class NavigationPoint : MonoBehaviour
{

    public Color Color;
    public NavigationPoint left;
    public NavigationPoint top;
    public NavigationPoint right;
    public NavigationPoint down;
    // Use this for initialization
    void Start ()
    {
        if (top!= null)
            top.down = this;
        if (right!= null)
            right.left = this;        
    }
	
	// Update is called once per frame
	void Update () {
        
    
    }

    void OnDrawGizmos()
    {        
        Gizmos.color = Color.yellow;
        if (left != null)
            DrawArrow.ForGizmo(transform.position, left.transform.position - transform.position);
        Gizmos.color = Color.green;
        if (down != null)
            DrawArrow.ForGizmo(transform.position, down.transform.position - transform.position);
        Gizmos.color = Color.magenta;
        if (top != null)
            DrawArrow.ForGizmo(transform.position, top.transform.position - transform.position);
        Gizmos.color = Color.cyan;
        if (right != null)
            DrawArrow.ForGizmo(transform.position, right.transform.position - transform.position);
    }

}
