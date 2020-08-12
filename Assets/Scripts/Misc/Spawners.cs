using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawners : MonoBehaviour {
    public GameObject northSpawner;
    public GameObject westSpawner;
    public GameObject enemyPrefab;

    public void SpawnEnemies()
    {
        GameObject Temp = GameObject.Find("Spawn Wave Panel");

        InputField id = Temp.transform.GetChild(1).GetComponent<InputField>();
        InputField count = Temp.transform.GetChild(2).GetComponent<InputField>();

        SpawnEnemies(int.Parse(id.text), int.Parse(count.text));
    }

    public void SpawnEnemies(int enemyID, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Debug.Log("Spawning Enemy");
            int chosenSpawner = Random.Range(0, 2);
            GameObject enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.GetComponent<EnemyController>().id = enemyID;
            enemy.name = "Enemy" + i; 

            if (chosenSpawner == 1)
                enemy.transform.position = northSpawner.transform.position;
            else
                enemy.transform.position = westSpawner.transform.position;

        }
    }

 
}
