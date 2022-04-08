using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Transform lookRoot;

    public float sprint_speed = 9f;
    public float move_speed = 4f;
    public float crouch_speed = 1.5f;

    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    private bool is_crouching;

    public void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
    }

    public void Update()
    {
        Sprint();
        Crouch();
    }

    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
        {
            playerMovement.speed = sprint_speed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
        {
            playerMovement.speed = move_speed;
        }
    }

    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (is_crouching)
            {
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = move_speed;
                is_crouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouch_speed;
                is_crouching = true;
            }
        }
    }
}
