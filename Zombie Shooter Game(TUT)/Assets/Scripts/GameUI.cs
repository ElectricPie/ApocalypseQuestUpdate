using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private int health;
    private int score;
    private int ammo;

    private string ammoDisplay;

    private string gameInfo = "";
    private Rect boxRect = new Rect(10, 10, 300, 50);

    void OnEnable()
    {
        PlayerBehaviour.OnUpdateHealth += HandleonUpdateHealth;
        AddScore.OnSendScore += HandleonSendScore;
        ShootBullet.OnUpdateAmmo += HandleAmmo;
    }

    void OnDisable()
    {
        PlayerBehaviour.OnUpdateHealth -= HandleonUpdateHealth;
        AddScore.OnSendScore -= HandleonSendScore;
        ShootBullet.OnUpdateAmmo -= HandleAmmo;
    }

    void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    void HandleonUpdateHealth(int newHealth)
    {
        health = newHealth;
        UpdateUI();
    }

    void HandleonSendScore(int theScore)
    {
        score += theScore;
        UpdateUI();
    }

    void HandleAmmo(int ammoCount)
    {
        ammo = ammoCount;
        if(ammo > 0)
        {
            ammoDisplay = ammo.ToString();
        }
        else
        {
            ammoDisplay = "Reloading";
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        gameInfo = "Score: " + score.ToString() + "\nHealth: " + health.ToString() + "\nAmmo:" + ammoDisplay;
    }

    void OnGUI()
    {
        GUI.Box(boxRect, gameInfo);
    }

}
