using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private float speed = 5f;
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = new Vector2(joyStick.Horizontal, joyStick.Vertical);

        Vector3 dirMovement = new Vector3(input.x, 0f, input.y);
        Vector3 moveDestination = transform.position + (dirMovement * speed * Time.deltaTime);
        transform.position = moveDestination;
        if (input.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.y));
        }
    }
}
