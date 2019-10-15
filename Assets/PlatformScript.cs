using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    public Color regularColor;
    public Color touchColor;

    private bool touched = false;

    public bool Touched
    {
        get { return touched; }
    }

    public void PlayerTouch()
    {
        touched = true;
        GetComponent<SpriteRenderer>().color = touchColor;
    }


	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = regularColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
