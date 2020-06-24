using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour {
    public float destroyTime = 0.7f;

    void Start()
    {
        Invoke("Die", destroyTime);
        transform.Rotate(Vector3.forward * 90);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        CancelInvoke("Die");
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * -(180 / (destroyTime * 50)));
    }
}
