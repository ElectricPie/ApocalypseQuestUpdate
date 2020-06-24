using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    //Private
    private float distanceFromTarget;

    private bool isAttacking = false;

    public GameObject arrowPrefab;
    public GameObject meleePrefab;
    public Transform arrowSpawn;
    public Transform meleeSpawn;

    private GameObject target;
    private PlayerController controller;
    private PlayerStats stats;
    private EnemyController enemyController;
    private SelectWeapon selectedWeapon;
    private Inventory inv;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = transform.parent.GetChild(1).GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
        selectedWeapon = GameObject.Find("Weapon Panel").GetComponent<SelectWeapon>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();

        target = controller.Target;

        distanceFromTarget = controller.DistanceToTarget;
    }
	
	// Update is called once per frame
	void Update () {
        target = controller.Target;
        //Debug.Log("Target: " + target);

        if (target != null)
            enemyController = target.GetComponent<EnemyController>();

        distanceFromTarget = controller.DistanceToTarget;
        float attackSpeed;

        if (!isAttacking & controller.InRange && target != null)
        {
            //Debug.Log("Selected Weapon: " + selectedWeapon.Melee);
            if (selectedWeapon.Melee)
            {
                if (inv.items[4].ID != -1)
                    attackSpeed = (float)inv.items[4].Speed;
                else
                    attackSpeed = 2;

                Invoke("MeleeAttack", attackSpeed);
            }
            else if (!selectedWeapon.Melee)
            {
                if (inv.items[5].ID != -1)
                    attackSpeed = (float)inv.items[5].Speed;
                else
                    attackSpeed = 2;

                Invoke("RangedAttack", attackSpeed);
            }
            isAttacking = true;
        }
        else if (isAttacking)
        {

        }
    }

    public void MeleeAttack()
    {
        /*
        int damage;

        if (inv.items[4].ID != -1)
        {
            //Debug.Log("Equipped");
            damage = inv.items[4].Damage;
        }
        else
            damage = 2;

        damage += stats.Strength / (int)(controller.level + 1);

        //Debug.Log("Player Damage" + damage);
        enemyController.TakeDamage(damage);*/

        if (target != null)
        {
            anim.SetBool("meleeAttack", true);
            anim.SetBool("isAttacking", true);

            Invoke("StopAttack", 0.6f);
            Instantiate(meleePrefab, meleeSpawn.position, meleeSpawn.rotation).transform.parent = gameObject.transform;
        }

        isAttacking = false;
    }

    void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    void RangedAttack()
    {
        anim.SetBool("meleeAttack", false);
        anim.SetBool("isAttacking", true);

        Invoke("DelayFire", 1);

        isAttacking = false;
    }

    void DelayFire()
    {
        if (target != null)
            Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation);

        Invoke("StopAttack", 0.2f);
    }
}