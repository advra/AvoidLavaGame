using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTextManager : MonoBehaviour {

    public GameObject player;
    public GameObject Lava;         //front topmost sprite
    public GameObject LeftLava;
    public GameObject RightLava;

    public GameObject leftArrow;
    public GameObject leftText;
    public GameObject rightArrow;
    public GameObject rightText;

    public GameObject cautionLeftArrow;
    public GameObject cautionLeftText;
    public GameObject cautionRightArrow;
    public GameObject cautionRightText;

    Vector2 leftTextStartPos;
    Vector2 _leftArrowOriginalPos;

    float _bottomLavaDist;
    float _leftLavaDist;
    float _rightLavaDist;

    PlayerControls _playerControls;
   

    // Use this for initialization
    void Start () {
        _playerControls = player.GetComponent<PlayerControls>();
        leftTextStartPos = leftText.transform.position;
        _leftArrowOriginalPos = leftArrow.transform.position;
        hideAllDanger();
    }
	
	// Update is called once per frame
	void Update () {

        if (!_playerControls.Alive)
        {
            hideAllDanger();
            return;
        }

        _bottomLavaDist = (player.transform.position.y - Lava.transform.position.y);
        _leftLavaDist = (player.transform.position.x - LeftLava.transform.position.x);
        _rightLavaDist = (RightLava.transform.position.x - player.transform.position.x);

        // display for bottom danger lava bottom
        if (_bottomLavaDist < 7f)
        {
            leftArrow.SetActive(true);
            leftText.SetActive(true);
            rightArrow.SetActive(true);
            rightText.SetActive(true);
        }
        else
        {
            hideAllDanger();
        }

        // display for caution text left and right
        if (_leftLavaDist < 3.25f)
        {
            cautionLeftArrow.transform.position = new Vector2(cautionLeftArrow.transform.position.x, player.transform.position.y);
            cautionLeftArrow.SetActive(true);
            cautionLeftText.SetActive(true);
        }
        else if (_rightLavaDist < 3.25f)
        {
            cautionRightArrow.transform.position = new Vector2(cautionRightArrow.transform.position.x, player.transform.position.y);
            cautionRightArrow.SetActive(true);
            cautionRightText.SetActive(true);
        }
        else
        {
            hideAllCaution();
        }

    }

    void hideAllDanger()
    {
        leftArrow.SetActive(false);
        leftText.SetActive(false);
        rightArrow.SetActive(false);
        rightText.SetActive(false);
    }

    void hideAllCaution()
    {
        cautionLeftArrow.SetActive(false);
        cautionLeftText.SetActive(false);
        cautionRightArrow.SetActive(false);
        cautionRightText.SetActive(false);
    }
}
