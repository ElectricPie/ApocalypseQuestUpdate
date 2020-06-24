using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimController : MonoBehaviour
{
    private int facing;

    private bool moving;

    private Vector3 lastPost;
    private Animator anim;
    private GameObject parent;
    private GameObject player;
    private PlayerController controller;
    private PlayerMovement movement;
    private NavMeshAgent agent;
    
    // Use this for initialization
    void Start()
    {
        facing = 0; //0 is up, 1 is left, 2 is down, 3 is right

        anim = GetComponent<Animator>();
        parent = transform.parent.gameObject;
        player = parent.transform.GetChild(0).gameObject;

        controller = player.GetComponent<PlayerController>();
        movement = player.GetComponent<PlayerMovement>();

        agent = player.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveSprite();
        CheckRotation();
        CheckMoving();
    }

    private void MoveSprite()
    {
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;

        transform.position = pos;
    }

    void CheckMoving()
    {
        if (agent.velocity != Vector3.zero)
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);
    }

    void CheckRotation()
    {
        float navRot = player.transform.rotation.eulerAngles.y;

        //Debug.Log("navRot: " + navRot);

        if ((navRot >= 0 && navRot <= 45) || (navRot < 360 && navRot >= 315)) //UP
        {
            facing = 0;
        }
        else if (navRot < 315 && navRot > 225) //Left
        {
            facing = 1;
        }
        else if (navRot <= 225 && navRot >= 135) //Down
        {
            facing = 2;
        }
        else if (navRot < 135 && navRot > 45) //Right
        {
            facing = 3;
        }



        anim.SetInteger("facing", facing);

        //Debug.Log("Facing: " + facing);
    }
}
