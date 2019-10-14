using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    public enum Color
    {
        RED,
        YELLOW,
        GREEN,
        WHITE
    }

    public Gradient red;
    public Gradient yellow;
    public Gradient green;
    public Gradient white;

    LineRenderer _lineRenderer;

    public void RenderGradient(Color c)
    {
        switch (c)
        {
            case Color.RED:
                _lineRenderer.colorGradient = red;
                break;
            case Color.YELLOW:
                _lineRenderer.colorGradient = yellow;
                break;
            case Color.GREEN:
                _lineRenderer.colorGradient = green;
                break;
            default:
                _lineRenderer.colorGradient = white;
                break;
        }
    }

    // Use this for initialization
    void Start () {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
