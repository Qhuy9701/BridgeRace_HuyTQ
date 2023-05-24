using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private float moveSpeed = 8f;

    public float DirectionZ => rb.velocity.z;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsStartGame)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector3(joyStick.Horizontal * moveSpeed, rb.velocity.y, joyStick.Vertical * moveSpeed);

        if (joyStick.Horizontal != 0 || joyStick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));
        }
    }
    public bool IsNotMoving()
    {
        return Mathf.Abs(joyStick.Horizontal) < 0.1f && Mathf.Abs(joyStick.Vertical) < 0.1f;
    }
}
