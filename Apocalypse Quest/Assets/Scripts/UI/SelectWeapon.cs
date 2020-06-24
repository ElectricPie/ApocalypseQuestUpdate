using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour {
    public Button meleeBtn;
    public Button rangedBtn;

    private bool melee = true;

    public void ToggleWeapon()
    {
        rangedBtn.interactable = !rangedBtn.interactable;
        meleeBtn.interactable = !meleeBtn.interactable;

        melee = !melee;
    }

    public bool Melee
    {
        get { return melee; }
    }
}
