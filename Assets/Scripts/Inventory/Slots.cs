using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IDropHandler
{
    public int id;

    private Inventory inv;
    private PlayerStats playerStats;
    private Image meleeBtnImg;
    private Image rangedBtnImg;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        meleeBtnImg = GameObject.Find("Melee Btn Img").GetComponent<Image>();
        rangedBtnImg = GameObject.Find("Ranged Btn Img").GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if (id >= 0 && id < 4) //Slots 0-3 are armour slots and are limited to armour
        {
            if (droppedItem.item.Type == "armour" && droppedItem.item.Slot == id)
            {
                //Debug.Log("Equipping armour");
                if (inv.items[id].ID == -1)
                {
                    EquipItem(droppedItem);
                }
                else
                {
                    SwapItems(droppedItem);
                }
            }
        }
        else if (id == 4) //Slots 4 and 5 are limited to weapons
        {
            if (droppedItem.item.Type == "meleeWeapon")
            {
                //Debug.Log("Equipping weapon");
                if (inv.items[id].ID == -1)
                {
                    EquipItem(droppedItem);
                }
                else
                {
                    SwapItems(droppedItem);
                }
            }
            meleeBtnImg.sprite = inv.items[4].Sprite;   
        }
        else if (id == 5)
        {
            if (droppedItem.item.Type == "rangedWeapon")
            {
                if (inv.items[id].ID == -1)
                {
                    EquipItem(droppedItem);
                }
                else
                {
                    SwapItems(droppedItem);
                }
            }
            rangedBtnImg.sprite = inv.items[5].Sprite;
        }
        else if (id > 5)
        {
            if (inv.items[id].ID == -1)
            {
                EquipItem(droppedItem);
            }
            else
            {
                SwapItems(droppedItem);
            }
        }
        playerStats.UpdateStats();
        if (inv.items[4].ID == -1)
            meleeBtnImg.sprite = Resources.Load<Sprite>("Sprites/UI/tempHand");

        if (inv.items[5].ID == -1)
            rangedBtnImg.sprite = Resources.Load<Sprite>("Sprites/UI/tempHand");
    }

    private void EquipItem(ItemData droppedItem)
    {
        inv.items[droppedItem.slot] = new Item();
        inv.items[id] = droppedItem.item;
        droppedItem.slot = id;
    }

    private void SwapItems(ItemData droppedItem)
    {
        Transform item = this.transform.GetChild(0);
        item.GetComponent<ItemData>().slot = droppedItem.slot; //Set this slots item to other slot
        item.transform.SetParent(inv.slots[droppedItem.slot].transform); //Sets this slot item to new place in list
        item.transform.position = inv.slots[droppedItem.slot].transform.position; //Moves this slots item to other slot

        inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item; //Sets the droped slots item to this slots item 

        droppedItem.slot = id; //sets the dropped items slot as this slots id
        droppedItem.transform.SetParent(this.transform); //Sets this slot as the parent for the dropped item
        droppedItem.transform.position = this.transform.position; //Moves the dropped item to the child of this slot

        inv.items[id] = droppedItem.item; //Sets the dropped item to be the right place in the inventory
    }

}
