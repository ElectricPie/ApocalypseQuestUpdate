using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentBtn : MonoBehaviour {
    public GameObject talentPanel;

    public void TogglePanel()
    {
        talentPanel.SetActive(!talentPanel.activeSelf);
    }
}
