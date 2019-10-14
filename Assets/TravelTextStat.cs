using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelTextStat : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
    }

    public void setDistance(float f)
    {
        text.text = "Traveled " + f.ToString("00.00");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
