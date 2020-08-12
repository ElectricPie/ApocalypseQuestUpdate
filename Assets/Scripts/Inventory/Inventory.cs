using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    //Public
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public GameObject inventorySlot;
    public GameObject inventoryItem;

    //Private
    int slotAmount;

    ItemDatabase database;

    GameObject inventoryPanel;
    GameObject slotPanel;
    GameObject characterPanel;

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        slotAmount = 21;

        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

        characterPanel = GameObject.Find("Character Panel");

        slots.Add(GameObject.Find("Head Slot"));
        slots.Add(GameObject.Find("Chest Slot"));
        slots.Add(GameObject.Find("Legs Slot"));
        slots.Add(GameObject.Find("Feet Slot"));
        slots.Add(GameObject.Find("Weapon 1 Slot"));
        slots.Add(GameObject.Find("Weapon 2 Slot"));

        for (int i = 0; i < 6; i++)
        {
            slots[i].GetComponent<Slots>().id = i;
            items.Add(new Item());
        }

        for (int i = 6; i < slotAmount + 5; i++)
        {
            items.Add(new Item());

            //Creats the slots for the items
            GameObject slot = Instantiate(inventorySlot);
            slot.name = "Slot " + (i);
            slot.transform.SetParent(slotPanel.transform);
            slot.transform.localPosition = new Vector3(slot.transform.localPosition.x, slot.transform.localPosition.y, 0);
            slot.transform.localScale = new Vector3(1, 1, 1);
            slots.Add(slot);

            slots[i].GetComponent<Slots>().id = i;
        }

        //Manualy added items
        /*
        AddItem(0);
        AddItem(1);
        AddItem(3);
        AddItem(4);
        AddItem(8);
        AddItem(4);
        AddItem(9);
        AddItem(10);
        
        AddItem(0);
        AddItem(1);
        AddItem(5);
        AddItem(11);
        AddItem(12);
        */

        /*
        for (int i = 0; i < 17; i++)
            AddItem(i);
        */

        Invoke("Hide", 0.1f);
        //Hide();
    }

    private void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);

        if (itemToAdd.Stackable && CheckIfItemInInventory(itemToAdd))
        {
            Debug.Log("Stacl");
            for (int i = 6; i < items.Count + 6; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    break;
                }
            }
        }
        else
        {
            for (int i = 6; i < items.Count + 6; i++)
            {
                if (items[i].ID == -1)
                {
                    //Creates the item in the UI
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.localPosition = new Vector3(itemObj.transform.localPosition.x, itemObj.transform.localPosition.y, 0);
                    itemObj.transform.localScale = new Vector3(1, 1, 1);

                    //Gets the sprite for the item
                    itemObj.transform.position = Vector2.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;

                    break;
                }
            }
        }
    }

    bool CheckIfItemInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID== item.ID)
            {
                return true;
            }
        }
        return false;
    }

    public void Hide()
    {
        inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
    }

    public void PickUpItem(int id)
    {
        AddItem(id);

        //Moves the item gameObjects to the correct place as creating new items puts them at world space (0,0,0)
        for (int i = 0; i < slots.Count; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).transform.position = slots[i].transform.position;
            }
            catch { }
        }
    }
}
