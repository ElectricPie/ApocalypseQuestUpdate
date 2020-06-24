using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Playground()
    {
        SceneManager.LoadScene("Test");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
