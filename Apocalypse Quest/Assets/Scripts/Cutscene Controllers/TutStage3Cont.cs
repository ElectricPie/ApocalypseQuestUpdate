using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutStage3Cont : MonoBehaviour {
    public GameObject target;

    public GameObject tutPanel;
    public Text tutTitle;
    public Text tutDes;

    public GameObject charBtn;
    public GameObject bagsBtn;

    public bool set2Done;

    // Use this for initialization
    void Start () {
        set2Done = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Target Active: " + target.activeSelf + " | set2Done: " + set2Done);
		if (!target.activeSelf && !set2Done)
        {
            tutPanel.SetActive(true);
            tutTitle.text = "Inventory";
            tutDes.text = "Hey, that target had a bow in it. You should try it out \n(Open you bags and character windows and drag the bow onto the ranged weapon slot";
            set2Done = true;
        }
	}
}
