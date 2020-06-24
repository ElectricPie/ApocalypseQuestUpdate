using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    //Public
    public float adjustmentAngle = 90;

    //Private
    private NavMeshAgent agent;
    private bool disengagin = false;

    private void Start()
    {
        //Debug.Log("Parent: " + transform.parent);
        //agent = transform.parent.gameObject.GetComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Movement(Vector3 destination)
    {
        if (!disengagin)
        {
            //Debug.Log("Moving");
            agent.Resume();
            agent.destination = destination;
        }
    }

    public void Rotation(GameObject target)
    {
        agent.Stop();

        Vector3 difference = target.transform.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;

        Quaternion newRot = Quaternion.Euler(new Vector3(0.0f, -rotZ + adjustmentAngle, 0.0f));

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * 10.0f);
    }

    public void Disengage()
    {
        disengagin = true;
        Vector3 thisPos = transform.position;

        agent.speed = 100;
        agent.angularSpeed = 0;
        agent.acceleration = 100;

        agent.destination = transform.position - (transform.forward * 3);
        //agent.destination = transform.position + (Vector3.back * 2);

        Invoke("StopDisengage", 0.5f);
    }

    void StopDisengage()
    {
        agent.speed = 3.5f;
        agent.angularSpeed = 480;
        agent.acceleration = 8;

        disengagin = false;
    }
}
