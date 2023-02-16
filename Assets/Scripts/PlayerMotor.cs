using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : PlayerController
{
    public int playerSpeed;
    public int playerJumpHeight;
    public LayerMask exitDoorMask;

    private bool facingRight = true;

    void Update()
    {
        UpdatePlayer();
    }
    void UpdatePlayer()
    {
        float pSpeed = Input.GetAxis("Horizontal");

        Vector2 move = Vector2.zero;

        if (!crouched && !Input.GetKey(KeyCode.LeftControl))
            move.x = Input.GetAxis("Horizontal");
        else if (!crouched && Input.GetKey(KeyCode.LeftControl))
            move.x = Input.GetAxis("Horizontal") * 2;
        if (Input.GetButton("Jump") && grounded)
        {
            if (Mathf.Abs(velocity.x) > 0)
            {
                velocity.y = playerJumpHeight + Mathf.Abs(velocity.x * 0.2f);
            }
            else
            {
                velocity.y = playerJumpHeight;
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }
        else
        {
            crouched = false;
        }

        if(Input.GetKeyDown(KeyCode.S)) 
        {
            Collider2D doorcheck = Physics2D.OverlapCircle(transform.position, 0.1f, exitDoorMask);
            if(doorcheck != null)
            {
                Generation.NextLevel();
                doorcheck.GetComponent<ExitDoor>().NextLevel();
            }
        }

        if (move.x > 0)
            facingRight = false;

        targetVelocity = move * playerSpeed;
    }
}