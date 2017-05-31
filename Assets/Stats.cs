using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    public int MoveCount;    
    public List<int> Triggers;
    public List<WriteText> Texts;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Restart()
    {
        MoveCount = 0;
        for (int i = 0; i < 23; i++)
        {
            GameObject go = GameObject.Find("Strikes (" + i.ToString() + ")");
            TextMesh tm = go.GetComponent<TextMesh>();
            tm.text = "";
            go.transform.FindChild("Strike").gameObject.SetActive(false);
        }
    }

    public void AddMove()
    {
        MoveCount++;
        int currentObj = MoveCount/5;
        int lineNumbers = MoveCount%5;
        if (MoveCount < 115)
        {
            GameObject go = GameObject.Find("Strikes (" + currentObj.ToString() + ")");
            TextMesh tm = go.GetComponent<TextMesh>();
            tm.text = "";
            for (int i = 0; i < lineNumbers; i++)
            {
                tm.text += '/';
            }
        }
        if ((lineNumbers == 0) && currentObj > 0)
        {
            GameObject st = GameObject.Find("Strikes (" + (currentObj - 1).ToString() + ")");
            st.transform.FindChild("Strike").gameObject.SetActive(true);
        }
        for (int i = 0; i < Triggers.Count; i++)
        {
            if (MoveCount == Triggers[i])
            {
                Texts[i].Animate = true;
            }
        }
    }
}
