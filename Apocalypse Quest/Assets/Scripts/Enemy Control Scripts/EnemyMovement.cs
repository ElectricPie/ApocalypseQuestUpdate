using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    //Public vairables
    public int aggroRange = 5;
    public int pursuitRange = 20;

    public float adjustmentAngle = 90.0f;
    public float distanceToTarget;

    //Private vairables
    private int originalAggroRange;

    private bool inPursuit = false;

    private Transform target;
    private Vector3 originPoint;
    private EnemyController Controller;
    private NavMeshAgent agent;


    // Use this for initialization
    void Start () {
        Controller = GetComponent<EnemyController>();
        originalAggroRange = aggroRange;
 
        target = Controller.Target.transform;

        originPoint = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }


	
	// Update is called once per frame
	void Update () {
        Controller = GetComponent<EnemyController>();
        target = Controller.Target.transform;
        distanceToTarget = Controller.DistanceToTarget;

        if(target != null)
        {
            Movement();
        }
    }

    void Movement()
    {
        float distanceFromOrigin = Vector3.Distance(transform.position, originPoint);
        
        if (distanceFromOrigin >= pursuitRange - (originalAggroRange))
        {
            aggroRange = 0;
        }
        else
        {
            aggroRange = originalAggroRange;
        }
        
        if (distanceToTarget < aggroRange)
        {
            inPursuit = true;
        }

        if (distanceFromOrigin >= pursuitRange)
        {
            inPursuit = false;
        }

        if (inPursuit)
        {
            if (!Controller.inRange)
            {
                MoveAgent(agent);
            }
            else
            {
                Rotation();
                agent.Stop();
            }
        }

        if (!inPursuit)
        {
            try
            {
                agent.Resume();
                agent.destination = originPoint;
            }
            catch{}
            
        }
    }

    private void MoveAgent(NavMeshAgent agent)
    {
        agent.Resume();
        agent.destination = target.position;
    }

    private void Rotation()
    {
        if (target != null)
        {
            Vector3 difference = target.position - transform.position;
            difference.Normalize();

            float rotY = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;

            Quaternion newRot = Quaternion.Euler(new Vector3(0.0f, -rotY + adjustmentAngle, 0.0f));

            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * 10.0f);
        }
    }
}
