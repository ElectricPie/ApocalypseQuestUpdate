using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {
    InputField IDInput;
    InputField healthInput;
    InputField levelInput;

    Inventory inv;

    PlayerController player;

    private void Start()
    {
        IDInput = GameObject.Find("ID Input").GetComponent<InputField>();
        healthInput = GameObject.Find("Health Input").GetComponent<InputField>();
        levelInput = GameObject.Find("Level Input").GetComponent<InputField>();

        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void AddItem()
    {
        int id = int.Parse(IDInput.text);
        inv.PickUpItem(id);
    }

    public void AddHealth()
    {
        int health = int.Parse(healthInput.text);
        player.CurrentHealth = health;
    }

    public void SetLevel()
    {
        int level = int.Parse(levelInput.text);
        player.level = level;
    }
}
