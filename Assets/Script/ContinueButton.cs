using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour {

    public TimeManager timeManager;
    public GameObject menuScreenPanel;
    public MenuButton menuButton;

    private Button button;

    // Use this for initialization
    void Start () {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(MenuContinueOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MenuContinueOnClick()
    {
        timeManager.RestoreTime();
        menuScreenPanel.SetActive(false);
        menuButton.gameObject.SetActive(true);
    }
}
