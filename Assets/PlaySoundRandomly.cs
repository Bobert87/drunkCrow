using UnityEngine;
using System.Collections;

public class PlaySoundRandomly : MonoBehaviour
{

    public float millisecsToNextCaw;
    public float timer = 0;
	// Use this for initialization
	void Start () {
        millisecsToNextCaw = Random.Range(10, 25);
    }
	
	// Update is called once per frame
	void Update () {
	    if (timer >= millisecsToNextCaw)
	    {
            GetComponent<AudioSource>().Play();
	        timer = 0;
	        millisecsToNextCaw = Random.Range(10, 25);
	    }
	    else
	    {
	        timer += Time.deltaTime;
	    }
	}
}
