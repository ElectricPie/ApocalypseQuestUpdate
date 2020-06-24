using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public int healthBase = 20;
    public float strenghtBase = 1;
    public float agilityBase = 1;
    public float intellectBase = 1;
    public int damageBase = 1;
    public double speedBase = 1.5;
    public double rangeBase = 2;

    private int maxHealth;
    private int strenght;
    private int agility;
    private int intellect;

    private int armour;
    private double dodge = 0.1;
    private double dodgeTalent;

    private int damage;
    private double speed;
    private double range;

    private Inventory inv;
    private PlayerController player;

    //Gets the UI text game objects
    Text levelText;

    Text healthText;
    Text strenghtText;
    Text agilityText;
    Text intellectText;

    Text armourText;

    Text damageText;
    Text speedText;
    Text rangeText;

    // Use this for initialization
    void Start() {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        //Gets the UI text game objects
        levelText = GameObject.Find("Level Value").GetComponent<Text>();

        healthText = GameObject.Find("Health Value").GetComponent<Text>();
        strenghtText = GameObject.Find("Strength Value").GetComponent<Text>();
        agilityText = GameObject.Find("Agility Value").GetComponent<Text>();
        intellectText = GameObject.Find("Intellect Value").GetComponent<Text>();

        armourText = GameObject.Find("Armour Value").GetComponent<Text>();

        damageText = GameObject.Find("Damage Value").GetComponent<Text>();
        speedText = GameObject.Find("Speed Value").GetComponent<Text>();
        rangeText = GameObject.Find("Range Value").GetComponent<Text>();

        strenght = Mathf.CeilToInt(Mathf.Pow(player.level + 4, 2) * strenghtBase);
        agility = Mathf.CeilToInt(Mathf.Pow(player.level + 4, 2) * agilityBase);
        intellect = Mathf.CeilToInt(Mathf.Pow(player.level + 4, 2) * intellectBase);

        maxHealth = Mathf.CeilToInt(Mathf.Pow(player.level + 2, 2) + (strenght / player.level));
        double dodgeCalc = ((player.level - (player.level / 2)) / (agility / player.level)) * 0.3 + dodgeTalent;
        dodge = Mathf.Round((float)dodgeCalc * 100f) / 100f;

        damage = damageBase;
        speed = speedBase;
        range = rangeBase;

        UpdateUI();
    }

    public void UpdateStats()
    {
        strenght = Mathf.CeilToInt(Mathf.Pow(player.level + 4, 2) * strenghtBase);
        agility = Mathf.CeilToInt(Mathf.Pow(player.level + 4, 2) * agilityBase);
        intellect = Mathf.CeilToInt(Mathf.Pow(player.level + 4, 2) * intellectBase);

        damage = damageBase;
        range = rangeBase;

        armour = 0;
        
        int healthToAdd = 0;
        double dodgeToAdd = 0;

        //Head
        if (inv.items[0].ID != -1)
        {
            healthToAdd += inv.items[0].Health;
            strenght += inv.items[0].Strength;
            agility += inv.items[0].Agility;
            intellect += inv.items[0].Intellect;

            armour += inv.items[0].Armour;
            dodgeToAdd += inv.items[0].Dodge;
        }

        //Chest
        if (inv.items[1].ID != -1)
        {
            healthToAdd += inv.items[1].Health;
            strenght += inv.items[1].Strength;
            agility += inv.items[1].Agility;
            intellect += inv.items[1].Intellect;

            armour += inv.items[1].Armour;
            dodgeToAdd += inv.items[1].Dodge;
        }

        //Legs
        if (inv.items[2].ID != -1)
        {
            healthToAdd += inv.items[2].Health;
            strenght += inv.items[2].Strength;
            agility += inv.items[2].Agility;
            intellect += inv.items[2].Intellect;

            armour += inv.items[2].Armour;
            dodgeToAdd += inv.items[2].Dodge;
        }

        //Feet
        if (inv.items[3].ID != -1)
        {
            healthToAdd += inv.items[3].Health;
            strenght += inv.items[3].Strength;
            agility += inv.items[3].Agility;
            intellect += inv.items[3].Intellect;

            armour += inv.items[3].Armour;
            dodgeToAdd += inv.items[3].Dodge;
        }

        //Weapon 1
        if (inv.items[4].ID != -1)
        {
            healthToAdd += inv.items[4].Health;
            strenght += inv.items[4].Strength;
            agility += inv.items[4].Agility;
            intellect += inv.items[4].Intellect;

            damage += inv.items[4].Damage;
            speed = inv.items[4].Speed;
            range = inv.items[4].Range;
        }

        //Weapon 2
        if (inv.items[5].ID != -1)
        {
            healthToAdd += inv.items[5].Health;
            strenght += inv.items[5].Strength;
            agility += inv.items[5].Agility;
            intellect += inv.items[5].Intellect;

            damage += inv.items[5].Damage;
            speed = inv.items[5].Speed;
            range = inv.items[5].Range;
        }

        maxHealth = Mathf.CeilToInt(Mathf.Pow(player.level + 2, 2) + (strenght / player.level)) + healthToAdd;
        double dodgeCalc = ((player.level - (player.level / 2)) / (agility / player.level)) * 0.3 + dodgeTalent;
        dodge = Mathf.Round((float)dodgeCalc * 100f) / 100f;
        //Debug.Log("Dodge: " + dodge);

        if (inv.items[4].ID != -1 && inv.items[5].ID != -1) {
            speed = inv.items[4].Speed + inv.items[5].Speed;

            if (inv.items[4].Range >= inv.items[5].Range)
            {
                range = inv.items[4].Range;
            }
            else
            {
                range = inv.items[5].Range;
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        levelText.text = player.level.ToString();
        healthText.text = maxHealth.ToString();
        strenghtText.text = strenght.ToString();
        agilityText.text = agility.ToString();
        intellectText.text = intellect.ToString();

        armourText.text = armour.ToString();

        damageText.text = damage.ToString();

        //Debug.Log(inv.items[4].ID + " | " + inv.items[5].ID);


        //C
        if (inv.items[4].ID != -1 && inv.items[5].ID != -1)
        {
            //Debug.Log("Dual");
            speedText.text = inv.items[4].Speed.ToString() + "(" + inv.items[5].Speed.ToString() + ")";
        }
        else if (inv.items[4].ID != -1 && inv.items[5].ID == -1)
        {
            //Debug.Log("Right");
            speedText.text = inv.items[4].Speed.ToString();
        }
        else if (inv.items[4].ID == -1 && inv.items[5].ID != -1)
        {
            //Debug.Log("Left");
            speedText.text = inv.items[5].Speed.ToString();
        }
        else
        {
            //Debug.Log("Fisty cuffs");
            speedText.text = speedBase.ToString();
        }

        //Sets the range
        if (inv.items[4].ID != -1 && inv.items[5].ID != -1)
        {
            //Debug.Log("Dual");
            rangeText.text = inv.items[4].Range.ToString() + "(" + inv.items[5].Range.ToString() + ")";
        }
        else if (inv.items[4].ID != -1 && inv.items[5].ID == -1)
        {
            //Debug.Log("Right");
            rangeText.text = inv.items[4].Range.ToString();
        }
        else if (inv.items[4].ID == -1 && inv.items[5].ID != -1)
        {
            //Debug.Log("Left");
            rangeText.text = inv.items[5].Range.ToString();
        }
        else
        {
            //Debug.Log("Fisty cuffs");
            rangeText.text = range.ToString();
        }

    }

    //
    public int Armour { get { return armour; } }
    public int Health { get { return maxHealth; } }
    public double Dodge { get { return dodge; } }
    public double DodgeTalent { set { dodgeTalent = value; } }
    public int Strength { get { return strenght; } }
    public int Agility { get { return agility; } }
}
