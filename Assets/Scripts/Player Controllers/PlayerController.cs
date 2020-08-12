using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //Public
    public Slider healthBar;
    public Text healthText;
    public GameObject damageIndicatorPrefab;
    public GameObject healIndicatorPrefab;
    public GameObject levelUpText;

    public bool teleporting = false;

    //Private
    private float currentHealth;
    private float shield;
    private float maxHealth;
    private float healthRegen = 0.1f;

    public float level;
    public float experiance;
    public float nextLevel;

    private float distanceToTarget;

    private double range;

    private bool inRange;
    public bool inCombat;
    private float combatTimer;

    private SelectWeapon selectedWeapon;
    private Inventory inv;
    private PlayerMovement movement;
    private GameObject target;
    private EnemyController targetController;
    private PlayerStats playerStats;
    public RaycastHit hit;

    float regenTimer = 1;


    //------------------------------
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        selectedWeapon = GameObject.Find("Weapon Panel").GetComponent<SelectWeapon>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerStats = this.GetComponent<PlayerStats>();

        nextLevel = CalculateNextLevel();
        maxHealth = playerStats.Health;
        currentHealth = maxHealth;

        levelUpText.SetActive(false);
    }

    void Update()
    {
        maxHealth = playerStats.Health;

        CheckInRange();
        Targeting();

        if (combatTimer < 0)
            inCombat = false;

        //Regenerates the player health
        if (currentHealth < maxHealth && !inCombat)
        {
            if (regenTimer < 0)
            {
                RegenHealthOC();
                regenTimer = 1;
            }
            else
                regenTimer -= Time.deltaTime;
        }
        else if (maxHealth < currentHealth)
            currentHealth = maxHealth;

        //If the player has a shield, changes the text colour
        if (shield > 0)
        {
            healthText.text = ("<color=#3399ff>" + (currentHealth + Mathf.Round(shield)) + "</color>/" + maxHealth).ToString();
            healthBar.value = CalculateHealthPercent(shield);

            healthBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.blue;
            shield -= Time.deltaTime * (maxHealth * 0.03f);
        }
        else
        {
            healthText.text = (currentHealth + "/" + maxHealth).ToString();
            healthBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0.3f, 0.9f, 0.2f);
                //Color(#5df040)
                //Color.green;
            healthBar.value = CalculateHealthPercent();
        }

        combatTimer -= Time.deltaTime;
    }

    public void Targeting()
    {
        //Checks for user input
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            //Checks what the player clicked on
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {

                if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Terrain")))
                {
                    if (!teleporting)
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            //Sets the players target
                            target = hit.collider.gameObject.transform.parent.gameObject;
                        }
                        else
                        {
                            //Sets the players target
                            target = null;
                            movement.Movement(hit.point);
                        }
                    }
                    else
                    {
                        //TODO
                        float distanceToTeleport = Vector3.Distance(transform.position, hit.point);
                        Debug.Log("Distance To Teleport: " + distanceToTeleport);
                        //if (distanceToTeleport <= 3)
                            transform.position = hit.point;
                            
                        teleporting = false;
                    }
                }
            }
        }
    }

    public void CheckInRange()
    {
        //Checks which button is toggled and ajusts range acordingly
        if (selectedWeapon.Melee)
            if (inv.items[4].ID != -1)
                range = inv.items[4].Range;
            else
                range = 0.7;
        else
            if (inv.items[5].ID != -1)
                range = inv.items[5].Range;
            else
                range = 5;

        if (target != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            //Debug.Log("Distance to Target: " + distanceToTarget);

            if (distanceToTarget >= range)
            {
                //Debug.Log("Out of range");
                movement.Movement(target.transform.position);
                inRange = false;
            }
            else
            {
                //Debug.Log("In Range");
                movement.Rotation(target);
                inRange = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        combatTimer = 10;
        inCombat = true;

        //TODO
        double playerDodge = playerStats.Dodge;
        float dodgeRNG = Mathf.Round(Random.Range(0.0f, 1.0f) * 100f) / 100f;
        //Debug.Log("Dodge Chance: " + playerDodge + " | RNG Dodge: " + dodgeRNG);

        if (dodgeRNG >= playerDodge) //Tests whether the player dodges an attack
        {
            int armour = playerStats.Armour;
            //Debug.Log("Armour: " + armour);
            float dr = ((float)armour / (armour + 73));
            //Debug.Log("Damage Reduction: " + dr/10);

            damage = (int)Mathf.Ceil(damage * (1 - dr));
            //Debug.Log("New Damage: " + damage);

            if (shield > 0)
            {
                shield -= damage * (1 - (dr / 10));
            }
            else
                currentHealth -= (damage);

            GameObject damageIndicator = Instantiate(damageIndicatorPrefab) as GameObject;

            Vector3 thisPos = transform.position;
            damageIndicator.transform.position = new Vector3(thisPos.x + 1, thisPos.y, thisPos.z + 1);
            damageIndicator.GetComponent<TextMesh>().text = damage.ToString();
        }
        else
            //Debug.Log("Dodged Attack");

        if (currentHealth <= 0)
            Die();
    }

    float CalculateHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }

    float CalculateHealthPercent(float sheild)
    {
        return (float)(currentHealth + shield) / maxHealth;
    }

    public void CheckLevel(int newExp)
    {
        experiance += newExp;

        if (experiance >=  nextLevel)
        {
            float overflow = experiance - nextLevel;

            Debug.Log("Level Up");
            level += 1;
            experiance = overflow;
            nextLevel = CalculateNextLevel();
            playerStats.UpdateStats();
            LevelUpText();
        }

        //Debug.Log("Experiance: " + experiance);
    }

    void LevelUpText()
    {
        levelUpText.SetActive(true);
        Text text = levelUpText.GetComponent<Text>();
        text.fontSize = 0;
        InvokeRepeating("LargenLevelUp", 0.005f, 0.005f);
    }

    void LargenLevelUp()
    {
        Text text = levelUpText.GetComponent<Text>();
        text.fontSize += 1;
        text.text = "Level Up";

        if (text.fontSize >= 90)
        {
            if (level == 2)
                Invoke("NewTalent", 2);
            else
                Invoke("DeactivateLevelText", 2);

            CancelInvoke("LargenLevelUp");
        }
    }

    void NewTalent()
    {
        Text text = levelUpText.GetComponent<Text>();
        text.fontSize = 70;
        text.text = "New Talent Available";
        Invoke("DeactivateLevelText", 2);
    }

    void DeactivateLevelText()
    {
        levelUpText.SetActive(false);
    }

    //The equations for calculating the experianced need to level up
    float CalculateNextLevel()
    {
        float expRequired;

        expRequired = (3 * level);
        //Debug.Log("Exp to next level: " + expRequired);

        return expRequired;
    }

    void Die()
    {
        Application.LoadLevel("Menu");
    }

    //When not in combat, heals the player
    void RegenHealthOC()
    {
        int healthToRegen = (int)(maxHealth * healthRegen);
        //Debug.Log("Regen: " + healthToRegen);

        GameObject healIndicator = Instantiate(healIndicatorPrefab) as GameObject;

        Vector3 thisPos = transform.position;
        healIndicator.transform.position = new Vector3(thisPos.x - 1, thisPos.y, thisPos.z + 1);
        
        if ((currentHealth + healthToRegen) > maxHealth)
        {
            //Debug.Log("Max Health");
            currentHealth = maxHealth;
            healIndicator.GetComponent<TextMesh>().text = (maxHealth - currentHealth).ToString();
        }
        else
        {
            //Debug.Log("Less Health");
            currentHealth += healthToRegen;
            healIndicator.GetComponent<TextMesh>().text = healthToRegen.ToString();
        }
    }


    //Gets
    public GameObject Target { get { return target; } }

    public EnemyController TargetController { get { return targetController; } }

    public bool InRange { get { return inRange; } }

    public float DistanceToTarget { get { return distanceToTarget; } }

    public float Level { get { return level; } }

    public float CurrentHealth { get { return currentHealth; } set { currentHealth += value; } }

    public float MaxHealth { get { return maxHealth; } }

    public float HealthRegen { set { healthRegen = value; } }
    public float Shield { set { shield = value; } }
}
