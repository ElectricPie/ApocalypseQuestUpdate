using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTriggers : MonoBehaviour {
    public GameObject tutPanel;
    public Text tutTitle;
    public Text tutDes;

    public string collisionTag = "";
    public string title = "";
    [TextArea(3, 10)]
    public string description = "";

    public GameObject UIElement;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        if (other.CompareTag(collisionTag))
        {
            tutPanel.SetActive(true);
            tutTitle.text = title;
            tutDes.text = description;

            if (UIElement != null)
                UIElement.SetActive(true);

            Destroy(gameObject);
        }
    }
}
