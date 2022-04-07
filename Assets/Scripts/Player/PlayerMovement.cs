using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_controller;
    private Vector3 move_directions;
    public float speed = 5f;
    private float gravity = 20f;
    public float jump_force = 10f;
    private float vertical_velocity;

    public void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        move_directions = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move_directions = transform.TransformDirection(move_directions * speed * Time.deltaTime);

        ApplyGravity();

        character_controller.Move(move_directions);
    }

    public void ApplyGravity()
    {
        vertical_velocity -= gravity * Time.deltaTime;
        move_directions.y = vertical_velocity * Time.deltaTime;
    }
}
