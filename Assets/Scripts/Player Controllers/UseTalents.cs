using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseTalents : MonoBehaviour {
    public GameObject healIndicatorPrefab;

    GameObject talentPanel;
    TalentUI talentChoice;
    PlayerController player;
    PlayerStats stats;

    private Button[] talentBtns = new Button[3];

    float talentOneTimer;
    float talentTwoTimer;
    float talentThreeTimer;

    // Use this for initialization
    void Start () {
        talentPanel = GameObject.Find("Talent Panel");
        talentChoice = talentPanel.GetComponent<TalentUI>();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();

        for (int i = 0; i < talentBtns.Length; i++)
            talentBtns[i] = GameObject.Find("Talent Btn Panel").gameObject.transform.GetChild(i).GetComponent<Button>();

        Invoke("Hide", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        switch (talentChoice.ActiveT1Talent)
        {
            case 1:
                //Debug.Log("First Aid");
                player.HealthRegen = 0.3f;
                talentBtns[0].interactable = false;
                break;
            case 3:
                stats.DodgeTalent = 0.3;
                stats.UpdateStats();
                talentBtns[0].interactable = false;
                break;
            case 5:
                //TODO
                if (player.CurrentHealth < (player.MaxHealth) * 0.3 && talentOneTimer <= 0)
                {
                    player.Shield = player.MaxHealth * 0.3f;
                    talentOneTimer = 20;
                }
                talentBtns[0].interactable = false;
                break;

            case 0:
            case 2:
            case 4:
                if (Input.GetKeyDown(KeyCode.Alpha1) && talentOneTimer < 0)
                    UseTalentOne();
                else
                    talentBtns[0].interactable = true;
                break;
        }


        if (talentChoice.ActiveT1Talent != 1)
            player.HealthRegen = 0.1f;

        if (talentChoice.ActiveT1Talent != 3)
            stats.DodgeTalent = 0.05;

        if (talentOneTimer >= 0)
        {
            talentOneTimer -= Time.deltaTime;
            talentBtns[0].interactable = false;
        }

        if (talentTwoTimer >= 0)
            talentTwoTimer -= Time.deltaTime;
        if (talentThreeTimer >= 0)
            talentThreeTimer -= Time.deltaTime;
    }

    public void BtnUse(int btnID)
    {
        if ( btnID == 0 && talentOneTimer < 0)
            UseTalentOne();
        else
            talentBtns[0].interactable = true;

        if (btnID == 1 && talentTwoTimer < 0)
            UseTalentTwo();
        else
            talentBtns[1].interactable = true;

        if (btnID == 2 && talentThreeTimer < 0)
            UseTalentThree();
        else
            talentBtns[2].interactable = true;
    }

    void UseTalentOne()
    {
        switch (talentChoice.ActiveT1Talent)
        {
            case 0:
                float missingHealth = player.MaxHealth - player.CurrentHealth;
                Debug.Log("Missing Health: " + missingHealth);
                Debug.Log("60%: " + missingHealth * 0.6);

                GameObject healIndicator = Instantiate(healIndicatorPrefab) as GameObject;

                Vector3 thisPos = transform.position;
                healIndicator.transform.position = new Vector3(thisPos.x - 1, thisPos.y, thisPos.z + 2);
                healIndicator.GetComponent<TextMesh>().text = missingHealth.ToString();

                player.CurrentHealth = (int)(missingHealth * 0.6);
                
                talentOneTimer = 10;
                break;
            case 2:
                Debug.Log("Disengagin");
                GetComponent<PlayerMovement>().Disengage();
                talentOneTimer = 15;
                break;
            case 4:
                player.teleporting = true;
                talentOneTimer = 5;
                break;
        }
        talentBtns[0].interactable = false;
    }

    void UseTalentTwo()
    {
        talentTwoTimer = 5;
    }

    void UseTalentThree()
    {
        talentThreeTimer = 5;
    }

    void Hide()
    {
        talentPanel.SetActive(false);
    }
}
