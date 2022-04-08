using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footsteps_sound;

    [SerializeField]
    private AudioClip[] footstep_clip;

    private CharacterController character_controller;

    [HideInInspector]
    public float volume_min, volume_max;

    private float accumulate_distance;

    [HideInInspector]
    public float steps_distance;

    public void Awake()
    {
        footsteps_sound = GetComponent<AudioSource>();
        character_controller = GetComponentInParent<CharacterController>();
    }

    public void Update()
    {
        CheckToPlayFootstepSound();
    }

    public void CheckToPlayFootstepSound()
    {
        if (!character_controller.isGrounded)
        {
            return;
        }

        if (character_controller.velocity.sqrMagnitude > 0)
        {
            // accumulate_distance is how far we went
            // steps_distance is how long is a step
            // so if we move longer than a step distance means that we are making a step, so that we ahve to play the sound
            accumulate_distance += Time.deltaTime;
            
            if (accumulate_distance > steps_distance)
            {
                footsteps_sound.volume = Random.Range(volume_min, volume_max);
                footsteps_sound.clip = footstep_clip[0];
                footsteps_sound.Play();

                accumulate_distance = 0f;
            }
        }
        else
        {
            accumulate_distance = 0f;
        }
    }
}
