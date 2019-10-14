using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLava : MonoBehaviour {

    public GameObject Player;
    public float speed = 0.5f;
    public float teleportThreshold;             //easy 30   //medium 20     //hard 15
    [SerializeField]
    private float _distanceFromPlayer;


    Vector3 _moveTo;
    bool _stopMoving = false;


	// Use this for initialization
	void Start () {
        if (teleportThreshold == 0) teleportThreshold = 15f;
        _moveTo = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (!Player.GetComponent<PlayerControls>().Playing || _stopMoving)
        {
            return;
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        checkDistanceFromPlayer();
    }

    void checkDistanceFromPlayer()
    {
        _distanceFromPlayer = Mathf.Abs(Player.transform.position.y - this.transform.position.y);

        // move towards player if too far
        if(_distanceFromPlayer > teleportThreshold)
        {
            _moveTo.y = Player.transform.position.y - 10f;
            transform.position = _moveTo;
        }
    }
}
