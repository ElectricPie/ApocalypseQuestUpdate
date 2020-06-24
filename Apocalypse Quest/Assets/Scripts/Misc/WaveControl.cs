using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour {
    public Spawners spawners;

	// Use this for initialization
	void Start () {
        Invoke("WaveOne", 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void WaveOne()
    {
        int enemyCount = 7;
        int enemyID = 0;

        spawners.SpawnEnemies(enemyID, enemyCount);
    }

    void WaveTwo()
    {

    }

    void WaveThree()
    {

    }
}
