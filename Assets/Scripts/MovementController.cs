using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController2D characterController;

    public float movementSpeed = 40f;
    private float movementDirection;    // Direction in which player should move. [-1, 1]; -1 -> left; 1 -> right.

    private bool jump = false;          // Whether the player should jump
    private bool crouch = false;        // Whether the player should crouch

    // Update is called once per frame
    void Update()
    {
        // Read input
        movementDirection = Input.GetAxis("Horizontal") * movementSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        // Move the player
        characterController.Move(movementDirection * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
