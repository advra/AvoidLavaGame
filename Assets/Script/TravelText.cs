using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelText : MonoBehaviour {

    public PlayerControls playerControls;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Traveled " + playerControls.Meters.ToString("0.000") + "m";
	}
}
