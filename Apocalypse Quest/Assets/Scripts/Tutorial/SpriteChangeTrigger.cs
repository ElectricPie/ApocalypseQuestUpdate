using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangeTrigger : MonoBehaviour {
    public string triggerTag = "";

    public GameObject level;
    public GameObject newLevel;
    public GameObject obstacle;

	// Use this for initialization
	void Start () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == triggerTag)
        {
            obstacle.SetActive(false);
            newLevel.SetActive(true);
            level.SetActive(false);
            transform.parent.gameObject.SetActive(false);
            //Destroy(transform.parent.gameObject);
        }
    }
}
