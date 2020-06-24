using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {
    private bool charState;
    private bool invState;

    private GameObject menu;
    private GameObject characterPanel;
    private GameObject inventoryPanel;
    private GameObject hotbar;
    private GameObject healthBar;

	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu");
        characterPanel = GameObject.Find("Character Panel");
        inventoryPanel = GameObject.Find("Inventory Panel");
        hotbar = GameObject.Find("Hotbar");
        healthBar = GameObject.Find("Health Bar");

        menu.SetActive(false);
	}
	
    public void OpenMenu()
    {
        charState = characterPanel.activeSelf;
        invState = inventoryPanel.activeSelf;

        characterPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        hotbar.SetActive(false);
        healthBar.SetActive(false);

        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        characterPanel.SetActive(charState);
        inventoryPanel.SetActive(invState);
        hotbar.SetActive(true);
        healthBar.SetActive(true);
        Time.timeScale = 1;
    }

    public void ResetCurrentSceen()
    {
        Scene scene = SceneManager.GetActiveScene();

        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
