using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    public TimeManager timeManager;
    public GameObject menuScreenPanel;

    private Button menuButton;

    // Use this for initialization
    void Start () {
        menuButton = gameObject.GetComponent<Button>();
        menuButton.onClick.AddListener(MenuOnClick);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOnClick();
        }
	}

    void MenuOnClick()
    {
        this.gameObject.SetActive(false);
        timeManager.PauseGame();
        menuScreenPanel.SetActive(true);
    }
}
