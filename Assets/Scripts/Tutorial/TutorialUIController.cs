using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIController : MonoBehaviour {
    public GameObject canvas;
    public GameObject rangedBtn;
    public GameObject charBtn;
    public GameObject bagsBtn;

    private PlayerCutsSceneController cutSceneCont;

    // Use this for initialization
    void Start () {
        cutSceneCont = GetComponent<PlayerCutsSceneController>();
        Invoke("DisableUI", 1);
	}
	
	// Update is called once per frame
	void Update () { 
		if (!cutSceneCont.CutScene)
        {
            canvas.SetActive(true);
        }
	}

    void DisableUI()
    {
        rangedBtn.SetActive(false);
        //charBtn.SetActive(false);
        //bagsBtn.SetActive(false);
        canvas.SetActive(false);
    }

    public void Close(GameObject panel)
    {
        panel.SetActive(false);
    }
}
