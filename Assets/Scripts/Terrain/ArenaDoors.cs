using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoors : MonoBehaviour {
    public GameObject player;
    public GameObject safeDoor;
    public GameObject northDoor;
    public GameObject westDoor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float northDistance = Vector3.Distance(player.transform.position, northDoor.transform.position);
        float westDistance = Vector3.Distance(player.transform.position, westDoor.transform.position);

        if (northDistance > 4)
            northDoor.SetActive(false);
        else
            northDoor.SetActive(true);

        if (westDistance > 8)
            westDoor.SetActive(false);
        else
            westDoor.SetActive(true);
    }
}
