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
        hideAll();
    }
	
	// Update is called once per frame
	void Update () {

        if (!_playerControls.Alive)
        {
            hideAll();
            return;
        }

        _bottomLavaDist = (player.transform.position.y - Lava.transform.position.y);
        _leftLavaDist = (player.transform.position.x - LeftLava.transform.position.x);
        _rightLavaDist = (RightLava.transform.position.x - player.transform.position.x);

        // display for bottom
        if (_bottomLavaDist < 7f)
        {
            //leftArrow.transform.position = _leftArrowOriginalPos;
            leftArrow.SetActive(true);
            leftText.SetActive(true);
            rightArrow.SetActive(true);
            rightText.SetActive(true);
        }
        else if(_leftLavaDist < 3.25f)
        {
            //leftArrow.transform.position = new Vector2(transform.position.x - 3f, transform.position.y - 1f);
            leftArrow.SetActive(true);
            leftText.SetActive(true);
        }
        else if (_rightLavaDist < 3.25f)
        {
            rightArrow.SetActive(true);
            rightText.SetActive(true);
        }
        else
        {
            hideAll();
        }
		
	}

    void hideAll()
    {
        leftArrow.SetActive(false);
        leftText.SetActive(false);
        rightArrow.SetActive(false);
        rightText.SetActive(false);
    }
}
