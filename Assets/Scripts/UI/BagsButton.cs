using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagsButton : MonoBehaviour {
    GameObject bagPanel;
    GameObject charPanel;
    Inventory inv;

	// Use this for initialization
	void Start () {
        bagPanel = GameObject.Find("Inventory Panel");
        charPanel = GameObject.Find("Character Panel");
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}

    public void ToggleBags()
    {
        bagPanel.SetActive(!bagPanel.activeSelf);
    }

    public void ToggleChar()
    {
        charPanel.SetActive(!charPanel.activeSelf);
    }


}
