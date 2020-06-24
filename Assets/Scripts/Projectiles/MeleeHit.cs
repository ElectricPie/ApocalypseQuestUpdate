using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour {
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
        //Debug.Log("Hit");
        //Debug.Log("Tag: " + other);
        if (other.CompareTag(damageTag))
        {

            Debug.Log("Hit + " + name);
            int strengthMod = (int)(player.GetComponent<PlayerStats>().Strength / (player.GetComponent<PlayerController>().level + 1));
            int damage = inv.items[4].Damage + strengthMod;

            other.transform.parent.SendMessage("TakeDamage", damage);
        }

        //Destroy(gameObject);
    }
}
