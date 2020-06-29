using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootData : MonoBehaviour {
    public int itemID = -1;

    private Inventory inv;
    private ItemDatabase itemDatabase;

    [SerializeField]
    private TextMesh m_textMesh = null;
    
	void Start () {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();

        m_textMesh.text = itemDatabase.FetchItemByID(itemID).Title;
        //Debug.Log("Looking for item");
        //Debug.Log("Item: " + itemDatabase.FetchItemByID(itemID));
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = itemDatabase.FetchItemByID(itemID).Sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Other: " + other.tag + " | name: " + other.transform.name);

        if (other.CompareTag("Player"))
        {
            inv.PickUpItem(itemID);
            Destroy(gameObject);
        }

    }
}
