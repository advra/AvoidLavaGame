using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRocks : MonoBehaviour {

    public GameObject Player;
    public GameObject RockPrefab;

    public ArrayList itemPool;

    public float yThreshold= 250f;                     // the distince if player passes, generate and place objects above player
    private float spawnHeight;

	// Use this for initialization
	void Start () {

        spawnHeight += Player.transform.position.y;

        itemPool = new ArrayList();

        for (int i = 0; i < 10; i++)
        {
            itemPool.Add(Instantiate(RockPrefab));
            
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    // Manually call this method if one of our rocks hit the lava
    public void Generate(GameObject go)
    {
        //GameObject go = itemPool.
    }

}
