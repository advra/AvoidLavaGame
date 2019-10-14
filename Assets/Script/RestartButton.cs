using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {

    Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(RestartLevelOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void RestartLevelOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel);
    }
}
