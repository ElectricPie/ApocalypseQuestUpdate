using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrotherControll : MonoBehaviour {
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;

    public GameObject explosiveBarrel;

    public GameObject dest1;
    public GameObject dest2;
    public GameObject dest3;
    public GameObject dest4;

    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (explosiveBarrel == null)
            agent.destination = dest4.transform.position;
        else if (target3 == null && target2 == null)
            agent.destination = dest3.transform.position;
        else if (target1 == null)
            agent.destination = dest2.transform.position;
        else
            Invoke("MoveToFirst", 2);
    }

    void MoveToFirst()
    {
        agent.destination = dest1.transform.position;
    }
}
