using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {

    Text text;
    [SerializeField]
    bool timing = false;
    [SerializeField]
    float _startTime = 0f;


    float _currentTime;
    float _minutes;
    float _seconds;
    float _ms;
    float _ns;

    public void StartTimer()
    {
        timing = true;
        _startTime = Time.time;
    }

    public void StopTimer()
    {
        timing = false;
    }

    public string getText()
    {
        return text.text;
    }

	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        text.text = "Time 00:00:000";
	}

	
	// Update is called once per frame
	void Update () {
        if (!timing)
        {
            return;
        }

        _currentTime = Time.time - _startTime;
        _minutes = (int)(_currentTime / 60f) % 60;
        _seconds = (int)(_currentTime % 60f);
        _ms = (int)(_currentTime * 1000f) % 1000;

        text.text =  "Time " + _minutes.ToString("00") + ":" + _seconds.ToString("00") + ":" + _ms.ToString("00");

    }
}
