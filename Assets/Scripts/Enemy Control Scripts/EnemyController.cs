using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //Public
    public int id = -1;
    public float dropChance;

    public GameObject lootPrefab;
    public GameObject indicatorPrefab;

    private int health;
    private int exp;

    private float distanceToTarget;

    public bool inRange;

    private GameObject target;
    private PlayerController targetController;

    private Vector3 originPoint;

    //Private
    private float baseRange;

    private NPCDatabase NPCDatabase;
    private NPC npc;

    void Start()
    {
        
        NPCDatabase = GameObject.Find("NPCS").GetComponent<NPCDatabase>();

        npc = NPCDatabase.FetchNPCByID(id);

        if (npc == null)
            Destroy(gameObject);

        //name = npc.Name;
        health = npc.Health;
        exp = npc.Exp;

        //Targets the player
        if (GameObject.FindWithTag("Player"))
        {
            target = GameObject.FindWithTag("Player");
            targetController = (PlayerController)target.GetComponent(typeof(PlayerController));
        }

        //Die();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        CheckInRangeOfTarget();
    }

    private void CheckInRangeOfTarget()
    {
        //Checks if the target is in range of the enemy
        if (distanceToTarget <= npc.Range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("Enemy Hit: " + name);
        //Debug.Log("Damage taken: " + damage);
        health -= damage;
        //Debug.Log("Enemy health: " + health);

        GameObject damageIndicator = Instantiate(indicatorPrefab) as GameObject;

        Vector3 thisPos = transform.position;
        damageIndicator.transform.position = new Vector3 (thisPos.x + 0.2f, thisPos.y, thisPos.z + 0.2f);
        damageIndicator.GetComponent<TextMesh>().text = damage.ToString();

        if (health <= 0)
            Die();
    }

    void Die()
    {
        //Debug.Log("Enemy Target: " + targetController);
        targetController.CheckLevel(exp);
        DropLoot();
        Destroy(gameObject);
    }

    void DropLoot()
    {
        Transform lootPost = this.transform;
        float dropRNG = Mathf.Round(Random.Range(0.0f, 1.0f) * 100f) / 100f;

        if (dropRNG <= dropChance)
        {
            GameObject newLoot = Instantiate(lootPrefab) as GameObject;
            newLoot.transform.position = lootPost.position;
            LootData loot = newLoot.GetComponent<LootData>();

            int lootTotal = npc.Loot.Count;
            int lootRNG = (int)Mathf.Round(Random.Range(0, lootTotal));

            Debug.Log("LootRNG: " + lootRNG);

            loot.itemID = npc.Loot[lootRNG];
        }

    }

    //
    public int ID { get { return id; } }
    public float DistanceToTarget { get { return distanceToTarget; } }
    public GameObject Target { get { return target; } }
    public PlayerController TargetController { get { return targetController; } }
}
