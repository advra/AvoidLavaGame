using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour {

    public GameObject Block1;

    public int preLoadedObjects = 35;
    public float blockSizes;

    List<GameObject> platforms;

    // Use this for initialization
    void Start () {
        platforms = new List<GameObject>();

        //GameObject go = Instantiate(Block1);
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
