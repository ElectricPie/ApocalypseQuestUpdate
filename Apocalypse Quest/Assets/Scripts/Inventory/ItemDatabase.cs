using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            string itemType = itemData[i]["type"].ToString();
            //Debug.Log("itemType: " + itemType);

            if (itemType == "meleeWeapon" || itemType == "rangedWeapon") {
                /*
                Debug.Log(itemData[i]["type"].ToString());
                Debug.Log((int)itemData[i]["id"]);
                Debug.Log(itemData[i]["title"].ToString());
                Debug.Log(itemData[i]["description"].ToString());
                Debug.Log((double)itemData[i]["range"]);
                Debug.Log((int)itemData[i]["value"]);
                Debug.Log((int)itemData[i]["stats"]["damage"]);
                Debug.Log((double)itemData[i]["stats"]["speed"]);
                Debug.Log((int)itemData[i]["stats"]["health"]);
                Debug.Log((int)itemData[i]["stats"]["strength"]);
                Debug.Log((int)itemData[i]["stats"]["agility"]);
                Debug.Log((int)itemData[i]["stats"]["intellect"]);
                Debug.Log((bool)itemData[i]["stackable"]);
                Debug.Log(itemData[i]["slug"].ToString());
                */

                database.Add(new Item(
                    itemData[i]["type"].ToString(),
                    (int)itemData[i]["id"],
                    itemData[i]["title"].ToString(),
                    itemData[i]["description"].ToString(),
                    (double)itemData[i]["range"],
                    (int)itemData[i]["value"],
                    (int)itemData[i]["stats"]["damage"],
                    (double)itemData[i]["stats"]["speed"],
                    (int)itemData[i]["stats"]["health"],
                    (int)itemData[i]["stats"]["strength"],
                    (int)itemData[i]["stats"]["agility"],
                    (int)itemData[i]["stats"]["intellect"],
                    (bool)itemData[i]["stackable"],
                    itemData[i]["slug"].ToString()
                    ));
            }
            else if (itemType == "armour")
            {
                /*
                Debug.Log(itemData[i]["type"].ToString());
                Debug.Log((int)itemData[i]["id"]);
                Debug.Log(itemData[i]["title"].ToString());
                Debug.Log(itemData[i]["description"].ToString());
                Debug.Log((int)itemData[i]["slot"]);
                Debug.Log((int)itemData[i]["value"]);
                Debug.Log((int)itemData[i]["stats"]["armour"]);
                Debug.Log((int)itemData[i]["stats"]["health"]);
                Debug.Log((int)itemData[i]["stats"]["strength"]);
                Debug.Log((int)itemData[i]["stats"]["agility"]);
                Debug.Log((int)itemData[i]["stats"]["intellect"]);
                Debug.Log((bool)itemData[i]["stackable"]);
                Debug.Log(itemData[i]["slug"].ToString());
                */

                database.Add(new Item(
                    itemData[i]["type"].ToString(),
                    (int)itemData[i]["id"],
                    itemData[i]["title"].ToString(),
                    itemData[i]["description"].ToString(),
                    (int)itemData[i]["slot"],
                    (int)itemData[i]["value"],
                    (int)itemData[i]["stats"]["armour"],
                    (int)itemData[i]["stats"]["health"],
                    (int)itemData[i]["stats"]["strength"],
                    (int)itemData[i]["stats"]["agility"],
                    (int)itemData[i]["stats"]["intellect"],
                    (double)itemData[i]["stats"]["dodge"],
                    (bool)itemData[i]["stackable"],
                    itemData[i]["slug"].ToString()
                    ));
            }
            //Debug.Log("------");
        }
    }

    public Item FetchItemByID(int id)
    {
        //Debug.Log("database.Count: " + database.Count);
        for (int i = 0; i < database.Count; i++)  
            if (database[i].ID == id)
                return database[i];

        return null;
    }
}

public class Item
{
    public string Type { get; set; }
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Slot { get; set; }
    public double Range { get; set; }
    public int Value { get; set; }
    public int Armour { get; set; }
    public int Damage { get; set; }
    public double Speed { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intellect { get; set; }
    public double Dodge { get; set; }
    public bool Stackable { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    //Empty item
    public Item()
    {
        this.ID = -1;
    }
    
    //Weapon item
    public Item(string type, int id, string title, string description, double range, int value, int damage,
                double speed, int health, int strength, int agility, int intellect, bool stackable, string slug)
    {
        this.Type = type;
        this.ID = id;
        this.Title = title;
        this.Description = description;
        this.Slot = -1;
        this.Range = range;
        this.Value = value;
        this.Damage = damage;
        this.Speed = speed;
        this.Health = health;
        this.Strength = strength;
        this.Agility = agility;
        this.Intellect = intellect;
        this.Stackable = stackable;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    //Armour
    public Item(string type, int id, string title, string description, int slot, int value, int armour,
            int health, int strength, int agility, int intellect, double dodge, bool stackable, string slug)
    {
        this.Type = type;
        this.ID = id;
        this.Title = title;
        this.Description = description;
        this.Slot = slot;
        this.Value = value;
        this.Armour = armour;
        this.Health = health;
        this.Strength = strength;
        this.Agility = agility;
        this.Intellect = intellect;
        this.Dodge = dodge;
        this.Stackable = stackable;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }
}