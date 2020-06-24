using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDummieController : MonoBehaviour {
    //Public
    public float dropChance;

    public GameObject lootPrefab;
    public GameObject indicatorPrefab;

    private float distanceToTarget;

    //Private
    private int id;
    private int health;
    private int exp;

    private float baseRange;

    private bool hitTypeRecived;

    private NPCDatabase NPCDatabase;
    private NPC npc;

    void Start()
    {
        id = 4;
        
        NPCDatabase = GameObject.Find("NPCS").GetComponent<NPCDatabase>();

        npc = NPCDatabase.FetchNPCByID(id);

        if (npc == null)
            Destroy(gameObject);

        //name = npc.Name;
        health = npc.Health;
        exp = npc.Exp;
    }

    public void CheckProjectileType(bool ranged)
    {
        hitTypeRecived = ranged;
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("Enemy Hit: " + name);
        //Debug.Log("Damage taken: " + damage);
 

        health -= damage;


        //Debug.Log("Enemy health: " + health);

        //Creates a damage indicator
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
        //DropLoot();

        DropLoot();
        Destroy(gameObject);
    }

    void DropLoot()
    {
        Transform lootPost = this.transform;
        float dropRNG = Mathf.Round(Random.Range(0.0f, 1.0f) * 100f);
        //Debug.Log("Drop RNG: " + dropRNG);

        if (dropRNG <= dropChance)
        {
            GameObject newLoot = Instantiate(lootPrefab) as GameObject;
            newLoot.transform.position = lootPost.position;
            
            /*
            LootData loot = newLoot.GetComponent<LootData>();

            int lootTotal = npc.Loot.Count;
            int lootRNG = (int)Mathf.Round(Random.Range(0, lootTotal));

            Debug.Log("LootRNG: " + lootRNG);

            loot.itemID = npc.Loot[lootRNG];
            */
        }

        Debug.Log("Dropped");
    }
}
