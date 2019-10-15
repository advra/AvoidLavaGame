using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreScreen : MonoBehaviour {

    public GameObject menuPanel;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject menuButton;
    public TimerText guiTimer;
    public Text comboText;
    public Text travelText;

    public PlayerControls playerControls;       // used to get reference to stats
    public Text timeTextStat;                   // stats are what is shown at the end of game screen
    public Text comboTextStat;      
    public Text travelTextStat;

    Image _panelImage;
    float _startAlpha;
    bool _display = false;
    Color _panelColor;


    // Use this for initialization
    void Start () {
        _panelImage = menuPanel.GetComponent<Image>();
        _panelColor = _panelImage.color;
        _startAlpha = _panelImage.color.a;
    }

    public void show()
    {
        _display = true;
        menuPanel.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        timeTextStat.text = guiTimer.getText();
        if(playerControls.MaxCombo <= 1)
        {
            comboTextStat.text = "Max Combo: " + playerControls.MaxCombo;
        }
        else
        {
            comboTextStat.text = "Max Combos: " + playerControls.MaxCombo;
        }
        travelTextStat.text = "Traveled: " + playerControls.Meters.ToString("0.000") + "meters";
        menuButton.SetActive(false);
        guiTimer.gameObject.SetActive(false);
        comboText.gameObject.SetActive(false);
        travelText.gameObject.SetActive(false);
    }

    void Update()
    {
        //if (_display)
        //{
        //    _panelImage.color = new Color(_panelColor.r, _panelColor.g, _panelColor.b, Mathf.Lerp(100, 255, progress/ 3.0f));
        //}
       
    }



}
