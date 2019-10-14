using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelManager : MonoBehaviour {

    public GameObject RockPlatformPrefab;
    public List<GameObject> platformPool;

    // modifity these in the editor
    public int numebrOfPlatforms = 20;
    public float levelWidth;
    public float minY;
    public float maxY;
    public float rotationRange;         // in units show in editor

    float _offsetFromCharacter = 5f;

    Vector3 spawnPosition;
    Quaternion spawnQuaternion;

    public List<GameObject> PlatformPool
    {
        get { return platformPool; }
    }

    public void setToUnused(GameObject go)
    {
        go.SetActive(false);
    }

    public void GeneratePlatform()
    {
        spawnPosition.y += Random.Range(minY, maxY);
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        spawnPosition.z = 2;
        spawnQuaternion = Quaternion.AngleAxis(Random.Range(-rotationRange, rotationRange), Vector3.forward);
        GameObject go = findAvailibleGameObject();

        if(go != null)
        {
            go.transform.position = spawnPosition;
            go.transform.rotation = spawnQuaternion;
            go.SetActive(true);
        }

    }

    private GameObject findAvailibleGameObject()
    {
        for(int i =0; i < platformPool.Count; i++)
        {
            if (!platformPool[i].activeInHierarchy)
            {
                return platformPool[i];
            }
        }

        return null;
    }

	// Use this for initialization
	void Start () {
        platformPool = new List<GameObject>();

        spawnPosition = new Vector3();
        spawnQuaternion = new Quaternion();

        spawnPosition.y += _offsetFromCharacter;

		for(int i = 0; i < numebrOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            spawnPosition.z = 2;
            spawnQuaternion = Quaternion.AngleAxis(Random.Range(-rotationRange, rotationRange), Vector3.forward);
            GameObject go = Instantiate(RockPlatformPrefab, spawnPosition, spawnQuaternion);
            platformPool.Add(go);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}




}
