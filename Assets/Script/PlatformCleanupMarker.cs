using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCleanupMarker : MonoBehaviour {

    public LevelManager levelManager;

    

	// Use this for initialization
	void Start () {
        //StartCoroutine(checkStrandlers());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // backup, in case collision event is missed
    //IEnumerator checkStrandlers()
    //{
    //    yield return new WaitForSeconds(30);
    //    if(transform.position < levelManager.PlatformPool.ForEach)
    //}

    // if platforms collide with this, mark them as unused and  reposition them back to the top of level
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Lava Cleaner Hit Ground");
            levelManager.setToUnused(collision.gameObject);
            levelManager.GeneratePlatform();
        }
    }
}
