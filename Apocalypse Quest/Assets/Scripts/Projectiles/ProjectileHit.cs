using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour {
    public int damage = 1;
    public string damageTag = "";

    private Inventory inv;
    private GameObject player;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Tag: " + other);
        if (other.CompareTag(damageTag)) {

            int agilityMod = (int)(player.GetComponent<PlayerStats>().Agility / (player.GetComponent<PlayerController>().level + 1));
            int damage = inv.items[5].Damage + agilityMod;

            //Debug.Log("Hit: " + other.transform.parent);

            //other.transform.parent.SendMessage("CheckProjectileType", true);
            other.transform.parent.SendMessage("TakeDamage", damage);
        }

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Invoke("Destroy", 1);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
