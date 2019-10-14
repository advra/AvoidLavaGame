using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public GameObject target;           // player
    public Vector2 offset;
    public GameObject LavaBottom;       // gives camera ref where lava is in world space

    public GameObject leftWall;
    public GameObject rightWall;

    // lock camera limits
    public float camMinX;
    public float camMaxX;

    // shake variables
    public float shakeMagnitude = 0.05f;
    public float shaketime = 0.25f;
    Vector3 cameraInitialPosition;

    PlayerControls playerControls;
    Camera _mainCamera;
    float _targetCameraSize;
    float _prevTargetCameraSize;

    float _cameraMinZoom = 5f;
    float _cameraMaxZoom = 8f;


    // Use this for initialization
    void Start () {
        playerControls = target.GetComponent<PlayerControls>();
        _mainCamera = GetComponent<Camera>();
        _targetCameraSize = 5;
    }
	
	// Update is called once per frame
	void Update () {

        _prevTargetCameraSize = _targetCameraSize;
        //////// Camera Zooming
        // if fast fling from player (via velocity), zoom out


        //TODO: Fix jitter bug
        // if user is aiming then camera size is set by convertUserDragToCameraSize()
        if (!playerControls.IsAiming)
        {
            if (playerControls.TrueVelocity.x > 8 || playerControls.TrueVelocity.y > 8)
            {
                _targetCameraSize = _cameraMaxZoom;
            }
            else if (playerControls.TrueVelocity.x > 5 || playerControls.TrueVelocity.y > 5)
            {
                _targetCameraSize = 7f;
            }
            else
            {
                _targetCameraSize = _cameraMinZoom;
            }

            if(_mainCamera.orthographicSize < _cameraMinZoom && _mainCamera.orthographicSize > _cameraMaxZoom)
            {
                return;
            }

            if (_targetCameraSize < _mainCamera.orthographicSize)
            {
                _mainCamera.orthographicSize = _mainCamera.orthographicSize - 1 * Time.deltaTime;

            }
            else
            {
                _mainCamera.orthographicSize = _mainCamera.orthographicSize + 1 * Time.deltaTime;
            }

        }
        else
        {
            if (_targetCameraSize < _mainCamera.orthographicSize)
            {
                _mainCamera.orthographicSize = _mainCamera.orthographicSize - 1 * Time.fixedUnscaledDeltaTime;
            }
            else
            {
                _mainCamera.orthographicSize = _mainCamera.orthographicSize + 1 * Time.fixedUnscaledDeltaTime;
            }
        }



        //////// Camera Movement

        //transform.position = Vector2.Lerp(transform.position, target.transform.position + offset, 0.1f);

        float x = Mathf.Lerp(transform.position.x, target.transform.position.x + offset.x, 0.1f);
        float y = Mathf.Lerp(transform.position.y, target.transform.position.y + offset.y, 0.1f);
        //Mathf.Clamp(y, LavaBottom.gameObject.transform.position.y, 500f);
        if (transform.position.x > camMaxX)
        {
            x = Mathf.Lerp(camMaxX, target.transform.position.x + offset.x, 0.1f);
        }

        if (transform.position.x < camMinX)
        {
            x = Mathf.Lerp(camMinX, target.transform.position.x + offset.x, 0.1f);
        }

        transform.position = new Vector3(x, y, -20);

        // update so walls stick to camera
        leftWall.transform.position = new Vector2(leftWall.transform.position.x, this.transform.position.y);
        rightWall.transform.position = new Vector2(rightWall.transform.position.x, this.transform.position.y);
    }

    // when user is aiming, zoom camera out
    public void convertUserDragToCameraSize(Vector2 distance)
    {
        if (distance.x > 225 || distance.y > 225)
        {
            _targetCameraSize = 8f;
        }
        else if (distance.x > 150 || distance.y > 150)
        {
            _targetCameraSize = 7f;
        }
        else if (distance.x > 75 || distance.y > 75)
        {
            _targetCameraSize = 6f;
        }
        else
        {
            _targetCameraSize = 5f;
        }
    }

    public void Shake()
    {
        cameraInitialPosition = transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shaketime);
    }


    void StartCameraShaking()
    {
        //float cameraSHakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        //float cameraSHakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraSHakingOffsetX = Random.Range(0, 0.001f);
        float cameraSHakingOffsetY = Random.Range(0, 0.001f);
        Vector3 cameraIntermediatePosition = transform.position;
        cameraInitialPosition.x += cameraSHakingOffsetX;
        cameraInitialPosition.y += cameraSHakingOffsetY;
        transform.position = cameraInitialPosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        transform.position = cameraInitialPosition;
    }

}
