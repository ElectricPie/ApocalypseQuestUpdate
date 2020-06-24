using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDamage : MonoBehaviour {
    public float destroyTime = 1;

	void Start () {
        Invoke("Die", destroyTime);
	}
	
    void Die()
    {
        Destroy(gameObject);
    }
}
