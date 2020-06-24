using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;
    private Inventory inv;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data =
            "<color=#FFFFFF><b>" +
            item.Title +
            "</b>\n\n" +
            item.Description + "\n";

        if (item.Type == "armour")
        {
            data += AddStat("Armour", item.Armour, inv.items[item.Slot].Armour); //Shows the diffrence in armour of the highlighted item and the currently eqquipped

            data += AddStat("Health", item.Health, inv.items[item.Slot].Health); //Shows the health of the armour and the diffrence between the equipped one

            data += AddStat("Strength", item.Strength, inv.items[item.Slot].Strength);//Shows the strenght of the armour and the diffrence between the equipped one

            data += AddStat("Agility", item.Agility, inv.items[item.Slot].Agility);//Shows the agility of the armour and the diffrence between the equipped one

            data += AddStat("Intellect", item.Intellect, inv.items[item.Slot].Intellect); //Shows the intellect of the armour and the diffrence between the equipped one
        }
        else if (item.Type == "meleeWeapon")
        {
            data += AddStat("Speed", item.Speed, inv.items[4].Speed, true);

            data += AddStat("Range", item.Range, inv.items[4].Range);

            data += AddStat("Damage", item.Damage, inv.items[4].Damage);

            data += AddStat("Health", item.Health, inv.items[4].Health); 

            data += AddStat("Strength", item.Strength, inv.items[4].Strength);

            data += AddStat("Agility", item.Agility, inv.items[4].Agility);

            data += AddStat("Intellect", item.Intellect, inv.items[4].Intellect);

        }
        else if (item.Type == "rangedWeapon")
        {
            data += AddStat("Speed", item.Speed, inv.items[5].Speed, true);

            data += AddStat("Range", item.Range, inv.items[5].Range);

            data += AddStat("Damage", item.Damage, inv.items[5].Damage);

            data += AddStat("Health", item.Health, inv.items[5].Health);

            data += AddStat("Strength", item.Strength, inv.items[5].Strength);

            data += AddStat("Agility", item.Agility, inv.items[5].Agility);

            data += AddStat("Intellect", item.Intellect, inv.items[5].Intellect);
        }

        data += "</color>";

        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

    private string AddStat(string stat, int item, int equipedItem)
    {
        string stringAdd;

        string posColour = "#33cc33"; //Green
        string negColour = "#ff5050"; //Yellow #ffff00 //Orange #ff6600 //Red #ff5050

        if (item > equipedItem)
        {
            stringAdd = stat + ": " + item + " (<color=" + posColour + ">+" + (item - equipedItem) + "</color>)\n";
        }
        else if (item < equipedItem)
        {
            stringAdd = stat + ": " + item + " (<color=" + negColour + ">" + (item - equipedItem) + "</color>)\n";
        }
        else
        {
            stringAdd = stat + ": " + item+ " (0)\n";
        }

        return stringAdd;
    }

    private string AddStat(string stat, double item, double equipedItem) //Overload for double values
    {
        string stringAdd;

        string posColour = "#33cc33"; //Green #33cc33
        string negColour = "#ff5050"; //Yellow #ffff00 //Orange #ff6600 //Red #ff5050

        if (item > equipedItem)
        {
            stringAdd = stat + ": " + item + " (<color=" + posColour + ">+" + (item - equipedItem) + "</color>)\n";
        }
        else if (item < equipedItem)
        {
            stringAdd = stat + ": " + item + " (<color=" + negColour + ">" + (item - equipedItem) + "</color>)\n";
        }
        else
        {
            stringAdd = stat + ": " + item + " (0)\n";
        }

        return stringAdd;
    }

    //For stats where lower is better
    private string AddStat(string stat, double item, double equipedItem, bool flip) //Overload for double values
    {
        string stringAdd;

        string posColour = "#33cc33"; //Green #33cc33
        string negColour = "#ff5050"; //Yellow #ffff00 //Orange #ff6600 //Red #ff5050

        if (item < equipedItem)
        {
            stringAdd = stat + ": " + item + " (<color=" + posColour + ">" + (item - equipedItem) + "</color>)\n";
        }
        else if (item > equipedItem)
        {
            stringAdd = stat + ": " + item + " (<color=" + negColour + ">+" + (item - equipedItem) + "</color>)\n";
        }
        else
        {
            stringAdd = stat + ": " + item + " (0)\n";
        }

        return stringAdd;
    }
}
