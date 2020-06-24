using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public delegate void UpdateHealth(int newHealth);
    public static event UpdateHealth OnUpdateHealth;

    public int health = 100;
    public bool isReloaded;

    private Animator gunAnim;

    void Start()
    {
        gunAnim = GetComponent<Animator>();
        SendHealthData();
    }

    void Update()
    {
        GameObject Hero = GameObject.Find("Hero");
        ShootBullet shootBullet = Hero.GetComponent<ShootBullet>();
        isReloaded = shootBullet.isReloaded;    

        Debug.Log("Reloaded PB: " + isReloaded);

        if(isReloaded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetBool("isFiring", true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<Animator>().SetBool("isFiring", false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player Health: " + health);
        health -= damage;
        SendHealthData();
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
    }

    void SendHealthData()
    {
        if (OnUpdateHealth != null)
        {
            OnUpdateHealth(health);
        }
    }
}