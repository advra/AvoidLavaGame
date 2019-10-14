using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBounce : MonoBehaviour {

    public float distance;
    public float speed;

    Vector3 startPos;
    Vector3 endPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        if(distance == 0) distance = 5f;
        if (speed == 0) speed = 2f;

        endPos = new Vector3(startPos.x, startPos.y - distance, startPos.z);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
