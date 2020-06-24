using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spawn
{
    public string name; 
    public Transform spawn;
}

public class ZombieSpawn : MonoBehaviour {
    public int timer = 5;
    public float timerIncrement = 0.1f;
    public GameObject zombiePrefab;

    private float counter = 0.0f;

    public List<Spawn> SpawnLocations = new List<Spawn>();

    void Update () {
	    if(counter <= timer)
        {
            counter += timerIncrement;
        }

        Random.Range(0, 3);

        if (counter >= 5) {
            Instantiate(zombiePrefab, SpawnLocations[Random.Range(0, 3)].spawn.position, SpawnLocations[Random.Range(0, SpawnLocations.Count)].spawn.rotation);
            counter = 0;
        }
	}
    
}
