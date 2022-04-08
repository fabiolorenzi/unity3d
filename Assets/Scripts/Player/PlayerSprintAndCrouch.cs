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

    private PlayerFootsteps player_footsteps;

    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_volume_min = 0.2f;
    private float walk_volume_max = 0.6f;

    private float walk_steps_distance = 0.4f;
    private float sprint_steps_distance = 0.25f;
    private float crouch_steps_distance = 0.5f;

    public void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
        player_footsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    public void Start()
    {
        player_footsteps.volume_min = walk_volume_min;
        player_footsteps.volume_max = walk_volume_max;
        player_footsteps.steps_distance = walk_steps_distance;
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
            player_footsteps.steps_distance = sprint_steps_distance;
            player_footsteps.volume_min = sprint_volume;
            player_footsteps.volume_max = sprint_volume;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
        {
            playerMovement.speed = move_speed;
            player_footsteps.steps_distance = walk_steps_distance;
            player_footsteps.volume_min = walk_volume_min;
            player_footsteps.volume_max = walk_volume_max;
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
                player_footsteps.steps_distance = walk_steps_distance;
                player_footsteps.volume_min = walk_volume_min;
                player_footsteps.volume_max = walk_volume_max;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouch_speed;
                is_crouching = true;
                player_footsteps.steps_distance = crouch_steps_distance;
                player_footsteps.volume_min = crouch_volume;
                player_footsteps.volume_max = crouch_volume;
            }
        }
    }
}
