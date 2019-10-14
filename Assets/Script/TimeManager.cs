using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slowFactor = 0.05f;
    public float slowLength = 2f;       // seconds

     void Update()
    {
        //Time.timeScale += (1f / slowLength) * Time.unscaledDeltaTime;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void EnableSlowMotion()
    {
        EnableSlowMotion(slowFactor);
    }

    public void EnableSlowMotion(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void RestoreTime()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }
}
