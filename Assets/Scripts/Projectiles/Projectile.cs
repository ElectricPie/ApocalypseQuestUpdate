using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 5.0f;
    public float destroyTime = 1.0f;

	// Use this for initialization
	void Start () {
        Invoke("Die", destroyTime);		
	}

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        CancelInvoke("Die");
    }

    // Update is called once per frame
    void FixedUpdate () {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
    }
}
