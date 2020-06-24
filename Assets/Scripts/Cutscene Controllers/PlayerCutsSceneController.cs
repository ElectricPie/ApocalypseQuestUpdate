using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerCutsSceneController : MonoBehaviour
{
    public GameObject player;

    public GameObject dest1;
    public GameObject dest2;

    public GameObject tutWindow;

    public GameObject levelSprite1;
    public GameObject levelSprite2;

    public GameObject trigger;

    public GameObject ladderObs;

    public GameObject tutPanel;
    public Text tutTitle;
    public Text tutDes;

    private bool cutScene;
    private bool doneFirst;

    private NavMeshAgent agent;

    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private UseTalents playerTalents;

    // Use this for initialization
    void Start()
    {
        cutScene = true;
        doneFirst = false;
        agent = player.GetComponent<NavMeshAgent>();

        playerController = player.GetComponent<PlayerController>();
        playerMovement = player.GetComponent<PlayerMovement>();

        Invoke("DisableComponents", 2);
    }

    void DisableComponents()
    {
        tutWindow.SetActive(false);

        trigger.SetActive(false);

        playerController.enabled = false;
        playerMovement.enabled = false;

        agent.destination = dest1.transform.position;
    }

    void EnableMovement()
    {
        playerController.enabled = true;
        playerMovement.enabled = true;

        levelSprite2.SetActive(true);
        levelSprite1.SetActive(false);

        trigger.SetActive(true);

        ladderObs.SetActive(true);

        cutScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x == dest1.transform.position.x &&
            player.transform.position.z == dest1.transform.position.z &&
            !doneFirst)
        {
            EnableMovement();
            ActivateTutWindow();
            doneFirst = true;
        }
    }

    void ActivateTutWindow()
    {
        tutPanel.SetActive(true);
        tutTitle.text = "Moving";
        tutDes.text = "Looks like the ladder fell apart as you climbed up brother. Looks like we will have to head up the stairs ahead. \n(To move left or right click on the ground)";
    }


    public bool CutScene { get { return cutScene; } }
}
