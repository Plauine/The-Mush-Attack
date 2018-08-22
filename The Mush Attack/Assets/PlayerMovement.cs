using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;

    float camRayLength = 100f;
    int floorMask;
    Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h,v);

        Turning();
    }



    private void Move(float h, float v)
    {

        // Set the movement vector based on the axis input.
        moveDirection.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        moveDirection = moveDirection.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rb.MovePosition(transform.position + moveDirection);

        Animate(moveDirection);
    }

    private void Turning()
    {
        // This is how the player is rotated inspired by the Survival Shooter Tutorial
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation(newRotation);

        }
    }    

    private void Animate(Vector3 moveDirection)
    {
        // This is to start or stop the Run animation 
        if (!moveDirection.Equals(Vector3.zero))
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
