using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentUI : MonoBehaviour {
    private PlayerController player;
    private Button[] talentBtns = new Button[3];
    private Button[] teir1Btns = new Button[6];
    private Button[] teir2Btns = new Button[6];
    private Button[] teir3Btns = new Button[3];

    private int activeT1Talent = -1;
    private int activeT2Talent = -1;
    private int activeT3Talent = -1;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        for (int i = 0; i < talentBtns.Length; i++)
            talentBtns[i] = GameObject.Find("Talent Btn Panel").gameObject.transform.GetChild(i).GetComponent<Button>();

        //Each teir array has seperate for statement to allow for expasions of teir sets 
            //i.e. teir 2 get additional talents that teir 1 doesnt
        for (int i = 0; i < teir1Btns.Length; i++)
            teir1Btns[i] = GameObject.Find("Teir 1").transform.GetChild(i).GetComponent<Button>();

        for (int i = 0; i < teir2Btns.Length; i++)
            teir2Btns[i] = GameObject.Find("Teir 2").transform.GetChild(i).GetComponent<Button>();

        for (int i = 0; i < teir3Btns.Length; i++)
            teir3Btns[i] = GameObject.Find("Teir 3").transform.GetChild(i).GetComponent<Button>();

        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        float playerLevel = player.Level;

        //Checks the players level and if high enough deactivates the object 
        if (playerLevel >= 2)
            this.gameObject.transform.GetChild(5).gameObject.SetActive(false);

        //Checks the players level and if high enough deactivates the object
        if (playerLevel >= 8)
            this.gameObject.transform.GetChild(6).gameObject.SetActive(false);

        //Checks the players level and if high enough deactivates the object
        if (playerLevel >= 15)
            this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
    }

    public void Teir1Btn(int buttonID)
    {
        activeT1Talent = buttonID;

        for (int i = 0; i < teir1Btns.Length; i++)
            teir1Btns[i].interactable = true;

        Image btnImg = talentBtns[0].transform.GetChild(0).GetComponent<Image>();
        Text btnTxt = talentBtns[0].transform.GetChild(1).GetComponent<Text>();

        Color alpha = btnImg.color;

        switch (activeT1Talent)
        {
            case 0:
                btnImg.sprite = Resources.Load<Sprite>("Sprites/UI/second_wind");
                btnTxt.text = "";
                alpha.a = 1;
                break;
            case 1:
                btnImg.sprite = Resources.Load<Sprite>("Sprites/UI/first_aid");
                btnTxt.text = "";
                alpha.a = 1;
                break;
            case 2:
                btnTxt.text = "Disengage";
                alpha.a = 0;
                //btnImg.sprite = Resources.Load<Sprite>("Sprites/UI/disengage");
                break;
            case 3:
                btnTxt.text = "Dodge";
                alpha.a = 0;
                //btnImg.sprite = Resources.Load<Sprite>("Sprites/UI/dodge");
                break;
            case 4:
                btnTxt.text = "Teleport";
                alpha.a = 0;
                //btnImg.sprite = Resources.Load<Sprite>("Sprites/UI/teleport");
                break;
            case 5:
                btnTxt.text = "Emergancy\nShield";
                alpha.a = 0;
                //btnImg.sprite = Resources.Load<Sprite>("Sprites/UI/emergancy_shield");
                break;
        }
        btnImg.color = alpha;
        
        teir1Btns[activeT1Talent].interactable = false;

        talentBtns[0].interactable = true;
    }

    public void Teir2Btn(int buttonID)
    {
        activeT2Talent = buttonID;

        for (int i = 0; i < teir2Btns.Length; i++)
            teir2Btns[i].interactable = true;

        teir2Btns[activeT2Talent].interactable = false;
    }

    public void Teir3Btn(int buttonID)
    {
        activeT3Talent = buttonID;

        for (int i = 0; i < teir3Btns.Length; i++)
            teir3Btns[i].interactable = true;

        teir3Btns[activeT3Talent].interactable = false;
    }

    public int ActiveT1Talent { get { return activeT1Talent; } }
}
