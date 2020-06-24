using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    //Private
    private float distanceFromTarget;

    private bool isAttacking = false;

    private GameObject target;
    private EnemyController controller;
    private NPCDatabase NPCDatabase;
    private NPC npc;

    // Use this for initialization
    void Start () {
        NPCDatabase = GameObject.Find("NPCS").GetComponent<NPCDatabase>();
        controller = GetComponent<EnemyController>();

        npc = NPCDatabase.FetchNPCByID(controller.ID);

        target = controller.Target;

        distanceFromTarget = controller.DistanceToTarget;
    }
	

	// Update is called once per frame
	void Update () {
        target = controller.Target;

        distanceFromTarget = controller.DistanceToTarget;

        if (!isAttacking & controller.inRange)
        {
            isAttacking = true;
            Invoke("Attack", (float)npc.Speed);
        }
        else if(isAttacking)
        {

        }
    }

    void Attack()
    {
        controller.TargetController.TakeDamage(npc.Damage);
        isAttacking = false;
    }
}
