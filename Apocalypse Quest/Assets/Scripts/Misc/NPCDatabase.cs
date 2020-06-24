using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class NPCDatabase : MonoBehaviour {
    private List<NPC> database = new List<NPC>();
    private JsonData NPCData;

	void Start () {
        NPCData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/NPCs.json"));
        ConstructNPCDatabase();
    }

    void ConstructNPCDatabase()
    {
        for (int i = 0; i < NPCData.Count; i++)
        {
            string NPCType = NPCData[i]["type"].ToString();

            if (NPCType == "enemy")
            {
                /*
                Debug.Log(NPCData[i]["type"].ToString());
                Debug.Log((int)NPCData[i]["id"]);
                Debug.Log(NPCData[i]["name"].ToString());
                Debug.Log((int)NPCData[i]["stats"]["health"]);
                Debug.Log((int)NPCData[i]["stats"]["damage"]);
                Debug.Log((double)NPCData[i]["stats"]["range"]);
                Debug.Log((double)NPCData[i]["stats"]["speed"]);
                Debug.Log((int)NPCData[i]["level"]);
                Debug.Log((int)NPCData[i]["exp"]);
                Debug.Log(NPCData[i]["slug"].ToString());
                */

                List<int> lootTemp = new List<int>();

                for (int j = 0; j < (int)NPCData[i]["loot"].Count; j++)
                    lootTemp.Add((int)NPCData[i]["loot"][j]);

                database.Add(new NPC(
                    NPCData[i]["type"].ToString(),
                    (int)NPCData[i]["id"],
                    NPCData[i]["name"].ToString(),
                    (int)NPCData[i]["stats"]["health"],
                    (int)NPCData[i]["stats"]["damage"],
                    (double)NPCData[i]["stats"]["range"],
                    (double)NPCData[i]["stats"]["speed"],
                    (int)NPCData[i]["level"],
                    (int)NPCData[i]["exp"],
                    lootTemp,
                    NPCData[i]["slug"].ToString()
                    ));
            }
        }
        //Debug.Log("Loaded NPCS");
    }

    public NPC FetchNPCByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];

        return null;
    }
}

public class NPC
{
    public string Type { get; set; }
    public int ID { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public double Range { get; set; }
    public double Speed { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public List<int> Loot { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public NPC()
    {
        this.ID = -1;
    }

    public NPC(string type, int id, string name, int health, int damage, double range, double speed, int level, int exp, List<int> loot, 
        string slug)
    {
        this.Type = type;
        this.ID = id;
        this.Name = name;
        this.Health = health;
        this.Damage = damage;
        this.Range = range;
        this.Speed = speed;
        this.Level = level;
        this.Exp = exp;
        this.Loot = loot;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }
}