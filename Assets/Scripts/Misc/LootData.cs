using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootData : MonoBehaviour {
    public int itemID = -1;

    private Inventory inv;
    private ItemDatabase itemDatabase;
    
	void Start () {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();

        transform.GetChild(0).GetComponent<TextMesh>().text = itemDatabase.FetchItemByID(itemID).Title;
        //Debug.Log("Looking for item");
        //Debug.Log("Item: " + itemDatabase.FetchItemByID(itemID));
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = itemDatabase.FetchItemByID(itemID).Sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Other: " + other.tag);

        if (other.CompareTag("Player"))
        {
            inv.PickUpItem(itemID);
            Destroy(gameObject);
        }

    }
}
