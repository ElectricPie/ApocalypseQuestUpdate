using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
    public Text text;

    private float count;

	// Use this for initialization
	void Start () {
        count = 0;

        Invoke("Disable", 2);
	}
	
	// Update is called once per frame
	void Update () {
        text.text += ".";
        count += Time.deltaTime;

        if (count > 0.5)
        {
            text.text = "Loading";
            count = 0;
        }
        else
        {

        }
	}

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
