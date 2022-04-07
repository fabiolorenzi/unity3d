using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot;
    [SerializeField]
    private Transform lookRoot;
    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensivity = 2f;
    [SerializeField]
    private int smooth_steps = 10;
    [SerializeField]
    private float smooth_weight = 0.4f;
    [SerializeField]
    private float roll_angle = 0.3f;
    [SerializeField]
    private float roll_speed = 3f;

    [SerializeField]
    private Vector2 default_look_limits = new Vector2(-70f, 80f);
    private Vector2 look_angles;
    private Vector2 current_mouse_look;
    private Vector2 smooth_move;
    private float current_roll_angle;
    private int last_look_frame;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    public void LockAndUnlockCursor()
    {
        if (Input.GetKey(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKey(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void LookAround()
    {
        current_mouse_look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        look_angles.x += current_mouse_look.x * sensivity * (invert ? 1f : -1f);
        look_angles.y += current_mouse_look.y * sensivity;

        look_angles.x = Mathf.Clamp(look_angles.x, default_look_limits.x, default_look_limits.y);

        current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw("Mouse X") * roll_angle, Time.deltaTime * roll_speed);

        lookRoot.localRotation = Quaternion.Euler(look_angles.x, 0f, current_roll_angle);
        playerRoot.localRotation = Quaternion.Euler(0f, look_angles.y, 0f);
    }
}
